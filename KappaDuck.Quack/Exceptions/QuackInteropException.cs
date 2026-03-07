// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Numerics;

namespace KappaDuck.Quack.Exceptions;

/// <summary>
/// An exception that is thrown when an interop call fails in Quack!.
/// </summary>
public sealed class QuackInteropException : QuackException
{
    private QuackInteropException(string message) : base(message)
    {
    }

    internal static void ThrowIf([DoesNotReturnIf(true)] bool condition, [CallerMemberName] string memberName = "")
    {
        if (condition)
            Throw(memberName);
    }

    internal static void ThrowIfFailed(bool success, [CallerMemberName] string memberName = "")
        => ThrowIf(!success, memberName);

    internal static void ThrowIfHandleInvalid(SafeHandle handle, [CallerMemberName] string memberName = "")
        => ThrowIf(handle.IsInvalid, memberName);

    internal static void ThrowIfNegative<T>(T value, [CallerMemberName] string memberName = "") where T : INumber<T>
        => ThrowIf(T.IsNegative(value), memberName);

    internal static unsafe void ThrowIfNull<T>(T* value, [CallerMemberName] string memberName = "") where T : unmanaged
    => ThrowIf(value is null, memberName);

    internal static unsafe void ThrowIfNull<T>(T** value, [CallerMemberName] string memberName = "") where T : unmanaged
        => ThrowIf(value is null, memberName);

    internal static void ThrowIfZero<T>(T value, [CallerMemberName] string memberName = "") where T : INumber<T>
        => ThrowIf(T.IsZero(value), memberName);

    [DoesNotReturn]
    private static void Throw(string memberName)
    {
        string error = SDL3.GetError() ?? string.Empty;
        SDL3.ClearError();

        throw new QuackInteropException($"Interop call failed in [{memberName}]: {error}");
    }
}
