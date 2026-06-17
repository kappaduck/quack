// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Core;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Interop.SDL.Marshalling;
using KappaDuck.Quack.Interop.SDL.Primitives;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Provides access to the application's event queue: pumping, polling, waiting,
/// pushing, flushing, inspecting and filtering events.
/// </summary>
public static class EventQueue
{
    static EventQueue() => QuackEngine.EnsureInitialized(Subsystem.Events);

    /// <summary>
    /// Run a specific filter function on the current event queue,
    /// removing any events for which the filter returns <see langword="false"/>.
    /// </summary>
    /// <param name="filter">The predicate to filter events.</param>
    public static void Filter(Predicate<Event> filter)
    {
        SDL3.FilterEvents((_, e) =>
        {
            unsafe
            {
                return filter(EventMarshaller.Convert(*e));
            }
        });
    }

    /// <summary>
    /// Removes all queued <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The <see cref="Event"/> type.</typeparam>
    public static void Flush<T>() where T : IEvent
    {
        (SDL_EventType min, SDL_EventType max) = EventRange.Of<T>();
        SDL3.FlushEvents(min, max);
    }

    /// <summary>
    /// Removes all events from the queue.
    /// </summary>
    public static void Flush() => SDL3.FlushEvents(SDL_EventType.SDL_EVENT_FIRST, SDL_EventType.SDL_EVENT_LAST);

    /// <summary>
    /// Returns whether any <typeparamref name="T"/> is currently queued.
    /// </summary>
    /// <typeparam name="T">The <see cref="Event"/> type.</typeparam>
    public static bool Has<T>() where T : IEvent
    {
        (SDL_EventType min, SDL_EventType max) = EventRange.Of<T>();
        return SDL3.HasEvents(min, max);
    }

    /// <summary>
    /// Peeks at events in the event queue without removing them.
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

        int count = SDL3.PeepEvents(buffer, buffer.Length, SDL_EventAction.Peek, SDL_EventType.SDL_EVENT_FIRST, SDL_EventType.SDL_EVENT_LAST);
        SDLThrowHelper.ThrowIfNegative(count);

        for (int i = 0; i < count; i++)
            events[i] = EventMarshaller.Convert(buffer[i]);

        return count;
    }

    /// <summary>
    /// Polls for the next event in the event queue.
    /// </summary>
    /// <param name="e">The next fetched event from the queue.</param>
    /// <returns><see langword="true"/> if an event was fetched; otherwise, <see langword="false"/>.</returns>
    public static bool Poll(out Event e)
    {
        if (!SDL3.PollEvent(out SDL_Event sdlEvent))
        {
            e = default;
            return false;
        }

        e = EventMarshaller.Convert(sdlEvent);
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
    /// Adds the specified event to the event queue.
    /// </summary>
    /// <param name="e">The event to push onto the queue.</param>
    /// <returns><see langword="true"/> if the event was pushed; otherwise, <see langword="false"/> if the event was filtered or the event queue being full.</returns>
    public static bool Push(Event e)
    {
        if (!EventMarshaller.TryConvert(e, out SDL_Event sdlEvent))
            return false;

        return SDL3.PushEvent(&sdlEvent);
    }

    /// <summary>
    /// Adds the specified events to the event queue.
    /// </summary>
    /// <remarks>
    /// If <paramref name="events"/> is empty, it will return 0.
    /// </remarks>
    /// <param name="events">The events to push onto the queue.</param>
    /// <returns>The number of events successfully pushed onto the queue.</returns>
    /// <exception cref="QuackInteropException">Thrown when failing to push events.</exception>
    public static int Push(ReadOnlySpan<Event> events)
    {
        if (events.IsEmpty)
            return 0;

        Span<SDL_Event> buffer = events.Length <= 32
            ? stackalloc SDL_Event[events.Length]
            : new SDL_Event[events.Length];

        int count = SDL3.PeepEvents(buffer, buffer.Length, SDL_EventAction.Add, SDL_EventType.SDL_EVENT_FIRST, SDL_EventType.SDL_EVENT_LAST);
        SDLThrowHelper.ThrowIfNegative(count);

        return count;
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

        int count = SDL3.PeepEvents(buffer, buffer.Length, SDL_EventAction.Get, SDL_EventType.SDL_EVENT_FIRST, SDL_EventType.SDL_EVENT_LAST);
        SDLThrowHelper.ThrowIfNegative(count);

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
        SDL_Event sdlEvent;

        if (!timeout.HasValue || timeout == Timeout.InfiniteTimeSpan)
        {
            SDL3.WaitEvent(out sdlEvent);
            e = EventMarshaller.Convert(sdlEvent);

            return e.HasValue;
        }

        SDL3.WaitEventTimeout(out sdlEvent, (int)timeout.Value.TotalMilliseconds);
        e = EventMarshaller.Convert(sdlEvent);

        return e.HasValue;
    }
}
