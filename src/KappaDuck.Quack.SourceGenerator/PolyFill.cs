// Copyright (c) KappaDuck.
// Licensed under the MIT license.

#if NETSTANDARD2_0
#pragma warning disable IDE0130

using System.ComponentModel;

namespace System.Runtime.CompilerServices
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    internal static class IsExternalInit;
}

namespace System
{
    internal readonly struct Index : IEquatable<Index>
    {
        private readonly int _value;

        public Index(int value, bool fromEnd = false)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Non-negative number required.");

            _value = fromEnd ? ~value : value;
        }

        public static Index Start => new(0);

        public static Index End => new(~0);

        public static Index FromStart(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Non-negative number required.");

            return new Index(value);
        }

        public static Index FromEnd(int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Non-negative number required.");

            return new Index(~value);
        }

        public int Value => _value < 0 ? ~_value : _value;

        public bool IsFromEnd => _value < 0;

        public int GetOffset(int length)
        {
            int offset = _value;

            if (IsFromEnd)
                offset += length + 1;

            return offset;
        }

        public static implicit operator Index(int value) => FromStart(value);

        public bool Equals(Index other) => _value == other._value;

        public override bool Equals(object? obj) => obj is Index other && _value == other._value;

        public override int GetHashCode() => _value;

        public override string ToString() => IsFromEnd ? "^" + (uint)Value : ((uint)Value).ToString();
    }

    internal readonly struct Range(Index start, Index end) : IEquatable<Range>
    {
        public Index Start { get; } = start;

        public Index End { get; } = end;

        public static Range StartAt(Index start) => new(start, Index.End);

        public static Range EndAt(Index end) => new(Index.Start, end);

        public static Range All => new(Index.Start, Index.End);

        public bool Equals(Range other) => other.Start.Equals(Start) && other.End.Equals(End);

        public override bool Equals(object? obj) => obj is Range other && other.Start.Equals(Start) && other.End.Equals(End);

        public override int GetHashCode() => (Start.GetHashCode() * 31) + End.GetHashCode();

        public override string ToString() => Start + ".." + End;

        public (int Offset, int Length) GetOffsetAndLength(int length)
        {
            int indexStart = Start.IsFromEnd ? length - Start.Value : Start.Value;
            int indexEnd = End.IsFromEnd ? length - End.Value : End.Value;

            if ((uint)indexEnd > (uint)length || (uint)indexStart > (uint)indexEnd)
                throw new ArgumentOutOfRangeException(nameof(length));

            return (indexStart, indexEnd - indexStart);
        }
    }
}

#pragma warning restore IDE0130
#endif
