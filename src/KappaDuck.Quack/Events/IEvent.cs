// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Events;

/// <summary>
/// Marker interface implemented by every event type, such as <see cref="QuitRequestedEvent"/>.
/// It has no members; it exists so the type-safe event operations can restrict their generic
/// parameter to event types.
/// </summary>
/// <remarks>
/// The <c>where T : IEvent</c> constraint is what lets methods like
/// <see cref="EventQueue.Flush{T}"/> and <see cref="EventManager.Enable{TEvent}"/> target a
/// single event type at compile time without naming a raw event-type value.
/// </remarks>
public interface IEvent;
