// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents an event watcher to monitor new events added to the event loop.
/// </summary>
public sealed class EventWatcher : IDisposable
{
    private readonly SDL3.Events.EventFilter _callback;

    internal EventWatcher(SDL3.Events.EventFilter callback)
    {
        _callback = callback;
        SDL3.Events.AddEventWatch(_callback);
    }

    /// <summary>
    /// Removes the event watcher from the event loop, stopping it from monitoring new events.
    /// </summary>
    public void Dispose() => SDL3.Events.RemoveEventWatch(_callback);
}
