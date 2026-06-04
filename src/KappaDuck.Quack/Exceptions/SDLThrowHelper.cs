// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using System.Diagnostics;
using System.Numerics;

namespace KappaDuck.Quack.Exceptions;

internal static class SDLThrowHelper
{
    [StackTraceHidden]
    internal static void ThrowIf([DoesNotReturnIf(true)] bool condition, [CallerMemberName] string member = "")
    {
        if (condition)
            Throw(member);
    }

    [StackTraceHidden]
    internal static void ThrowIfFailed(bool success, [CallerMemberName] string member = "") => ThrowIf(!success, member);

    internal static void ThrowIfNegative<T>(T value, [CallerMemberName] string member = "") where T : INumber<T>
        => ThrowIf(T.IsNegative(value), member);

    internal static void ThrowIfNull<T>([NotNull] T? value, [CallerMemberName] string memberName = "") where T : class
        => ThrowIf(value is null, memberName);

    internal static unsafe void ThrowIfNull<T>(T* value, [CallerMemberName] string member = "") where T : unmanaged
        => ThrowIf(value is null, member);

    internal static unsafe void ThrowIfNull<T>(T** value, [CallerMemberName] string member = "") where T : unmanaged
        => ThrowIf(value is null, member);

    internal static void ThrowIfZero<T>(T value, [CallerMemberName] string member = "") where T : INumber<T>
        => ThrowIf(T.IsZero(value), member);

    [DoesNotReturn]
    [StackTraceHidden]
    private static void Throw(string member)
    {
        string error = SDL3.GetError() ?? "unknown SDL error";
        SDL3.ClearError();

        ThrowHelper.ThrowInterop(nameof(SDL3), error, member);
    }
}
