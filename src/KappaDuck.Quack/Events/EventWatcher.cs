// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Events;

/// <summary>
/// Represents an event watcher to monitor new events added to the event queue.
/// </summary>
/// <remarks>
/// You can dispose the watcher to stop monitoring events.
/// </remarks>
public sealed class EventWatcher : IDisposable
{
    private readonly Native.EventFilter _callback;

    internal EventWatcher(Native.EventFilter callback)
    {
        _callback = callback;
        Native.SDL_AddEventWatch(_callback);
    }

    /// <inheritdoc/>
    public void Dispose() => Native.SDL_RemoveEventWatch(_callback);
}
