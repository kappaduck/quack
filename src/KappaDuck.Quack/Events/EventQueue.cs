// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Core;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Interop.SDL.Primitives.Events;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Provides event queue functionalities to manage the application's event queue.
/// </summary>
public static class EventQueue
{
    static EventQueue() => QuackEngine.EnsureInitialized(Subsystem.Events);

    /// <summary>
    /// Determines whether the <typeparamref name="TEvent"/> is in the event queue.
    /// </summary>
    /// <typeparam name="TEvent">The event type.</typeparam>
    /// <returns><see langword="true"/> if the event type is in the event queue otherwise <see langword="false"/></returns>
    public static bool Contains<TEvent>() where TEvent : IEvent
        => SDL3.HasEvent(EventType.Of<TEvent>());

    /// <summary>
    /// Removes all queued <typeparamref name="TEvent"/>.
    /// </summary>
    /// <typeparam name="TEvent">The event type.</typeparam>
    public static void Flush<TEvent>() where TEvent : IEvent
        => SDL3.FlushEvent(EventType.Of<TEvent>());

    /// <summary>
    /// Removes all events from the queue.
    /// </summary>
    public static void Flush() => SDL3.FlushEvents(EventType.None, EventType.End);

    /// <summary>
    /// Peeks events in the event queue without removing them.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If <paramref name="events"/> is empty, it will return 0.
    /// </para>
    /// <para>
    /// You may have to call <see cref="Pump"/> before peeking to ensure that the events are ready to be filtered.
    /// </para>
    /// </remarks>
    /// <param name="events">The buffer to store the peeked events at the front of event queue.</param>
    /// <returns>The number of events peeked.</returns>
    /// <exception cref="QuackInteropException">Thrown when failing to peek events.</exception>
    public static int Peek(Span<Event> events)
    {
        if (events.IsEmpty)
            return 0;

        Span<SDL_Event> buffer = events.Length <= 32
            ? stackalloc SDL_Event[events.Length]
            : new SDL_Event[events.Length];

        int count = SDL3.PeepEvents(buffer, buffer.Length, SDL_EventAction.Peek, EventType.None, EventType.End);
        SDLThrowHelper.ThrowIfNegative(count);

        for (int i = 0; i < count; i++)
            events[i] = EventType.Convert(in buffer[i]);

        return count;
    }

    /// <summary>
    /// Peeks events based on an event type in the event queue without removing them.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If <paramref name="events"/> is empty, it will return 0.
    /// </para>
    /// <para>
    /// You may have to call <see cref="Pump"/> before peeking to ensure that the events are ready to be filtered.
    /// </para>
    /// </remarks>
    /// <typeparam name="TEvent">The event type to peek.</typeparam>
    /// <param name="events">The buffer to store the peeked events at the front of event queue.</param>
    /// <returns>The number of events peeked.</returns>
    /// <exception cref="QuackInteropException">Thrown when failing to peek events.</exception>
    public static int Peek<TEvent>(Span<TEvent> events) where TEvent : IEvent
    {
        if (events.IsEmpty)
            return 0;

        Span<SDL_Event> buffer = events.Length <= 32
            ? stackalloc SDL_Event[events.Length]
            : new SDL_Event[events.Length];

        SDL_EventType type = EventType.Of<TEvent>();

        int count = SDL3.PeepEvents(buffer, buffer.Length, SDL_EventAction.Peek, type, type);
        SDLThrowHelper.ThrowIfNegative(count);

        for (int i = 0; i < count; i++)
            events[i] = (TEvent)EventType.Convert(in buffer[i]).Value!;

        return count;
    }

    /// <summary>
    /// Polls for the next event in the event queue.
    /// </summary>
    /// <param name="e">The next fetched event from the queue.</param>
    /// <returns><see langword="true"/> if an event was fetched; otherwise, <see langword="false"/>.</returns>
    public static bool Poll(out Event e)
    {
        if (!SDL3.PollEvent(out SDL_Event native))
        {
            MainThreadDispatcher.Drain();

            e = default;
            return false;
        }

        e = EventType.Convert(in native);
        return true;
    }

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
    public static void Pump() => SDL3.PumpEvents();

    /// <summary>
    /// Runs <paramref name="match"/> over the events currently in the queue, keeping those for
    /// which it returns <see langword="true"/> and removing the rest.
    /// </summary>
    /// <param name="match">
    /// A predicate evaluated once for each queued event. Return <see langword="true"/> to keep the
    /// event or <see langword="false"/> to remove it from the queue.
    /// </param>
    /// <remarks>
    /// This is a single pass over the events already in the queue; it has no effect on events that
    /// arrive afterward. Use <see cref="EventManager.SetGlobalFilter"/> to filter events as they arrive.
    /// </remarks>
    public static void Retain(Predicate<Event> match)
    {
        SDL3.FilterEvents((_, e) =>
        {
            unsafe
            {
                return match(EventType.Convert(in *e));
            }
        });
    }

    /// <summary>
    /// Retrieves events from the event queue and removes them.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If <paramref name="events"/> is empty, it will return 0.
    /// </para>
    /// <para>
    /// You may have to call <see cref="Pump"/> before retrieving to ensure that the events are ready to be filtered.
    /// </para>
    /// </remarks>
    /// <param name="events">The buffer to store the retrieved events from the front of event queue.</param>
    /// <returns>The number of events retrieved.</returns>
    /// <exception cref="QuackInteropException">Thrown when failing to retrieve events.</exception>
    public static int Retrieve(Span<Event> events)
    {
        if (events.IsEmpty)
            return 0;

        Span<SDL_Event> buffer = events.Length <= 32
            ? stackalloc SDL_Event[events.Length]
            : new SDL_Event[events.Length];

        int count = SDL3.PeepEvents(buffer, buffer.Length, SDL_EventAction.Get, EventType.None, EventType.End);
        SDLThrowHelper.ThrowIfNegative(count);

        for (int i = 0; i < count; i++)
            events[i] = EventType.Convert(in buffer[i]);

        return count;
    }

    /// <summary>
    /// Retrieves events based on an event type from the event queue and removes them.
    /// </summary>
    /// <remarks>
    /// <para>
    /// If <paramref name="events"/> is empty, it will return 0.
    /// </para>
    /// <para>
    /// You may have to call <see cref="Pump"/> before retrieving to ensure that the events are ready to be filtered.
    /// </para>
    /// </remarks>
    /// <typeparam name="TEvent">The event type to peek.</typeparam>
    /// <param name="events">The buffer to store the retrieved events from the front of event queue.</param>
    /// <returns>The number of events retrieved.</returns>
    /// <exception cref="QuackInteropException">Thrown when failing to retrieve events.</exception>
    public static int Retrieve<TEvent>(Span<TEvent> events) where TEvent : IEvent
    {
        if (events.IsEmpty)
            return 0;

        Span<SDL_Event> buffer = events.Length <= 32
            ? stackalloc SDL_Event[events.Length]
            : new SDL_Event[events.Length];

        SDL_EventType type = EventType.Of<TEvent>();

        int count = SDL3.PeepEvents(buffer, buffer.Length, SDL_EventAction.Get, type, type);
        SDLThrowHelper.ThrowIfNegative(count);

        for (int i = 0; i < count; i++)
            events[i] = (TEvent)EventType.Convert(in buffer[i]).Value!;

        return count;
    }

    /// <summary>
    /// Waits indefinitely or up to the specified timeout for the next event in the event queue.
    /// </summary>
    /// <remarks>
    /// The timeout is not guaranteed to be precise. The actual wait time may be longer than the specified timeout.
    /// </remarks>
    /// <param name="e">The next fetched event from the queue.</param>
    /// <param name="timeout">The maximum time to wait for an event. If <see langword="null"/>, waits indefinitely.</param>
    /// <returns><see langword="true"/> if an event was fetched; otherwise, <see langword="false"/> if the timeout elapsed without any events available.</returns>
    public static bool Wait(out Event e, TimeSpan? timeout = null)
    {
        SDL_Event native;

        if (!timeout.HasValue || timeout == Timeout.InfiniteTimeSpan)
        {
            SDL3.WaitEvent(out native);
            e = EventType.Convert(in native);

            return e.HasValue;
        }

        SDL3.WaitEventTimeout(out native, (int)timeout.Value.TotalMilliseconds);
        e = EventType.Convert(in native);

        return e.HasValue;
    }
}
