// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

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

    [DoesNotReturn]
    private static void Throw(string memberName)
    {
        string error = SDL3.GetError();
        SDL3.ClearError();

        throw new QuackInteropException($"Interop call failed in [{memberName}]: {error}");
    }
}
