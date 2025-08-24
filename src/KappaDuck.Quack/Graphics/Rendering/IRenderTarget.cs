// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Events;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Drawing;
using KappaDuck.Quack.Graphics.Primitives;
using System.Drawing;

namespace KappaDuck.Quack.Graphics.Rendering;

/// <summary>
/// Represents a render target that can be used to render graphics.
/// </summary>
public interface IRenderTarget
{
    /// <summary>
    /// Clears the render target with a default color.
    /// </summary>
    void Clear();

    /// <summary>
    /// Clears the render target with the specified color.
    /// </summary>
    /// <param name="color">The color to clear the render target with.</param>
    void Clear(Color color);

    /// <summary>
    /// Draws the specified drawable to the render target.
    /// </summary>
    /// <param name="drawable">The drawable to draw to the render target.</param>
    void Draw(IDrawable drawable);

    /// <summary>
    /// Draws the specified vertices to the render target.
    /// </summary>
    /// <param name="vertices">The vertices to draw to the render target.</param>
    void Draw(ReadOnlySpan<Vertex> vertices);

    /// <summary>
    /// Draws the specified vertices to the render target.
    /// </summary>
    /// <param name="vertices">The vertices to draw to the render target.</param>
    /// <param name="indices">The indices to draw the vertices in the correct order.</param>
    void Draw(ReadOnlySpan<Vertex> vertices, ReadOnlySpan<int> indices);

    /// <summary>
    /// Maps a point in render target coordinates to pixel coordinates.
    /// </summary>
    /// <remarks>
    /// This takes into account several states:
    /// <list type="bullet">
    /// <item>The window dimensions</item>
    /// <item>The logical presentation settings</item>
    /// <item>The scale</item>
    /// <item>The viewport</item>
    /// </list>
    /// </remarks>
    /// <param name="point">The point in render target coordinates.</param>
    /// <returns>The point in pixel coordinates.</returns>
    Vector2 MapCoordinatesToPixels(Vector2 point);

    /// <summary>
    /// Maps the event coordinates to the render target coordinates.
    /// </summary>
    /// <remarks>
    /// This takes into account several states:
    /// <list type="bullet">
    /// <item>The window dimensions</item>
    /// <item>The logical presentation settings</item>
    /// <item>The scale</item>
    /// <item>The viewport</item>
    /// </list>
    /// <para>
    /// Various event types are converted with this function: mouse, touch, pen, etc.
    /// </para>
    /// <para>
    /// Touch coordinates are converted from normalized coordinates in the window to non-normalized rendering coordinates.
    /// </para>
    /// <para>
    /// Relative mouse coordinates (x relative and y relative event fields) are also converted.
    /// Applications that do not want these fields converted should use <see cref="MapPixelsToCoordinates(Vector2)"/>
    /// on the specific event fields instead of converting the entire event structure.
    /// </para>
    /// <para>
    /// Once converted, coordinates may be outside the rendering area.
    /// </para>
    /// </remarks>
    /// <param name="e">The event to map the coordinates for.</param>
    void MapEventToCoordinates(ref Event e);

    /// <summary>
    /// Maps a point in pixel coordinates to the render target coordinates.
    /// </summary>
    /// <remarks>
    /// This takes into account several states:
    /// <list type="bullet">
    /// <item>The window dimensions</item>
    /// <item>The logical presentation settings</item>
    /// <item>The scale</item>
    /// <item>The viewport</item>
    /// </list>
    /// </remarks>
    /// <param name="point">The point in pixel coordinates.</param>
    /// <returns>The point in render target coordinates.</returns>
    Vector2 MapPixelsToCoordinates(Vector2 point);
}
