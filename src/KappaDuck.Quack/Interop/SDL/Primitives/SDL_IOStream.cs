// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Interop.SDL.Primitives;

internal readonly struct SDL_IOStream;

[StructLayout(LayoutKind.Sequential)]
internal readonly struct SDL_IOStreamInterface
{
    internal uint Version { get; init; }

    internal delegate* unmanaged[Cdecl]<void*, long> Size { get; init; }

    internal delegate* unmanaged[Cdecl]<void*, long, SDL_IOWhence, long> Seek { get; init; }

    internal delegate* unmanaged[Cdecl]<void*, void*, nuint, SDL_IOStatus*, nuint> Read { get; init; }

    internal delegate* unmanaged[Cdecl]<void*, void*, nuint, SDL_IOStatus*, nuint> Write { get; init; }

    internal delegate* unmanaged[Cdecl]<void*, SDL_IOStatus*, byte> Flush { get; init; }

    internal delegate* unmanaged[Cdecl]<void*, byte> Close { get; init; }
}
