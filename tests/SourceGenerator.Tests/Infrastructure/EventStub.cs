// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace SourceGenerator.Tests.Infrastructure;

internal static class EventStub
{
    public const string Source = """
        using System;
        using System.Runtime.CompilerServices;

        namespace System.Runtime.CompilerServices
        {
            [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
            public sealed class UnionAttribute : Attribute { }

            public interface IUnion
            {
                object? Value { get; }
            }
        }

        namespace KappaDuck.Quack.Events
        {
            public interface IEvent { }

            [AttributeUsage(AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
            internal sealed class QuackEventAttribute : Attribute
            {
                public QuackEventAttribute(KappaDuck.Quack.Interop.SDL.Primitives.Events.SDL_EventType type) => Type = type;

                public KappaDuck.Quack.Interop.SDL.Primitives.Events.SDL_EventType Type { get; }

                public string? NativeField { get; set; }
            }

            [Union]
            public readonly partial struct Event : IUnion;

            internal static partial class EventMapper
            {
                internal const KappaDuck.Quack.Interop.SDL.Primitives.Events.SDL_EventType None = 0;
                internal const KappaDuck.Quack.Interop.SDL.Primitives.Events.SDL_EventType End = (KappaDuck.Quack.Interop.SDL.Primitives.Events.SDL_EventType)65535;
            }
        }

        namespace KappaDuck.Quack.Interop.SDL.Primitives.Events
        {
            internal enum SDL_EventType : uint
            {
                None = 0,
                Quit = 0x100,
                KeyDown = 0x300,
                KeyUp = 0x301,
                MouseMotion = 0x400,
                WindowResized = 0x202,
                ClipboardUpdate = 0x900,
            }

            internal readonly struct SDL_KeyboardEvent
            {
                public uint WindowId { get; }
            }

            internal readonly struct SDL_MouseMotionEvent
            {
                public uint WindowId { get; }
            }

            internal readonly struct SDL_WindowEvent
            {
                public uint WindowId { get; }
                public int Data1 { get; }
                public int Data2 { get; }
            }

            internal readonly struct SDL_Event
            {
                public SDL_EventType Type { get; }
                public SDL_KeyboardEvent Keyboard { get; }
                public SDL_MouseMotionEvent Motion { get; }
                public SDL_WindowEvent Window { get; }
            }
        }
        """;
}
