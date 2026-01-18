// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Windows;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents an event which is processed by the event loop.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
public struct Event
{
    [FieldOffset(0)]
    private unsafe fixed byte _padding[128];

    /// <summary>
    /// Gets the type of the event.
    /// </summary>
    [field: FieldOffset(0)]
    public EventType Type { get; }

    /// <summary>
    /// Gets the display event.
    /// </summary>
    /// <remarks>
    /// The event is available when <see cref="EventType"/> is
    /// <list type="bullet">
    /// <item><see cref="EventType.ContentScaleChanged"/></item>
    /// <item><see cref="EventType.CurrentModeChanged"/></item>
    /// <item><see cref="EventType.DesktopModeChanged"/></item>
    /// <item><see cref="EventType.DisplayAdded"/></item>
    /// <item><see cref="EventType.DisplayMoved"/></item>
    /// <item><see cref="EventType.DisplayOrientationChanged"/></item>
    /// <item><see cref="EventType.DisplayRemoved"/></item>
    /// <item><see cref="EventType.UsableBoundsChanged"/></item>
    /// </list>
    /// </remarks>
    [field: FieldOffset(0)]
    public DisplayEvent Display { get; }

    /// <summary>
    /// Gets the keyboard device event.
    /// </summary>
    /// <remarks>
    /// The event is available when <see cref="EventType"/> is
    /// <list type="bullet">
    /// <item><see cref="EventType.KeyboardAdded"/></item>
    /// <item><see cref="EventType.KeyboardRemoved"/></item>
    /// </list>
    /// </remarks>
    [field: FieldOffset(0)]
    public KeyboardDeviceEvent KeyboardDevice { get; }

    /// <summary>
    /// Gets the keyboard event.
    /// </summary>
    /// <remarks>
    /// The event is available when <see cref="EventType"/> is
    /// <list type="bullet">
    /// <item><see cref="EventType.KeyDown"/></item>
    /// <item><see cref="EventType.KeyUp"/></item>
    /// </list>
    /// </remarks>
    [field: FieldOffset(0)]
    public KeyboardEvent Keyboard { get; }

    /// <summary>
    /// Gets the mouse button event.
    /// </summary>
    /// <remarks>
    /// The event is available when <see cref="EventType"/> is
    /// <list type="bullet">
    /// <item><see cref="EventType.MouseButtonDown"/></item>
    /// <item><see cref="EventType.MouseButtonUp"/></item>
    /// </list>
    /// </remarks>
    [field: FieldOffset(0)]
    public MouseButtonEvent Mouse { get; }

    /// <summary>
    /// Gets the mouse device event.
    /// </summary>
    /// <remarks>
    /// The event is available when <see cref="EventType"/> is
    /// <list type="bullet">
    /// <item><see cref="EventType.MouseAdded"/></item>
    /// <item><see cref="EventType.MouseRemoved"/></item>
    /// </list>
    /// </remarks>
    [field: FieldOffset(0)]
    public MouseDeviceEvent MouseDevice { get; }

    /// <summary>
    /// Gets the mouse motion event.
    /// </summary>
    /// <remarks>
    /// The event is available when <see cref="EventType"/> is <see cref="EventType.MouseMotion"/>
    /// </remarks>
    [field: FieldOffset(0)]
    public MouseMotionEvent Motion { get; }

    /// <summary>
    /// Gets the mouse wheel event.
    /// </summary>
    /// <remarks>
    /// The event is available when <see cref="EventType"/> is <see cref="EventType.MouseWheel"/>
    /// </remarks>
    [field: FieldOffset(0)]
    public MouseWheelEvent Wheel { get; }

    /// <summary>
    /// Gets the renderer event.
    /// </summary>
    /// <remarks>
    /// The event is available when <see cref="EventType"/> is
    /// <list type="bullet">
    /// <item><see cref="EventType.RenderDeviceLost"/></item>
    /// <item><see cref="EventType.RenderDeviceReset"/></item>
    /// <item><see cref="EventType.RenderTargetsReset"/></item>
    /// </list>
    /// </remarks>
    [field: FieldOffset(0)]
    public RendererEvent Renderer { get; }

    /// <summary>
    /// Gets the window event.
    /// </summary>
    /// <remarks>
    /// <para>
    /// It is already managed by <see cref="WindowBase.Poll(out Event)"/>
    /// manage manually.
    /// </para>
    /// The event is available when <see cref="EventType"/> is
    /// <list type="bullet">
    /// <item><see cref="EventType.EnterFullScreen"/></item>
    /// <item><see cref="EventType.FocusGained"/></item>
    /// <item><see cref="EventType.FocusLost"/></item>
    /// <item><see cref="EventType.HdrStateChanged"/></item>
    /// <item><see cref="EventType.IccProfileChanged"/></item>
    /// <item><see cref="EventType.LeaveFullScreen"/></item>
    /// <item><see cref="EventType.MouseEnter"/></item>
    /// <item><see cref="EventType.MouseLeave"/></item>
    /// <item><see cref="EventType.WindowCloseRequested"/></item>
    /// <item><see cref="EventType.WindowDestroyed"/></item>
    /// <item><see cref="EventType.WindowDisplayChanged"/></item>
    /// <item><see cref="EventType.WindowDisplayScaleChanged"/></item>
    /// <item><see cref="EventType.WindowExposed"/></item>
    /// <item><see cref="EventType.WindowHidden"/></item>
    /// <item><see cref="EventType.WindowHitTest"/></item>
    /// <item><see cref="EventType.WindowMaximized"/></item>
    /// <item><see cref="EventType.WindowMinimized"/></item>
    /// <item><see cref="EventType.WindowMoved"/></item>
    /// <item><see cref="EventType.WindowOccluded"/></item>
    /// <item><see cref="EventType.WindowPixelSizeChanged"/></item>
    /// <item><see cref="EventType.WindowResized"/></item>
    /// <item><see cref="EventType.WindowRestored"/></item>
    /// <item><see cref="EventType.WindowSafeAreaChanged"/></item>
    /// <item><see cref="EventType.WindowShown"/></item>
    /// </list>
    /// </remarks>
    [field: FieldOffset(0)]
    public WindowEvent Window { get; }
}
