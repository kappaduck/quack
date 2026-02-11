// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Geometry;

/// <summary>
/// Represents a floating point rectangle.
/// </summary>
/// <param name="x">The x-coordinate of the top-left corner of the rectangle.</param>
/// <param name="y">The y-coordinate of the top-left corner of the rectangle.</param>
/// <param name="width">The width of the rectangle.</param>
/// <param name="height">The height of the rectangle.</param>
[StructLayout(LayoutKind.Sequential)]
public struct Rect(float x, float y, float width, float height) : IEquatable<Rect>, ISpanFormattable, IUtf8SpanFormattable
{
    /// <summary>
    /// Creates an empty rectangle at the origin (0, 0).
    /// </summary>
    public Rect() : this(0f, 0f, 0f, 0f)
    {
    }

    /// <summary>
    /// Creates a rectangle from a position and size.
    /// </summary>
    /// <param name="position">The position of the top-left corner of the rectangle.</param>
    /// <param name="size">The size of the rectangle.</param>
    public Rect(Vector2 position, Size size) : this(position.X, position.Y, size.Width, size.Height)
    {
    }

    /// <summary>
    /// Gets or sets the x-coordinate of the top-left corner.
    /// </summary>
    /// <remarks>
    /// Updating the x-coordinate will adjust <see cref="MaxX"/>.
    /// </remarks>
    public float X { get; set; } = x;

    /// <summary>
    /// Gets or sets the y-coordinate of the top-left corner.
    /// </summary>
    /// <remarks>
    /// Updating the y-coordinate will adjust <see cref="MaxY"/>.
    /// </remarks>
    public float Y { get; set; } = y;

    /// <summary>
    /// Gets or sets the width measured from the x-coordinate.
    /// </summary>
    /// <remarks>
    /// Updating the x-coordinate will adjust <see cref="MaxX"/>.
    /// </remarks>
    public float Width { get; set; } = width;

    /// <summary>
    /// Gets or sets the height measured from the y-coordinate.
    /// </summary>
    /// <remarks>
    /// Updating the y-coordinate will adjust <see cref="MaxY"/>.
    /// </remarks>
    public float Height { get; set; } = height;

    /// <summary>
    /// Gets the x-coordinate of the right side of the rectangle.
    /// </summary>
    public readonly float MaxX => X + Width;

    /// <summary>
    /// Gets the y-coordinate of the bottom side of the rectangle.
    /// </summary>
    public readonly float MaxY => Y + Height;

    /// <summary>
    /// Gets or sets the center point of the rectangle.
    /// </summary>
    public Vector2 Center
    {
        readonly get => new(X + (Width / 2f), Y + (Height / 2f));
        set
        {
            X = value.X - (Width / 2f);
            Y = value.Y - (Height / 2f);
        }
    }

    /// <summary>
    /// Gets a value indicating whether the rectangle is empty.
    /// </summary>
    public readonly bool IsEmpty => MathF.ApproximatelyZero(Width) && MathF.ApproximatelyZero(Height);
}
