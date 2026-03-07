// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Events.Extensions;

/// <summary>
/// Provides extension methods for the <see cref="EventType"/>.
/// </summary>
public static class EventTypeExtensions
{
    extension(EventType type)
    {
        /// <summary>
        /// Gets a value indicating whether the specified event type is enabled in the event loop.
        /// </summary>
        public bool Enabled => SDL3.Events.EventEnabled(type);
    }

    extension(EventType)
    {
        /// <summary>
        /// Disables the specified event type.
        /// </summary>
        /// <param name="type">The event type to disable.</param>
        public static void Disable(EventType type) => SDL3.Events.SetEventEnabled(type, enabled: false);

        /// <summary>
        /// Enables the specified event type.
        /// </summary>
        /// <param name="type">The event type to enable.</param>
        public static void Enable(EventType type) => SDL3.Events.SetEventEnabled(type, enabled: true);

        /// <summary>
        /// Clears all specified events from the event queue.
        /// </summary>
        /// <param name="first">The first event type to flush.</param>
        /// <param name="last">The last event type to flush. If <see langword="null"/>, only the <paramref name="first"/> event type is flushed.</param>
        /// <remarks>
        /// This will unconditionally remove events from the event queue based on the specified range, inclusive.
        /// It's also normal to just ignore events you don't need in your event loop without flushing them.
        /// This function only affects currently queued events. If you want to make sure that all pending OS events are flushed,
        /// call <see cref="EventManager.Pump"/> on the main thread immediately before flushing.
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="first"/> is greater than <paramref name="last"/>.</exception>
        public static void Flush(EventType first, EventType? last = null)
        {
            ThrowIfInvalidRange(first, last);
            SDL3.Events.FlushEvents(first, last ?? first);
        }

        /// <summary>
        /// Determines whether the specified event type is in the event queue.
        /// </summary>
        /// <param name="type">The event type to check.</param>
        /// <returns><see langword="true"/> if the event type is in the queue; otherwise, <see langword="false"/>.</returns>
        public static bool Has(EventType type) => SDL3.Events.HasEvent(type);

        /// <summary>
        /// Determines whether any events in the specified range (inclusive) are in the event queue.
        /// </summary>
        /// <param name="first">The first event type to check.</param>
        /// <param name="last">The last event type to check.</param>
        /// <returns><see langword="true"/> if any events in the specified range are in the queue; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="first"/> is greater than <paramref name="last"/>.</exception>
        public static bool Has(EventType first, EventType last)
        {
            ThrowIfInvalidRange(first, last);
            return SDL3.Events.HasEvents(first, last);
        }

        private static void ThrowIfInvalidRange(EventType min, EventType? max)
        {
            if (max is null)
                return;

            ArgumentOutOfRangeException.ThrowIfGreaterThan((int)min, (int)max);
        }
    }
}
