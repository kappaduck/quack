// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.SDL;
using System.Diagnostics.CodeAnalysis;

namespace KappaDuck.Quack.Exceptions;

/// <summary>
/// An exception that is thrown when a native error occurs in the Quack engine.
/// </summary>
[PublicAPI]
public sealed class QuackNativeException : QuackException
{
    internal QuackNativeException()
    {
    }

    internal QuackNativeException(string? message) : base(message)
    {
    }

    internal QuackNativeException(string message, Exception innerException) : base(message, innerException)
    {
    }

    internal static void ThrowIf([DoesNotReturnIf(true)] bool condition, [CallerMemberName] string memberName = "")
    {
        if (condition)
            Throw(memberName);
    }

    internal static void ThrowIfFailed(bool condition, [CallerMemberName] string memberName = "")
        => ThrowIf(!condition, memberName);

    internal static void ThrowIfNegative(int value, [CallerMemberName] string memberName = "")
        => ThrowIf(int.IsNegative(value), memberName);

    [DoesNotReturn]
    private static void Throw(string memberName)
    {
        string message = Native.SDL_GetError();
        Native.SDL_ClearError();

        throw new QuackNativeException($"{memberName} failed: {message}");
    }
}
