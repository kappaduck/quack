// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Interop.SDL.Marshalling;

/// <summary>
/// Custom marshaller for a pointer to an array owned by the caller.
/// </summary>
/// <typeparam name="T">The managed element type.</typeparam>
/// <typeparam name="TUnmanaged">The unmanaged element type.</typeparam>
[ContiguousCollectionMarshaller]
[CustomMarshaller(typeof(Span<>), MarshalMode.ManagedToUnmanagedOut, typeof(CallerArrayMarshaller<,>))]
internal static unsafe class CallerArrayMarshaller<T, TUnmanaged> where TUnmanaged : unmanaged
{
    internal static Span<T> AllocateContainerForManagedElements(TUnmanaged* unmanaged, int length)
        => unmanaged is null ? [] : new T[length];

    internal static ReadOnlySpan<TUnmanaged> GetUnmanagedValuesSource(TUnmanaged* unmanaged, int length)
        => unmanaged is null ? [] : new ReadOnlySpan<TUnmanaged>(unmanaged, length);

    internal static Span<T> GetManagedValuesDestination(Span<T> managed) => managed;

    internal static void Free(TUnmanaged* unmanaged) => SDL3.Memory.Free(unmanaged);
}
