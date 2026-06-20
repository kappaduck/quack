// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using System.Diagnostics;

namespace KappaDuck.Quack.Exceptions;

internal static class ThrowHelper
{
    [DoesNotReturn]
    [StackTraceHidden]
    internal static void Throw(string message, string member) => throw new QuackException($"{message} ({member})");

    [StackTraceHidden]
    internal static void ThrowIf([DoesNotReturnIf(true)] bool condition, string message, [CallerMemberName] string member = "")
    {
        if (condition)
            Throw(message, member);
    }

    [DoesNotReturn]
    [StackTraceHidden]
    internal static void ThrowFormat(string message) => throw new FormatException(message);

    [DoesNotReturn]
    [StackTraceHidden]
    internal static void ThrowInterop(string module, string message, string member)
        => throw new QuackInteropException($"[{module}] {message} ({member})");

    [DoesNotReturn]
    [StackTraceHidden]
    internal static void ThrowInvalidOperation(string message)
        => throw new InvalidOperationException(message);

    [DoesNotReturn]
    [StackTraceHidden]
    internal static void ThrowOperationCanceled(string message)
        => throw new OperationCanceledException(message);
}
