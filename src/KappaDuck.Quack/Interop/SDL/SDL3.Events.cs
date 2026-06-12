// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Interop.SDL.Primitives;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL3
{
    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_AddEventWatch")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static partial bool AddEventWatch(EventFilter callback, nint data = default);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_PumpEvents")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void PumpEvents();

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_PollEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool PollEvent(out SDL_Event e);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_WaitEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool WaitEvent(out SDL_Event e);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_WaitEventTimeout")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool WaitEventTimeout(out SDL_Event e, int timeout);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_PushEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool PushEvent(SDL_Event* e);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_FlushEvents")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void FlushEvents(SDL_EventType minType, SDL_EventType maxType);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_HasEvents")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool HasEvents(SDL_EventType minType, SDL_EventType maxType);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_PeepEvents")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int PeepEvents(Span<SDL_Event> events, int numevents, SDL_EventAction action, SDL_EventType minType, SDL_EventType maxType);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetEventFilter")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetEventFilter(EventFilter filter, void* data = null);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_FilterEvents")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void FilterEvents(EventFilter filter, void* data = null);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_RemoveEventWatch")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void RemoveEventWatch(EventFilter filter, void* data = null);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.I1)]
    internal delegate bool EventFilter(void* data, SDL_Event* e);
}
