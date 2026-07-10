// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace KappaDuck.Quack.SourceGenerator;

internal sealed record LocationInfo(string FilePath, TextSpan TextSpan, LinePositionSpan LineSpan)
{
    public Location ToLocation() => Location.Create(FilePath, TextSpan, LineSpan);

    public static LocationInfo? CreateFrom(ISymbol symbol) => CreateFrom(symbol.Locations.FirstOrDefault());

    public static LocationInfo? CreateFrom(Location? location)
    {
        if (location?.SourceTree is null)
            return null;

        return new LocationInfo(location.SourceTree.FilePath, location.SourceSpan, location.GetLineSpan().Span);
    }
}
