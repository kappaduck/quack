// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Input;

/// <summary>
/// A set of keyboard modifier keys held down, such as Shift, Ctrl or Alt.
/// </summary>
[Flags]
public enum Keymod : ushort
{
    /// <summary>
    /// No modifier is applicable.
    /// </summary>
    None = 0x0000,

    /// <summary>
    /// The left Shift key is down.
    /// </summary>
    LeftShift = 0x0001,

    /// <summary>
    /// The right Shift key is down.
    /// </summary>
    RightShift = 0x0002,

    /// <summary>
    /// Either Shift key is down.
    /// </summary>
    Shift = LeftShift | RightShift,

    /// <summary>
    /// The Level 5 Shift key is down.
    /// </summary>
    Level5 = 0x0004,

    /// <summary>
    /// The left Ctrl (Control) key is down.
    /// </summary>
    LeftControl = 0x0040,

    /// <summary>
    /// The right Ctrl (Control) key is down.
    /// </summary>
    RightControl = 0x0080,

    /// <summary>
    /// Either Ctrl key is down.
    /// </summary>
    Control = LeftControl | RightControl,

    /// <summary>
    /// The left Alt key is down.
    /// </summary>
    LeftAlt = 0x0100,

    /// <summary>
    /// The right Alt key is down.
    /// </summary>
    RightAlt = 0x0200,

    /// <summary>
    /// Either Alt key is down.
    /// </summary>
    Alt = LeftAlt | RightAlt,

    /// <summary>
    /// The left GUI key (often the Windows key) is down.
    /// </summary>
    LeftGui = 0x0400,

    /// <summary>
    /// The right GUI key (often the Windows key) is down.
    /// </summary>
    RightGui = 0x0800,

    /// <summary>
    /// Either GUI key (often the Windows or Command key) is down.
    /// </summary>
    Gui = LeftGui | RightGui,

    /// <summary>
    /// The Num Lock key (may be located on an extended keypad) is down.
    /// </summary>
    NumLock = 0x1000,

    /// <summary>
    /// The Caps Lock key is down.
    /// </summary>
    CapsLock = 0x2000,

    /// <summary>
    /// The Mode (AltGr) key is down.
    /// </summary>
    Mode = 0x4000,

    /// <summary>
    /// The Scroll Lock key is down.
    /// </summary>
    ScrollLock = 0x8000
}
