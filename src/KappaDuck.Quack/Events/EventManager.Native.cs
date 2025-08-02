// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.SDL;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace KappaDuck.Quack.Events;

public static partial class EventManager
{
    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_EventEnabled(EventType type);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_FlushEvents(EventType minType, EventType maxType);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_HasEvent(EventType type);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_HasEvents(EventType minType, EventType maxType);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_PeepEvents(Span<Event> events, int count, EventAction action, EventType minType, EventType maxType);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_PollEvent(out Event e);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_PumpEvents();

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_PushEvent(ref Event e);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_SetEventEnabled(EventType type, [MarshalAs(UnmanagedType.U1)] bool enabled);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_WaitEvent(out Event e);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_WaitEventTimeout(out Event e, int timeout);

    private enum EventAction
    {
        Add = 0,
        Peek = 1,
        Get = 2,
    }
}
