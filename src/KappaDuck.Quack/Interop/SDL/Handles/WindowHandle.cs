// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.Handles;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace KappaDuck.Quack.Interop.SDL.Handles;

/// <summary>
/// Represents a safe handle to a window.
/// </summary>
public sealed partial class WindowHandle : SafeHandleZeroInvalid
{
    /// <summary>
    /// Marshaller needs a public parameterless constructor.
    /// </summary>
    public WindowHandle() : base(ownsHandle: true)
    {
    }

    private WindowHandle(nint handle, bool ownsHandle) : base(ownsHandle)
         => SetHandle(handle);

    /// <summary>
    /// Converts this handle to a non-owning handle.
    /// </summary>
    /// <returns>The non-owning handle.</returns>
    public WindowHandle ToNonOwningHandle() => new(handle, ownsHandle: false);

    /// <inheritdoc/>
    protected override bool ReleaseHandle()
    {
        SDL_DestroyWindow(handle);

        SetHandle(nint.Zero);
        SetHandleAsInvalid();

        return true;
    }

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyWindow(nint window);
}
