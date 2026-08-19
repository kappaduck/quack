// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Interop.SDL.Primitives;

[StructLayout(LayoutKind.Sequential)]
internal readonly struct SDL_MessageBoxColorScheme
{
    internal SDL_MessageBoxColor Background { get; init; }

    internal SDL_MessageBoxColor Text { get; init; }

    internal SDL_MessageBoxColor ButtonBorder { get; init; }

    internal SDL_MessageBoxColor ButtonBackground { get; init; }

    internal SDL_MessageBoxColor ButtonSelected { get; init; }
}
