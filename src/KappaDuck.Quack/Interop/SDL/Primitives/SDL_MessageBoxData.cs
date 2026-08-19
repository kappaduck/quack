// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Interop.SDL.Primitives;

[StructLayout(LayoutKind.Sequential)]
internal readonly struct SDL_MessageBoxData
{
    internal SDL_MessageBoxState State { get; init; }

    internal SDL_Window* Window { get; init; }

    internal byte* Title { get; init; }

    internal byte* Message { get; init; }

    internal int Count { get; init; }

    internal SDL_MessageBoxButtonData* Buttons { get; init; }

    internal SDL_MessageBoxColorScheme* ColorScheme { get; init; }
}
