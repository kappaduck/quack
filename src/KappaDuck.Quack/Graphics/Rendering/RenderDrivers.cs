// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using System.Collections.Immutable;

namespace KappaDuck.Quack.Graphics.Rendering;

/// <summary>
/// A collection of available rendering drivers.
/// </summary>
/// <remarks>
/// A render driver is a set of code that handles rendering and texture management on a particular display.
/// Normally there is only one, but some drivers may have several available with different capabilities.
/// </remarks>
public static class RenderDrivers
{
    private static readonly Lazy<ImmutableArray<string>> _drivers = new(GetAll);

    /// <summary>
    /// Gets all built-in rendering driver names.
    /// </summary>
    public static IReadOnlyCollection<string> All => _drivers.Value;

    private static ImmutableArray<string> GetAll()
    {
        ImmutableArray<string>.Builder builder = ImmutableArray.CreateBuilder<string>(SDL3.GetNumRenderDrivers());

        for (int i = 0; i < builder.Capacity; i++)
            builder.Add(SDL3.GetRenderDriver(i));

        return builder.MoveToImmutable();
    }
}
