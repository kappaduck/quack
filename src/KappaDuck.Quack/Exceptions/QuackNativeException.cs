// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.Marshalling;
using KappaDuck.Quack.Interop.SDL;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace KappaDuck.Quack.Exceptions;

/// <summary>
/// An exception that is thrown when an error occurs from native calls.
/// </summary>
public sealed partial class QuackNativeException : QuackException
{
    internal QuackNativeException(string? message) : base(message)
    {
    }

    internal static void ThrowIf([DoesNotReturnIf(true)] bool condition)
    {
        if (condition)
            Throw();
    }

    internal static void ThrowIfFailed(bool result) => ThrowIf(!result);

    internal static void ThrowIfNegative(int value) => ThrowIf(int.IsNegative(value));

    internal static unsafe void ThrowIfNull<T>(T* value) where T : unmanaged => ThrowIf(value is null);

    internal static void ThrowIfZero(uint value) => ThrowIf(value == 0);

    [DoesNotReturn]
    private static void Throw()
    {
        string message = SDL_GetError();
        SDL_ClearError();

        throw new QuackNativeException(message);
    }

    [LibraryImport(SDL.Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ClearError();

    [LibraryImport(SDL.Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SDLOwnedStringMarshaller))]
    private static partial string SDL_GetError();
}
