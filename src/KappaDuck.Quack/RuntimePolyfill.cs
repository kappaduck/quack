// Copyright (c) KappaDuck.
// Licensed under the MIT license.

#if!NET11_0_OR_GREATER
#pragma warning disable IDE0130, CS1591
namespace System.Runtime.CompilerServices;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
public sealed class UnionAttribute : Attribute;

public interface IUnion
{
    object? Value { get; }
}
#pragma warning restore IDE0130, CS1591
#endif
