// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Events;
using KappaDuck.Quack.Interop.SDL.Primitives;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL3
{
    internal static partial class Events
    {
        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_AddEventWatch"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool AddEventWatch(EventFilter callback, nint data = default);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_EventEnabled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool EventEnabled(EventType type);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_FlushEvents"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial void FlushEvents(EventType min, EventType max);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_HasEvent"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool HasEvent(EventType type);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_HasEvents"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool HasEvents(EventType min, EventType max);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_PeepEvents"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial int PeepEvents(Span<Event> events, int count, EventAction action, EventType min, EventType max);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_PeepEvents"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial int PeepEvents(Span<Event> events, int count, EventAction action, uint min, uint max);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_PollEvent"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool PollEvent(out Event e);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_PumpEvents"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial void PumpEvents();

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_PushEvent"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool PushEvent(ref Event e);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_RemoveEventWatch"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool RemoveEventWatch(EventFilter callback, nint data = default);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetEventEnabled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial void SetEventEnabled(EventType type, [MarshalAs(UnmanagedType.U1)] bool enabled);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_WaitEvent"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool WaitEvent(out Event e);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_WaitEventTimeout"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool WaitEventTimeout(out Event e, int timeout);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal delegate bool EventFilter(nint userdata, ref Event e);
    }
}
