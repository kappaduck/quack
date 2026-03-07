// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Interop.SDL.Primitives;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Provides event management functionality.
/// </summary>
public static class EventManager
{
    private const uint None = 0;
    private const uint LastEvent = 0xFFFF;

    /// <summary>
    /// Peeks at events in the event queue without removing them.
    /// </summary>
    /// <param name="events">The buffer to store the peeked events at the front of event queue.</param>
    /// <param name="first">The first event type to peek, inclusive.</param>
    /// <param name="last">The last event type to peek, inclusive. If <see langword="null"/>, only the <paramref name="first"/> event type is peeked.</param>
    /// <returns>The number of events peeked.</returns>
    /// <remarks>
    /// You may have to call <see cref="Pump"/> before peeking to ensure that the events are ready to be filtered.
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="events"/> is empty.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="first"/> is greater than <paramref name="last"/>.</exception>
    /// <exception cref="QuackInteropException">Thrown when failing to peek events.</exception>
    public static int Peek(Span<Event> events, EventType first, EventType? last = null)
    {
        ArgumentOutOfRangeException.ThrowIfZero(events.Length);
        ThrowIfInvalidRange(first, last);

        int count = SDL3.Events.PeepEvents(events, events.Length, EventAction.Peek, first, last ?? first);
        QuackInteropException.ThrowIfNegative(count);

        return count;
    }

    /// <summary>
    /// Polls for the next event in the event queue.
    /// </summary>
    /// <param name="e">The next fetched event from the queue.</param>
    /// <returns><see langword="true"/> if an event was fetched; otherwise, <see langword="false"/>.</returns>
    public static bool Poll(out Event e) => SDL3.Events.PollEvent(out e);

    /// <summary>
    /// Pump the event loop, gathering events from the input devices.
    /// </summary>
    /// <remarks>
    /// This function updates the event queue and internal input device state.
    /// Gathers all the pending input information from devices and places it in the event queue.
    /// Without calls to <see cref="Pump"/> no events would ever be placed on the queue.
    /// Often the need for calls to <see cref="Pump"/> is hidden from the user since <see cref="Poll(out Event)"/> and <see cref="Wait(out Event, TimeSpan?)"/>
    /// implicitly call <see cref="Pump"/>. However, if you are not polling or waiting for events(e.g.you are filtering them),
    /// then you must call <see cref="Pump"/> to force an event queue update.
    /// </remarks>
    public static void Pump() => SDL3.Events.PumpEvents();

    /// <summary>
    /// Adds the specified event to the event queue.
    /// </summary>
    /// <param name="e">The event to push onto the queue.</param>
    /// <returns><see langword="true"/> if the event was pushed; otherwise, <see langword="false"/> if the event was filtered or the event queue being full.</returns>
    public static bool Push(ref Event e) => SDL3.Events.PushEvent(ref e);

    /// <summary>
    /// Adds the specified events to the event queue.
    /// </summary>
    /// <param name="events">The events to push onto the queue.</param>
    /// <returns>The number of events successfully pushed onto the queue.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="events"/> is empty.</exception>
    /// <exception cref="QuackInteropException">Thrown when failing to push events.</exception>
    public static int Push(Span<Event> events)
    {
        ArgumentOutOfRangeException.ThrowIfZero(events.Length);

        int count = SDL3.Events.PeepEvents(events, events.Length, EventAction.Add, None, LastEvent);
        QuackInteropException.ThrowIfNegative(count);

        return count;
    }

    /// <summary>
    /// Retrieves events from the event queue and removes them.
    /// </summary>
    /// <param name="events">The buffer to store the retrieved events from the front of event queue.</param>
    /// <param name="min">The minimum event type to retrieve, inclusive.</param>
    /// <param name="max">The maximum event type to retrieve, inclusive. If <see langword="null"/>, only the <paramref name="min"/> event type is retrieved.</param>
    /// <returns>The number of events retrieved.</returns>
    /// <remarks>
    /// You may have to call <see cref="Pump"/> before retrieving to ensure that the events are ready to be filtered.
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="events"/> is empty.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="min"/> is greater than <paramref name="max"/>.</exception>
    /// <exception cref="QuackInteropException">Thrown when failing to retrieve events.</exception>
    public static int Retrieve(Span<Event> events, EventType min, EventType? max = null)
    {
        ArgumentOutOfRangeException.ThrowIfZero(events.Length);
        ThrowIfInvalidRange(min, max);

        int count = SDL3.Events.PeepEvents(events, events.Length, EventAction.Get, min, max ?? min);
        QuackInteropException.ThrowIfNegative(count);

        return count;
    }

    /// <summary>
    /// Waits indefinitely or up to the specified timeout for the next event in the event queue.
    /// </summary>
    /// <param name="e">The next fetched event from the queue.</param>
    /// <param name="timeout">The maximum time to wait for an event. If <see langword="null"/>, waits indefinitely.</param>
    /// <returns><see langword="true"/> if an event was fetched; otherwise, <see langword="false"/> if the timeout elapsed without any events available.</returns>
    /// <remarks>
    /// The timeout is not guaranteed to be precise. The actual wait time may be longer than the specified timeout.
    /// </remarks>
    public static bool Wait(out Event e, TimeSpan? timeout = null)
    {
        if (!timeout.HasValue || timeout == Timeout.InfiniteTimeSpan)
            return SDL3.Events.WaitEvent(out e);

        return SDL3.Events.WaitEventTimeout(out e, (int)timeout.Value.TotalMilliseconds);
    }

    /// <summary>
    /// Creates an event watcher that invokes the specified callback function whenever an event is added to the event queue.
    /// </summary>
    /// <param name="callback">The callback function to be invoked when an event is added.</param>
    public static EventWatcher Watch(Action<Event> callback)
    {
        return new EventWatcher((_, ref e) =>
        {
            callback(e);
            return true;
        });
    }

    private static void ThrowIfInvalidRange(EventType first, EventType? last)
    {
        if (last is null)
            return;

        ArgumentOutOfRangeException.ThrowIfGreaterThan((int)first, (int)last);
    }
}
