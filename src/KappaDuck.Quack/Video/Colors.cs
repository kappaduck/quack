// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Video;

/// <summary>
/// Provides the standard set of named <see cref="Color"/> values.
/// </summary>
/// <remarks>
/// The names and values follow the CSS color keywords. Every color is fully opaque (alpha 255),
/// except <see cref="Transparent"/>.
/// </remarks>
public static class Colors
{
    /// <summary>
    /// Gets a fully transparent color (#00000000).
    /// </summary>
    public static Color Transparent { get; } = Color.FromHex(0x00000000);

    /// <summary>
    /// Gets the color <c>AliceBlue</c> (#F0F8FF).
    /// </summary>
    public static Color AliceBlue { get; } = Color.FromHex(0xF0F8FFFF);

    /// <summary>
    /// Gets the color <c>AntiqueWhite</c> (#FAEBD7).
    /// </summary>
    public static Color AntiqueWhite { get; } = Color.FromHex(0xFAEBD7FF);

    /// <summary>
    /// Gets the color <c>Aqua</c> (#00FFFF).
    /// </summary>
    public static Color Aqua { get; } = Color.FromHex(0x00FFFFFF);

    /// <summary>
    /// Gets the color <c>Aquamarine</c> (#7FFFD4).
    /// </summary>
    public static Color Aquamarine { get; } = Color.FromHex(0x7FFFD4FF);

    /// <summary>
    /// Gets the color <c>Azure</c> (#F0FFFF).
    /// </summary>
    public static Color Azure { get; } = Color.FromHex(0xF0FFFFFF);

    /// <summary>
    /// Gets the color <c>Beige</c> (#F5F5DC).
    /// </summary>
    public static Color Beige { get; } = Color.FromHex(0xF5F5DCFF);

    /// <summary>
    /// Gets the color <c>Bisque</c> (#FFE4C4).
    /// </summary>
    public static Color Bisque { get; } = Color.FromHex(0xFFE4C4FF);

    /// <summary>
    /// Gets the color <c>Black</c> (#000000).
    /// </summary>
    public static Color Black { get; } = Color.FromHex(0x000000FF);

    /// <summary>
    /// Gets the color <c>BlanchedAlmond</c> (#FFEBCD).
    /// </summary>
    public static Color BlanchedAlmond { get; } = Color.FromHex(0xFFEBCDFF);

    /// <summary>
    /// Gets the color <c>Blue</c> (#0000FF).
    /// </summary>
    public static Color Blue { get; } = Color.FromHex(0x0000FFFF);

    /// <summary>
    /// Gets the color <c>BlueViolet</c> (#8A2BE2).
    /// </summary>
    public static Color BlueViolet { get; } = Color.FromHex(0x8A2BE2FF);

    /// <summary>
    /// Gets the color <c>Brown</c> (#A52A2A).
    /// </summary>
    public static Color Brown { get; } = Color.FromHex(0xA52A2AFF);

    /// <summary>
    /// Gets the color <c>BurlyWood</c> (#DEB887).
    /// </summary>
    public static Color BurlyWood { get; } = Color.FromHex(0xDEB887FF);

    /// <summary>
    /// Gets the color <c>CadetBlue</c> (#5F9EA0).
    /// </summary>
    public static Color CadetBlue { get; } = Color.FromHex(0x5F9EA0FF);

    /// <summary>
    /// Gets the color <c>Chartreuse</c> (#7FFF00).
    /// </summary>
    public static Color Chartreuse { get; } = Color.FromHex(0x7FFF00FF);

    /// <summary>
    /// Gets the color <c>Chocolate</c> (#D2691E).
    /// </summary>
    public static Color Chocolate { get; } = Color.FromHex(0xD2691EFF);

    /// <summary>
    /// Gets the color <c>Coral</c> (#FF7F50).
    /// </summary>
    public static Color Coral { get; } = Color.FromHex(0xFF7F50FF);

    /// <summary>
    /// Gets the color <c>CornflowerBlue</c> (#6495ED).
    /// </summary>
    public static Color CornflowerBlue { get; } = Color.FromHex(0x6495EDFF);

    /// <summary>
    /// Gets the color <c>Cornsilk</c> (#FFF8DC).
    /// </summary>
    public static Color Cornsilk { get; } = Color.FromHex(0xFFF8DCFF);

    /// <summary>
    /// Gets the color <c>Crimson</c> (#DC143C).
    /// </summary>
    public static Color Crimson { get; } = Color.FromHex(0xDC143CFF);

    /// <summary>
    /// Gets the color <c>Cyan</c> (#00FFFF).
    /// </summary>
    public static Color Cyan { get; } = Color.FromHex(0x00FFFFFF);

    /// <summary>
    /// Gets the color <c>DarkBlue</c> (#00008B).
    /// </summary>
    public static Color DarkBlue { get; } = Color.FromHex(0x00008BFF);

    /// <summary>
    /// Gets the color <c>DarkCyan</c> (#008B8B).
    /// </summary>
    public static Color DarkCyan { get; } = Color.FromHex(0x008B8BFF);

    /// <summary>
    /// Gets the color <c>DarkGoldenrod</c> (#B8860B).
    /// </summary>
    public static Color DarkGoldenrod { get; } = Color.FromHex(0xB8860BFF);

    /// <summary>
    /// Gets the color <c>DarkGray</c> (#A9A9A9).
    /// </summary>
    public static Color DarkGray { get; } = Color.FromHex(0xA9A9A9FF);

    /// <summary>
    /// Gets the color <c>DarkGreen</c> (#006400).
    /// </summary>
    public static Color DarkGreen { get; } = Color.FromHex(0x006400FF);

    /// <summary>
    /// Gets the color <c>DarkKhaki</c> (#BDB76B).
    /// </summary>
    public static Color DarkKhaki { get; } = Color.FromHex(0xBDB76BFF);

    /// <summary>
    /// Gets the color <c>DarkMagenta</c> (#8B008B).
    /// </summary>
    public static Color DarkMagenta { get; } = Color.FromHex(0x8B008BFF);

    /// <summary>
    /// Gets the color <c>DarkOliveGreen</c> (#556B2F).
    /// </summary>
    public static Color DarkOliveGreen { get; } = Color.FromHex(0x556B2FFF);

    /// <summary>
    /// Gets the color <c>DarkOrange</c> (#FF8C00).
    /// </summary>
    public static Color DarkOrange { get; } = Color.FromHex(0xFF8C00FF);

    /// <summary>
    /// Gets the color <c>DarkOrchid</c> (#9932CC).
    /// </summary>
    public static Color DarkOrchid { get; } = Color.FromHex(0x9932CCFF);

    /// <summary>
    /// Gets the color <c>DarkRed</c> (#8B0000).
    /// </summary>
    public static Color DarkRed { get; } = Color.FromHex(0x8B0000FF);

    /// <summary>
    /// Gets the color <c>DarkSalmon</c> (#E9967A).
    /// </summary>
    public static Color DarkSalmon { get; } = Color.FromHex(0xE9967AFF);

    /// <summary>
    /// Gets the color <c>DarkSeaGreen</c> (#8FBC8F).
    /// </summary>
    public static Color DarkSeaGreen { get; } = Color.FromHex(0x8FBC8FFF);

    /// <summary>
    /// Gets the color <c>DarkSlateBlue</c> (#483D8B).
    /// </summary>
    public static Color DarkSlateBlue { get; } = Color.FromHex(0x483D8BFF);

    /// <summary>
    /// Gets the color <c>DarkSlateGray</c> (#2F4F4F).
    /// </summary>
    public static Color DarkSlateGray { get; } = Color.FromHex(0x2F4F4FFF);

    /// <summary>
    /// Gets the color <c>DarkTurquoise</c> (#00CED1).
    /// </summary>
    public static Color DarkTurquoise { get; } = Color.FromHex(0x00CED1FF);

    /// <summary>
    /// Gets the color <c>DarkViolet</c> (#9400D3).
    /// </summary>
    public static Color DarkViolet { get; } = Color.FromHex(0x9400D3FF);

    /// <summary>
    /// Gets the color <c>DeepPink</c> (#FF1493).
    /// </summary>
    public static Color DeepPink { get; } = Color.FromHex(0xFF1493FF);

    /// <summary>
    /// Gets the color <c>DeepSkyBlue</c> (#00BFFF).
    /// </summary>
    public static Color DeepSkyBlue { get; } = Color.FromHex(0x00BFFFFF);

    /// <summary>
    /// Gets the color <c>DimGray</c> (#696969).
    /// </summary>
    public static Color DimGray { get; } = Color.FromHex(0x696969FF);

    /// <summary>
    /// Gets the color <c>DodgerBlue</c> (#1E90FF).
    /// </summary>
    public static Color DodgerBlue { get; } = Color.FromHex(0x1E90FFFF);

    /// <summary>
    /// Gets the color <c>Firebrick</c> (#B22222).
    /// </summary>
    public static Color Firebrick { get; } = Color.FromHex(0xB22222FF);

    /// <summary>
    /// Gets the color <c>FloralWhite</c> (#FFFAF0).
    /// </summary>
    public static Color FloralWhite { get; } = Color.FromHex(0xFFFAF0FF);

    /// <summary>
    /// Gets the color <c>ForestGreen</c> (#228B22).
    /// </summary>
    public static Color ForestGreen { get; } = Color.FromHex(0x228B22FF);

    /// <summary>
    /// Gets the color <c>Fuchsia</c> (#FF00FF).
    /// </summary>
    public static Color Fuchsia { get; } = Color.FromHex(0xFF00FFFF);

    /// <summary>
    /// Gets the color <c>Gainsboro</c> (#DCDCDC).
    /// </summary>
    public static Color Gainsboro { get; } = Color.FromHex(0xDCDCDCFF);

    /// <summary>
    /// Gets the color <c>GhostWhite</c> (#F8F8FF).
    /// </summary>
    public static Color GhostWhite { get; } = Color.FromHex(0xF8F8FFFF);

    /// <summary>
    /// Gets the color <c>Gold</c> (#FFD700).
    /// </summary>
    public static Color Gold { get; } = Color.FromHex(0xFFD700FF);

    /// <summary>
    /// Gets the color <c>Goldenrod</c> (#DAA520).
    /// </summary>
    public static Color Goldenrod { get; } = Color.FromHex(0xDAA520FF);

    /// <summary>
    /// Gets the color <c>Gray</c> (#808080).
    /// </summary>
    public static Color Gray { get; } = Color.FromHex(0x808080FF);

    /// <summary>
    /// Gets the color <c>Green</c> (#008000).
    /// </summary>
    public static Color Green { get; } = Color.FromHex(0x008000FF);

    /// <summary>
    /// Gets the color <c>GreenYellow</c> (#ADFF2F).
    /// </summary>
    public static Color GreenYellow { get; } = Color.FromHex(0xADFF2FFF);

    /// <summary>
    /// Gets the color <c>Honeydew</c> (#F0FFF0).
    /// </summary>
    public static Color Honeydew { get; } = Color.FromHex(0xF0FFF0FF);

    /// <summary>
    /// Gets the color <c>HotPink</c> (#FF69B4).
    /// </summary>
    public static Color HotPink { get; } = Color.FromHex(0xFF69B4FF);

    /// <summary>
    /// Gets the color <c>IndianRed</c> (#CD5C5C).
    /// </summary>
    public static Color IndianRed { get; } = Color.FromHex(0xCD5C5CFF);

    /// <summary>
    /// Gets the color <c>Indigo</c> (#4B0082).
    /// </summary>
    public static Color Indigo { get; } = Color.FromHex(0x4B0082FF);

    /// <summary>
    /// Gets the color <c>Ivory</c> (#FFFFF0).
    /// </summary>
    public static Color Ivory { get; } = Color.FromHex(0xFFFFF0FF);

    /// <summary>
    /// Gets the color <c>Khaki</c> (#F0E68C).
    /// </summary>
    public static Color Khaki { get; } = Color.FromHex(0xF0E68CFF);

    /// <summary>
    /// Gets the color <c>Lavender</c> (#E6E6FA).
    /// </summary>
    public static Color Lavender { get; } = Color.FromHex(0xE6E6FAFF);

    /// <summary>
    /// Gets the color <c>LavenderBlush</c> (#FFF0F5).
    /// </summary>
    public static Color LavenderBlush { get; } = Color.FromHex(0xFFF0F5FF);

    /// <summary>
    /// Gets the color <c>LawnGreen</c> (#7CFC00).
    /// </summary>
    public static Color LawnGreen { get; } = Color.FromHex(0x7CFC00FF);

    /// <summary>
    /// Gets the color <c>LemonChiffon</c> (#FFFACD).
    /// </summary>
    public static Color LemonChiffon { get; } = Color.FromHex(0xFFFACDFF);

    /// <summary>
    /// Gets the color <c>LightBlue</c> (#ADD8E6).
    /// </summary>
    public static Color LightBlue { get; } = Color.FromHex(0xADD8E6FF);

    /// <summary>
    /// Gets the color <c>LightCoral</c> (#F08080).
    /// </summary>
    public static Color LightCoral { get; } = Color.FromHex(0xF08080FF);

    /// <summary>
    /// Gets the color <c>LightCyan</c> (#E0FFFF).
    /// </summary>
    public static Color LightCyan { get; } = Color.FromHex(0xE0FFFFFF);

    /// <summary>
    /// Gets the color <c>LightGoldenrodYellow</c> (#FAFAD2).
    /// </summary>
    public static Color LightGoldenrodYellow { get; } = Color.FromHex(0xFAFAD2FF);

    /// <summary>
    /// Gets the color <c>LightGray</c> (#D3D3D3).
    /// </summary>
    public static Color LightGray { get; } = Color.FromHex(0xD3D3D3FF);

    /// <summary>
    /// Gets the color <c>LightGreen</c> (#90EE90).
    /// </summary>
    public static Color LightGreen { get; } = Color.FromHex(0x90EE90FF);

    /// <summary>
    /// Gets the color <c>LightPink</c> (#FFB6C1).
    /// </summary>
    public static Color LightPink { get; } = Color.FromHex(0xFFB6C1FF);

    /// <summary>
    /// Gets the color <c>LightSalmon</c> (#FFA07A).
    /// </summary>
    public static Color LightSalmon { get; } = Color.FromHex(0xFFA07AFF);

    /// <summary>
    /// Gets the color <c>LightSeaGreen</c> (#20B2AA).
    /// </summary>
    public static Color LightSeaGreen { get; } = Color.FromHex(0x20B2AAFF);

    /// <summary>
    /// Gets the color <c>LightSkyBlue</c> (#87CEFA).
    /// </summary>
    public static Color LightSkyBlue { get; } = Color.FromHex(0x87CEFAFF);

    /// <summary>
    /// Gets the color <c>LightSlateGray</c> (#778899).
    /// </summary>
    public static Color LightSlateGray { get; } = Color.FromHex(0x778899FF);

    /// <summary>
    /// Gets the color <c>LightSteelBlue</c> (#B0C4DE).
    /// </summary>
    public static Color LightSteelBlue { get; } = Color.FromHex(0xB0C4DEFF);

    /// <summary>
    /// Gets the color <c>LightYellow</c> (#FFFFE0).
    /// </summary>
    public static Color LightYellow { get; } = Color.FromHex(0xFFFFE0FF);

    /// <summary>
    /// Gets the color <c>Lime</c> (#00FF00).
    /// </summary>
    public static Color Lime { get; } = Color.FromHex(0x00FF00FF);

    /// <summary>
    /// Gets the color <c>LimeGreen</c> (#32CD32).
    /// </summary>
    public static Color LimeGreen { get; } = Color.FromHex(0x32CD32FF);

    /// <summary>
    /// Gets the color <c>Linen</c> (#FAF0E6).
    /// </summary>
    public static Color Linen { get; } = Color.FromHex(0xFAF0E6FF);

    /// <summary>
    /// Gets the color <c>Magenta</c> (#FF00FF).
    /// </summary>
    public static Color Magenta { get; } = Color.FromHex(0xFF00FFFF);

    /// <summary>
    /// Gets the color <c>Maroon</c> (#800000).
    /// </summary>
    public static Color Maroon { get; } = Color.FromHex(0x800000FF);

    /// <summary>
    /// Gets the color <c>MediumAquamarine</c> (#66CDAA).
    /// </summary>
    public static Color MediumAquamarine { get; } = Color.FromHex(0x66CDAAFF);

    /// <summary>
    /// Gets the color <c>MediumBlue</c> (#0000CD).
    /// </summary>
    public static Color MediumBlue { get; } = Color.FromHex(0x0000CDFF);

    /// <summary>
    /// Gets the color <c>MediumOrchid</c> (#BA55D3).
    /// </summary>
    public static Color MediumOrchid { get; } = Color.FromHex(0xBA55D3FF);

    /// <summary>
    /// Gets the color <c>MediumPurple</c> (#9370DB).
    /// </summary>
    public static Color MediumPurple { get; } = Color.FromHex(0x9370DBFF);

    /// <summary>
    /// Gets the color <c>MediumSeaGreen</c> (#3CB371).
    /// </summary>
    public static Color MediumSeaGreen { get; } = Color.FromHex(0x3CB371FF);

    /// <summary>
    /// Gets the color <c>MediumSlateBlue</c> (#7B68EE).
    /// </summary>
    public static Color MediumSlateBlue { get; } = Color.FromHex(0x7B68EEFF);

    /// <summary>
    /// Gets the color <c>MediumSpringGreen</c> (#00FA9A).
    /// </summary>
    public static Color MediumSpringGreen { get; } = Color.FromHex(0x00FA9AFF);

    /// <summary>
    /// Gets the color <c>MediumTurquoise</c> (#48D1CC).
    /// </summary>
    public static Color MediumTurquoise { get; } = Color.FromHex(0x48D1CCFF);

    /// <summary>
    /// Gets the color <c>MediumVioletRed</c> (#C71585).
    /// </summary>
    public static Color MediumVioletRed { get; } = Color.FromHex(0xC71585FF);

    /// <summary>
    /// Gets the color <c>MidnightBlue</c> (#191970).
    /// </summary>
    public static Color MidnightBlue { get; } = Color.FromHex(0x191970FF);

    /// <summary>
    /// Gets the color <c>MintCream</c> (#F5FFFA).
    /// </summary>
    public static Color MintCream { get; } = Color.FromHex(0xF5FFFAFF);

    /// <summary>
    /// Gets the color <c>MistyRose</c> (#FFE4E1).
    /// </summary>
    public static Color MistyRose { get; } = Color.FromHex(0xFFE4E1FF);

    /// <summary>
    /// Gets the color <c>Moccasin</c> (#FFE4B5).
    /// </summary>
    public static Color Moccasin { get; } = Color.FromHex(0xFFE4B5FF);

    /// <summary>
    /// Gets the color <c>NavajoWhite</c> (#FFDEAD).
    /// </summary>
    public static Color NavajoWhite { get; } = Color.FromHex(0xFFDEADFF);

    /// <summary>
    /// Gets the color <c>Navy</c> (#000080).
    /// </summary>
    public static Color Navy { get; } = Color.FromHex(0x000080FF);

    /// <summary>
    /// Gets the color <c>OldLace</c> (#FDF5E6).
    /// </summary>
    public static Color OldLace { get; } = Color.FromHex(0xFDF5E6FF);

    /// <summary>
    /// Gets the color <c>Olive</c> (#808000).
    /// </summary>
    public static Color Olive { get; } = Color.FromHex(0x808000FF);

    /// <summary>
    /// Gets the color <c>OliveDrab</c> (#6B8E23).
    /// </summary>
    public static Color OliveDrab { get; } = Color.FromHex(0x6B8E23FF);

    /// <summary>
    /// Gets the color <c>Orange</c> (#FFA500).
    /// </summary>
    public static Color Orange { get; } = Color.FromHex(0xFFA500FF);

    /// <summary>
    /// Gets the color <c>OrangeRed</c> (#FF4500).
    /// </summary>
    public static Color OrangeRed { get; } = Color.FromHex(0xFF4500FF);

    /// <summary>
    /// Gets the color <c>Orchid</c> (#DA70D6).
    /// </summary>
    public static Color Orchid { get; } = Color.FromHex(0xDA70D6FF);

    /// <summary>
    /// Gets the color <c>PaleGoldenrod</c> (#EEE8AA).
    /// </summary>
    public static Color PaleGoldenrod { get; } = Color.FromHex(0xEEE8AAFF);

    /// <summary>
    /// Gets the color <c>PaleGreen</c> (#98FB98).
    /// </summary>
    public static Color PaleGreen { get; } = Color.FromHex(0x98FB98FF);

    /// <summary>
    /// Gets the color <c>PaleTurquoise</c> (#AFEEEE).
    /// </summary>
    public static Color PaleTurquoise { get; } = Color.FromHex(0xAFEEEEFF);

    /// <summary>
    /// Gets the color <c>PaleVioletRed</c> (#DB7093).
    /// </summary>
    public static Color PaleVioletRed { get; } = Color.FromHex(0xDB7093FF);

    /// <summary>
    /// Gets the color <c>PapayaWhip</c> (#FFEFD5).
    /// </summary>
    public static Color PapayaWhip { get; } = Color.FromHex(0xFFEFD5FF);

    /// <summary>
    /// Gets the color <c>PeachPuff</c> (#FFDAB9).
    /// </summary>
    public static Color PeachPuff { get; } = Color.FromHex(0xFFDAB9FF);

    /// <summary>
    /// Gets the color <c>Peru</c> (#CD853F).
    /// </summary>
    public static Color Peru { get; } = Color.FromHex(0xCD853FFF);

    /// <summary>
    /// Gets the color <c>Pink</c> (#FFC0CB).
    /// </summary>
    public static Color Pink { get; } = Color.FromHex(0xFFC0CBFF);

    /// <summary>
    /// Gets the color <c>Plum</c> (#DDA0DD).
    /// </summary>
    public static Color Plum { get; } = Color.FromHex(0xDDA0DDFF);

    /// <summary>
    /// Gets the color <c>PowderBlue</c> (#B0E0E6).
    /// </summary>
    public static Color PowderBlue { get; } = Color.FromHex(0xB0E0E6FF);

    /// <summary>
    /// Gets the color <c>Purple</c> (#800080).
    /// </summary>
    public static Color Purple { get; } = Color.FromHex(0x800080FF);

    /// <summary>
    /// Gets the color <c>RebeccaPurple</c> (#663399).
    /// </summary>
    public static Color RebeccaPurple { get; } = Color.FromHex(0x663399FF);

    /// <summary>
    /// Gets the color <c>Red</c> (#FF0000).
    /// </summary>
    public static Color Red { get; } = Color.FromHex(0xFF0000FF);

    /// <summary>
    /// Gets the color <c>RosyBrown</c> (#BC8F8F).
    /// </summary>
    public static Color RosyBrown { get; } = Color.FromHex(0xBC8F8FFF);

    /// <summary>
    /// Gets the color <c>RoyalBlue</c> (#4169E1).
    /// </summary>
    public static Color RoyalBlue { get; } = Color.FromHex(0x4169E1FF);

    /// <summary>
    /// Gets the color <c>SaddleBrown</c> (#8B4513).
    /// </summary>
    public static Color SaddleBrown { get; } = Color.FromHex(0x8B4513FF);

    /// <summary>
    /// Gets the color <c>Salmon</c> (#FA8072).
    /// </summary>
    public static Color Salmon { get; } = Color.FromHex(0xFA8072FF);

    /// <summary>
    /// Gets the color <c>SandyBrown</c> (#F4A460).
    /// </summary>
    public static Color SandyBrown { get; } = Color.FromHex(0xF4A460FF);

    /// <summary>
    /// Gets the color <c>SeaGreen</c> (#2E8B57).
    /// </summary>
    public static Color SeaGreen { get; } = Color.FromHex(0x2E8B57FF);

    /// <summary>
    /// Gets the color <c>SeaShell</c> (#FFF5EE).
    /// </summary>
    public static Color SeaShell { get; } = Color.FromHex(0xFFF5EEFF);

    /// <summary>
    /// Gets the color <c>Sienna</c> (#A0522D).
    /// </summary>
    public static Color Sienna { get; } = Color.FromHex(0xA0522DFF);

    /// <summary>
    /// Gets the color <c>Silver</c> (#C0C0C0).
    /// </summary>
    public static Color Silver { get; } = Color.FromHex(0xC0C0C0FF);

    /// <summary>
    /// Gets the color <c>SkyBlue</c> (#87CEEB).
    /// </summary>
    public static Color SkyBlue { get; } = Color.FromHex(0x87CEEBFF);

    /// <summary>
    /// Gets the color <c>SlateBlue</c> (#6A5ACD).
    /// </summary>
    public static Color SlateBlue { get; } = Color.FromHex(0x6A5ACDFF);

    /// <summary>
    /// Gets the color <c>SlateGray</c> (#708090).
    /// </summary>
    public static Color SlateGray { get; } = Color.FromHex(0x708090FF);

    /// <summary>
    /// Gets the color <c>Snow</c> (#FFFAFA).
    /// </summary>
    public static Color Snow { get; } = Color.FromHex(0xFFFAFAFF);

    /// <summary>
    /// Gets the color <c>SpringGreen</c> (#00FF7F).
    /// </summary>
    public static Color SpringGreen { get; } = Color.FromHex(0x00FF7FFF);

    /// <summary>
    /// Gets the color <c>SteelBlue</c> (#4682B4).
    /// </summary>
    public static Color SteelBlue { get; } = Color.FromHex(0x4682B4FF);

    /// <summary>
    /// Gets the color <c>Tan</c> (#D2B48C).
    /// </summary>
    public static Color Tan { get; } = Color.FromHex(0xD2B48CFF);

    /// <summary>
    /// Gets the color <c>Teal</c> (#008080).
    /// </summary>
    public static Color Teal { get; } = Color.FromHex(0x008080FF);

    /// <summary>
    /// Gets the color <c>Thistle</c> (#D8BFD8).
    /// </summary>
    public static Color Thistle { get; } = Color.FromHex(0xD8BFD8FF);

    /// <summary>
    /// Gets the color <c>Tomato</c> (#FF6347).
    /// </summary>
    public static Color Tomato { get; } = Color.FromHex(0xFF6347FF);

    /// <summary>
    /// Gets the color <c>Turquoise</c> (#40E0D0).
    /// </summary>
    public static Color Turquoise { get; } = Color.FromHex(0x40E0D0FF);

    /// <summary>
    /// Gets the color <c>Violet</c> (#EE82EE).
    /// </summary>
    public static Color Violet { get; } = Color.FromHex(0xEE82EEFF);

    /// <summary>
    /// Gets the color <c>Wheat</c> (#F5DEB3).
    /// </summary>
    public static Color Wheat { get; } = Color.FromHex(0xF5DEB3FF);

    /// <summary>
    /// Gets the color <c>White</c> (#FFFFFF).
    /// </summary>
    public static Color White { get; } = Color.FromHex(0xFFFFFFFF);

    /// <summary>
    /// Gets the color <c>WhiteSmoke</c> (#F5F5F5).
    /// </summary>
    public static Color WhiteSmoke { get; } = Color.FromHex(0xF5F5F5FF);

    /// <summary>
    /// Gets the color <c>Yellow</c> (#FFFF00).
    /// </summary>
    public static Color Yellow { get; } = Color.FromHex(0xFFFF00FF);

    /// <summary>
    /// Gets the color <c>YellowGreen</c> (#9ACD32).
    /// </summary>
    public static Color YellowGreen { get; } = Color.FromHex(0x9ACD32FF);
}
