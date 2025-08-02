// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.SDL;
using KappaDuck.Quack.Interop.SDL.Handles;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace KappaDuck.Quack.Windows;

public partial class Window
{
    [LibraryImport(SDLNative.Library, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial WindowHandle SDL_CreateWindow(string title, int width, int height, WindowState state);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetWindowID(WindowHandle handle);
}
