// // Copyright (c) KappaDuck. All rights reserved.
// // The source code is licensed under MIT License.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Interop.SDL;
using KappaDuck.Quack.Interop.SDL.Primitives;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Provides event management functionalities.
/// </summary>
[PublicAPI]
public static class EventManager
{
    private const uint None = 0;
    private const uint LastEvent = 0xFFFF;

    /// <summary>
    /// Disables the specified event type.
    /// </summary>
    /// <param name="type">The event type to disable.</param>
    public static void Disable(EventType type) => Native.SDL_SetEventEnabled(type, enabled: false);

    /// <summary>
    /// Enables the specified event type.
    /// </summary>
    /// <param name="type">The event type to enable.</param>
    public static void Enable(EventType type) => Native.SDL_SetEventEnabled(type, enabled: true);

    /// <summary>
    /// Clears all specified events from the event queue.
    /// </summary>
    /// <param name="min">The minimum event type to flush.</param>
    /// <param name="max">The maximum event type to flush. If <see langword="null"/>, only the <paramref name="min"/> event type is flushed.</param>
    /// <remarks>
    /// This will unconditionally remove events from the event queue based on the specified range, inclusive.
    /// It's also normal to just ignore events you don't need in your event loop without flushing them.
    /// This function only affects currently queued events. If you want to make sure that all pending OS events are flushed,
    /// call <see cref="Pump"/> on the main thread immediately before flushing.
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="min"/> is greater than <paramref name="max"/>.</exception>
    public static void Flush(EventType min, EventType? max = null)
    {
        ThrowIfInvalidRange(min, max);
        Native.SDL_FlushEvents(min, max ?? min);
    }

    /// <summary>
    /// Determines whether the specified event type is in the event queue.
    /// </summary>
    /// <param name="type">The event type to check.</param>
    /// <returns><see langword="true"/> if the event type is in the queue; otherwise, <see langword="false"/>.</returns>
    public static bool Has(EventType type) => Native.SDL_HasEvent(type);

    /// <summary>
    /// Determines whether any events in the specified range (inclusive) are in the event queue.
    /// </summary>
    /// <param name="min">The minimum event type to check.</param>
    /// <param name="max">The maximum event type to check.</param>
    /// <returns><see langword="true"/> if any events in the specified range are in the queue; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="min"/> is greater than <paramref name="max"/>.</exception>
    public static bool Has(EventType min, EventType max)
    {
        ThrowIfInvalidRange(min, max);
        return Native.SDL_HasEvents(min, max);
    }

    /// <summary>
    /// Determines whether the specified event type is enabled.
    /// </summary>
    /// <param name="type">The event type to check.</param>
    /// <returns>><see langword="true"/> if the event type is enabled; otherwise, <see langword="false"/>.</returns>
    public static bool IsEnabled(EventType type) => Native.SDL_EventEnabled(type);

    /// <summary>
    /// Peeks at events in the event queue without removing them.
    /// </summary>
    /// <param name="events">The buffer to store the peeked events at the front of event queue.</param>
    /// <param name="min">The minimum event type to peek, inclusive.</param>
    /// <param name="max">The maximum event type to peek, inclusive. If <see langword="null"/>, only the <paramref name="min"/> event type is peeked.</param>
    /// <returns>The number of events peeked.</returns>
    /// <remarks>
    /// You may have to call <see cref="Pump"/> before peeking to ensure that the events are ready to be filtered.
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="events"/> is empty.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="min"/> is greater than <paramref name="max"/>.</exception>
    /// <exception cref="QuackNativeException">Thrown when the underlying native call fails.</exception>
    public static int Peek(Span<Event> events, EventType min, EventType? max = null)
    {
        ArgumentOutOfRangeException.ThrowIfZero(events.Length);
        ThrowIfInvalidRange(min, max);

        int count = Native.SDL_PeepEvents(events, events.Length, EventAction.Peek, min, max ?? min);
        QuackNativeException.ThrowIfNegative(count);

        return count;
    }

    /// <summary>
    /// Polls for the next event in the event queue.
    /// </summary>
    /// <param name="e">The next fetched event from the queue.</param>
    /// <returns><see langword="true"/> if an event was fetched; otherwise, <see langword="false"/>.</returns>
    public static bool Poll(out Event e) => Native.SDL_PollEvent(out e);

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
    public static void Pump() => Native.SDL_PumpEvents();

    /// <summary>
    /// Adds the specified event to the event queue.
    /// </summary>
    /// <param name="e">The event to push onto the queue.</param>
    /// <returns><see langword="true"/> if the event was pushed; otherwise, <see langword="false"/> if the event was filtered or the event queue being full.</returns>
    public static bool Push(ref Event e) => Native.SDL_PushEvent(ref e);

    /// <summary>
    /// Adds the specified events to the event queue.
    /// </summary>
    /// <param name="events">The events to push onto the queue.</param>
    /// <returns>The number of events successfully pushed onto the queue.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="events"/> is empty.</exception>
    /// <exception cref="QuackNativeException">Thrown when the underlying native call fails.</exception>
    public static int Push(Span<Event> events)
    {
        ArgumentOutOfRangeException.ThrowIfZero(events.Length);

        int count = Native.SDL_PeepEvents(events, events.Length, EventAction.Add, None, LastEvent);
        QuackNativeException.ThrowIfNegative(count);

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
    /// <exception cref="QuackNativeException">Thrown when the underlying native call fails.</exception>
    public static int Retrieve(Span<Event> events, EventType min, EventType? max = null)
    {
        ArgumentOutOfRangeException.ThrowIfZero(events.Length);
        ThrowIfInvalidRange(min, max);

        int count = Native.SDL_PeepEvents(events, events.Length, EventAction.Get, min, max ?? min);
        QuackNativeException.ThrowIfNegative(count);

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
            return Native.SDL_WaitEvent(out e);

        return Native.SDL_WaitEventTimeout(out e, (int)timeout.Value.TotalMilliseconds);
    }

    private static void ThrowIfInvalidRange(EventType min, EventType? max)
    {
        if (max is null)
            return;

        ArgumentOutOfRangeException.ThrowIfGreaterThan((int)min, (int)max);
    }
}
