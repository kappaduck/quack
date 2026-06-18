// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Input;

/// <summary>
/// The virtual key produced by a physical key under the current keyboard layout. Use this for
/// shortcuts and text-oriented input.
/// </summary>
public enum Key : uint
{
    /// <summary>
    /// An unknown or unhandled key.
    /// </summary>
    Unknown = 0x00000000,

    /// <summary>
    /// The Backspace key.
    /// </summary>
    Backspace = 0x00000008,

    /// <summary>
    /// The Tab key.
    /// </summary>
    Tab = 0x00000009,

    /// <summary>
    /// The Return key.
    /// </summary>
    Return = 0x0000000D,

    /// <summary>
    /// The Escape key.
    /// </summary>
    Escape = 0x0000001B,

    /// <summary>
    /// The Space key.
    /// </summary>
    Space = 0x00000020,

    /// <summary>
    /// The Exclaim key.
    /// </summary>
    Exclaim = 0x00000021,

    /// <summary>
    /// The Double Apostrophe key.
    /// </summary>
    DoubleApostrophe = 0x00000022,

    /// <summary>
    /// The Hash key.
    /// </summary>
    Hash = 0x00000023,

    /// <summary>
    /// The Dollar key.
    /// </summary>
    Dollar = 0x00000024,

    /// <summary>
    /// The Percent key.
    /// </summary>
    Percent = 0x00000025,

    /// <summary>
    /// The Ampersand key.
    /// </summary>
    Ampersand = 0x00000026,

    /// <summary>
    /// The Apostrophe key.
    /// </summary>
    Apostrophe = 0x00000027,

    /// <summary>
    /// The Left Paren key.
    /// </summary>
    LeftParen = 0x00000028,

    /// <summary>
    /// The Right Paren key.
    /// </summary>
    RightParen = 0x00000029,

    /// <summary>
    /// The Asterisk key.
    /// </summary>
    Asterisk = 0x0000002A,

    /// <summary>
    /// The Plus key.
    /// </summary>
    Plus = 0x0000002B,

    /// <summary>
    /// The Comma key.
    /// </summary>
    Comma = 0x0000002C,

    /// <summary>
    /// The Minus key.
    /// </summary>
    Minus = 0x0000002D,

    /// <summary>
    /// The Period key.
    /// </summary>
    Period = 0x0000002E,

    /// <summary>
    /// The Slash key.
    /// </summary>
    Slash = 0x0000002F,

    /// <summary>
    /// The Num 0 key.
    /// </summary>
    Num0 = 0x00000030,

    /// <summary>
    /// The Num 1 key.
    /// </summary>
    Num1 = 0x00000031,

    /// <summary>
    /// The Num 2 key.
    /// </summary>
    Num2 = 0x00000032,

    /// <summary>
    /// The Num 3 key.
    /// </summary>
    Num3 = 0x00000033,

    /// <summary>
    /// The Num 4 key.
    /// </summary>
    Num4 = 0x00000034,

    /// <summary>
    /// The Num 5 key.
    /// </summary>
    Num5 = 0x00000035,

    /// <summary>
    /// The Num 6 key.
    /// </summary>
    Num6 = 0x00000036,

    /// <summary>
    /// The Num 7 key.
    /// </summary>
    Num7 = 0x00000037,

    /// <summary>
    /// The Num 8 key.
    /// </summary>
    Num8 = 0x00000038,

    /// <summary>
    /// The Num 9 key.
    /// </summary>
    Num9 = 0x00000039,

    /// <summary>
    /// The Colon key.
    /// </summary>
    Colon = 0x0000003A,

    /// <summary>
    /// The Semicolon key.
    /// </summary>
    Semicolon = 0x0000003B,

    /// <summary>
    /// The Less key.
    /// </summary>
    Less = 0x0000003C,

    /// <summary>
    /// The Equals key.
    /// </summary>
    Equals = 0x0000003D,

    /// <summary>
    /// The Greater key.
    /// </summary>
    Greater = 0x0000003E,

    /// <summary>
    /// The Question key.
    /// </summary>
    Question = 0x0000003F,

    /// <summary>
    /// The At key.
    /// </summary>
    At = 0x00000040,

    /// <summary>
    /// The Left Bracket key.
    /// </summary>
    LeftBracket = 0x0000005B,

    /// <summary>
    /// The Backslash key.
    /// </summary>
    Backslash = 0x0000005C,

    /// <summary>
    /// The Right Bracket key.
    /// </summary>
    RightBracket = 0x0000005D,

    /// <summary>
    /// The Caret key.
    /// </summary>
    Caret = 0x0000005E,

    /// <summary>
    /// The Underscore key.
    /// </summary>
    Underscore = 0x0000005F,

    /// <summary>
    /// The Grave key.
    /// </summary>
    Grave = 0x00000060,

    /// <summary>
    /// The A key.
    /// </summary>
    A = 0x00000061,

    /// <summary>
    /// The B key.
    /// </summary>
    B = 0x00000062,

    /// <summary>
    /// The C key.
    /// </summary>
    C = 0x00000063,

    /// <summary>
    /// The D key.
    /// </summary>
    D = 0x00000064,

    /// <summary>
    /// The E key.
    /// </summary>
    E = 0x00000065,

    /// <summary>
    /// The F key.
    /// </summary>
    F = 0x00000066,

    /// <summary>
    /// The G key.
    /// </summary>
    G = 0x00000067,

    /// <summary>
    /// The H key.
    /// </summary>
    H = 0x00000068,

    /// <summary>
    /// The I key.
    /// </summary>
    I = 0x00000069,

    /// <summary>
    /// The J key.
    /// </summary>
    J = 0x0000006A,

    /// <summary>
    /// The K key.
    /// </summary>
    K = 0x0000006B,

    /// <summary>
    /// The L key.
    /// </summary>
    L = 0x0000006C,

    /// <summary>
    /// The M key.
    /// </summary>
    M = 0x0000006D,

    /// <summary>
    /// The N key.
    /// </summary>
    N = 0x0000006E,

    /// <summary>
    /// The O key.
    /// </summary>
    O = 0x0000006F,

    /// <summary>
    /// The P key.
    /// </summary>
    P = 0x00000070,

    /// <summary>
    /// The Q key.
    /// </summary>
    Q = 0x00000071,

    /// <summary>
    /// The R key.
    /// </summary>
    R = 0x00000072,

    /// <summary>
    /// The S key.
    /// </summary>
    S = 0x00000073,

    /// <summary>
    /// The T key.
    /// </summary>
    T = 0x00000074,

    /// <summary>
    /// The U key.
    /// </summary>
    U = 0x00000075,

    /// <summary>
    /// The V key.
    /// </summary>
    V = 0x00000076,

    /// <summary>
    /// The W key.
    /// </summary>
    W = 0x00000077,

    /// <summary>
    /// The X key.
    /// </summary>
    X = 0x00000078,

    /// <summary>
    /// The Y key.
    /// </summary>
    Y = 0x00000079,

    /// <summary>
    /// The Z key.
    /// </summary>
    Z = 0x0000007A,

    /// <summary>
    /// The Left Brace key.
    /// </summary>
    LeftBrace = 0x0000007B,

    /// <summary>
    /// The Pipe key.
    /// </summary>
    Pipe = 0x0000007C,

    /// <summary>
    /// The Right Brace key.
    /// </summary>
    RightBrace = 0x0000007D,

    /// <summary>
    /// The Tilde key.
    /// </summary>
    Tilde = 0x0000007E,

    /// <summary>
    /// The Delete key.
    /// </summary>
    Delete = 0x0000007F,

    /// <summary>
    /// The Plus Minus key.
    /// </summary>
    PlusMinus = 0x000000B1,

    /// <summary>
    /// The Left Tab key.
    /// </summary>
    LeftTab = 0x20000001,

    /// <summary>
    /// The Level5Shift key.
    /// </summary>
    Level5Shift = 0x20000002,

    /// <summary>
    /// The Multi Key Compose key.
    /// </summary>
    MultiKeyCompose = 0x20000003,

    /// <summary>
    /// The Left Meta key.
    /// </summary>
    LeftMeta = 0x20000004,

    /// <summary>
    /// The Right Meta key.
    /// </summary>
    RightMeta = 0x20000005,

    /// <summary>
    /// The Left Hyper key.
    /// </summary>
    LeftHyper = 0x20000006,

    /// <summary>
    /// The Right Hyper key.
    /// </summary>
    RightHyper = 0x20000007,

    /// <summary>
    /// The Caps Lock key.
    /// </summary>
    CapsLock = 0x40000039,

    /// <summary>
    /// The F1 key.
    /// </summary>
    F1 = 0x4000003A,

    /// <summary>
    /// The F2 key.
    /// </summary>
    F2 = 0x4000003B,

    /// <summary>
    /// The F3 key.
    /// </summary>
    F3 = 0x4000003C,

    /// <summary>
    /// The F4 key.
    /// </summary>
    F4 = 0x4000003D,

    /// <summary>
    /// The F5 key.
    /// </summary>
    F5 = 0x4000003E,

    /// <summary>
    /// The F6 key.
    /// </summary>
    F6 = 0x4000003F,

    /// <summary>
    /// The F7 key.
    /// </summary>
    F7 = 0x40000040,

    /// <summary>
    /// The F8 key.
    /// </summary>
    F8 = 0x40000041,

    /// <summary>
    /// The F9 key.
    /// </summary>
    F9 = 0x40000042,

    /// <summary>
    /// The F10 key.
    /// </summary>
    F10 = 0x40000043,

    /// <summary>
    /// The F11 key.
    /// </summary>
    F11 = 0x40000044,

    /// <summary>
    /// The F12 key.
    /// </summary>
    F12 = 0x40000045,

    /// <summary>
    /// The Print Screen key.
    /// </summary>
    PrintScreen = 0x40000046,

    /// <summary>
    /// The Scroll Lock key.
    /// </summary>
    ScrollLock = 0x40000047,

    /// <summary>
    /// The Pause key.
    /// </summary>
    Pause = 0x40000048,

    /// <summary>
    /// The Insert key.
    /// </summary>
    Insert = 0x40000049,

    /// <summary>
    /// The Home key.
    /// </summary>
    Home = 0x4000004A,

    /// <summary>
    /// The Page Up key.
    /// </summary>
    PageUp = 0x4000004B,

    /// <summary>
    /// The End key.
    /// </summary>
    End = 0x4000004D,

    /// <summary>
    /// The Page Down key.
    /// </summary>
    PageDown = 0x4000004E,

    /// <summary>
    /// The Right key.
    /// </summary>
    Right = 0x4000004F,

    /// <summary>
    /// The Left key.
    /// </summary>
    Left = 0x40000050,

    /// <summary>
    /// The Down key.
    /// </summary>
    Down = 0x40000051,

    /// <summary>
    /// The Up key.
    /// </summary>
    Up = 0x40000052,

    /// <summary>
    /// The Num Lock Clear key.
    /// </summary>
    NumLockClear = 0x40000053,

    /// <summary>
    /// The Keypad Divide key.
    /// </summary>
    KeypadDivide = 0x40000054,

    /// <summary>
    /// The Keypad Multiply key.
    /// </summary>
    KeypadMultiply = 0x40000055,

    /// <summary>
    /// The Keypad Minus key.
    /// </summary>
    KeypadMinus = 0x40000056,

    /// <summary>
    /// The Keypad Plus key.
    /// </summary>
    KeypadPlus = 0x40000057,

    /// <summary>
    /// The Keypad Enter key.
    /// </summary>
    KeypadEnter = 0x40000058,

    /// <summary>
    /// The Keypad1 key.
    /// </summary>
    Keypad1 = 0x40000059,

    /// <summary>
    /// The Keypad2 key.
    /// </summary>
    Keypad2 = 0x4000005A,

    /// <summary>
    /// The Keypad3 key.
    /// </summary>
    Keypad3 = 0x4000005B,

    /// <summary>
    /// The Keypad4 key.
    /// </summary>
    Keypad4 = 0x4000005C,

    /// <summary>
    /// The Keypad5 key.
    /// </summary>
    Keypad5 = 0x4000005D,

    /// <summary>
    /// The Keypad6 key.
    /// </summary>
    Keypad6 = 0x4000005E,

    /// <summary>
    /// The Keypad7 key.
    /// </summary>
    Keypad7 = 0x4000005F,

    /// <summary>
    /// The Keypad8 key.
    /// </summary>
    Keypad8 = 0x40000060,

    /// <summary>
    /// The Keypad9 key.
    /// </summary>
    Keypad9 = 0x40000061,

    /// <summary>
    /// The Keypad0 key.
    /// </summary>
    Keypad0 = 0x40000062,

    /// <summary>
    /// The Keypad Period key.
    /// </summary>
    KeypadPeriod = 0x40000063,

    /// <summary>
    /// The Application key.
    /// </summary>
    Application = 0x40000065,

    /// <summary>
    /// The Power key.
    /// </summary>
    Power = 0x40000066,

    /// <summary>
    /// The Keypad Equals key.
    /// </summary>
    KeypadEquals = 0x40000067,

    /// <summary>
    /// The F13 key.
    /// </summary>
    F13 = 0x40000068,

    /// <summary>
    /// The F14 key.
    /// </summary>
    F14 = 0x40000069,

    /// <summary>
    /// The F15 key.
    /// </summary>
    F15 = 0x4000006A,

    /// <summary>
    /// The F16 key.
    /// </summary>
    F16 = 0x4000006B,

    /// <summary>
    /// The F17 key.
    /// </summary>
    F17 = 0x4000006C,

    /// <summary>
    /// The F18 key.
    /// </summary>
    F18 = 0x4000006D,

    /// <summary>
    /// The F19 key.
    /// </summary>
    F19 = 0x4000006E,

    /// <summary>
    /// The F20 key.
    /// </summary>
    F20 = 0x4000006F,

    /// <summary>
    /// The F21 key.
    /// </summary>
    F21 = 0x40000070,

    /// <summary>
    /// The F22 key.
    /// </summary>
    F22 = 0x40000071,

    /// <summary>
    /// The F23 key.
    /// </summary>
    F23 = 0x40000072,

    /// <summary>
    /// The F24 key.
    /// </summary>
    F24 = 0x40000073,

    /// <summary>
    /// The Execute key.
    /// </summary>
    Execute = 0x40000074,

    /// <summary>
    /// The Help key.
    /// </summary>
    Help = 0x40000075,

    /// <summary>
    /// The Menu key.
    /// </summary>
    Menu = 0x40000076,

    /// <summary>
    /// The Select key.
    /// </summary>
    Select = 0x40000077,

    /// <summary>
    /// The Stop key.
    /// </summary>
    Stop = 0x40000078,

    /// <summary>
    /// The Again key.
    /// </summary>
    Again = 0x40000079,

    /// <summary>
    /// The Undo key.
    /// </summary>
    Undo = 0x4000007A,

    /// <summary>
    /// The Cut key.
    /// </summary>
    Cut = 0x4000007B,

    /// <summary>
    /// The Copy key.
    /// </summary>
    Copy = 0x4000007C,

    /// <summary>
    /// The Paste key.
    /// </summary>
    Paste = 0x4000007D,

    /// <summary>
    /// The Find key.
    /// </summary>
    Find = 0x4000007E,

    /// <summary>
    /// The Mute key.
    /// </summary>
    Mute = 0x4000007F,

    /// <summary>
    /// The Volume Up key.
    /// </summary>
    VolumeUp = 0x40000080,

    /// <summary>
    /// The Volume Down key.
    /// </summary>
    VolumeDown = 0x40000081,

    /// <summary>
    /// The Keypad Comma key.
    /// </summary>
    KeypadComma = 0x40000085,

    /// <summary>
    /// The Keypad Equals As400 key.
    /// </summary>
    KeypadEqualsAs400 = 0x40000086,

    /// <summary>
    /// The Alt Erase key.
    /// </summary>
    AltErase = 0x40000099,

    /// <summary>
    /// The Sys Req key.
    /// </summary>
    SysReq = 0x4000009A,

    /// <summary>
    /// The Cancel key.
    /// </summary>
    Cancel = 0x4000009B,

    /// <summary>
    /// The Clear key.
    /// </summary>
    Clear = 0x4000009C,

    /// <summary>
    /// The Prior key.
    /// </summary>
    Prior = 0x4000009D,

    /// <summary>
    /// The Return2 key.
    /// </summary>
    Return2 = 0x4000009E,

    /// <summary>
    /// The Separator key.
    /// </summary>
    Separator = 0x4000009F,

    /// <summary>
    /// The Out key.
    /// </summary>
    Out = 0x400000A0,

    /// <summary>
    /// The Oper key.
    /// </summary>
    Oper = 0x400000A1,

    /// <summary>
    /// The Clear Again key.
    /// </summary>
    ClearAgain = 0x400000A2,

    /// <summary>
    /// The Cr Sel key.
    /// </summary>
    CrSel = 0x400000A3,

    /// <summary>
    /// The Ex Sel key.
    /// </summary>
    ExSel = 0x400000A4,

    /// <summary>
    /// The Keypad00 key.
    /// </summary>
    Keypad00 = 0x400000B0,

    /// <summary>
    /// The Keypad000 key.
    /// </summary>
    Keypad000 = 0x400000B1,

    /// <summary>
    /// The Thousands Separator key.
    /// </summary>
    ThousandsSeparator = 0x400000B2,

    /// <summary>
    /// The Decimal Separator key.
    /// </summary>
    DecimalSeparator = 0x400000B3,

    /// <summary>
    /// The Currency Unit key.
    /// </summary>
    CurrencyUnit = 0x400000B4,

    /// <summary>
    /// The Currency Subunit key.
    /// </summary>
    CurrencySubunit = 0x400000B5,

    /// <summary>
    /// The Keypad Left Paren key.
    /// </summary>
    KeypadLeftParen = 0x400000B6,

    /// <summary>
    /// The Keypad Right Paren key.
    /// </summary>
    KeypadRightParen = 0x400000B7,

    /// <summary>
    /// The Keypad Left Brace key.
    /// </summary>
    KeypadLeftBrace = 0x400000B8,

    /// <summary>
    /// The Keypad Right Brace key.
    /// </summary>
    KeypadRightBrace = 0x400000B9,

    /// <summary>
    /// The Keypad Tab key.
    /// </summary>
    KeypadTab = 0x400000BA,

    /// <summary>
    /// The Keypad Backspace key.
    /// </summary>
    KeypadBackspace = 0x400000BB,

    /// <summary>
    /// The Keypad A key.
    /// </summary>
    KeypadA = 0x400000BC,

    /// <summary>
    /// The Keypad B key.
    /// </summary>
    KeypadB = 0x400000BD,

    /// <summary>
    /// The Keypad C key.
    /// </summary>
    KeypadC = 0x400000BE,

    /// <summary>
    /// The Keypad D key.
    /// </summary>
    KeypadD = 0x400000BF,

    /// <summary>
    /// The Keypad E key.
    /// </summary>
    KeypadE = 0x400000C0,

    /// <summary>
    /// The Keypad F key.
    /// </summary>
    KeypadF = 0x400000C1,

    /// <summary>
    /// The Keypad Xor key.
    /// </summary>
    KeypadXor = 0x400000C2,

    /// <summary>
    /// The Keypad Power key.
    /// </summary>
    KeypadPower = 0x400000C3,

    /// <summary>
    /// The Keypad Percent key.
    /// </summary>
    KeypadPercent = 0x400000C4,

    /// <summary>
    /// The Keypad Less key.
    /// </summary>
    KeypadLess = 0x400000C5,

    /// <summary>
    /// The Keypad Greater key.
    /// </summary>
    KeypadGreater = 0x400000C6,

    /// <summary>
    /// The Keypad Ampersand key.
    /// </summary>
    KeypadAmpersand = 0x400000C7,

    /// <summary>
    /// The Keypad Double Ampersand key.
    /// </summary>
    KeypadDoubleAmpersand = 0x400000C8,

    /// <summary>
    /// The Keypad Vertical Bar key.
    /// </summary>
    KeypadVerticalBar = 0x400000C9,

    /// <summary>
    /// The Keypad Double Vertical Bar key.
    /// </summary>
    KeypadDoubleVerticalBar = 0x400000CA,

    /// <summary>
    /// The Keypad Colon key.
    /// </summary>
    KeypadColon = 0x400000CB,

    /// <summary>
    /// The Keypad Hash key.
    /// </summary>
    KeypadHash = 0x400000CC,

    /// <summary>
    /// The Keypad Space key.
    /// </summary>
    KeypadSpace = 0x400000CD,

    /// <summary>
    /// The Keypad At key.
    /// </summary>
    KeypadAt = 0x400000CE,

    /// <summary>
    /// The Keypad Exclam key.
    /// </summary>
    KeypadExclam = 0x400000CF,

    /// <summary>
    /// The Keypad Mem Store key.
    /// </summary>
    KeypadMemStore = 0x400000D0,

    /// <summary>
    /// The Keypad Mem Recall key.
    /// </summary>
    KeypadMemRecall = 0x400000D1,

    /// <summary>
    /// The Keypad Mem Clear key.
    /// </summary>
    KeypadMemClear = 0x400000D2,

    /// <summary>
    /// The Keypad Mem Add key.
    /// </summary>
    KeypadMemAdd = 0x400000D3,

    /// <summary>
    /// The Keypad Mem Subtract key.
    /// </summary>
    KeypadMemSubtract = 0x400000D4,

    /// <summary>
    /// The Keypad Mem Multiply key.
    /// </summary>
    KeypadMemMultiply = 0x400000D5,

    /// <summary>
    /// The Keypad Mem Divide key.
    /// </summary>
    KeypadMemDivide = 0x400000D6,

    /// <summary>
    /// The Keypad Plus Minus key.
    /// </summary>
    KeypadPlusMinus = 0x400000D7,

    /// <summary>
    /// The Keypad Clear key.
    /// </summary>
    KeypadClear = 0x400000D8,

    /// <summary>
    /// The Keypad Clear Entry key.
    /// </summary>
    KeypadClearEntry = 0x400000D9,

    /// <summary>
    /// The Keypad Binary key.
    /// </summary>
    KeypadBinary = 0x400000DA,

    /// <summary>
    /// The Keypad Octal key.
    /// </summary>
    KeypadOctal = 0x400000DB,

    /// <summary>
    /// The Keypad Decimal key.
    /// </summary>
    KeypadDecimal = 0x400000DC,

    /// <summary>
    /// The Keypad Hexadecimal key.
    /// </summary>
    KeypadHexadecimal = 0x400000DD,

    /// <summary>
    /// The Left Control key.
    /// </summary>
    LeftControl = 0x400000E0,

    /// <summary>
    /// The Left Shift key.
    /// </summary>
    LeftShift = 0x400000E1,

    /// <summary>
    /// The Left Alt key.
    /// </summary>
    LeftAlt = 0x400000E2,

    /// <summary>
    /// The Left Gui key.
    /// </summary>
    LeftGui = 0x400000E3,

    /// <summary>
    /// The Right Control key.
    /// </summary>
    RightControl = 0x400000E4,

    /// <summary>
    /// The Right Shift key.
    /// </summary>
    RightShift = 0x400000E5,

    /// <summary>
    /// The Right Alt key.
    /// </summary>
    RightAlt = 0x400000E6,

    /// <summary>
    /// The Right Gui key.
    /// </summary>
    RightGui = 0x400000E7,

    /// <summary>
    /// The Mode key.
    /// </summary>
    Mode = 0x40000101,

    /// <summary>
    /// The Sleep key.
    /// </summary>
    Sleep = 0x40000102,

    /// <summary>
    /// The Wake key.
    /// </summary>
    Wake = 0x40000103,

    /// <summary>
    /// The Channel Increment key.
    /// </summary>
    ChannelIncrement = 0x40000104,

    /// <summary>
    /// The Channel Decrement key.
    /// </summary>
    ChannelDecrement = 0x40000105,

    /// <summary>
    /// The Media Play key.
    /// </summary>
    MediaPlay = 0x40000106,

    /// <summary>
    /// The Media Pause key.
    /// </summary>
    MediaPause = 0x40000107,

    /// <summary>
    /// The Media Record key.
    /// </summary>
    MediaRecord = 0x40000108,

    /// <summary>
    /// The Media Fast Forward key.
    /// </summary>
    MediaFastForward = 0x40000109,

    /// <summary>
    /// The Media Rewind key.
    /// </summary>
    MediaRewind = 0x4000010A,

    /// <summary>
    /// The Media Next Track key.
    /// </summary>
    MediaNextTrack = 0x4000010B,

    /// <summary>
    /// The Media Previous Track key.
    /// </summary>
    MediaPreviousTrack = 0x4000010C,

    /// <summary>
    /// The Media Stop key.
    /// </summary>
    MediaStop = 0x4000010D,

    /// <summary>
    /// The Media Eject key.
    /// </summary>
    MediaEject = 0x4000010E,

    /// <summary>
    /// The Media Play Pause key.
    /// </summary>
    MediaPlayPause = 0x4000010F,

    /// <summary>
    /// The Media Select key.
    /// </summary>
    MediaSelect = 0x40000110,

    /// <summary>
    /// The Ac New key.
    /// </summary>
    AcNew = 0x40000111,

    /// <summary>
    /// The Ac Open key.
    /// </summary>
    AcOpen = 0x40000112,

    /// <summary>
    /// The Ac Close key.
    /// </summary>
    AcClose = 0x40000113,

    /// <summary>
    /// The Ac Exit key.
    /// </summary>
    AcExit = 0x40000114,

    /// <summary>
    /// The Ac Save key.
    /// </summary>
    AcSave = 0x40000115,

    /// <summary>
    /// The Ac Print key.
    /// </summary>
    AcPrint = 0x40000116,

    /// <summary>
    /// The Ac Properties key.
    /// </summary>
    AcProperties = 0x40000117,

    /// <summary>
    /// The Ac Search key.
    /// </summary>
    AcSearch = 0x40000118,

    /// <summary>
    /// The Ac Home key.
    /// </summary>
    AcHome = 0x40000119,

    /// <summary>
    /// The Ac Back key.
    /// </summary>
    AcBack = 0x4000011A,

    /// <summary>
    /// The Ac Forward key.
    /// </summary>
    AcForward = 0x4000011B,

    /// <summary>
    /// The Ac Stop key.
    /// </summary>
    AcStop = 0x4000011C,

    /// <summary>
    /// The Ac Refresh key.
    /// </summary>
    AcRefresh = 0x4000011D,

    /// <summary>
    /// The Ac Bookmarks key.
    /// </summary>
    AcBookmarks = 0x4000011E,

    /// <summary>
    /// The Soft Left key.
    /// </summary>
    SoftLeft = 0x4000011F,

    /// <summary>
    /// The Soft Right key.
    /// </summary>
    SoftRight = 0x40000120,

    /// <summary>
    /// The Call key.
    /// </summary>
    Call = 0x40000121,

    /// <summary>
    /// The End Call key.
    /// </summary>
    EndCall = 0x40000122
}
