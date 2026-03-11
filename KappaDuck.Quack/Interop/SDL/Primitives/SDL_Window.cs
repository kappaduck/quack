// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.Handles;

namespace KappaDuck.Quack.Interop.SDL.Primitives;

internal sealed partial class SDL_Window : SafeHandleZeroInvalid
{
    public SDL_Window() : base(ownsHandle: true)
    {
    }

    private SDL_Window(nint handle, bool ownsHandle) : base(handle, ownsHandle)
    {
    }

    internal static SDL_Window Zero { get; } = new();

    internal SDL_Window ToNonOwningHandle() => new(handle: handle, ownsHandle: false);

    protected override bool ReleaseHandle()
    {
        SDL_DestroyWindow(handle);
        return true;
    }

    [LibraryImport(nameof(SDL3)), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    private static partial void SDL_DestroyWindow(nint window);
}
