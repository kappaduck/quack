// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;

namespace KappaDuck.Quack.Graphics.Rendering;

/// <summary>
/// A 2D camera that defines what part of the scene is visible and where it is displayed on the render target.
/// </summary>
/// <remarks>
/// <para>
/// A view is described by two independent things: <see cref="Center"/>, <see cref="Size"/> and <see cref="Rotation"/>
/// control what part of the scene is visible, while <see cref="Viewport"/> controls where on the render target it is
/// displayed, as a rectangle normalized to the target's size. The default viewport, (0, 0, 1, 1), covers the whole
/// target.
/// </para>
/// <para>
/// Combine multiple views to build split-screens, mini-maps, or picture-in-picture panels: draw the scene once per
/// view, each with its own <see cref="Viewport"/>. Obtain a <see cref="ViewScope"/> from
/// <see cref="Renderer.WithView(View)"/> to draw through a view.
/// </para>
/// </remarks>
public sealed class View
{
    /// <summary>
    /// Creates a view centered at the origin with the given size and a viewport covering the whole target.
    /// </summary>
    /// <param name="size">The width and height of the visible scene, in world units.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="size"/> has a negative or zero width or height.</exception>
    public View(SizeF size) : this(PointF.Origin, size)
    {
    }

    /// <summary>
    /// Creates a view with the given center and size and a viewport covering the whole target.
    /// </summary>
    /// <param name="center">The point of the scene that appears at the center of the viewport.</param>
    /// <param name="size">The width and height of the visible scene, in world units.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="size"/> has a negative or zero width or height.</exception>
    public View(PointF center, SizeF size)
    {
        Center = center;
        Size = size;
    }

    /// <summary>
    /// Creates a view that shows the given rectangle of the scene, with a viewport covering the whole target.
    /// </summary>
    /// <param name="rect">The rectangle of the scene to show, in world units.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="rect"/> has a negative or zero width or height.</exception>
    public View(Rect rect) : this(rect.Center, rect.Size)
    {
    }

    /// <summary>
    /// Gets or sets the rectangle, in world units, that the rendered content is confined to, or <see langword="null"/>
    /// to let the view move freely.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Use this to stop a camera from showing past the edges of a level: set it to the level's world rectangle, and
    /// move <see cref="Center"/> however you like for example following a player. The visible content stays
    /// confined to <see cref="Bounds"/> without <see cref="Center"/> itself being altered.
    /// </para>
    /// <para>
    /// Along an axis where <see cref="Size"/> is larger than <see cref="Bounds"/>. For example while zoomed out,
    /// or on a mini-map. The axis is centered on <see cref="Bounds"/> instead of clamped, since no position keeps
    /// the view entirely inside it.
    /// </para>
    /// <para>
    /// Ignores <see cref="Rotation"/>: the confinement treats <see cref="Size"/> as an axis-aligned extent, so a
    /// rotated view can still show a sliver past the edge at its corners.
    /// </para>
    /// </remarks>
    public Rect? Bounds { get; set; }

    /// <summary>
    /// Gets or sets the point of the scene that appears at the center of the viewport. Defaults to (0, 0).
    /// </summary>
    /// <remarks>
    /// Kept exactly as set, even outside <see cref="Bounds"/>. Only the rendered content is confined. Reading it
    /// back (for example to check how far a followed target has wandered) never reflects clamping.
    /// </remarks>
    public PointF Center { get; set; }

    /// <summary>
    /// Gets or sets the rotation of the view, clockwise on screen. Defaults to no rotation.
    /// </summary>
    public Angle Rotation { get; set; }

    /// <summary>
    /// Gets or sets the width and height of the visible scene, in world units.
    /// </summary>
    /// <remarks>
    /// Shrinking the size zooms in; growing it zooms out. The aspect ratio is independent from the viewport's, so a
    /// mismatch stretches the scene. Match them (or letterbox) if you need it undistorted.
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">The width or height is negative or zero.</exception>
    public SizeF Size
    {
        get;
        set
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value.Width);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value.Height);

            field = value;
        }
    }

    /// <summary>
    /// Gets or sets the rectangle, normalized to the render target's size, where this view is displayed.
    /// </summary>
    /// <remarks>
    /// Expressed in the 0–1 range so it stays correct across different target sizes. (0, 0, 1, 1), the default,
    /// covers the whole target. Use a quarter of the target for split-screen (e.g. (0, 0, 0.5, 0.5) for the
    /// top-left player) or a small corner rectangle for a mini-map (e.g. (0.75, 0.75, 0.2, 0.2)).
    /// </remarks>
    public Rect Viewport { get; set; } = new(0f, 0f, 1f, 1f);

    /// <summary>
    /// Computes the pixel rectangle this view occupies on a render target of the given size, from <see cref="Viewport"/>.
    /// </summary>
    /// <remarks>Assign the result to <see cref="Renderer.Viewport"/> before drawing through this view.</remarks>
    /// <param name="targetSize">
    /// The size of the render target, accounting for logical presentation. Typically
    /// <see cref="Renderer.CurrentOutputSize"/>, not <see cref="Renderer.OutputSize"/>.
    /// </param>
    /// <returns>The viewport, in pixels, clamped to at least 1x1.</returns>
    public RectI ComputeViewport(Size targetSize)
    {
        int x = (int)MathF.Round(Viewport.X * targetSize.Width);
        int y = (int)MathF.Round(Viewport.Y * targetSize.Height);
        int width = Math.Max(1, (int)MathF.Round(Viewport.Width * targetSize.Width));
        int height = Math.Max(1, (int)MathF.Round(Viewport.Height * targetSize.Height));

        return new RectI(x, y, width, height);
    }

    /// <summary>
    /// Computes the transform that maps the scene into this view, ready to combine into a <see cref="RenderState"/>.
    /// </summary>
    /// <remarks>
    /// <paramref name="viewportSize"/> must be the pixel size this view is displayed at the size from
    /// <see cref="ComputeViewport"/> and not the full render target, so the scene fills the viewport correctly.
    /// </remarks>
    /// <param name="viewportSize">The pixel size of this view's viewport.</param>
    /// <returns>A transform mapping scene coordinates to viewport-local render coordinates.</returns>
    public Transform GetTransform(Size viewportSize)
    {
        Vector2 scale = new(viewportSize.Width / Size.Width, viewportSize.Height / Size.Height);
        PointF center = new(viewportSize.Width / 2f, viewportSize.Height / 2f);

        // Rotation is negated: rotating the camera clockwise makes the scene appear to turn counter-clockwise.
        return Transform.Create(center, -Rotation, scale, GetConfinedCenter());
    }

    /// <summary>
    /// Moves the view relative to its current <see cref="Center"/>.
    /// </summary>
    /// <param name="offset">The displacement to add to the center.</param>
    public void Move(Vector2 offset) => Center += offset;

    /// <summary>
    /// Moves <see cref="Center"/> towards a target by at most <paramref name="maxDistance"/>, stopping exactly at
    /// it once within reach.
    /// </summary>
    /// <remarks>Call every frame with the point you want to follow (e.g. a player's position) for a smooth chase camera.</remarks>
    /// <param name="target">The point to move towards.</param>
    /// <param name="maxDistance">The maximum distance to move this step. Should be non-negative.</param>
    public void MoveTowards(PointF target, float maxDistance)
    {
        Vector2 displacement = target - Center;
        float distance = displacement.Magnitude;

        if (distance <= maxDistance || MathF.ApproximatelyZero(distance))
        {
            Center = target;
            return;
        }

        Center += displacement / distance * maxDistance;
    }

    /// <summary>
    /// Rotates the view relative to its current <see cref="Rotation"/>.
    /// </summary>
    /// <param name="angle">The rotation to add.</param>
    public void Rotate(Angle angle) => Rotation += angle;

    /// <summary>
    /// Resets the view to show the given rectangle of the scene, clearing any rotation.
    /// </summary>
    /// <param name="rect">The rectangle of the scene to show, in world units.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="rect"/> has a negative or zero width or height.</exception>
    public void Reset(Rect rect)
    {
        Center = rect.Center;
        Size = rect.Size;
        Rotation = Angle.Zero;
    }

    /// <summary>
    /// Zooms the view by scaling <see cref="Size"/> around its current value.
    /// </summary>
    /// <param name="factor">Values greater than 1 zoom out (show more of the scene); values between 0 and 1 zoom in.</param>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="factor"/> is negative or zero.</exception>
    public void Zoom(float factor)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(factor);
        Size = new SizeF(Size.Width * factor, Size.Height * factor);
    }

    private PointF GetConfinedCenter()
    {
        if (Bounds is not { } bounds)
            return Center;

        float halfWidth = Size.Width / 2f;
        float halfHeight = Size.Height / 2f;

        float x = Size.Width >= bounds.Width
            ? bounds.Center.X
            : Math.Clamp(Center.X, bounds.Left + halfWidth, bounds.Right - halfWidth);

        float y = Size.Height >= bounds.Height
            ? bounds.Center.Y
            : Math.Clamp(Center.Y, bounds.Top + halfHeight, bounds.Bottom - halfHeight);

        return new PointF(x, y);
    }
}

/// <summary>
/// A scope obtained from <see cref="Renderer.WithView(View)"/> that carries the render state to draw through a view,
/// and restores the renderer's full-target viewport when disposed.
/// </summary>
/// <remarks>
/// Pass <see cref="State"/> to <see cref="Renderer.Draw(Drawing.IDrawable, RenderState)"/> for everything drawn through the view.
/// </remarks>
public readonly ref struct ViewScope : IDisposable
{
    private readonly Renderer _renderer;

    internal ViewScope(Renderer renderer, RenderState state)
    {
        _renderer = renderer;
        State = state;
    }

    /// <summary>
    /// Gets the render state carrying the view's transform. Pass it to every draw call made through this view.
    /// </summary>
    public RenderState State { get; }

    /// <summary>
    /// Restores the renderer's viewport to cover the whole target.
    /// </summary>
    public void Dispose() => _renderer.Viewport = null;
}
