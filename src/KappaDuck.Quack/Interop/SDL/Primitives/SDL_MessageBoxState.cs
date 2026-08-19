// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Interop.SDL.Primitives;

[Flags]
internal enum SDL_MessageBoxState : uint
{
    Error = 0x00000010u,
    Warning = 0x00000020u,
    Information = 0x00000040u,
    ButtonsLeftToRight = 0x00000080u,
    ButtonsRightToLeft = 0x00000100u
}
