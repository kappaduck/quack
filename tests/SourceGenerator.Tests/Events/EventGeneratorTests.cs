// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using SourceGenerator.Tests.Infrastructure;

namespace SourceGenerator.Tests.Events;

internal sealed class EventGeneratorTests
{
    [Test]
    public Task QuackAttributeWithNoConstructorShouldBeAddedToEvent()
    {
        const string source = """
            using KappaDuck.Quack.Events;
            using KappaDuck.Quack.Interop.SDL.Primitives.Events;

            namespace KappaDuck.Quack.Events;

            [QuackEvent(SDL_EventType.Quit)]
            public readonly struct QuitRequestedEvent : IEvent;
            """;

        return EventGeneratorRunner.Verify(source);
    }

    [Test]
    public Task QuackAttributeWithConstructorShouldBeAddedToEvent()
    {
        const string source = """
            using KappaDuck.Quack.Events;
            using KappaDuck.Quack.Interop.SDL.Primitives.Events;

            namespace KappaDuck.Quack.Events;

            [QuackEvent(SDL_EventType.KeyDown)]
            public readonly struct KeyPressedEvent : IEvent
            {
                internal KeyPressedEvent(SDL_KeyboardEvent e) => WindowId = e.WindowId;

                public uint WindowId { get; }
            }
            """;

        return EventGeneratorRunner.Verify(source);
    }

    [Test]
    public Task EventWithoutIEventShouldReportsQuack001()
    {
        const string source = """
            using KappaDuck.Quack.Events;
            using KappaDuck.Quack.Interop.SDL.Primitives.Events;

            namespace KappaDuck.Quack.Events;

            [QuackEvent(SDL_EventType.Quit)]
            public readonly struct QuitRequestedEvent;
            """;

        return EventGeneratorRunner.Verify(source);
    }

    [Test]
    public Task EventWithMultipleConstructorsShouldReportsQuack002()
    {
        const string source = """
            using KappaDuck.Quack.Events;
            using KappaDuck.Quack.Interop.SDL.Primitives.Events;

            namespace KappaDuck.Quack.Events;

            [QuackEvent(SDL_EventType.KeyDown)]
            public readonly struct KeyPressedEvent : IEvent
            {
                internal KeyPressedEvent(SDL_KeyboardEvent e) => WindowId = e.WindowId;
                internal KeyPressedEvent(uint id) => WindowId = id;
                public uint WindowId { get; }
            }
            """;

        return EventGeneratorRunner.Verify(source);
    }

    [Test]
    public Task UnmatchedPayloadTypeShouldReportsQuack003()
    {
        const string source = """
            using KappaDuck.Quack.Events;
            using KappaDuck.Quack.Interop.SDL.Primitives.Events;

            namespace KappaDuck.Quack.Interop.SDL.Primitives.Events
            {
                internal readonly struct SDL_UnknownPayload { }
            }

            namespace KappaDuck.Quack.Events
            {
                [QuackEvent(SDL_EventType.KeyDown)]
                public readonly struct WeirdEvent : IEvent
                {
                    internal WeirdEvent(SDL_UnknownPayload e) { }
                }
            }
            """;

        return EventGeneratorRunner.Verify(source);
    }

    [Test]
    public Task MalformedAttributeIsSkippedWithoutCrashing()
    {
        const string source = """
            using KappaDuck.Quack.Events;

            namespace KappaDuck.Quack.Events;

            [QuackEvent]
            public readonly struct HalfTypedEvent : IEvent;
            """;

        return EventGeneratorRunner.Verify(source);
    }
}
