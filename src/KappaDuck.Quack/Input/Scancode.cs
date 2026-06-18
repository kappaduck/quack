// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Input;

/// <summary>
/// The physical key on a keyboard, independent of layout and language. Use this when key position
/// matters more than the symbol it produces, such as WASD movement.
/// </summary>
public enum Scancode
{
    /// <summary>
    /// An unknown or unhandled key.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// The A key.
    /// </summary>
    A = 4,

    /// <summary>
    /// The B key.
    /// </summary>
    B = 5,

    /// <summary>
    /// The C key.
    /// </summary>
    C = 6,

    /// <summary>
    /// The D key.
    /// </summary>
    D = 7,

    /// <summary>
    /// The E key.
    /// </summary>
    E = 8,

    /// <summary>
    /// The F key.
    /// </summary>
    F = 9,

    /// <summary>
    /// The G key.
    /// </summary>
    G = 10,

    /// <summary>
    /// The H key.
    /// </summary>
    H = 11,

    /// <summary>
    /// The I key.
    /// </summary>
    I = 12,

    /// <summary>
    /// The J key.
    /// </summary>
    J = 13,

    /// <summary>
    /// The K key.
    /// </summary>
    K = 14,

    /// <summary>
    /// The L key.
    /// </summary>
    L = 15,

    /// <summary>
    /// The M key.
    /// </summary>
    M = 16,

    /// <summary>
    /// The N key.
    /// </summary>
    N = 17,

    /// <summary>
    /// The O key.
    /// </summary>
    O = 18,

    /// <summary>
    /// The P key.
    /// </summary>
    P = 19,

    /// <summary>
    /// The Q key.
    /// </summary>
    Q = 20,

    /// <summary>
    /// The R key.
    /// </summary>
    R = 21,

    /// <summary>
    /// The S key.
    /// </summary>
    S = 22,

    /// <summary>
    /// The T key.
    /// </summary>
    T = 23,

    /// <summary>
    /// The U key.
    /// </summary>
    U = 24,

    /// <summary>
    /// The V key.
    /// </summary>
    V = 25,

    /// <summary>
    /// The W key.
    /// </summary>
    W = 26,

    /// <summary>
    /// The X key.
    /// </summary>
    X = 27,

    /// <summary>
    /// The Y key.
    /// </summary>
    Y = 28,

    /// <summary>
    /// The Z key.
    /// </summary>
    Z = 29,

    /// <summary>
    /// The Num 1 key.
    /// </summary>
    Num1 = 30,

    /// <summary>
    /// The Num 2 key.
    /// </summary>
    Num2 = 31,

    /// <summary>
    /// The Num 3 key.
    /// </summary>
    Num3 = 32,

    /// <summary>
    /// The Num 4 key.
    /// </summary>
    Num4 = 33,

    /// <summary>
    /// The Num 5 key.
    /// </summary>
    Num5 = 34,

    /// <summary>
    /// The Num 6 key.
    /// </summary>
    Num6 = 35,

    /// <summary>
    /// The Num 7 key.
    /// </summary>
    Num7 = 36,

    /// <summary>
    /// The Num 8 key.
    /// </summary>
    Num8 = 37,

    /// <summary>
    /// The Num 9 key.
    /// </summary>
    Num9 = 38,

    /// <summary>
    /// The Num 0 key.
    /// </summary>
    Num0 = 39,

    /// <summary>
    /// The Return key.
    /// </summary>
    Return = 40,

    /// <summary>
    /// The Escape key.
    /// </summary>
    Escape = 41,

    /// <summary>
    /// The Backspace key.
    /// </summary>
    Backspace = 42,

    /// <summary>
    /// The Tab key.
    /// </summary>
    Tab = 43,

    /// <summary>
    /// The Space key.
    /// </summary>
    Space = 44,

    /// <summary>
    /// The Minus key.
    /// </summary>
    Minus = 45,

    /// <summary>
    /// The Equals key.
    /// </summary>
    Equals = 46,

    /// <summary>
    /// The Left Bracket key.
    /// </summary>
    LeftBracket = 47,

    /// <summary>
    /// The Right Bracket key.
    /// </summary>
    RightBracket = 48,

    /// <summary>
    /// The Backslash key.
    /// </summary>
    Backslash = 49,

    /// <summary>
    /// The Non Us Hash key.
    /// </summary>
    NonUsHash = 50,

    /// <summary>
    /// The Semicolon key.
    /// </summary>
    Semicolon = 51,

    /// <summary>
    /// The Apostrophe key.
    /// </summary>
    Apostrophe = 52,

    /// <summary>
    /// The Grave key.
    /// </summary>
    Grave = 53,

    /// <summary>
    /// The Comma key.
    /// </summary>
    Comma = 54,

    /// <summary>
    /// The Period key.
    /// </summary>
    Period = 55,

    /// <summary>
    /// The Slash key.
    /// </summary>
    Slash = 56,

    /// <summary>
    /// The Caps Lock key.
    /// </summary>
    CapsLock = 57,

    /// <summary>
    /// The F1 key.
    /// </summary>
    F1 = 58,

    /// <summary>
    /// The F2 key.
    /// </summary>
    F2 = 59,

    /// <summary>
    /// The F3 key.
    /// </summary>
    F3 = 60,

    /// <summary>
    /// The F4 key.
    /// </summary>
    F4 = 61,

    /// <summary>
    /// The F5 key.
    /// </summary>
    F5 = 62,

    /// <summary>
    /// The F6 key.
    /// </summary>
    F6 = 63,

    /// <summary>
    /// The F7 key.
    /// </summary>
    F7 = 64,

    /// <summary>
    /// The F8 key.
    /// </summary>
    F8 = 65,

    /// <summary>
    /// The F9 key.
    /// </summary>
    F9 = 66,

    /// <summary>
    /// The F10 key.
    /// </summary>
    F10 = 67,

    /// <summary>
    /// The F11 key.
    /// </summary>
    F11 = 68,

    /// <summary>
    /// The F12 key.
    /// </summary>
    F12 = 69,

    /// <summary>
    /// The Print Screen key.
    /// </summary>
    PrintScreen = 70,

    /// <summary>
    /// The Scroll Lock key.
    /// </summary>
    ScrollLock = 71,

    /// <summary>
    /// The Pause key.
    /// </summary>
    Pause = 72,

    /// <summary>
    /// The Insert key.
    /// </summary>
    Insert = 73,

    /// <summary>
    /// The Home key.
    /// </summary>
    Home = 74,

    /// <summary>
    /// The Page Up key.
    /// </summary>
    PageUp = 75,

    /// <summary>
    /// The Delete key.
    /// </summary>
    Delete = 76,

    /// <summary>
    /// The End key.
    /// </summary>
    End = 77,

    /// <summary>
    /// The Page Down key.
    /// </summary>
    PageDown = 78,

    /// <summary>
    /// The Right key.
    /// </summary>
    Right = 79,

    /// <summary>
    /// The Left key.
    /// </summary>
    Left = 80,

    /// <summary>
    /// The Down key.
    /// </summary>
    Down = 81,

    /// <summary>
    /// The Up key.
    /// </summary>
    Up = 82,

    /// <summary>
    /// The Num Lock Clear key.
    /// </summary>
    NumLockClear = 83,

    /// <summary>
    /// The Keypad Divide key.
    /// </summary>
    KeypadDivide = 84,

    /// <summary>
    /// The Keypad Multiply key.
    /// </summary>
    KeypadMultiply = 85,

    /// <summary>
    /// The Keypad Minus key.
    /// </summary>
    KeypadMinus = 86,

    /// <summary>
    /// The Keypad Plus key.
    /// </summary>
    KeypadPlus = 87,

    /// <summary>
    /// The Keypad Enter key.
    /// </summary>
    KeypadEnter = 88,

    /// <summary>
    /// The Keypad1 key.
    /// </summary>
    Keypad1 = 89,

    /// <summary>
    /// The Keypad2 key.
    /// </summary>
    Keypad2 = 90,

    /// <summary>
    /// The Keypad3 key.
    /// </summary>
    Keypad3 = 91,

    /// <summary>
    /// The Keypad4 key.
    /// </summary>
    Keypad4 = 92,

    /// <summary>
    /// The Keypad5 key.
    /// </summary>
    Keypad5 = 93,

    /// <summary>
    /// The Keypad6 key.
    /// </summary>
    Keypad6 = 94,

    /// <summary>
    /// The Keypad7 key.
    /// </summary>
    Keypad7 = 95,

    /// <summary>
    /// The Keypad8 key.
    /// </summary>
    Keypad8 = 96,

    /// <summary>
    /// The Keypad9 key.
    /// </summary>
    Keypad9 = 97,

    /// <summary>
    /// The Keypad0 key.
    /// </summary>
    Keypad0 = 98,

    /// <summary>
    /// The Keypad Period key.
    /// </summary>
    KeypadPeriod = 99,

    /// <summary>
    /// The Non Us Backslash key.
    /// </summary>
    NonUsBackslash = 100,

    /// <summary>
    /// The Application key.
    /// </summary>
    Application = 101,

    /// <summary>
    /// The Power key.
    /// </summary>
    Power = 102,

    /// <summary>
    /// The Keypad Equals key.
    /// </summary>
    KeypadEquals = 103,

    /// <summary>
    /// The F13 key.
    /// </summary>
    F13 = 104,

    /// <summary>
    /// The F14 key.
    /// </summary>
    F14 = 105,

    /// <summary>
    /// The F15 key.
    /// </summary>
    F15 = 106,

    /// <summary>
    /// The F16 key.
    /// </summary>
    F16 = 107,

    /// <summary>
    /// The F17 key.
    /// </summary>
    F17 = 108,

    /// <summary>
    /// The F18 key.
    /// </summary>
    F18 = 109,

    /// <summary>
    /// The F19 key.
    /// </summary>
    F19 = 110,

    /// <summary>
    /// The F20 key.
    /// </summary>
    F20 = 111,

    /// <summary>
    /// The F21 key.
    /// </summary>
    F21 = 112,

    /// <summary>
    /// The F22 key.
    /// </summary>
    F22 = 113,

    /// <summary>
    /// The F23 key.
    /// </summary>
    F23 = 114,

    /// <summary>
    /// The F24 key.
    /// </summary>
    F24 = 115,

    /// <summary>
    /// The Execute key.
    /// </summary>
    Execute = 116,

    /// <summary>
    /// The Help key.
    /// </summary>
    Help = 117,

    /// <summary>
    /// The Menu key.
    /// </summary>
    Menu = 118,

    /// <summary>
    /// The Select key.
    /// </summary>
    Select = 119,

    /// <summary>
    /// The Stop key.
    /// </summary>
    Stop = 120,

    /// <summary>
    /// The Again key.
    /// </summary>
    Again = 121,

    /// <summary>
    /// The Undo key.
    /// </summary>
    Undo = 122,

    /// <summary>
    /// The Cut key.
    /// </summary>
    Cut = 123,

    /// <summary>
    /// The Copy key.
    /// </summary>
    Copy = 124,

    /// <summary>
    /// The Paste key.
    /// </summary>
    Paste = 125,

    /// <summary>
    /// The Find key.
    /// </summary>
    Find = 126,

    /// <summary>
    /// The Mute key.
    /// </summary>
    Mute = 127,

    /// <summary>
    /// The Volume Up key.
    /// </summary>
    VolumeUp = 128,

    /// <summary>
    /// The Volume Down key.
    /// </summary>
    VolumeDown = 129,

    /// <summary>
    /// The Keypad Comma key.
    /// </summary>
    KeypadComma = 133,

    /// <summary>
    /// The Keypad Equals As400 key.
    /// </summary>
    KeypadEqualsAs400 = 134,

    /// <summary>
    /// The International1 key.
    /// </summary>
    International1 = 135,

    /// <summary>
    /// The International2 key.
    /// </summary>
    International2 = 136,

    /// <summary>
    /// The International3 key.
    /// </summary>
    International3 = 137,

    /// <summary>
    /// The International4 key.
    /// </summary>
    International4 = 138,

    /// <summary>
    /// The International5 key.
    /// </summary>
    International5 = 139,

    /// <summary>
    /// The International6 key.
    /// </summary>
    International6 = 140,

    /// <summary>
    /// The International7 key.
    /// </summary>
    International7 = 141,

    /// <summary>
    /// The International8 key.
    /// </summary>
    International8 = 142,

    /// <summary>
    /// The International9 key.
    /// </summary>
    International9 = 143,

    /// <summary>
    /// The Lang1 key.
    /// </summary>
    Lang1 = 144,

    /// <summary>
    /// The Lang2 key.
    /// </summary>
    Lang2 = 145,

    /// <summary>
    /// The Lang3 key.
    /// </summary>
    Lang3 = 146,

    /// <summary>
    /// The Lang4 key.
    /// </summary>
    Lang4 = 147,

    /// <summary>
    /// The Lang5 key.
    /// </summary>
    Lang5 = 148,

    /// <summary>
    /// The Lang6 key.
    /// </summary>
    Lang6 = 149,

    /// <summary>
    /// The Lang7 key.
    /// </summary>
    Lang7 = 150,

    /// <summary>
    /// The Lang8 key.
    /// </summary>
    Lang8 = 151,

    /// <summary>
    /// The Lang9 key.
    /// </summary>
    Lang9 = 152,

    /// <summary>
    /// The Alt Erase key.
    /// </summary>
    AltErase = 153,

    /// <summary>
    /// The Sys Req key.
    /// </summary>
    SysReq = 154,

    /// <summary>
    /// The Cancel key.
    /// </summary>
    Cancel = 155,

    /// <summary>
    /// The Clear key.
    /// </summary>
    Clear = 156,

    /// <summary>
    /// The Prior key.
    /// </summary>
    Prior = 157,

    /// <summary>
    /// The Return2 key.
    /// </summary>
    Return2 = 158,

    /// <summary>
    /// The Separator key.
    /// </summary>
    Separator = 159,

    /// <summary>
    /// The Out key.
    /// </summary>
    Out = 160,

    /// <summary>
    /// The Oper key.
    /// </summary>
    Oper = 161,

    /// <summary>
    /// The Clear Again key.
    /// </summary>
    ClearAgain = 162,

    /// <summary>
    /// The Cr Sel key.
    /// </summary>
    CrSel = 163,

    /// <summary>
    /// The Ex Sel key.
    /// </summary>
    ExSel = 164,

    /// <summary>
    /// The Keypad00 key.
    /// </summary>
    Keypad00 = 176,

    /// <summary>
    /// The Keypad000 key.
    /// </summary>
    Keypad000 = 177,

    /// <summary>
    /// The Thousands Separator key.
    /// </summary>
    ThousandsSeparator = 178,

    /// <summary>
    /// The Decimal Separator key.
    /// </summary>
    DecimalSeparator = 179,

    /// <summary>
    /// The Currency Unit key.
    /// </summary>
    CurrencyUnit = 180,

    /// <summary>
    /// The Currency Subunit key.
    /// </summary>
    CurrencySubunit = 181,

    /// <summary>
    /// The Keypad Left Paren key.
    /// </summary>
    KeypadLeftParen = 182,

    /// <summary>
    /// The Keypad Right Paren key.
    /// </summary>
    KeypadRightParen = 183,

    /// <summary>
    /// The Keypad Left Brace key.
    /// </summary>
    KeypadLeftBrace = 184,

    /// <summary>
    /// The Keypad Right Brace key.
    /// </summary>
    KeypadRightBrace = 185,

    /// <summary>
    /// The Keypad Tab key.
    /// </summary>
    KeypadTab = 186,

    /// <summary>
    /// The Keypad Backspace key.
    /// </summary>
    KeypadBackspace = 187,

    /// <summary>
    /// The Keypad A key.
    /// </summary>
    KeypadA = 188,

    /// <summary>
    /// The Keypad B key.
    /// </summary>
    KeypadB = 189,

    /// <summary>
    /// The Keypad C key.
    /// </summary>
    KeypadC = 190,

    /// <summary>
    /// The Keypad D key.
    /// </summary>
    KeypadD = 191,

    /// <summary>
    /// The Keypad E key.
    /// </summary>
    KeypadE = 192,

    /// <summary>
    /// The Keypad F key.
    /// </summary>
    KeypadF = 193,

    /// <summary>
    /// The Keypad Xor key.
    /// </summary>
    KeypadXor = 194,

    /// <summary>
    /// The Keypad Power key.
    /// </summary>
    KeypadPower = 195,

    /// <summary>
    /// The Keypad Percent key.
    /// </summary>
    KeypadPercent = 196,

    /// <summary>
    /// The Keypad Less key.
    /// </summary>
    KeypadLess = 197,

    /// <summary>
    /// The Keypad Greater key.
    /// </summary>
    KeypadGreater = 198,

    /// <summary>
    /// The Keypad Ampersand key.
    /// </summary>
    KeypadAmpersand = 199,

    /// <summary>
    /// The Keypad Double Ampersand key.
    /// </summary>
    KeypadDoubleAmpersand = 200,

    /// <summary>
    /// The Keypad Vertical Bar key.
    /// </summary>
    KeypadVerticalBar = 201,

    /// <summary>
    /// The Keypad Double Vertical Bar key.
    /// </summary>
    KeypadDoubleVerticalBar = 202,

    /// <summary>
    /// The Keypad Colon key.
    /// </summary>
    KeypadColon = 203,

    /// <summary>
    /// The Keypad Hash key.
    /// </summary>
    KeypadHash = 204,

    /// <summary>
    /// The Keypad Space key.
    /// </summary>
    KeypadSpace = 205,

    /// <summary>
    /// The Keypad At key.
    /// </summary>
    KeypadAt = 206,

    /// <summary>
    /// The Keypad Exclam key.
    /// </summary>
    KeypadExclam = 207,

    /// <summary>
    /// The Keypad Mem Store key.
    /// </summary>
    KeypadMemStore = 208,

    /// <summary>
    /// The Keypad Mem Recall key.
    /// </summary>
    KeypadMemRecall = 209,

    /// <summary>
    /// The Keypad Mem Clear key.
    /// </summary>
    KeypadMemClear = 210,

    /// <summary>
    /// The Keypad Mem Add key.
    /// </summary>
    KeypadMemAdd = 211,

    /// <summary>
    /// The Keypad Mem Subtract key.
    /// </summary>
    KeypadMemSubtract = 212,

    /// <summary>
    /// The Keypad Mem Multiply key.
    /// </summary>
    KeypadMemMultiply = 213,

    /// <summary>
    /// The Keypad Mem Divide key.
    /// </summary>
    KeypadMemDivide = 214,

    /// <summary>
    /// The Keypad Plus Minus key.
    /// </summary>
    KeypadPlusMinus = 215,

    /// <summary>
    /// The Keypad Clear key.
    /// </summary>
    KeypadClear = 216,

    /// <summary>
    /// The Keypad Clear Entry key.
    /// </summary>
    KeypadClearEntry = 217,

    /// <summary>
    /// The Keypad Binary key.
    /// </summary>
    KeypadBinary = 218,

    /// <summary>
    /// The Keypad Octal key.
    /// </summary>
    KeypadOctal = 219,

    /// <summary>
    /// The Keypad Decimal key.
    /// </summary>
    KeypadDecimal = 220,

    /// <summary>
    /// The Keypad Hexadecimal key.
    /// </summary>
    KeypadHexadecimal = 221,

    /// <summary>
    /// The Left Control key.
    /// </summary>
    LeftControl = 224,

    /// <summary>
    /// The Left Shift key.
    /// </summary>
    LeftShift = 225,

    /// <summary>
    /// The Left Alt key.
    /// </summary>
    LeftAlt = 226,

    /// <summary>
    /// The Left Gui key.
    /// </summary>
    LeftGui = 227,

    /// <summary>
    /// The Right Control key.
    /// </summary>
    RightControl = 228,

    /// <summary>
    /// The Right Shift key.
    /// </summary>
    RightShift = 229,

    /// <summary>
    /// The Right Alt key.
    /// </summary>
    RightAlt = 230,

    /// <summary>
    /// The Right Gui key.
    /// </summary>
    RightGui = 231,

    /// <summary>
    /// The Mode key.
    /// </summary>
    Mode = 257,

    /// <summary>
    /// The Sleep key.
    /// </summary>
    Sleep = 258,

    /// <summary>
    /// The Wake key.
    /// </summary>
    Wake = 259,

    /// <summary>
    /// The Channel Increment key.
    /// </summary>
    ChannelIncrement = 260,

    /// <summary>
    /// The Channel Decrement key.
    /// </summary>
    ChannelDecrement = 261,

    /// <summary>
    /// The Media Play key.
    /// </summary>
    MediaPlay = 262,

    /// <summary>
    /// The Media Pause key.
    /// </summary>
    MediaPause = 263,

    /// <summary>
    /// The Media Record key.
    /// </summary>
    MediaRecord = 264,

    /// <summary>
    /// The Media Fast Forward key.
    /// </summary>
    MediaFastForward = 265,

    /// <summary>
    /// The Media Rewind key.
    /// </summary>
    MediaRewind = 266,

    /// <summary>
    /// The Media Next Track key.
    /// </summary>
    MediaNextTrack = 267,

    /// <summary>
    /// The Media Previous Track key.
    /// </summary>
    MediaPreviousTrack = 268,

    /// <summary>
    /// The Media Stop key.
    /// </summary>
    MediaStop = 269,

    /// <summary>
    /// The Media Eject key.
    /// </summary>
    MediaEject = 270,

    /// <summary>
    /// The Media Play Pause key.
    /// </summary>
    MediaPlayPause = 271,

    /// <summary>
    /// The Media Select key.
    /// </summary>
    MediaSelect = 272,

    /// <summary>
    /// The Ac New key.
    /// </summary>
    AcNew = 273,

    /// <summary>
    /// The Ac Open key.
    /// </summary>
    AcOpen = 274,

    /// <summary>
    /// The Ac Close key.
    /// </summary>
    AcClose = 275,

    /// <summary>
    /// The Ac Exit key.
    /// </summary>
    AcExit = 276,

    /// <summary>
    /// The Ac Save key.
    /// </summary>
    AcSave = 277,

    /// <summary>
    /// The Ac Print key.
    /// </summary>
    AcPrint = 278,

    /// <summary>
    /// The Ac Properties key.
    /// </summary>
    AcProperties = 279,

    /// <summary>
    /// The Ac Search key.
    /// </summary>
    AcSearch = 280,

    /// <summary>
    /// The Ac Home key.
    /// </summary>
    AcHome = 281,

    /// <summary>
    /// The Ac Back key.
    /// </summary>
    AcBack = 282,

    /// <summary>
    /// The Ac Forward key.
    /// </summary>
    AcForward = 283,

    /// <summary>
    /// The Ac Stop key.
    /// </summary>
    AcStop = 284,

    /// <summary>
    /// The Ac Refresh key.
    /// </summary>
    AcRefresh = 285,

    /// <summary>
    /// The Ac Bookmarks key.
    /// </summary>
    AcBookmarks = 286,

    /// <summary>
    /// The Soft Left key.
    /// </summary>
    SoftLeft = 287,

    /// <summary>
    /// The Soft Right key.
    /// </summary>
    SoftRight = 288,

    /// <summary>
    /// The Call key.
    /// </summary>
    Call = 289,

    /// <summary>
    /// The End Call key.
    /// </summary>
    EndCall = 290
}
