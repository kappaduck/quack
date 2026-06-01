// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Interop.SDL.Marshalling;

/// <summary>
/// Custom marshaller for a pointer to an array owned by the caller.
/// </summary>
/// <remarks>
/// It will free the allocated array.
/// </remarks>
/// <typeparam name="T">The managed element type</typeparam>
/// <typeparam name="TUnmanaged">The unmanaged element type</typeparam>
[ContiguousCollectionMarshaller]
[CustomMarshaller(typeof(Span<>), MarshalMode.ManagedToUnmanagedOut, typeof(CallerArrayMarshaller<,>))]
internal static class CallerArrayMarshaller<T, TUnmanaged> where TUnmanaged : unmanaged
{
    internal static Span<T> AllocateContainerForManagedElements(TUnmanaged* unmanaged, int length)
        => unmanaged is null ? [] : new T[length];

    internal static ReadOnlySpan<TUnmanaged> GetUnmanagedValuesSource(TUnmanaged* unmanaged, int length)
        => unmanaged is null ? [] : new ReadOnlySpan<TUnmanaged>(unmanaged, length);

    internal static Span<T> GetManagedValuesDestination(Span<T> managed) => managed;

    internal static void Free(TUnmanaged* unmanaged) => SDL3.Free(unmanaged);
}
