// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Diagnostics.CodeAnalysis;

namespace KappaDuck.Quack.Exceptions;

/// <summary>
/// An exception that is thrown when a native error occurs in the Quack engine.
/// </summary>
public sealed class QuackNativeException : QuackException
{
    private QuackNativeException(string? message) : base(message)
    {
    }

    internal static void ThrowIf([DoesNotReturnIf(true)] bool condition, [CallerMemberName] string memberName = "")
    {
        if (condition)
            Throw(memberName);
    }

    internal static void ThrowIfFailed(bool condition, [CallerMemberName] string memberName = "")
        => ThrowIf(!condition, memberName);

    internal static void ThrowIfHandleInvalid(SafeHandle handle, [CallerMemberName] string memberName = "")
        => ThrowIf(handle.IsInvalid, memberName);

    internal static void ThrowIfNegative(int value, [CallerMemberName] string memberName = "")
        => ThrowIf(int.IsNegative(value), memberName);

    internal static unsafe void ThrowIfNull<T>(T* value, [CallerMemberName] string memberName = "") where T : unmanaged
        => ThrowIf(value is null, memberName);

    internal static void ThrowIfZero(uint value, [CallerMemberName] string memberName = "")
        => ThrowIf(value == 0, memberName);

    [DoesNotReturn]
    private static void Throw(string memberName)
    {
        string message = Native.SDL_GetError();
        Native.SDL_ClearError();

        throw new QuackNativeException($"{memberName} failed: {message}");
    }
}
