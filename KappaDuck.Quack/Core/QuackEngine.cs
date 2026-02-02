// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Interop.SDL.Primitives;
using System.Diagnostics;

namespace KappaDuck.Quack.Core;

/// <summary>
/// Represents the core engine of Quack!.
/// </summary>
public static class QuackEngine
{
    private static readonly Lock _lock = new();

    private static int _refCount;
    private static Subsystem _subsystems;
    private static readonly long _startTimestamp = Stopwatch.GetTimestamp();

    /// <summary>
    /// Gets the application metatadata provided from <see cref="SetMetadata(ApplicationMetadata)"/>.
    /// </summary>
    public static ApplicationMetadata? Metadata { get; private set; }

    /// <summary>
    /// Gets the elapsed time since the engine started.
    /// </summary>
    public static TimeSpan ElapsedTime => Stopwatch.GetElapsedTime(_startTimestamp);

    /// <summary>
    /// Sets the application metadata.
    /// </summary>
    /// <param name="metadata">The application metadata.</param>
    /// <remarks>
    /// <para>
    /// You can set it only once; every subsequent call will be ignored.
    /// You must call at the very beginning of your application before any module initialization.
    /// </para>
    /// </remarks>
    /// <exception cref="QuackInteropException">Failed to set application metadata property.</exception>
    [MemberNotNull(nameof(Metadata))]
    public static void SetMetadata(ApplicationMetadata metadata)
    {
        if (_refCount > 0 || Metadata is not null)
        {
            Metadata ??= new ApplicationMetadata();
            return;
        }

        Metadata = metadata;

        SetMetadataProperty("SDL.app.metadata.identifier", metadata.Identifier);
        SetMetadataProperty("SDL.app.metadata.name", metadata.Name);
        SetMetadataProperty("SDL.app.metadata.version", metadata.Version);
        SetMetadataProperty("SDL.app.metadata.creator", metadata.Author);
        SetMetadataProperty("SDL.app.metadata.copyright", metadata.Copyright);
        SetMetadataProperty("SDL.app.metadata.url", metadata.Url?.ToString());
        SetMetadataProperty("SDL.app.metadata.type", metadata.Type.Name);

        static void SetMetadataProperty(string name, string? value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            QuackInteropException.ThrowIfFailed(SDL3.Properties.SetAppMetadataProperty(name, value));
        }
    }

    internal static void AddRef(Subsystem subsystem)
    {
        lock (_lock)
        {
            if ((_subsystems & subsystem) != 0)
            {
                _refCount++;
                return;
            }

            if ((_subsystems & Subsystem.TTF) == Subsystem.TTF)
                QuackInteropException.ThrowIfFailed(SDL3_ttf.Init());

            if ((subsystem & ~Subsystem.TTF) != 0)
                QuackInteropException.ThrowIfFailed(SDL3.InitSubSystem(subsystem));

            _subsystems |= subsystem;
            _refCount++;
        }
    }

    internal static void DangerousAddRef(Subsystem subsystem)
    {
        lock (_lock)
        {
            if ((_subsystems & subsystem) != 0)
                return;

            if ((_subsystems & Subsystem.TTF) == Subsystem.TTF)
                QuackInteropException.ThrowIfFailed(SDL3_ttf.Init());

            if ((subsystem & ~Subsystem.TTF) != 0)
                QuackInteropException.ThrowIfFailed(SDL3.InitSubSystem(subsystem));

            _subsystems |= subsystem;
        }
    }

    internal static void Release()
    {
        lock (_lock)
        {
            if (--_refCount == 0)
            {
                SDL3_ttf.Quit();
                SDL3.Quit();
            }
        }
    }
}
