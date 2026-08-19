// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Interop.SDL.Primitives;

[StructLayout(LayoutKind.Sequential)]
internal readonly struct SDL_MessageBoxButtonData
{
    internal SDL_MessageBoxButtonState State { get; init; }

    internal int ButtonId { get; init; }

    internal byte* Text { get; init; }
}
