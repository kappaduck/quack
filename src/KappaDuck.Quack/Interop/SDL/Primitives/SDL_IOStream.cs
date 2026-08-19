// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Interop.SDL.Primitives;

internal readonly struct SDL_IOStream;

[StructLayout(LayoutKind.Sequential)]
internal readonly struct SDL_IOStreamInterface
{
    internal uint Version { get; init; }

    internal delegate* unmanaged[Cdecl]<nint, long> Size { get; init; }

    internal delegate* unmanaged[Cdecl]<nint, long, SDL_IOWhence, long> Seek { get; init; }

    internal delegate* unmanaged[Cdecl]<nint, void*, nuint, SDL_IOStatus*, nuint> Read { get; init; }

    internal delegate* unmanaged[Cdecl]<nint, void*, nuint, SDL_IOStatus*, nuint> Write { get; init; }

    internal delegate* unmanaged[Cdecl]<nint, SDL_IOStatus*, byte> Flush { get; init; }

    internal delegate* unmanaged[Cdecl]<nint, byte> Close { get; init; }
}
