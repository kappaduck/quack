// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using System.Numerics;
using Numerics = System.Numerics;

namespace KappaDuck.Quack.Geometry;

/// <summary>
/// Represents an immutable 2D affine transform.
/// </summary>
/// <remarks>
/// <para>
/// Transforms are combined with the <c>*</c> operator, applied <b>right to left</b>: in the
/// product <c>a * b</c>, <c>b</c> is applied to a point first, then <c>a</c>.
/// </para>
/// <para>
/// Rotations are expressed in degrees. Positive angles rotate clockwise as seen on screen,
/// where the Y axis points down.
/// </para>
/// </remarks>
public readonly struct Transform : IEquatable<Transform>
{
    private readonly Matrix3x2 _matrix;

    private Transform(Matrix3x2 matrix) => _matrix = matrix;

    /// <summary>
    /// Gets the identity transform, which leaves every point unchanged.
    /// </summary>
    public static Transform Identity => new(Matrix3x2.Identity);

    /// <summary>
    /// Gets the inverse computation of this transform.
    /// </summary>
    /// <remarks>
    /// If this transform cannot be inverted, it will returns the <see cref="Identity"/> instead.
    /// </remarks>
    public Transform Inverted => Matrix3x2.Invert(_matrix, out Matrix3x2 inv) ? new(inv) : Identity;

    /// <summary>Creates a transform that moves points by the given offset.</summary>
    /// <param name="x">The horizontal offset.</param>
    /// <param name="y">The vertical offset.</param>
    /// <returns>A translation transform.</returns>
    public static Transform Translation(float x, float y) => new(Matrix3x2.CreateTranslation(x, y));

    /// <summary>Creates a transform that scales points about the origin.</summary>
    /// <param name="x">The horizontal scale factor.</param>
    /// <param name="y">The vertical scale factor.</param>
    /// <returns>A scaling transform.</returns>
    public static Transform Scaling(float x, float y) => new(Matrix3x2.CreateScale(x, y));

    /// <summary>Creates a transform that rotates points about the origin.</summary>
    /// <param name="rotation">The rotation, clockwise on screen.</param>
    /// <returns>A rotation transform.</returns>
    public static Transform Rotation(Angle rotation) => new(Matrix3x2.CreateRotation(rotation.Radians));

    /// <summary>Creates a transform that rotates points about a given center.</summary>
    /// <param name="rotation">The rotation, clockwise on screen.</param>
    /// <param name="center">The point that stays fixed during the rotation.</param>
    /// <returns>A rotation transform about <paramref name="center"/>.</returns>
    public static Transform Rotation(Angle rotation, Point center)
        => new(Matrix3x2.CreateRotation(rotation.Radians, ToNumerics(center)));

    /// <summary>
    /// Builds the transform for a placed object by scaling, then rotating, then positioning it,
    /// using <paramref name="origin"/> as the pivot for both scaling and rotation.
    /// </summary>
    /// <param name="position">The final position of the object's origin, in the target space.</param>
    /// <param name="rotation">The rotation angle, clockwise on screen.</param>
    /// <param name="scale">The horizontal and vertical scale factors.</param>
    /// <param name="origin">The pivot point in the object's local space — the point that lands exactly on <paramref name="position"/>. Defaults to the local origin <c>(0, 0)</c>.</param>
    /// <returns>The combined transform placing the object in the target space.</returns>
    public static Transform Create(Point position, Angle rotation, Vector2 scale, Point origin = default)
        => new(Matrix3x2.CreateTranslation(-ToNumerics(origin))
             * Matrix3x2.CreateScale(ToNumerics(scale))
             * Matrix3x2.CreateRotation(rotation.Radians)
             * Matrix3x2.CreateTranslation(ToNumerics(position)));

    /// <summary>Applies this transform to a point.</summary>
    /// <param name="point">The point to transform.</param>
    /// <returns>The transformed point.</returns>
    public Point TransformPoint(Point point) => ToGeometry(Numerics.Vector2.Transform(ToNumerics(point), _matrix));

    /// <summary>
    /// Computes the smallest axis-aligned rectangle that contains <paramref name="rect"/>
    /// after this transform is applied to its four corners.
    /// </summary>
    /// <param name="rect">The rectangle to transform.</param>
    /// <returns>
    /// The bounding rectangle of the transformed corners. When the transform includes rotation
    /// or shear, this rectangle is larger than the transformed shape it encloses.
    /// </returns>
    public Rect TransformRect(Rect rect)
    {
        Point a = TransformPoint(new(rect.X, rect.Y));
        Point b = TransformPoint(new(rect.X + rect.Width, rect.Y));
        Point c = TransformPoint(new(rect.X, rect.Y + rect.Height));
        Point d = TransformPoint(new(rect.X + rect.Width, rect.Y + rect.Height));

        float minX = MathF.Min(MathF.Min(a.X, b.X), MathF.Min(c.X, d.X));
        float minY = MathF.Min(MathF.Min(a.Y, b.Y), MathF.Min(c.Y, d.Y));
        float maxX = MathF.Max(MathF.Max(a.X, b.X), MathF.Max(c.X, d.X));
        float maxY = MathF.Max(MathF.Max(a.Y, b.Y), MathF.Max(c.Y, d.Y));

        return new(minX, minY, maxX - minX, maxY - minY);
    }

    /// <summary>
    /// Attempts to split this transform into separate translation, rotation, and scale components.
    /// </summary>
    /// <remarks>
    /// A transform built only from <see cref="Translation"/>, <see cref="Rotation(Angle)"/>,
    /// <see cref="Scaling"/>, and <see cref="Create"/> always decomposes successfully. Shear
    /// only arises from combining non-uniform scaling with rotation, and cannot be expressed as
    /// a single rotation followed by a scale.
    /// </remarks>
    /// <param name="translation"> When this method returns <see langword="true"/>, receives the translation component.</param>
    /// <param name="rotation">When this method returns <see langword="true"/>, receives the rotation in angle, clockwise on screen; otherwise <c>0</c>.</param>
    /// <param name="scale">When this method returns <see langword="true"/>, receives the scale component; otherwise <see cref="Vector2.One"/>. A negative Y value means the transform includes a reflection, which can be applied as a vertical flip.</param>
    /// <returns><see langword="true"/> if this transform is a pure translation, rotation, and scale; <see langword="false"/> if it contains shear and therefore has no such decomposition.</returns>
    public bool TryDecompose(out Point translation, out Angle rotation, out Vector2 scale)
    {
        Matrix3x2 m = _matrix;
        translation = ToGeometry(m.Translation);

        float sx = MathF.Sqrt((m.M11 * m.M11) + (m.M12 * m.M12));
        float sy = MathF.Sqrt((m.M21 * m.M21) + (m.M22 * m.M22));

        float shear = (m.M11 * m.M21) + (m.M12 * m.M22);
        if (MathF.Abs(shear) > 1e-4f * (sx * sy))
        {
            rotation = Angle.Zero;
            scale = Vector2.One;
            return false;
        }

        if (m.GetDeterminant() < 0)
            sy = -sy;

        rotation = Angle.FromRadians(MathF.Atan2(m.M12, m.M11));
        scale = new(sx, sy);

        return true;
    }

    /// <summary>Combines two transforms into one.</summary>
    /// <param name="left">The transform applied second.</param>
    /// <param name="right">The transform applied first.</param>
    /// <returns>A single transform equivalent to applying <paramref name="right"/> first, then <paramref name="left"/>, to a point.</returns>
    public static Transform operator *(Transform left, Transform right) => new(right._matrix * left._matrix);

    /// <summary>Determines whether this transform is exactly equal to another.</summary>
    /// <remarks>
    /// Comparison is exact, with no tolerance. Two transforms that are mathematically equivalent
    /// but produced by different sequences of operations may still compare as unequal because of
    /// floating-point rounding. Compare decomposed values if you need approximate equality.
    /// </remarks>
    /// <param name="other">The transform to compare with.</param>
    /// <returns><see langword="true"/> if every matrix component is exactly equal; otherwise <see langword="false"/>.</returns>
    public bool Equals(Transform other) => _matrix.Equals(other._matrix);

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Transform t && Equals(t);

    /// <inheritdoc/>
    public override int GetHashCode() => _matrix.GetHashCode();

    /// <summary>Determines whether two transforms are exactly equal.</summary>
    /// <param name="left">The first transform.</param>
    /// <param name="right">The second transform.</param>
    /// <returns><see langword="true"/> if the transforms are exactly equal; otherwise <see langword="false"/>.</returns>
    public static bool operator ==(Transform left, Transform right) => left.Equals(right);

    /// <summary>Determines whether two transforms differ.</summary>
    /// <param name="left">The first transform.</param>
    /// <param name="right">The second transform.</param>
    /// <returns><see langword="true"/> if the transforms differ; otherwise <see langword="false"/>.</returns>
    public static bool operator !=(Transform left, Transform right) => !(left == right);

    private static Numerics.Vector2 ToNumerics(Point point) => new(point.X, point.Y);

    private static Numerics.Vector2 ToNumerics(Vector2 vector) => new(vector.X, vector.Y);

    private static Point ToGeometry(Numerics.Vector2 vector) => new(vector.X, vector.Y);
}
