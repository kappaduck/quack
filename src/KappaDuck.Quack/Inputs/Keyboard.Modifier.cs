// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Inputs;

/// <summary>
/// The keyboard modifiers.
/// </summary>
public sealed partial class Keyboard
{
    /// <summary>
    /// The keyboard modifiers.
    /// </summary>
    [Flags]
    public enum Modifier : ushort
    {
        /// <summary>
        /// No modifier is applicable.
        /// </summary>
        None = 0x0000,

        /// <summary>
        /// The shift key.
        /// </summary>
        LeftShift = 0x0001,

        /// <summary>
        /// The right shift key.
        /// </summary>
        RightShift = 0x0002,

        /// <summary>
        /// Any Shift key.
        /// </summary>
        Shift = LeftShift | RightShift,

        /// <summary>
        /// The Level 5 shift key.
        /// </summary>
        Level5 = 0x0004,

        /// <summary>
        /// The left control key.
        /// </summary>
        LeftCtrl = 0x0040,

        /// <summary>
        /// The right control key.
        /// </summary>
        RightCtrl = 0x0080,

        /// <summary>
        /// Any Control key.
        /// </summary>
        Ctrl = LeftCtrl | RightCtrl,

        /// <summary>
        /// The left alt key.
        /// </summary>
        LeftAlt = 0x0100,

        /// <summary>
        /// The right alt key.
        /// </summary>
        RightAlt = 0x0200,

        /// <summary>
        /// Any Alt key.
        /// </summary>
        Alt = LeftAlt | RightAlt,

        /// <summary>
        /// The left meta key. (Windows key, Command key, etc...)
        /// </summary>
        LeftGui = 0x0400,

        /// <summary>
        /// The right meta key. (Windows key, Command key, etc...)
        /// </summary>
        RightGui = 0x0800,

        /// <summary>
        /// Any Meta key.
        /// </summary>
        Gui = LeftGui | RightGui,

        /// <summary>
        /// The num lock key (can be located on an extended keypad).
        /// </summary>
        NumLock = 0x1000,

        /// <summary>
        /// The caps lock key.
        /// </summary>
        CapsLock = 0x2000,

        /// <summary>
        /// The !AltGr key.
        /// </summary>
        Mode = 0x4000,

        /// <summary>
        /// The scroll lock key.
        /// </summary>
        ScrollLock = 0x8000
    }
}
