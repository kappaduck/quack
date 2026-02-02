// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Exceptions;

/// <summary>
/// An exception that is thrown when an error occurs in Quack!.
/// </summary>
public class QuackException : Exception
{
    internal QuackException(string message) : base(message)
    {
    }

    internal static void ThrowIf([DoesNotReturnIf(true)] bool condition, string message, [CallerMemberName] string memberName = "")
    {
        if (condition)
            throw new QuackException($"[{memberName}] {message}");
    }
}
