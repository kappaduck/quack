// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using Microsoft.CodeAnalysis;
using SourceGenerator.Tests.Infrastructure;

namespace SourceGenerator.Tests.Events;

internal sealed class EventGeneratorPipelineTests
{
    [Test]
    public async Task GeneratedCodeCompilesWithoutErrors()
    {
        const string source = """
            using KappaDuck.Quack.Events;
            using KappaDuck.Quack.Interop.SDL.Primitives.Events;

            namespace KappaDuck.Quack.Events;

            [QuackEvent(SDL_EventType.Quit)]
            public readonly struct QuitRequestedEvent : IEvent;

            [QuackEvent(SDL_EventType.KeyDown, NativeField = "Keyboard")]
            public readonly struct KeyPressedEvent : IEvent
            {
                internal KeyPressedEvent(SDL_KeyboardEvent e) => WindowId = e.WindowId;
                public uint WindowId { get; }
            }
            """;

        GeneratorResult result = EventGeneratorRunner.Run(source);

        await result.CompilationDiagnostics.Should().BeEmpty();
    }

    [Test]
    public async Task OutputIsDeterministicRegardlessOfDeclarationOrder()
    {
        const string ordered = """
            using KappaDuck.Quack.Events;
            using KappaDuck.Quack.Interop.SDL.Primitives.Events;
            namespace KappaDuck.Quack.Events;
            [QuackEvent(SDL_EventType.Quit)] public readonly struct QuitRequestedEvent : IEvent;
            [QuackEvent(SDL_EventType.ClipboardUpdate)] public readonly struct ClipboardUpdatedEvent : IEvent;
            """;

        const string reversed = """
            using KappaDuck.Quack.Events;
            using KappaDuck.Quack.Interop.SDL.Primitives.Events;
            namespace KappaDuck.Quack.Events;
            [QuackEvent(SDL_EventType.ClipboardUpdate)] public readonly struct ClipboardUpdatedEvent : IEvent;
            [QuackEvent(SDL_EventType.Quit)] public readonly struct QuitRequestedEvent : IEvent;
            """;

        GeneratorResult first = EventGeneratorRunner.Run(ordered);
        GeneratorResult second = EventGeneratorRunner.Run(reversed);

        await first.EventUnion.Should().BeEqualTo(second.EventUnion);
        await first.EventTypeMap.Should().BeEqualTo(second.EventTypeMap);
    }

    [Test]
    public async Task PipelineOutputsAreCachedOnReRun()
    {
        const string source = """
            using KappaDuck.Quack.Events;
            using KappaDuck.Quack.Interop.SDL.Primitives.Events;

            namespace KappaDuck.Quack.Events;

            [QuackEvent(SDL_EventType.Quit)]
            public readonly struct QuitRequestedEvent : IEvent;

            [QuackEvent(SDL_EventType.KeyDown, NativeField = "Keyboard")]
            public readonly struct KeyPressedEvent : IEvent
            {
                internal KeyPressedEvent(SDL_KeyboardEvent e) => WindowId = e.WindowId;
                public uint WindowId { get; }
            }
            """;

        IEnumerable<IReadOnlyList<IncrementalStepRunReason>> stages = EventGeneratorRunner.RunAndTrack(source, "events", "combined")
                                                                                          .Select(s => s.Value);

        foreach (IReadOnlyList<IncrementalStepRunReason> stage in stages)
        {
            await stage.Should().NotBeEmpty();
            await stage.Should().All(static reason => reason is IncrementalStepRunReason.Cached or IncrementalStepRunReason.Unchanged);
        }
    }
}
