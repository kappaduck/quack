// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents an event watcher to monitor new events added to the event queue.
/// </summary>
/// <remarks>
/// You can dispose the watcher to stop monitoring events.
/// </remarks>
public sealed class EventWatcher : IDisposable
{
    private readonly SDL3.EventFilter _callback;

    internal EventWatcher(SDL3.EventFilter callback)
    {
        _callback = callback;
        SDL3.AddEventWatch(_callback);
    }

    /// <inheritdoc/>
    public void Dispose() => SDL3.RemoveEventWatch(_callback);
}
