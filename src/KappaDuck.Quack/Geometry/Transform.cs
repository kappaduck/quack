// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Numerics;
using NumericVector2 = System.Numerics.Vector2;

namespace KappaDuck.Quack.Geometry;

/// <summary>
/// Represents a 2D transformation including translation, rotation, scale, and origin.
/// </summary>
public record struct Transform
{
    private Matrix3x2 _matrix;

    /// <summary>
    /// Gets the identity transformation.
    /// </summary>
    public static Transform Identity { get; } = new(Matrix3x2.Identity);

    private Transform(Matrix3x2 matrix) => _matrix = matrix;

    internal Vector2 CachedTranslation { get; init; }

    internal float CachedRotationInDegrees { get; init; }

    internal Vector2 CachedScale { get; init; }

    internal Vector2 Origin { get; init; }

    /// <summary>
    /// Applies the transformation to a point.
    /// </summary>
    /// <param name="point">The point to transform.</param>
    /// <returns>The transformed point.</returns>
    public readonly Vector2 Apply(Vector2 point) => ToVector2(NumericVector2.Transform(point.ToNumerics(), _matrix));

    /// <summary>
    /// Combines a local transform with a parent/world transform.
    /// </summary>
    /// <remarks>
    /// This applies the local transform first, then the parent/world transform.
    /// Use this to compute the world transform of a child relative to its parent.
    /// </remarks>
    /// <param name="parent">The parent/world transform.</param>
    /// <returns>The combined world transform.</returns>
    public Transform Combine(Transform parent)
    {
        Matrix3x2 combined = parent._matrix * _matrix;

        Vector2 translation = new(combined.M31, combined.M32);
        float rotation = MathF.Atan2(combined.M21, combined.M11) * (180.0f / MathF.PI);

        float scaleX = MathF.Sqrt((combined.M11 * combined.M11) + (combined.M21 * combined.M21));
        float scaleY = MathF.Sqrt((combined.M12 * combined.M12) + (combined.M22 * combined.M22));

        return new Transform(combined) with
        {
            CachedTranslation = translation,
            CachedRotationInDegrees = rotation,
            CachedScale = new Vector2(scaleX, scaleY),
            Origin = Origin
        };
    }

    /// <summary>
    /// Creates a new transform with the specified origin.
    /// </summary>
    /// <param name="origin">The origin point of the transform.</param>
    /// <returns>The new transform with the specified origin.</returns>
    public Transform WithOrigin(Vector2 origin) => this with { Origin = origin };

    /// <summary>
    /// Creates a rotation transformation.
    /// </summary>
    /// <param name="angle">The angle to rotate.</param>
    /// <returns>The rotation transformation.</returns>
    public Transform Rotate(Angle angle)
    {
        Matrix3x2 rotation = Matrix3x2.CreateRotation(angle.Radians, Origin.ToNumerics());
        return WithMatrix(_matrix * rotation) with
        {
            CachedRotationInDegrees = CachedRotationInDegrees + angle.Degrees
        };
    }

    /// <summary>
    /// Creates a scaling transformation.
    /// </summary>
    /// <param name="factors">The scale factors.</param>
    /// <returns>The scaling transformation.</returns>
    public Transform Scale(Vector2 factors)
    {
        Matrix3x2 scaling = Matrix3x2.CreateScale(factors.ToNumerics(), Origin.ToNumerics());
        return WithMatrix(_matrix * scaling) with
        {
            CachedScale = new Vector2(CachedScale.X * factors.X, CachedScale.Y * factors.Y)
        };
    }

    /// <summary>
    /// Creates a uniform scaling transformation.
    /// </summary>
    /// <param name="uniformFactor">The uniform scale factor.</param>
    /// <returns>The uniform scaling transformation.</returns>
    public Transform Scale(float uniformFactor) => Scale(new Vector2(uniformFactor, uniformFactor));

    /// <summary>
    /// Creates a translation transformation.
    /// </summary>
    /// <param name="offset">The translation vector.</param>
    /// <returns>The translation transformation.</returns>
    public Transform Translate(Vector2 offset)
    {
        Matrix3x2 translationMatrix = Matrix3x2.CreateTranslation(offset.ToNumerics());
        return WithMatrix(_matrix * translationMatrix) with
        {
            CachedTranslation = CachedTranslation + offset
        };
    }

    private static Vector2 ToVector2(NumericVector2 vec) => new(vec.X, vec.Y);

    private Transform WithMatrix(Matrix3x2 matrix) => this with { _matrix = matrix };
}
