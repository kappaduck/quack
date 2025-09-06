// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Interop.SDL;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Global event functions.
/// </summary>
public static class EventManager
{
    /// <summary>
    /// Disable a specific event type.
    /// </summary>
    /// <param name="type">The event type to disable.</param>
    public static void Disable(EventType type) => SDL.Events.SDL_SetEventEnabled(type, enabled: false);

    /// <summary>
    /// Enable a specific event type.
    /// </summary>
    /// <param name="type">The event type to enable.</param>
    public static void Enable(EventType type) => SDL.Events.SDL_SetEventEnabled(type, enabled: true);

    /// <summary>
    /// Clear a range of events from the event queue.
    /// </summary>
    /// <param name="type">The low end of event type to be cleared, inclusive.</param>
    /// <param name="maxType">
    /// The high end of event type to be cleared, exclusive. If <see langword="null"/> then <paramref name="type"/> is used.
    /// </param>
    /// <remarks>
    /// This will unconditionally remove any events from the queue that are in the range of <paramref name="type"/> to <paramref name="maxType"/>, inclusive.
    /// It's also normal to just ignore events you don't care about in your event loop without calling this function.
    /// This function only affects currently queued events. If you want to make sure that all pending OS events are flushed,
    /// you can call <see cref="Pump"/> on the main thread immediately before the flush call.
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="type"/> is greater than <paramref name="maxType"/>.</exception>
    public static void Flush(EventType type, EventType? maxType = null)
    {
        ThrowIfTypeGreaterThanMaxType(type, maxType);

        SDL.Events.SDL_FlushEvents(type, maxType ?? type);
    }

    /// <summary>
    /// Check for the existence of a certain event type in the event queue.
    /// </summary>
    /// <param name="type">The event type to check if exists.</param>
    /// <returns><see langword="true"/> if the event type exists in the queue; otherwise, <see langword="false"/>.</returns>
    public static bool Has(EventType type) => SDL.Events.SDL_HasEvent(type);

    /// <summary>
    /// Check for the existence of a range of event types in the event queue.
    /// </summary>
    /// <param name="type">The low end of event type to check for existence, inclusive.</param>
    /// <param name="maxType">The high end of event type to check for existence, exclusive. If <see langword="null"/> then <paramref name="type"/> is used.</param>
    /// <returns><see langword="true"/> if events with type >= <paramref name="type"/> and &lt;= <paramref name="maxType"/> are present, or <see langword="false"/> if not.</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="type"/> is greater than <paramref name="maxType"/>.</exception>
    public static bool Has(EventType type, EventType? maxType)
    {
        ThrowIfTypeGreaterThanMaxType(type, maxType);

        return SDL.Events.SDL_HasEvents(type, maxType ?? type);
    }

    /// <summary>
    /// Query the state of processing events by type.
    /// </summary>
    /// <param name="type">The event type to check.</param>
    /// <returns><see langword="true"/> if the event is being processed, <see langword="false"/> if not.</returns>
    public static bool IsEnabled(EventType type) => SDL.Events.SDL_EventEnabled(type);

    /// <summary>
    /// Retrieve events from the event queue without removing them.
    /// </summary>
    /// <param name="events">Destination buffer for the events at the front of the event queue.</param>
    /// <param name="minType">The low end of event type to be retrieved, inclusive.</param>
    /// <param name="maxType">the high end of event type to be retrieved, inclusive. If <see langword="null"/> then minType is used.</param>
    /// <returns>The number of events actually stored.</returns>
    /// <remarks>
    /// You may have to call <see cref="Pump"/> before calling this function. Otherwise, the events may not be ready to be filtered.
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="minType"/> is greater than <paramref name="maxType"/>.</exception>
    /// <exception cref="QuackNativeException">Failed while peeping events.</exception>
    public static int Peek(Span<Event> events, EventType minType, EventType? maxType = null)
    {
        ThrowIfTypeGreaterThanMaxType(minType, maxType);

        int peekedEvents = SDL.Events.SDL_PeepEvents(events, events.Length, SDL.Events.EventAction.Peek, minType, maxType ?? minType);
        QuackNativeException.ThrowIfNegative(peekedEvents);

        return peekedEvents;
    }

    /// <summary>
    /// Polls pending events from the event queue.
    /// </summary>
    /// <param name="e">The next filled event from the event queue.</param>
    /// <returns><see langword="true"/> if an event was polled; otherwise, <see langword="false"/>.</returns>
    public static bool Poll(out Event e) => SDL.Events.SDL_PollEvent(out e);

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
    public static void Pump() => SDL.Events.SDL_PumpEvents();

    /// <summary>
    /// Add an event to the event queue.
    /// </summary>
    /// <param name="e">The event to push to the event queue.</param>
    /// <returns>
    /// <see langword="true"/> on success, <see langword="false"/> if the event was filtered or on failure.
    /// A common reason for error is the event queue being full.
    /// </returns>
    public static bool Push(ref Event e) => SDL.Events.SDL_PushEvent(ref e);

    /// <summary>
    /// Add events to the back of the event queue.
    /// </summary>
    /// <param name="events">Added events to the event queue.</param>
    /// <returns>The number of events actually added.</returns>
    /// <exception cref="QuackNativeException">Failed while added events.</exception>"
    public static int Push(Span<Event> events)
    {
        int added = SDL.Events.SDL_PeepEvents(events, events.Length, SDL.Events.EventAction.Add, EventType.None, EventType.LastEvent);
        QuackNativeException.ThrowIfNegative(added);

        return added;
    }

    /// <summary>
    /// Retrieve events from the event queue by removing them.
    /// </summary>
    /// <param name="events">Destination buffer for the retrieved events.</param>
    /// <param name="minType">The low end of event type to be retrieved, inclusive.</param>
    /// <param name="maxType">the high end of event type to be retrieved, inclusive. If <see langword="null"/> then minType is used.</param>
    /// <returns>The number of events actually stored.</returns>
    /// <remarks>
    /// You may have to call <see cref="Pump"/> before calling this function. Otherwise, the events may not be ready to be filtered.
    /// </remarks>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="minType"/> is greater than <paramref name="maxType"/>.</exception>
    /// <exception cref="QuackNativeException">Failed while retrieving events.</exception>
    public static int Retrieve(Span<Event> events, EventType minType, EventType? maxType = null)
    {
        ThrowIfTypeGreaterThanMaxType(minType, maxType);

        int retrievedEvents = SDL.Events.SDL_PeepEvents(events, events.Length, SDL.Events.EventAction.Get, minType, maxType ?? minType);
        QuackNativeException.ThrowIfNegative(retrievedEvents);

        return retrievedEvents;
    }

    /// <summary>
    /// Wait until the specified timeout (in milliseconds) for the next available event.
    /// </summary>
    /// <param name="e">The next filled event from the queue.</param>
    /// <param name="timeSpan">The maximum time (milliseconds) to wait for the next available event.</param>
    /// <returns><see langword="true"/> if this got an event or <see langword="false"/> if the timeout elapsed without any events available.</returns>
    /// <remarks>
    /// The timeout is not guaranteed, the actual wait time could be longer due to system scheduling.
    /// </remarks>
    public static bool Wait(out Event e, TimeSpan? timeSpan = null)
    {
        if (timeSpan is null || timeSpan == Timeout.InfiniteTimeSpan)
            return SDL.Events.SDL_WaitEvent(out e);

        return SDL.Events.SDL_WaitEventTimeout(out e, (int)timeSpan.Value.TotalMilliseconds);
    }

    private static void ThrowIfTypeGreaterThanMaxType(EventType type, EventType? maxType)
    {
        if (type > maxType)
            throw new ArgumentOutOfRangeException(nameof(type), type, $"Event type {type} is greater than max type {maxType}.");
    }
}
