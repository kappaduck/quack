// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents a single event polled from the event queue.
/// </summary>
/// <remarks>
/// Initializes a new <see cref="Event"/> that holds a <see cref="QuitEvent"/>.
/// </remarks>
/// <param name="value">The quit event.</param>
[Union]
public readonly struct Event(QuitEvent value) : IUnion
{
    private readonly EventKind _kind = EventKind.Quit;

    /// <summary>
    /// Gets a value indicating whether this event holds a value or not.
    /// </summary>
    public bool HasValue => _kind != EventKind.None;

    /// <summary>
    /// Gets the underlying value by boxing or <see langword="null"/> if this event holds none.
    /// </summary>
    public object? Value => _kind switch
    {
        EventKind.Quit => value,
        EventKind.None => null,
        _ => null
    };

    /// <summary>
    /// Attempts to retrieve this event as a <see cref="QuitEvent"/>.
    /// </summary>
    /// <param name="e">The quit event.</param>
    /// <returns><see langword="true"/> if this event holds a <see cref="QuitEvent"/>; otherwise <see langword="false"/></returns>
    public bool TryGetValue(out QuitEvent e)
    {
        e = value;
        return _kind == EventKind.Quit;
    }
}
