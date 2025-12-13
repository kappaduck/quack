// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Inputs;

/// <summary>
/// The physical key codes.
/// </summary>
public sealed partial class Keyboard
{
    /// <summary>
    /// The physical key codes.
    /// </summary>
    /// <remarks>
    /// These values are from usage page 0x07 (USB keyboard page).
    /// </remarks>
    public enum Scancode
    {
        /// <summary>
        /// Unknown key.
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
        /// The 1 key.
        /// </summary>
        Number1 = 30,

        /// <summary>
        /// The 2 key.
        /// </summary>
        Number2 = 31,

        /// <summary>
        /// The 3 key.
        /// </summary>
        Number3 = 32,

        /// <summary>
        /// The 4 key.
        /// </summary>
        Number4 = 33,

        /// <summary>
        /// The 5 key.
        /// </summary>
        Number5 = 34,

        /// <summary>
        /// The 6 key.
        /// </summary>
        Number6 = 35,

        /// <summary>
        /// The 7 key.
        /// </summary>
        Number7 = 36,

        /// <summary>
        /// The 8 key.
        /// </summary>
        Number8 = 37,

        /// <summary>
        /// The 9 key.
        /// </summary>
        Number9 = 38,

        /// <summary>
        /// The 0 key.
        /// </summary>
        Number0 = 39,

        /// <summary>
        /// The return key.
        /// </summary>
        Return = 40,

        /// <summary>
        /// The escape key.
        /// </summary>
        Escape = 41,

        /// <summary>
        /// The backspace key.
        /// </summary>
        Backspace = 42,

        /// <summary>
        /// The tab key.
        /// </summary>
        Tab = 43,

        /// <summary>
        /// The space key.
        /// </summary>
        Space = 44,

        /// <summary>
        /// The minus key.
        /// </summary>
        Minus = 45,

        /// <summary>
        /// The equals key.
        /// </summary>
        Equals = 46,

        /// <summary>
        /// The left bracket key.
        /// </summary>
        LeftBracket = 47,

        /// <summary>
        /// The right bracket key.
        /// </summary>
        RightBracket = 48,

        /// <summary>
        /// The backslash key.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Located at the lower left of the return key on ISO keyboards and at the right end
        /// of the QWERTY row on ANSI keyboards.
        /// </para>
        /// <para>
        /// Produces REVERSE SOLIDUS (backslash) and VERTICAL LINE in a US layout, REVERSE
        /// SOLIDUS and VERTICAL LINE in a UK Mac layout, NUMBER SIGN and TILDE in a UK
        /// Windows layout. DOLLAR SIGN and POUND SIGN in a Swiss German layout. NUMBER SIGN and
        /// APOSTROPHE in a German layout. GRAVE ACCENT and POUND SIGN in a French Mac layout,
        /// and ASTERISK and MICRO SIGN in a French Windows layout.
        /// </para>
        /// </remarks>
        Backslash = 49,

        /// <summary>
        /// The non-US hash key.
        /// </summary>
        /// <remarks>
        /// ISO USB keyboards actually use this code instead of 49 for the same key, but all
        /// OSes I've seen treat the two codes identically. So, as an implementor, unless
        /// your keyboard generates both of those codes and your OS treats them differently,
        /// you should generate <see cref="Backslash"/> instead of this code. As a user, you
        /// should not rely on this code because SDL will never generate it with most (all?) keyboards.
        /// </remarks>
        NonUsHash = 50,

        /// <summary>
        /// The semicolon key.
        /// </summary>
        Semicolon = 51,

        /// <summary>
        /// The apostrophe key.
        /// </summary>
        Apostrophe = 52,

        /// <summary>
        /// The grave key.
        /// </summary>
        /// <remarks>
        /// Located in the top left corner (on both ANSI and ISO keyboards). Produces GRAVE
        /// ACCENT and TILDE in a US Windows layout and in US and UK Mac layouts on ANSI
        /// keyboards, GRAVE ACCENT and NOT SIGN in a UK Windows layout, SECTION SIGN and
        /// PLUS-MINUS SIGN in US and UK Mac layouts on ISO keyboards, SECTION SIGN and DEGREE
        /// SIGN in a Swiss German layout (Mac: only on ISO keyboards), CIRCUMFLEX ACCENT and
        /// DEGREE SIGN in a German layout (Mac: only on ISO keyboards), SUPERSCRIPT TWO and
        /// TILDE in a French Windows layout, COMMERCIAL AT and NUMBER SIGN in a French Mac
        /// on ISO keyboards, and LESS-THAN SIGN and GREATER-THAN SIGN in a Swiss German, German,
        /// or French Mac layout on ANSI keyboards.
        /// </remarks>
        Grave = 53,

        /// <summary>
        /// The comma key.
        /// </summary>
        Comma = 54,

        /// <summary>
        /// The period key.
        /// </summary>
        Period = 55,

        /// <summary>
        /// The slash key.
        /// </summary>
        Slash = 56,

        /// <summary>
        /// The caps lock key.
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
        /// The print screen key.
        /// </summary>
        PrintScreen = 70,

        /// <summary>
        /// The scroll lock key.
        /// </summary>
        ScrollLock = 71,

        /// <summary>
        /// The pause key.
        /// </summary>
        Pause = 72,

        /// <summary>
        /// The insert key.
        /// </summary>
        /// <remarks>
        /// Insert on PC, help on some Mac keyboards (but does send code <see cref="Insert"/>, not <see cref="Help"/>).
        /// </remarks>
        Insert = 73,

        /// <summary>
        /// The home key.
        /// </summary>
        Home = 74,

        /// <summary>
        /// The page up key.
        /// </summary>
        PageUp = 75,

        /// <summary>
        /// The delete key.
        /// </summary>
        Delete = 76,

        /// <summary>
        /// The end key.
        /// </summary>
        End = 77,

        /// <summary>
        /// The page down key.
        /// </summary>
        PageDown = 78,

        /// <summary>
        /// The right key.
        /// </summary>
        Right = 79,

        /// <summary>
        /// The left key.
        /// </summary>
        Left = 80,

        /// <summary>
        /// The down key.
        /// </summary>
        Down = 81,

        /// <summary>
        /// The up key.
        /// </summary>
        Up = 82,

        /// <summary>
        /// The num lock key.
        /// </summary>
        /// <remarks>
        /// Num lock on PC, clear on Mac keyboards.
        /// </remarks>
        NumLock = 83,

        /// <summary>
        /// The keypad divide key.
        /// </summary>
        KeypadDivide = 84,

        /// <summary>
        /// The keypad multiply key.
        /// </summary>
        KeypadMultiply = 85,

        /// <summary>
        /// The keypad minus key.
        /// </summary>
        KeypadMinus = 86,

        /// <summary>
        /// The keypad plus key.
        /// </summary>
        KeypadPlus = 87,

        /// <summary>
        /// The keypad enter key.
        /// </summary>
        KeypadEnter = 88,

        /// <summary>
        /// The keypad 1 key.
        /// </summary>
        Keypad1 = 89,

        /// <summary>
        /// The keypad 2 key.
        /// </summary>
        Keypad2 = 90,

        /// <summary>
        /// The keypad 3 key.
        /// </summary>
        Keypad3 = 91,

        /// <summary>
        /// The keypad 4 key.
        /// </summary>
        Keypad4 = 92,

        /// <summary>
        /// The keypad 5 key.
        /// </summary>
        Keypad5 = 93,

        /// <summary>
        /// The keypad 6 key.
        /// </summary>
        Keypad6 = 94,

        /// <summary>
        /// The keypad 7 key.
        /// </summary>
        Keypad7 = 95,

        /// <summary>
        /// The keypad 8 key.
        /// </summary>
        Keypad8 = 96,

        /// <summary>
        /// The keypad 9 key.
        /// </summary>
        Keypad9 = 97,

        /// <summary>
        /// The keypad 0 key.
        /// </summary>
        Keypad0 = 98,

        /// <summary>
        /// The keypad period key.
        /// </summary>
        KeypadPeriod = 99,

        /// <summary>
        /// The non-US backslash key.
        /// </summary>
        /// <remarks>
        /// This is the additional key that ISO keyboards have over ANSI ones,
        /// located between left shift and Y. Produces GRAVE ACCENT and TILDE in a
        /// US or UK Mac layout, REVERSE SOLIDUS (backslash) and VERTICAL LINE in a
        /// US or UK Windows layout, and LESS-THAN SIGN and GREATER-THAN SIGN
        /// in a Swiss German, German, or French layout.
        /// </remarks>
        NonUsBackslash = 100,

        /// <summary>
        /// The application key.
        /// </summary>
        /// <remarks>
        /// Windows contextual menu, compose.
        /// </remarks>
        Application = 101,

        /// <summary>
        /// The power key.
        /// </summary>
        /// <remarks>
        /// The USB document mentions this is a status flag, not a physical key.
        /// Some Mac keyboards do have a power key.
        /// </remarks>
        Power = 102,

        /// <summary>
        /// The keypad equals key.
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
        /// The execute key.
        /// </summary>
        Execute = 116,

        /// <summary>
        /// The help key.
        /// </summary>
        /// <remarks>
        /// AL Integrated Help Center.
        /// </remarks>
        Help = 117,

        /// <summary>
        /// The menu key (show menu).
        /// </summary>
        Menu = 118,

        /// <summary>
        /// The select key.
        /// </summary>
        Select = 119,

        /// <summary>
        /// The stop key (AC Stop).
        /// </summary>
        Stop = 120,

        /// <summary>
        /// The again key (AC Redo/Repeat).
        /// </summary>
        Again = 121,

        /// <summary>
        /// The undo key (AC Undo).
        /// </summary>
        Undo = 122,

        /// <summary>
        /// The cut key (AC Cut).
        /// </summary>
        Cut = 123,

        /// <summary>
        /// The copy key (AC Copy).
        /// </summary>
        Copy = 124,

        /// <summary>
        /// The paste key (AC Paste).
        /// </summary>
        Paste = 125,

        /// <summary>
        /// The find key (AC Find).
        /// </summary>
        Find = 126,

        /// <summary>
        /// The mute key.
        /// </summary>
        Mute = 127,

        /// <summary>
        /// The volume up key.
        /// </summary>
        VolumeUp = 128,

        /// <summary>
        /// The volume down key.
        /// </summary>
        VolumeDown = 129,

        /// <summary>
        /// The keypad comma key.
        /// </summary>
        KeypadComma = 133,

        /// <summary>
        /// The keypad equals as 400 key.
        /// </summary>
        KeypadEqualsAs400 = 134,

        /// <summary>
        /// The International1 key.
        /// </summary>
        /// <remarks>
        /// Used on Asian keyboards, see footnotes in USB doc.
        /// </remarks>
        International1 = 135,

        /// <summary>
        /// The International2 key.
        /// </summary>
        International2 = 136,

        /// <summary>
        /// The International3 key (Yen).
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
        /// The Lang1 key (Hangul/English toggle).
        /// </summary>
        Lang1 = 144,

        /// <summary>
        /// The Lang2 key (Hanja conversion).
        /// </summary>
        Lang2 = 145,

        /// <summary>
        /// The Lang3 key (Katakana).
        /// </summary>
        Lang3 = 146,

        /// <summary>
        /// The Lang4 key (Hiragana).
        /// </summary>
        Lang4 = 147,

        /// <summary>
        /// The Lang5 key (Senkaku / Hankaku).
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
        /// The Erase-Eaze key.
        /// </summary>
        AltErase = 153,

        /// <summary>
        /// The SysReq key.
        /// </summary>
        SystemRequest = 154,

        /// <summary>
        /// The Cancel key (AC Cancel).
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
        /// The Operation key.
        /// </summary>
        Operation = 161,

        /// <summary>
        /// The Clear/Again key.
        /// </summary>
        ClearAgain = 162,

        /// <summary>
        /// The CrSel key.
        /// </summary>
        CrSel = 163,

        /// <summary>
        /// The ExSel key.
        /// </summary>
        ExSel = 164,

        /// <summary>
        /// The keypad 00 key.
        /// </summary>
        Keypad00 = 176,

        /// <summary>
        /// The keypad 000 key.
        /// </summary>
        Keypad000 = 177,

        /// <summary>
        /// The thousands separator key.
        /// </summary>
        ThousandsSeparator = 178,

        /// <summary>
        /// The decimal separator key.
        /// </summary>
        DecimalSeparator = 179,

        /// <summary>
        /// The currency unit key.
        /// </summary>
        CurrencyUnit = 180,

        /// <summary>
        /// The currency subunit key.
        /// </summary>
        CurrencySubUnit = 181,

        /// <summary>
        /// The keypad left parenthesis key.
        /// </summary>
        KeypadLeftParenthesis = 182,

        /// <summary>
        /// The keypad right parenthesis key.
        /// </summary>
        KeypadRightParenthesis = 183,

        /// <summary>
        /// The keypad left brace key.
        /// </summary>
        KeypadLeftBrace = 184,

        /// <summary>
        /// The keypad right brace key.
        /// </summary>
        KeypadRightBrace = 185,

        /// <summary>
        /// The keypad tab key.
        /// </summary>
        KeypadTab = 186,

        /// <summary>
        /// The keypad backspace key.
        /// </summary>
        KeypadBackspace = 187,

        /// <summary>
        /// The keypad A key.
        /// </summary>
        KeypadA = 188,

        /// <summary>
        /// The keypad B key.
        /// </summary>
        KeypadB = 189,

        /// <summary>
        /// The keypad C key.
        /// </summary>
        KeypadC = 190,

        /// <summary>
        /// The keypad D key.
        /// </summary>
        KeypadD = 191,

        /// <summary>
        /// The keypad E key.
        /// </summary>
        KeypadE = 192,

        /// <summary>
        /// The keypad F key.
        /// </summary>
        KeypadF = 193,

        /// <summary>
        /// The keypad XOR key.
        /// </summary>
        KeypadXor = 194,

        /// <summary>
        /// The keypad power key.
        /// </summary>
        KeypadPower = 195,

        /// <summary>
        /// The keypad percent key.
        /// </summary>
        KeypadPercent = 196,

        /// <summary>
        /// The keypad less key.
        /// </summary>
        KeypadLess = 197,

        /// <summary>
        /// The keypad greater key.
        /// </summary>
        KeypadGreater = 198,

        /// <summary>
        /// The keypad ampersand key.
        /// </summary>
        KeypadAmpersand = 199,

        /// <summary>
        /// The keypad double ampersand key.
        /// </summary>
        KeypadDoubleAmpersand = 200,

        /// <summary>
        /// The keypad vertical bar key.
        /// </summary>
        KeypadVerticalBar = 201,

        /// <summary>
        /// The keypad double vertical bar key.
        /// </summary>
        KeypadDoubleVerticalBar = 202,

        /// <summary>
        /// The keypad colon key.
        /// </summary>
        KeypadColon = 203,

        /// <summary>
        /// The keypad hash key.
        /// </summary>
        KeypadHash = 204,

        /// <summary>
        /// The keypad space key.
        /// </summary>
        KeypadSpace = 205,

        /// <summary>
        /// The keypad at key.
        /// </summary>
        KeypadAt = 206,

        /// <summary>
        /// The keypad exclamation key.
        /// </summary>
        KeypadExclamation = 207,

        /// <summary>
        /// The keypad memory store key.
        /// </summary>
        KeypadMemoryStore = 208,

        /// <summary>
        /// The keypad memory recall key.
        /// </summary>
        KeypadMemoryRecall = 209,

        /// <summary>
        /// The keypad memory clear key.
        /// </summary>
        KeypadMemoryClear = 210,

        /// <summary>
        /// The keypad memory add key.
        /// </summary>
        KeypadMemoryAdd = 211,

        /// <summary>
        /// The keypad memory subtract key.
        /// </summary>
        KeypadMemorySubtract = 212,

        /// <summary>
        /// The keypad memory multiply key.
        /// </summary>
        KeypadMemoryMultiply = 213,

        /// <summary>
        /// The keypad memory divide key.
        /// </summary>
        KeypadMemoryDivide = 214,

        /// <summary>
        /// The keypad plus/minus key.
        /// </summary>
        KeypadPlusMinus = 215,

        /// <summary>
        /// The keypad clear key.
        /// </summary>
        KeypadClear = 216,

        /// <summary>
        /// The keypad clear entry key.
        /// </summary>
        KeypadClearEntry = 217,

        /// <summary>
        /// The keypad binary key.
        /// </summary>
        KeypadBinary = 218,

        /// <summary>
        /// The keypad octal key.
        /// </summary>
        KeypadOctal = 219,

        /// <summary>
        /// The keypad decimal key.
        /// </summary>
        KeypadDecimal = 220,

        /// <summary>
        /// The keypad hexadecimal key.
        /// </summary>
        KeypadHexadecimal = 221,

        /// <summary>
        /// The left control key.
        /// </summary>
        LeftCtrl = 224,

        /// <summary>
        /// The left shift key.
        /// </summary>
        LeftShift = 225,

        /// <summary>
        /// The left alt key.
        /// </summary>
        LeftAlt = 226,

        /// <summary>
        /// The left GUI key (windows, command (apple), meta).
        /// </summary>
        LeftGui = 227,

        /// <summary>
        /// The right control key.
        /// </summary>
        RightCtrl = 228,

        /// <summary>
        /// The right shift key.
        /// </summary>
        RightShift = 229,

        /// <summary>
        /// The right alt key.
        /// </summary>
        RightAlt = 230,

        /// <summary>
        /// The right GUI key (windows, command (apple), meta).
        /// </summary>
        RightGui = 231,

        /// <summary>
        /// The mode key.
        /// </summary>
        Mode = 257,

        /// <summary>
        /// The sleep key.
        /// </summary>
        Sleep = 258,

        /// <summary>
        /// The wake key.
        /// </summary>
        Wake = 259,

        /// <summary>
        /// The channel increment key.
        /// </summary>
        ChannelIncrement = 260,

        /// <summary>
        /// The channel decrement key.
        /// </summary>
        ChannelDecrement = 261,

        /// <summary>
        /// The media play key.
        /// </summary>
        MediaPlay = 262,

        /// <summary>
        /// The media pause key.
        /// </summary>
        MediaPause = 263,

        /// <summary>
        /// The media record key.
        /// </summary>
        MediaRecord = 264,

        /// <summary>
        /// The media fast-forward key.
        /// </summary>
        MediaFastForward = 265,

        /// <summary>
        /// The media rewind key.
        /// </summary>
        MediaRewind = 266,

        /// <summary>
        /// The media next track key.
        /// </summary>
        MediaNextTrack = 267,

        /// <summary>
        /// The media previous track key.
        /// </summary>
        MediaPreviousTrack = 268,

        /// <summary>
        /// The media stop key.
        /// </summary>
        MediaStop = 269,

        /// <summary>
        /// The media eject key.
        /// </summary>
        MediaEject = 270,

        /// <summary>
        /// The media play/pause key.
        /// </summary>
        MediaPlayPause = 271,

        /// <summary>
        /// The media select key.
        /// </summary>
        MediaSelect = 272,

        /// <summary>
        /// The AC new key.
        /// </summary>
        AcNew = 273,

        /// <summary>
        /// The AC open key.
        /// </summary>
        AcOpen = 274,

        /// <summary>
        /// The AC close key.
        /// </summary>
        AcClose = 275,

        /// <summary>
        /// The AC exit key.
        /// </summary>
        AcExit = 276,

        /// <summary>
        /// The AC save key.
        /// </summary>
        AcSave = 277,

        /// <summary>
        /// The AC print key.
        /// </summary>
        AcPrint = 278,

        /// <summary>
        /// The AC properties key.
        /// </summary>
        AcProperties = 279,

        /// <summary>
        /// The AC search key.
        /// </summary>
        AcSearch = 280,

        /// <summary>
        /// The AC home key.
        /// </summary>
        AcHome = 281,

        /// <summary>
        /// The AC back key.
        /// </summary>
        AcBack = 282,

        /// <summary>
        /// The AC forward key.
        /// </summary>
        AcForward = 283,

        /// <summary>
        /// The AC stop key.
        /// </summary>
        AcStop = 284,

        /// <summary>
        /// The AC refresh key.
        /// </summary>
        AcRefresh = 285,

        /// <summary>
        /// The AC bookmarks key.
        /// </summary>
        AcBookmarks = 286,

        /// <summary>
        /// The soft left key.
        /// </summary>
        /// <remarks>
        /// Usually situated below the display on phones and used as a multi-function feature key for selecting
        /// a software defined function shown on the bottom left of the display.
        /// </remarks>
        SoftLeft = 287,

        /// <summary>
        /// The soft right key.
        /// </summary>
        /// <remarks>
        /// Usually situated below the display on phones and used as a multi-function feature key for selecting
        /// a software defined function shown on the bottom right of the display.
        /// </remarks>
        SoftRight = 288,

        /// <summary>
        /// The call key.
        /// </summary>
        /// <remarks>
        /// Used for accepting phone calls.
        /// </remarks>
        Call = 289,

        /// <summary>
        /// The end call key.
        /// </summary>
        /// <remarks>
        /// Used for rejecting phone calls.
        /// </remarks>
        EndCall = 290,
    }
}
