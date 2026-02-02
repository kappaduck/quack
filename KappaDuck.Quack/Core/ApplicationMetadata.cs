// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Core;

/// <summary>
/// Represents metadata information about the application.
/// </summary>
/// <remarks>
/// <para>It is not required but strongly recommended to identify your application.</para>
/// <para>
/// There are several benefits to providing application metadata, including:
/// <list type="bullet">
/// <item>Displaying in the "About" section of the application.</item>
/// <item>Improved user support and troubleshooting.</item>
/// <item>The name of the application can be shown on some audio mixers.</item>
/// </list>
/// </para>
/// </remarks>
public sealed record ApplicationMetadata
{
    /// <summary>
    /// Creates an empty application metadata.
    /// </summary>
    public ApplicationMetadata()
    {
    }

    /// <summary>
    /// Creates a basic application metadata.
    /// </summary>
    /// <param name="name">The name of the application.</param>
    /// <param name="version">The version of the application.</param>
    /// <param name="identifier">The reverse domain name of the application.</param>
    public ApplicationMetadata(string name, string version, string identifier)
    {
        Name = name;
        Version = version;
        Identifier = identifier;
    }

    /// <summary>
    /// Gets or initializes the reverse domain name of the application. e.g., "com.kappaduck.quack.myapp".
    /// </summary>
    /// <remarks>
    /// This is used by desktop compositors to identify and group windows together. It will
    /// match the application with associated desktop settings and icons.
    /// </remarks>
    public string? Identifier { get; init; }

    /// <summary>
    /// Gets or initializes the name of the application, e.g., "Quack! Application".
    /// </summary>
    /// <remarks>
    /// <para>
    /// This will show up anywhere the operating system displays the application name separately
    /// from the window title, such as volume control panels.
    /// </para>
    /// <para>The default value is "Quack! Application".</para>
    /// </remarks>
    public string? Name { get; init; } = "Quack! Application";

    /// <summary>
    /// Gets or initializes the version of the application, e.g., "1.0.0".
    /// </summary>
    /// <remarks>
    /// There is no enforced format for the version, but it is recommended to follow
    /// the <see href="https://semver.org/">Semantic Versioning scheme</see>.
    /// </remarks>
    public string? Version { get; init; }

    /// <summary>
    /// Gets or initializes the author/creator/developer of the application, e.g., "KappaDuck".
    /// </summary>
    public string? Author { get; init; }

    /// <summary>
    /// Gets or initializes the copyright notice for the application, e.g., "Copyright 2025 (c) KappaDuck".
    /// </summary>
    public string? Copyright { get; init; }

    /// <summary>
    /// Gets or initializes the URL associated with the application, e.g., <see href="https://quack.kappaduck.com"/>.
    /// </summary>
    public Uri? Url { get; init; }

    /// <summary>
    /// Gets or sets the type of the application.
    /// </summary>
    /// <remarks>By default, it is <see cref="ApplicationType.Application"/>.</remarks>
    public ApplicationType Type { get; init; } = ApplicationType.Application;
}
