// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Core;
using KappaDuck.Quack.Interop.SDL.Primitives.Events;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Provides event management functionalities.
/// </summary>
public static class EventManager
{
    private static Predicate<Event>? _filter;

    static EventManager() => QuackEngine.EnsureInitialized(Subsystem.Events);

    /// <summary>
    /// Removes the global filter installed by <see cref="SetGlobalFilter"/>.
    /// </summary>
    public static void ClearGlobalFilter()
    {
        _filter = null;
        SDL3.SetEventFilter(null, null);
    }

    /// <summary>
    /// Disables <typeparamref name="TEvent"/>
    /// </summary>
    /// <typeparam name="TEvent">The event type to disable.</typeparam>
    public static void Disable<TEvent>() where TEvent : IEvent
        => SDL3.SetEventEnabled(EventType.Of<TEvent>(), enabled: false);

    /// <summary>
    /// Enables <typeparamref name="TEvent"/>
    /// </summary>
    /// <typeparam name="TEvent">The event type to enable.</typeparam>
    public static void Enable<TEvent>() where TEvent : IEvent
        => SDL3.SetEventEnabled(EventType.Of<TEvent>(), enabled: true);

    /// <summary>
    /// Installs a global filter invoked for every event before it is added to the queue. Returning
    /// <see langword="false"/> from the filter drops the event.
    /// </summary>
    /// <param name="filter">
    /// A predicate evaluated for each incoming event. Return <see langword="true"/> to keep the event
    /// or <see langword="false"/> to drop it. Only one global filter can be active at a time; calling
    /// this replaces any previous one.
    /// </param>
    /// <remarks>
    /// <para>
    /// Use a filter only when you need to drop events. To observe events without affecting whether
    /// they are queued, use <see cref="Watch"/> instead.
    /// </para>
    /// <para>
    /// Returning <see langword="false"/> removes the event from the queue but does not undo it: any
    /// internal state the event carries is still applied — a dropped resize event, for example, still
    /// updates the window's recorded size. This lets you selectively drop events as they arrive.
    /// </para>
    /// <para>
    /// Only events that would enter the queue are filtered. Events you <see cref="EventQueue.Push(Event)"/>
    /// pass through the filter; events disabled with <see cref="Disable{T}"/> never reach it.
    /// </para>
    /// <para>
    /// The filter may run on a background thread, so keep it fast and thread-safe. The exception is
    /// window-exposed events, which are always delivered on the main thread so you can redraw in
    /// response to them.
    /// </para>
    /// </remarks>
    public static void SetGlobalFilter(Predicate<Event> filter)
    {
        _filter = filter;
        SDL3.SetEventFilter(&OnFilter);
    }

    /// <summary>
    /// Creates an event watcher that invokes the specified callback function whenever an event is added to the event queue.
    /// </summary>
    /// <param name="callback">The callback function to be invoked when an event is added.</param>
    public static EventWatcher Watch(Action<Event> callback)
    {
        return new EventWatcher((_, e) =>
        {
            unsafe
            {
                callback(EventType.Convert(*e));
            }

            return true;
        });
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte OnFilter(void* data, SDL_Event* e)
    {
        Predicate<Event>? filter = _filter;
        if (filter is null)
            return 1;

        unsafe
        {
            SDL_Event native = *e;
            Event evt = EventType.Convert(native);
            return filter(evt) ? (byte)1 : (byte)0;
        }
    }
}
