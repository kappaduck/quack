// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Exceptions;

/// <summary>
/// Represents the base exception for all Quack-related errors.
/// </summary>
public class QuackException : Exception
{
    internal QuackException()
    {
    }

    internal QuackException(string message) : base(message)
    {
    }

    internal QuackException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
