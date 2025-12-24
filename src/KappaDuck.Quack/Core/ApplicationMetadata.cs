// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Core;

/// <summary>
/// Represents metadata about the application.
/// </summary>
/// <remarks>
/// <para>
/// This is not required, but strongly recommended for better identification of the application.
/// </para>
/// <para>
/// There are several locations where this metadata can be useful,
/// such as "About" in the menu, the name of the application can be shown on some audio mixers, etc.
/// </para>
/// <para>
/// It is managed by <see cref="QuackEngine.SetMetadata(ApplicationMetadata)"/>. The engine will keep a reference to it.
/// </para>
/// </remarks>
public sealed record ApplicationMetadata
{
    /// <summary>
    /// Gets the reverse domain identifier of the application, e.g. com.kappaduck.quack.demo.
    /// </summary>
    /// <remarks>
    /// This is used by desktop compositors to identify and group windows together, as
    /// well as match applications with associated desktop settings and icons.
    /// </remarks>
    public string? Id { get; init; }

    /// <summary>
    /// Gets the human-readable name of the application, e.g. Quack! Playground.
    /// </summary>
    /// <remarks>
    /// This will show up anywhere the OS shows the name of the application
    /// separately from window titles, such as volume control applets, etc.
    /// This defaults to "Quack! Application" if not set.
    /// </remarks>
    public string Name { get; init; } = "Quack! Application";

    /// <summary>
    /// Gets the version of the application, e.g. 1.0.0.
    /// </summary>
    /// <remarks>
    /// There are no restrictions on the version number format.
    /// </remarks>
    public string? Version { get; init; }

    /// <summary>
    /// Gets the human-readable name of the author/creator/developer of the application, e.g. KappaDuck.
    /// </summary>
    public string? Author { get; init; }

    /// <summary>
    /// Gets the human-readable copyright notice, e.g. Copyright 2025 (c) KappaDuck.
    /// </summary>
    /// <remarks>
    /// Keep this to one line, don't paste a copy of a whole software license here.
    /// </remarks>
    public string? Copyright { get; init; }

    /// <summary>
    /// Gets the url of the application, e.g. https://kappaduck.com.
    /// </summary>
    public Uri? Url { get; init; }

    /// <summary>
    /// Gets the type of the application. Default is <see cref="ApplicationType.Application"/>.
    /// </summary>
    public ApplicationType Type { get; init; } = ApplicationType.Application;
}
