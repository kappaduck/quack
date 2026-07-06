// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Input;

/// <summary>
/// Cursor types.
/// </summary>
public enum CursorType
{
    /// <summary>
    /// Default cursor. Usually an arrow.
    /// </summary>
    Default = 0,

    /// <summary>
    /// Text selection. Usually an I-beam.
    /// </summary>
    Text = 1,

    /// <summary>
    /// Wait. Usually an hourglass or watch or spinning ball.
    /// </summary>
    Wait = 2,

    /// <summary>
    /// Crosshair.
    /// </summary>
    Crosshair = 3,

    /// <summary>
    /// Program is busy but still interactive. Usually it's WAIT with an arrow.
    /// </summary>
    Progress = 4,

    /// <summary>
    /// Double arrow pointing northwest and southeast.
    /// </summary>
    DoubleResizeNorthWestSouthEast = 5,

    /// <summary>
    /// Double arrow pointing northeast and southwest.
    /// </summary>
    DoubleResizeNorthEastSouthWest = 6,

    /// <summary>
    /// Double arrow pointing west and east.
    /// </summary>
    DoubleResizeEastWest = 7,

    /// <summary>
    /// Double arrow pointing north and south.
    /// </summary>
    DoubleResizeNorthSouth = 8,

    /// <summary>
    /// Four pointed arrow pointing north, south, east, and west.
    /// </summary>
    Move = 9,
    /// <summary>
    /// Not permitted. Usually a slashed circle or crossbones.
    /// </summary>
    NotAllowed = 10,

    /// <summary>
    /// Pointer that indicates a link. Usually a pointing hand.
    /// </summary>
    PointingHand = 11,

    /// <summary>
    /// Window resize top-left. This may be a single arrow or a double arrow like <see cref="DoubleResizeNorthWestSouthEast"/>.
    /// </summary>
    ResizeNorthWestSouthEast = 12,

    /// <summary>
    /// Window resize top. May be <see cref="ResizeNorthSouth"/>.
    /// </summary>
    ResizeNorthSouth = 13,

    /// <summary>
    /// Window resize top-right. May be <see cref="ResizeNorthEast"/>.
    /// </summary>
    ResizeNorthEast = 14,

    /// <summary>
    /// Window resize right. May be <see cref="ResizeEastWest"/>.
    /// </summary>
    ResizeEastWest = 15,

    /// <summary>
    /// Window resize bottom-right. May be <see cref="ResizeSouthEast"/>.
    /// </summary>
    ResizeSouthEast = 16,

    /// <summary>
    /// Window resize bottom. May be <see cref="ResizeSouth"/>.
    /// </summary>
    ResizeSouth = 17,

    /// <summary>
    /// Window resize bottom-left. May be <see cref="ResizeSouthWest"/>.
    /// </summary>
    ResizeSouthWest = 18,

    /// <summary>
    /// Window resize left. May be <see cref="ResizeWest"/>.
    /// </summary>
    ResizeWest = 19,

    /// <summary>
    /// A context menu is available for the object under the cursor.
    /// </summary>
    ContextMenu = 20,

    /// <summary>
    /// Help is available for the object under the cursor.
    /// </summary>
    Help = 21,

    /// <summary>
    /// A set of cells may be selected.
    /// </summary>
    Cell = 22,

    /// <summary>
    /// Text selection. May be <see cref="Text"/>.
    /// </summary>
    VerticalText = 23,

    /// <summary>
    /// A shortcut is to be created.
    /// </summary>
    Alias = 24,

    /// <summary>
    /// Something is to be copied.
    /// </summary>
    Copy = 25,

    /// <summary>
    /// The dragged item cannot be dropped at this location. May be <see cref="NotAllowed"/>.
    /// </summary>
    NoDrop = 26,

    /// <summary>
    /// The object under the cursor can be grabbed.
    /// </summary>
    Grab = 27,

    /// <summary>
    /// An object is currently being grabbed.
    /// </summary>
    Grabbing = 28,

    /// <summary>
    /// Column resize. May be <see cref="ResizeEastWest"/>.
    /// </summary>
    ResizeColumn = 29,

    /// <summary>
    /// Row resize. May be <see cref="ResizeNorthSouth"/>.
    /// </summary>
    ResizeRow = 30,

    /// <summary>
    /// Four pointed arrow pointing north, south, east, and west, indicating that scrolling is available in every direction.
    /// </summary>
    AllScroll = 31,

    /// <summary>
    /// Zoom in.
    /// </summary>
    ZoomIn = 32,

    /// <summary>
    /// Zoom out.
    /// </summary>
    ZoomOut = 33
}
