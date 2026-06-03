// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Exceptions;

/// <summary>
/// Represents an error that occurred while interacting with native libraries or unmanaged code.
/// </summary>
public class QuackInteropException : QuackException
{
    internal QuackInteropException()
    {
    }

    internal QuackInteropException(string message) : base(message)
    {
    }

    internal QuackInteropException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
