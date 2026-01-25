// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Interop.SDL.Primitives;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct SDL_MessageBoxData
{
    internal uint Flags { get; set; }

    internal IntPtr Window { get; set; }

    internal byte* Title { get; set; }

    internal byte* Message { get; set; }

    internal int NumButtons { get; set; }

    internal SDL_MessageBoxButtonData* Buttons { get; set; }

    private readonly nint _colorScheme;
}
