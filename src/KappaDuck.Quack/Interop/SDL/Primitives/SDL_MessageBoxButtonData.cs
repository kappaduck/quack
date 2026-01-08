// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Interop.SDL.Primitives;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct SDL_MessageBoxButtonData
{
    public uint Flags { get; set; }

    public int ButtonId { get; set; }

    public byte* Text { get; set; }
}
