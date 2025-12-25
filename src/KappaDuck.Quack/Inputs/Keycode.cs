// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Inputs;

/// <summary>
/// The virtual key codes.
/// </summary>
public enum Keycode : uint
{
    /// <summary>
    /// Unknown key code.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// '\b'.
    /// </summary>
    Backspace = 0x00000008u,

    /// <summary>
    /// '\t'.
    /// </summary>
    Tab = 0x00000009u,

    /// <summary>
    /// '\r'.
    /// </summary>
    Return = 0x0000000du,

    /// <summary>
    /// '\x1B'.
    /// </summary>
    Escape = 0x0000001bu,

    /// <summary>
    /// ' '.
    /// </summary>
    Space = 0x00000020u,

    /// <summary>
    /// '!'.
    /// </summary>
    Exclamation = 0x00000021u,

    /// <summary>
    /// '"'.
    /// </summary>
    DoubleQuote = 0x00000022u,

    /// <summary>
    /// '#'.
    /// </summary>
    Hash = 0x00000023u,

    /// <summary>
    /// '$'.
    /// </summary>
    Dollar = 0x00000024u,

    /// <summary>
    /// '%'.
    /// </summary>
    Percent = 0x00000025u,

    /// <summary>
    /// '&amp;'.
    /// </summary>
    Ampersand = 0x00000026u,

    /// <summary>
    /// '''.
    /// </summary>
    Quote = 0x00000027u,

    /// <summary>
    /// '('.
    /// </summary>
    LeftParenthesis = 0x00000028u,

    /// <summary>
    /// ')'.
    /// </summary>
    RightParenthesis = 0x00000029u,

    /// <summary>
    /// '*'.
    /// </summary>
    Asterisk = 0x0000002au,

    /// <summary>
    /// '+'.
    /// </summary>
    Plus = 0x0000002bu,

    /// <summary>
    /// ','.
    /// </summary>
    Comma = 0x0000002cu,

    /// <summary>
    /// '-'.
    /// </summary>
    Minus = 0x0000002du,

    /// <summary>
    /// '.'.
    /// </summary>
    Period = 0x0000002eu,

    /// <summary>
    /// '/'.
    /// </summary>
    Slash = 0x0000002fu,

    /// <summary>
    /// '0'.
    /// </summary>
    Number0 = 0x00000030u,

    /// <summary>
    /// '1'.
    /// </summary>
    Number1 = 0x00000031u,

    /// <summary>
    /// '2'.
    /// </summary>
    Number2 = 0x00000032u,

    /// <summary>
    /// '3'.
    /// </summary>
    Number3 = 0x00000033u,

    /// <summary>
    /// '4'.
    /// </summary>
    Number4 = 0x00000034u,

    /// <summary>
    /// '5'.
    /// </summary>
    Number5 = 0x00000035u,

    /// <summary>
    /// '6'.
    /// </summary>
    Number6 = 0x00000036u,

    /// <summary>
    /// '7'.
    /// </summary>
    Number7 = 0x00000037u,

    /// <summary>
    /// '8'.
    /// </summary>
    Number8 = 0x00000038u,

    /// <summary>
    /// '9'.
    /// </summary>
    Number9 = 0x00000039u,

    /// <summary>
    /// ':'.
    /// </summary>
    Colon = 0x0000003au,

    /// <summary>
    /// ';'.
    /// </summary>
    SemiColon = 0x0000003bu,

    /// <summary>
    /// '&lt;'.
    /// </summary>
    Less = 0x0000003cu,

    /// <summary>
    /// '='.
    /// </summary>
    Equals = 0x0000003du,

    /// <summary>
    /// '&gt;'.
    /// </summary>
    Greater = 0x0000003eu,

    /// <summary>
    /// '?'.
    /// </summary>
    Question = 0x0000003fu,

    /// <summary>
    /// '@'.
    /// </summary>
    At = 0x00000040u,

    /// <summary>
    /// '['.
    /// </summary>
    LeftBracket = 0x0000005bu,

    /// <summary>
    /// '\'.
    /// </summary>
    Backslash = 0x0000005cu,

    /// <summary>
    /// ']'.
    /// </summary>
    RightBracket = 0x0000005du,

    /// <summary>
    /// '^'.
    /// </summary>
    Caret = 0x0000005eu,

    /// <summary>
    /// '_'.
    /// </summary>
    Underscore = 0x0000005fu,

    /// <summary>
    /// '`'.
    /// </summary>
    Grave = 0x00000060u,

    /// <summary>
    /// 'a'.
    /// </summary>
    A = 0x00000061u,

    /// <summary>
    /// 'b'.
    /// </summary>
    B = 0x00000062u,

    /// <summary>
    /// 'c'.
    /// </summary>
    C = 0x00000063u,

    /// <summary>
    /// 'd'.
    /// </summary>
    D = 0x00000064u,

    /// <summary>
    /// 'e'.
    /// </summary>
    E = 0x00000065u,

    /// <summary>
    /// 'f'.
    /// </summary>
    F = 0x00000066u,

    /// <summary>
    /// 'g'.
    /// </summary>
    G = 0x00000067u,

    /// <summary>
    /// 'h'.
    /// </summary>
    H = 0x00000068u,

    /// <summary>
    /// 'i'.
    /// </summary>
    I = 0x00000069u,

    /// <summary>
    /// 'j'.
    /// </summary>
    J = 0x0000006au,

    /// <summary>
    /// 'k'.
    /// </summary>
    K = 0x0000006bu,

    /// <summary>
    /// 'l'.
    /// </summary>
    L = 0x0000006cu,

    /// <summary>
    /// 'm'.
    /// </summary>
    M = 0x0000006du,

    /// <summary>
    /// 'n'.
    /// </summary>
    N = 0x0000006eu,

    /// <summary>
    /// 'o'.
    /// </summary>
    O = 0x0000006fu,

    /// <summary>
    /// 'p'.
    /// </summary>
    P = 0x00000070u,

    /// <summary>
    /// 'q'.
    /// </summary>
    Q = 0x00000071u,

    /// <summary>
    /// 'r'.
    /// </summary>
    R = 0x00000072u,

    /// <summary>
    /// 's'.
    /// </summary>
    S = 0x00000073u,

    /// <summary>
    /// 't'.
    /// </summary>
    T = 0x00000074u,

    /// <summary>
    /// 'u'.
    /// </summary>
    U = 0x00000075u,

    /// <summary>
    /// 'v'.
    /// </summary>
    V = 0x00000076u,

    /// <summary>
    /// 'w'.
    /// </summary>
    W = 0x00000077u,

    /// <summary>
    /// 'x'.
    /// </summary>
    X = 0x00000078u,

    /// <summary>
    /// 'y'.
    /// </summary>
    Y = 0x00000079u,

    /// <summary>
    /// 'z'.
    /// </summary>
    Z = 0x0000007au,

    /// <summary>
    /// '{'.
    /// </summary>
    LeftBrace = 0x0000007bu,

    /// <summary>
    /// '|'.
    /// </summary>
    Pipe = 0x0000007cu,

    /// <summary>
    /// '}'.
    /// </summary>
    RightBrace = 0x0000007du,

    /// <summary>
    /// '~'.
    /// </summary>
    Tilde = 0x0000007eu,

    /// <summary>
    /// '\x7F'.
    /// </summary>
    Delete = 0x0000007fu,

    /// <summary>
    /// '+/-'.
    /// </summary>
    PlusMinus = 0x000000b1u,

    /// <summary>
    /// Extended key Left tab.
    /// </summary>
    LeftTab = 0x20000001u,

    /// <summary>
    /// Extended key Level 5 Shift.
    /// </summary>
    Level5Shift = 0x20000002u,

    /// <summary>
    /// Extended key Multi-key Compose.
    /// </summary>
    MultiKeyCompose = 0x20000003u,

    /// <summary>
    /// Extended key Left Meta.
    /// </summary>
    LeftMeta = 0x20000004u,

    /// <summary>
    /// Extended key Right Meta.
    /// </summary>
    RightMeta = 0x20000005u,

    /// <summary>
    /// Extended key Left Hyper.
    /// </summary>
    LeftHyper = 0x20000006u,

    /// <summary>
    /// Extended key Right Hyper.
    /// </summary>
    RightHyper = 0x20000007u,

    /// <summary>
    /// Caps lock.
    /// </summary>
    CapsLock = 0x40000039u,

    /// <summary>
    /// F1.
    /// </summary>
    F1 = 0x4000003au,

    /// <summary>
    /// F2.
    /// </summary>
    F2 = 0x4000003bu,

    /// <summary>
    /// F3.
    /// </summary>
    F3 = 0x4000003cu,

    /// <summary>
    /// F4.
    /// </summary>
    F4 = 0x4000003du,

    /// <summary>
    /// F5.
    /// </summary>
    F5 = 0x4000003eu,

    /// <summary>
    /// F6.
    /// </summary>
    F6 = 0x4000003fu,

    /// <summary>
    /// F7.
    /// </summary>
    F7 = 0x40000040u,

    /// <summary>
    /// F8.
    /// </summary>
    F8 = 0x40000041u,

    /// <summary>
    /// F9.
    /// </summary>
    F9 = 0x40000042u,

    /// <summary>
    /// F10.
    /// </summary>
    F10 = 0x40000043u,

    /// <summary>
    /// F11.
    /// </summary>
    F11 = 0x40000044u,

    /// <summary>
    /// F12.
    /// </summary>
    F12 = 0x40000045u,

    /// <summary>
    /// Print screen.
    /// </summary>
    PrintScreen = 0x40000046u,

    /// <summary>
    /// Scroll lock.
    /// </summary>
    ScrollLock = 0x40000047u,

    /// <summary>
    /// Pause.
    /// </summary>
    Pause = 0x40000048u,

    /// <summary>
    /// Insert.
    /// </summary>
    Insert = 0x40000049u,

    /// <summary>
    /// Home.
    /// </summary>
    Home = 0x4000004au,

    /// <summary>
    /// Page up.
    /// </summary>
    PageUp = 0x4000004bu,

    /// <summary>
    /// End.
    /// </summary>
    End = 0x4000004du,

    /// <summary>
    /// Page down.
    /// </summary>
    PageDown = 0x4000004eu,

    /// <summary>
    /// Right.
    /// </summary>
    Right = 0x4000004fu,

    /// <summary>
    /// Left.
    /// </summary>
    Left = 0x40000050u,

    /// <summary>
    /// Down.
    /// </summary>
    Down = 0x40000051u,

    /// <summary>
    /// Up.
    /// </summary>
    Up = 0x40000052u,

    /// <summary>
    /// Num lock.
    /// </summary>
    NumLock = 0x40000053u,

    /// <summary>
    /// Keypad divide.
    /// </summary>
    KeypadDivide = 0x40000054u,

    /// <summary>
    /// Keypad multiply.
    /// </summary>
    KeypadMultiply = 0x40000055u,

    /// <summary>
    /// Keypad '-'.
    /// </summary>
    KeypadMinus = 0x40000056u,

    /// <summary>
    /// Keypad '+'.
    /// </summary>
    KeypadPlus = 0x40000057u,

    /// <summary>
    /// Keypad enter.
    /// </summary>
    KeypadEnter = 0x40000058u,

    /// <summary>
    /// Keypad 1.
    /// </summary>
    Keypad1 = 0x40000059u,

    /// <summary>
    /// Keypad 2.
    /// </summary>
    Keypad2 = 0x4000005au,

    /// <summary>
    /// Keypad 3.
    /// </summary>
    Keypad3 = 0x4000005bu,

    /// <summary>
    /// Keypad 4.
    /// </summary>
    Keypad4 = 0x4000005cu,

    /// <summary>
    /// Keypad 5.
    /// </summary>
    Keypad5 = 0x4000005du,

    /// <summary>
    /// Keypad 6.
    /// </summary>
    Keypad6 = 0x4000005eu,

    /// <summary>
    /// Keypad 7.
    /// </summary>
    Keypad7 = 0x4000005fu,

    /// <summary>
    /// Keypad 8.
    /// </summary>
    Keypad8 = 0x40000060u,

    /// <summary>
    /// Keypad 9.
    /// </summary>
    Keypad9 = 0x40000061u,

    /// <summary>
    /// Keypad 0.
    /// </summary>
    Keypad0 = 0x40000062u,

    /// <summary>
    /// Keypad '.'.
    /// </summary>
    KeypadPeriod = 0x40000063u,

    /// <summary>
    /// Application.
    /// </summary>
    Application = 0x40000065u,

    /// <summary>
    /// Power.
    /// </summary>
    Power = 0x40000066u,

    /// <summary>
    /// Keypad '='.
    /// </summary>
    KeypadEquals = 0x40000067u,

    /// <summary>
    /// F13.
    /// </summary>
    F13 = 0x40000068u,

    /// <summary>
    /// F14.
    /// </summary>
    F14 = 0x40000069u,

    /// <summary>
    /// F15.
    /// </summary>
    F15 = 0x4000006au,

    /// <summary>
    /// F16.
    /// </summary>
    F16 = 0x4000006bu,

    /// <summary>
    /// F17.
    /// </summary>
    F17 = 0x4000006cu,

    /// <summary>
    /// F18.
    /// </summary>
    F18 = 0x4000006du,

    /// <summary>
    /// F19.
    /// </summary>
    F19 = 0x4000006eu,

    /// <summary>
    /// F20.
    /// </summary>
    F20 = 0x4000006fu,

    /// <summary>
    /// F21.
    /// </summary>
    F21 = 0x40000070u,

    /// <summary>
    /// F22.
    /// </summary>
    F22 = 0x40000071u,

    /// <summary>
    /// F23.
    /// </summary>
    F23 = 0x40000072u,

    /// <summary>
    /// F24.
    /// </summary>
    F24 = 0x40000073u,

    /// <summary>
    /// Execute.
    /// </summary>
    Execute = 0x40000074u,

    /// <summary>
    /// Help.
    /// </summary>
    Help = 0x40000075u,

    /// <summary>
    /// Menu.
    /// </summary>
    Menu = 0x40000076u,

    /// <summary>
    /// Select.
    /// </summary>
    Select = 0x40000077u,

    /// <summary>
    /// Stop.
    /// </summary>
    Stop = 0x40000078u,

    /// <summary>
    /// Again.
    /// </summary>
    Again = 0x40000079u,

    /// <summary>
    /// Undo.
    /// </summary>
    Undo = 0x4000007au,

    /// <summary>
    /// Cut.
    /// </summary>
    Cut = 0x4000007bu,

    /// <summary>
    /// Copy.
    /// </summary>
    Copy = 0x4000007cu,

    /// <summary>
    /// Paste.
    /// </summary>
    Paste = 0x4000007du,

    /// <summary>
    /// Find.
    /// </summary>
    Find = 0x4000007eu,

    /// <summary>
    /// Mute.
    /// </summary>
    Mute = 0x4000007fu,

    /// <summary>
    /// Volume up.
    /// </summary>
    VolumeUp = 0x40000080u,

    /// <summary>
    /// Volume down.
    /// </summary>
    VolumeDown = 0x40000081u,

    /// <summary>
    /// Keypad ','.
    /// </summary>
    KeypadComma = 0x40000085u,

    /// <summary>
    /// Keypad '=' as400.
    /// </summary>
    KeypadEqualsAs400 = 0x40000086u,

    /// <summary>
    /// Alternative erase.
    /// </summary>
    AlternativeErase = 0x40000099u,

    /// <summary>
    /// System request.
    /// </summary>
    SystemRequest = 0x4000009au,

    /// <summary>
    /// Cancel.
    /// </summary>
    Cancel = 0x4000009bu,

    /// <summary>
    /// Clear.
    /// </summary>
    Clear = 0x4000009cu,

    /// <summary>
    /// Prior.
    /// </summary>
    Prior = 0x4000009du,

    /// <summary>
    /// Return 2.
    /// </summary>
    Return2 = 0x4000009eu,

    /// <summary>
    /// Separator.
    /// </summary>
    Separator = 0x4000009fu,

    /// <summary>
    /// Out.
    /// </summary>
    Out = 0x400000a0u,

    /// <summary>
    /// Operation.
    /// </summary>
    Operation = 0x400000a1u,

    /// <summary>
    /// Clear again.
    /// </summary>
    ClearAgain = 0x400000a2u,

    /// <summary>
    /// CRSEL.
    /// </summary>
    Crsel = 0x400000a3u,

    /// <summary>
    /// EXSEL.
    /// </summary>
    Exsel = 0x400000a4u,

    /// <summary>
    /// Keypad 00.
    /// </summary>
    Keypad00 = 0x400000b0u,

    /// <summary>
    /// Keypad 000.
    /// </summary>
    Keypad000 = 0x400000b1u,

    /// <summary>
    /// Thousands separator.
    /// </summary>
    ThousandsSeparator = 0x400000b2u,

    /// <summary>
    /// Decimal separator.
    /// </summary>
    DecimalSeparator = 0x400000b3u,

    /// <summary>
    /// Currency unit.
    /// </summary>
    CurrencyUnit = 0x400000b4u,

    /// <summary>
    /// Currency subunit.
    /// </summary>
    CurrencySubUnit = 0x400000b5u,

    /// <summary>
    /// Keypad '('.
    /// </summary>
    KeypadLeftParenthesis = 0x400000b6u,

    /// <summary>
    /// Keypad ')'.
    /// </summary>
    KeypadRightParenthesis = 0x400000b7u,

    /// <summary>
    /// Keypad '{'.
    /// </summary>
    KeypadLeftBrace = 0x400000b8u,

    /// <summary>
    /// Keypad '}'.
    /// </summary>
    KeypadRightBrace = 0x400000b9u,

    /// <summary>
    /// Keypad tab.
    /// </summary>
    KeypadTab = 0x400000bau,

    /// <summary>
    /// Keypad backspace.
    /// </summary>
    KeypadBackspace = 0x400000bbu,

    /// <summary>
    /// Keypad A.
    /// </summary>
    KeypadA = 0x400000bcu,

    /// <summary>
    /// Keypad B.
    /// </summary>
    KeypadB = 0x400000bdu,

    /// <summary>
    /// Keypad C.
    /// </summary>
    KeypadC = 0x400000beu,

    /// <summary>
    /// Keypad D.
    /// </summary>
    KeypadD = 0x400000bfu,

    /// <summary>
    /// Keypad E.
    /// </summary>
    KeypadE = 0x400000c0u,

    /// <summary>
    /// Keypad F.
    /// </summary>
    KeypadF = 0x400000c1u,

    /// <summary>
    /// Keypad XOR.
    /// </summary>
    KeypadXor = 0x400000c2u,

    /// <summary>
    /// Keypad power.
    /// </summary>
    KeypadPower = 0x400000c3u,

    /// <summary>
    /// Keypad '%'.
    /// </summary>
    KeypadPercent = 0x400000c4u,

    /// <summary>
    /// Keypad '&lt;'.
    /// </summary>
    KeypadLess = 0x400000c5u,

    /// <summary>
    /// Keypad '&gt;'.
    /// </summary>
    KeypadGreater = 0x400000c6u,

    /// <summary>
    /// Keypad '&amp;'.
    /// </summary>
    KeypadAmpersand = 0x400000c7u,

    /// <summary>
    /// Keypad double '&amp;'.
    /// </summary>
    KeypadDoubleAmpersand = 0x400000c8u,

    /// <summary>
    /// Keypad '|'.
    /// </summary>
    KeypadVerticalBar = 0x400000c9u,

    /// <summary>
    /// Keypad double '|'.
    /// </summary>
    KeypadDoubleVerticalBar = 0x400000cau,

    /// <summary>
    /// Keypad ':'.
    /// </summary>
    KeypadColon = 0x400000cbu,

    /// <summary>
    /// Keypad '#'.
    /// </summary>
    KeypadHash = 0x400000ccu,

    /// <summary>
    /// Keypad space.
    /// </summary>
    KeypadSpace = 0x400000cdu,

    /// <summary>
    /// Keypad '@'.
    /// </summary>
    KeypadAt = 0x400000ceu,

    /// <summary>
    /// Keypad '!'.
    /// </summary>
    KeypadExclamation = 0x400000cfu,

    /// <summary>
    /// Keypad memory store.
    /// </summary>
    KeypadMemoryStore = 0x400000d0u,

    /// <summary>
    /// Keypad memory recall.
    /// </summary>
    KeypadMemoryRecall = 0x400000d1u,

    /// <summary>
    /// Keypad memory clear.
    /// </summary>
    KeypadMemoryClear = 0x400000d2u,

    /// <summary>
    /// Keypad memory add.
    /// </summary>
    KeypadMemoryAdd = 0x400000d3u,

    /// <summary>
    /// Keypad memory subtract.
    /// </summary>
    KeypadMemorySubtract = 0x400000d4u,

    /// <summary>
    /// Keypad memory multiply.
    /// </summary>
    KeypadMemoryMultiply = 0x400000d5u,

    /// <summary>
    /// Keypad memory divide.
    /// </summary>
    KeypadMemoryDivide = 0x400000d6u,

    /// <summary>
    /// Keypad '+/-'.
    /// </summary>
    KeypadPlusMinus = 0x400000d7u,

    /// <summary>
    /// Keypad clear.
    /// </summary>
    KeypadClear = 0x400000d8u,

    /// <summary>
    /// Keypad clear entry.
    /// </summary>
    KeypadClearEntry = 0x400000d9u,

    /// <summary>
    /// Keypad binary.
    /// </summary>
    KeypadBinary = 0x400000dau,

    /// <summary>
    /// Keypad octal.
    /// </summary>
    KeypadOctal = 0x400000dbu,

    /// <summary>
    /// Keypad decimal.
    /// </summary>
    KeypadDecimal = 0x400000dcu,

    /// <summary>
    /// Keypad hexadecimal.
    /// </summary>
    KeypadHexadecimal = 0x400000ddu,

    /// <summary>
    /// Left control.
    /// </summary>
    LeftCtrl = 0x400000e0u,

    /// <summary>
    /// Left shift.
    /// </summary>
    LeftShift = 0x400000e1u,

    /// <summary>
    /// Left alt.
    /// </summary>
    LeftAlt = 0x400000e2u,

    /// <summary>
    /// Left GUI.
    /// </summary>
    LeftGui = 0x400000e3u,

    /// <summary>
    /// Right control.
    /// </summary>
    RightCtrl = 0x400000e4u,

    /// <summary>
    /// Right shift.
    /// </summary>
    RightShift = 0x400000e5u,

    /// <summary>
    /// Right alt.
    /// </summary>
    RightAlt = 0x400000e6u,

    /// <summary>
    /// Right Meta.
    /// </summary>
    RightGui = 0x400000e7u,

    /// <summary>
    /// Mode.
    /// </summary>
    Mode = 0x40000101u,

    /// <summary>
    /// Sleep.
    /// </summary>
    Sleep = 0x40000102u,

    /// <summary>
    /// Wake.
    /// </summary>
    Wake = 0x40000103u,

    /// <summary>
    /// Channel increment.
    /// </summary>
    ChannelIncrement = 0x40000104u,

    /// <summary>
    /// Channel decrement.
    /// </summary>
    ChannelDecrement = 0x40000105u,

    /// <summary>
    /// Media play.
    /// </summary>
    MediaPlay = 0x40000106u,

    /// <summary>
    /// Media pause.
    /// </summary>
    MediaPause = 0x40000107u,

    /// <summary>
    /// Media record.
    /// </summary>
    MediaRecord = 0x40000108u,

    /// <summary>
    /// Media fast-forward.
    /// </summary>
    MediaFastForward = 0x40000109u,

    /// <summary>
    /// Media rewind.
    /// </summary>
    MediaRewind = 0x4000010au,

    /// <summary>
    /// Media next track.
    /// </summary>
    MediaNextTract = 0x4000010bu,

    /// <summary>
    /// Media previous track.
    /// </summary>
    MediaPreviousTrack = 0x4000010cu,

    /// <summary>
    /// Media stop.
    /// </summary>
    MediaStop = 0x4000010du,

    /// <summary>
    /// Media eject.
    /// </summary>
    MediaEject = 0x4000010eu,

    /// <summary>
    /// Media play pause.
    /// </summary>
    MediaPlayPause = 0x4000010fu,

    /// <summary>
    /// Media select.
    /// </summary>
    MediaSelect = 0x40000110u,

    /// <summary>
    /// AC new.
    /// </summary>
    AcNew = 0x40000111u,

    /// <summary>
    /// AC open.
    /// </summary>
    AcOpen = 0x40000112u,

    /// <summary>
    /// AC close.
    /// </summary>
    AcClose = 0x40000113u,

    /// <summary>
    /// AC exit.
    /// </summary>
    AcExit = 0x40000114u,

    /// <summary>
    /// AC save.
    /// </summary>
    AcSave = 0x40000115u,

    /// <summary>
    /// AC print.
    /// </summary>
    AcPrint = 0x40000116u,

    /// <summary>
    /// AC properties.
    /// </summary>
    AcProperties = 0x40000117u,

    /// <summary>
    /// AC search.
    /// </summary>
    AcSearch = 0x40000118u,

    /// <summary>
    /// AC home.
    /// </summary>
    AcHome = 0x40000119u,

    /// <summary>
    /// AC back.
    /// </summary>
    AcBack = 0x4000011au,

    /// <summary>
    /// AC forward.
    /// </summary>
    AcForward = 0x4000011bu,

    /// <summary>
    /// AC stop.
    /// </summary>
    AcStop = 0x4000011cu,

    /// <summary>
    /// AC refresh.
    /// </summary>
    AcRefresh = 0x4000011du,

    /// <summary>
    /// AC bookmarks.
    /// </summary>
    AcBookmarks = 0x4000011eu,

    /// <summary>
    /// Soft left.
    /// </summary>
    SoftLeft = 0x4000011fu,

    /// <summary>
    /// Soft right.
    /// </summary>
    SoftRight = 0x40000120u,

    /// <summary>
    /// Call.
    /// </summary>
    Call = 0x40000121u,

    /// <summary>
    /// End call.
    /// </summary>
    EndCall = 0x40000122u
}
