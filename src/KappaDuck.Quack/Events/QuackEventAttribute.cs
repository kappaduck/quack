// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Interop.SDL.Primitives.Events;

namespace KappaDuck.Quack.Events;

/// <summary>
/// Marks an <see cref="IEvent"/> as a built-in engine event backed by a native <see cref="SDL_EventType"/>.
/// </summary>
/// <remarks>
/// <para>
/// <c>KappaDuck.Quack.SourceGenerator</c> looks for this attribute at compile time to wire the decorated
/// type into <see cref="Event"/> and <see cref="EventMapper"/>. Adding a new built-in event only
/// requires a new type decorated with this attribute; <see cref="Event"/> and <see cref="EventMapper"/>
/// pick it up automatically and nothing else needs to change.
/// </para>
/// <para>
/// The decorated type must implement <see cref="IEvent"/> and declare either no constructor (a
/// marker event with no payload, e.g. <see cref="QuitRequestedEvent"/>) or exactly one constructor
/// with a single native SDL payload parameter (e.g. <see cref="WindowResizedEvent"/> taking a
/// <c>SDL_WindowEvent</c>). The generator resolves which field of <see cref="SDL_Event"/> to read by
/// matching that parameter's type against <see cref="SDL_Event"/>'s fields; set <see cref="NativeField"/>
/// only if that match is ambiguous or fails.
/// </para>
/// </remarks>
/// <param name="type">The native SDL event type this event maps to.</param>
[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
internal sealed class QuackEventAttribute(SDL_EventType type) : Attribute
{
    /// <summary>
    /// Gets the native SDL event type this event maps to.
    /// </summary>
    public SDL_EventType Type { get; } = type;

    /// <summary>
    /// Gets or sets the name of the <see cref="SDL_Event"/> field to pass to the constructor.
    /// Only needed when the automatic match by parameter type is ambiguous or fails.
    /// </summary>
    public string? NativeField { get; set; }
}
