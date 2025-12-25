// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Diagnostics.CodeAnalysis;

namespace KappaDuck.Quack.Exceptions;

/// <summary>
/// An exception that is thrown when an error occurs in the Quack engine.
/// </summary>
public class QuackException : Exception
{
    internal QuackException(string? message) : base(message)
    {
    }

    internal static void ThrowIf([DoesNotReturnIf(true)] bool condition, string message, [CallerMemberName] string memberName = "")
    {
        if (condition)
            throw new QuackException($"{memberName} failed: {message}");
    }
}
