// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Interop.SDL.Primitives;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct SDL_MessageBoxButtonData
{
    internal uint Flags { get; set; }

    internal int ButtonId { get; set; }

    internal byte* Text { get; set; }
}
