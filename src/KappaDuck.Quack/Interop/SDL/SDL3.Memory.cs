// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL3
{
    /// <summary>
    /// Free the allocated memory.
    /// </summary>
    /// <remarks>
    /// If <paramref name="memory"/> is <see langword="null"/>, this function does nothing.
    /// </remarks>
    /// <typeparam name="TUnmanaged">The unmanaged element type.</typeparam>
    /// <param name="memory">A pointer to the allocated memory</param>
    internal static void Free<TUnmanaged>(TUnmanaged* memory) where TUnmanaged : unmanaged
    {
        if (memory is null)
            return;

        SDL_free(memory);
    }

    /// <summary>
    /// Free the allocated memory
    /// </summary>
    /// <remarks>
    /// If <paramref name="memory"/> is <see langword="null"/>, this function does nothing.
    /// </remarks>
    /// <typeparam name="TUnmanaged">The unmanaged element type.</typeparam>
    /// <param name="memory">A pointer to the allocated memory</param>
    internal static void Free<TUnmanaged>(TUnmanaged** memory) where TUnmanaged : unmanaged
    {
        if (memory is null)
            return;

        SDL_free(memory);
    }

    [LibraryImport(nameof(SDL3))]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_free(void* memory);
}
