// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Graphics.Drawing;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL3
{
    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_ComposeCustomBlendMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial uint ComposeCustomBlendMode(BlendFactor source, BlendFactor destination, BlendOperation operation, BlendFactor sourceAlpha, BlendFactor destinationAlpha, BlendOperation alphaOperation);
}
