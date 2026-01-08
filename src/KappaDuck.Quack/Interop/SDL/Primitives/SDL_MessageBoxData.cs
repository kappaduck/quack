// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Interop.SDL.Primitives;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct SDL_MessageBoxData
{
    public uint Flags { get; set; }

    public IntPtr Window { get; set; }

    public byte* Title { get; set; }

    public byte* Message { get; set; }

    public int NumButtons { get; set; }

    public SDL_MessageBoxButtonData* Buttons { get; set; }

    private readonly nint _colorScheme;
}
