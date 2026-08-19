// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Interop.SDL.Primitives;

[StructLayout(LayoutKind.Sequential)]
internal readonly struct SDL_MessageBoxColor
{
    internal byte R { get; init; }

    internal byte G { get; init; }

    internal byte B { get; init; }
}
