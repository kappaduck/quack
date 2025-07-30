// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Diagnostics.CodeAnalysis;

namespace KappaDuck.Quack.Exceptions;

/// <summary>
/// An exception that is thrown when an error occurs in the Quack engine.
/// </summary>
public class QuackException : Exception
{
    internal QuackException()
    {
    }

    internal QuackException(string? message) : base(message)
    {
    }

    internal QuackException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    internal static void ThrowIf([DoesNotReturnIf(true)] bool condition, string? message)
    {
        if (condition)
            throw new QuackException(message);
    }

    internal static void ThrowIfNull<T>([NotNull] T? value, string? message) => ThrowIf(value is null, message);
}
