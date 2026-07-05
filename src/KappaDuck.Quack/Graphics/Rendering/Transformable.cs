// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;

namespace KappaDuck.Quack.Graphics.Rendering;

/// <summary>
/// A base class for objects that can be positioned, rotated, scaled and pivoted in 2D space.
/// </summary>
/// <remarks>
/// The <see cref="Transform"/> is derived from <see cref="Position"/>, <see cref="Rotation"/>, <see cref="Scale"/> and
/// <see cref="Origin"/>, and is recomputed only when one of them changes.
/// </remarks>
public abstract class Transformable
{
    private bool _dirty;

    /// <summary>
    /// Gets or sets the position of the object's <see cref="Origin"/> in its parent's space. Defaults to (0, 0).
    /// </summary>
    public PointF Position
    {
        get;
        set
        {
            field = value;
            _dirty = true;
        }
    }

    /// <summary>
    /// Gets or sets the local point that <see cref="Position"/> refers to and that rotation and scaling pivot around.
    /// </summary>
    /// <remarks>Defaults to (0, 0), the top-left corner. Set it to the object's center to rotate and scale about the middle.</remarks>
    public PointF Origin
    {
        get;
        set
        {
            field = value;
            _dirty = true;
        }
    }

    /// <summary>
    /// Gets or sets the rotation applied to the object, clockwise on screen. Defaults to no rotation.
    /// </summary>
    public Angle Rotation
    {
        get;
        set
        {
            field = value;
            _dirty = true;
        }
    }

    /// <summary>
    /// Gets or sets the horizontal and vertical scale factors. Defaults to (1, 1).
    /// </summary>
    public Vector2 Scale
    {
        get;
        set
        {
            field = value;
            _dirty = true;
        }
    } = Vector2.One;

    /// <summary>
    /// Gets the combined transform that places the object in its parent's space, built from
    /// <see cref="Position"/>, <see cref="Rotation"/>, <see cref="Scale"/> and <see cref="Origin"/>.
    /// </summary>
    public Transform Transform
    {
        get
        {
            if (_dirty)
            {
                field = Transform.Create(Position, Rotation, Scale, Origin);
                _dirty = false;
            }

            return field;
        }
    } = Transform.Identity;

    /// <summary>
    /// Moves the object relative to its current <see cref="Position"/>.
    /// </summary>
    /// <param name="offset">The displacement to add to the position.</param>
    public void Move(Vector2 offset) => Position += offset;

    /// <summary>
    /// Moves the object towards a target position by at most <paramref name="maxDistance"/>, stopping exactly at it once within reach.
    /// </summary>
    /// <param name="target">The position to move towards.</param>
    /// <param name="maxDistance">The maximum distance to move this step. Should be non-negative.</param>
    public void MoveTowards(PointF target, float maxDistance)
    {
        Vector2 displacement = target - Position;
        float distance = displacement.Magnitude;

        if (distance <= maxDistance || MathF.ApproximatelyZero(distance))
        {
            Position = target;
            return;
        }

        Position += displacement / distance * maxDistance;
    }

    /// <summary>
    /// Rotates the object relative to its current <see cref="Rotation"/>.
    /// </summary>
    /// <param name="angle">The angle to add to the rotation, clockwise on screen.</param>
    public void Rotate(Angle angle) => Rotation += angle;

    /// <summary>
    /// Scales the object relative to its current <see cref="Scale"/>.
    /// </summary>
    /// <param name="factor">The horizontal and vertical factors to multiply the scale by.</param>
    public void ScaleBy(Vector2 factor) => Scale *= factor;

    /// <summary>
    /// Scales the object uniformly relative to its current <see cref="Scale"/>.
    /// </summary>
    /// <param name="factor">The factor to multiply both scale components by.</param>
    public void ScaleBy(float factor) => Scale *= factor;
}
