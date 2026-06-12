// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Events;

/// <summary>
/// Marker interface implemented by every event type, enabling type-safe queue
/// operations such as <see cref="EventQueue.Has{T}"/> and <see cref="EventQueue.Flush{T}"/>.
/// </summary>
public interface IEvent;
