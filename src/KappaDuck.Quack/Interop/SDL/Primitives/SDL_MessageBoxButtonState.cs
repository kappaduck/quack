// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Interop.SDL.Primitives;

[Flags]
internal enum SDL_MessageBoxButtonState : uint
{
    None = 0x00000000,
    ReturnKeyDefault = 0x00000001,
    EscapeKeyDefault = 0x00000002
}
