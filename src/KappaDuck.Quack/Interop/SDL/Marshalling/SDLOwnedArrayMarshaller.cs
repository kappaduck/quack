// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Runtime.InteropServices.Marshalling;

namespace KappaDuck.Quack.Interop.SDL.Marshalling;

/// <summary>
/// Custom marshaller for a pointer to an array that is owned by SDL, and it is freed automatically by SDL.
/// It helps to marshal a pointer to an array into a managed span which is safe to use.
/// </summary>
/// <typeparam name="T">The type of the elements in the array.</typeparam>
/// <typeparam name="TUnmanaged">The type of the elements in the unmanaged array.</typeparam>
[ContiguousCollectionMarshaller]
[CustomMarshaller(typeof(Span<>), MarshalMode.Default, typeof(SDLOwnedArrayMarshaller<,>))]
internal static unsafe class SDLOwnedArrayMarshaller<T, TUnmanaged> where TUnmanaged : unmanaged
{
    internal static TUnmanaged* AllocateContainerForUnmanagedElements(Span<T> managed, out int length)
        => throw new NotSupportedException("We do not support allocating unmanaged arrays for SDL.");

    internal static Span<T> AllocateContainerForManagedElements(TUnmanaged* unmanaged, int length)
        => unmanaged is null ? Span<T>.Empty : new T[length];

    internal static ReadOnlySpan<T> GetManagedValuesSource(Span<T> managed) => managed;

    internal static Span<TUnmanaged> GetUnmanagedValuesDestination(TUnmanaged* unmanaged, int length)
        => new(unmanaged, length);

    internal static ReadOnlySpan<TUnmanaged> GetUnmanagedValuesSource(TUnmanaged* unmanaged, int length)
        => new(unmanaged, length);

    internal static Span<T> GetManagedValuesDestination(Span<T> managed) => managed;
}
