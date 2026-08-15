// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using System.Collections.Immutable;

namespace KappaDuck.Quack.SourceGenerator;

internal readonly struct EquatableArray<T>(ImmutableArray<T> array) : IEquatable<EquatableArray<T>> where T : IEquatable<T>
{
    public static readonly EquatableArray<T> Empty = new([]);

    public int Count => array.IsDefault ? 0 : array.Length;

    public T this[int index] => array[index];

    public bool Equals(EquatableArray<T> other)
    {
        ImmutableArray<T> left = AsImmutableArray();
        ImmutableArray<T> right = other.AsImmutableArray();

        if (left.Length != right.Length)
            return false;

        for (int i = 0; i < left.Length; i++)
        {
            if (!EqualityComparer<T>.Default.Equals(left[i], right[i]))
                return false;
        }

        return true;
    }

    public override bool Equals(object? obj) => obj is EquatableArray<T> other && Equals(other);

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;

            foreach (T item in AsImmutableArray().AsSpan())
                hash = (hash * 31) + (item?.GetHashCode() ?? 0);

            return hash;
        }
    }

    public ReadOnlySpan<T>.Enumerator GetEnumerator() => AsImmutableArray().AsSpan().GetEnumerator();

    private ImmutableArray<T> AsImmutableArray() => array.IsDefault ? [] : array;
}
