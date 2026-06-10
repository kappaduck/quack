// Copyright (c) KappaDuck.
// Licensed under the MIT license.

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
    private static readonly long _startTimestamp = Stopwatch.GetTimestamp();

    private static int _refCount;
    private static Subsystem _subsystems;

    /// <summary>
    /// Gets the elapsed time since the engine started.
    /// </summary>
    public static TimeSpan ElapsedTime => Stopwatch.GetElapsedTime(_startTimestamp);

    /// <summary>
    /// Gets the application metatadata provided through <see cref="SetMetadata(ApplicationMetadata)"/>.
    /// </summary>
    public static ApplicationMetadata? Metadata { get; private set; }

    /// <summary>
    /// Sets the application metadata.
    /// </summary>
    /// <param name="metadata">The application metadata.</param>
    /// <remarks>
    /// <para>
    /// You can set it only once; every subsequent call is ignored. You must call it at the very
    /// beginning of your application, before any module initialization.
    /// </para>
    /// </remarks>
    /// <exception cref="QuackInteropException">Failed to set an application metadata property.</exception>
    public static void SetMetadata(ApplicationMetadata metadata)
    {
        lock (_lock)
        {
            if (_refCount > 0 || Metadata is not null)
                return;

            Metadata = metadata;

            SetMetadataProperty("SDL.app.metadata.identifier", metadata.Identifier);
            SetMetadataProperty("SDL.app.metadata.name", metadata.Name);
            SetMetadataProperty("SDL.app.metadata.version", metadata.Version);
            SetMetadataProperty("SDL.app.metadata.creator", metadata.Author);
            SetMetadataProperty("SDL.app.metadata.copyright", metadata.Copyright);
            SetMetadataProperty("SDL.app.metadata.url", metadata.Url?.ToString());
            SetMetadataProperty("SDL.app.metadata.type", metadata.Type switch
            {
                ApplicationType.Game => nameof(ApplicationType.Game),
                ApplicationType.MediaPlayer => nameof(ApplicationType.MediaPlayer),
                ApplicationType.Application => nameof(ApplicationType.Application),
                _ => nameof(ApplicationType.Application)
            });

        }

        static void SetMetadataProperty(string name, string? value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            SDLThrowHelper.ThrowIfFailed(SDL3.SetAppMetadataProperty(name, value));
        }
    }

    /// <summary>
    /// Initializes the given <paramref name="subsystem"/> if needed and increments the reference count.
    /// </summary>
    /// <param name="subsystem">The subsystem(s) to initialize.</param>
    /// <exception cref="QuackInteropException">Failed to initialize a subsystem.</exception>
    internal static void AddRef(Subsystem subsystem)
    {
        lock (_lock)
        {
            _refCount++;

            Subsystem missing = subsystem & ~_subsystems;
            if (missing == Subsystem.None)
                return;

            Initialize(missing);
            _subsystems |= missing;
        }
    }

    /// <summary>
    /// Initializes the given <paramref name="subsystem"/> without taking a reference on it.
    /// </summary>
    /// <param name="subsystem">The subsystem(s) to initialize.</param>
    /// <remarks>
    /// The subsystem is brought up but is not reference-counted, so it can be released by
    /// <see cref="Release"/> once the last counted reference is gone. Only use this when another
    /// owner guarantees the engine stays alive for the whole duration you need the subsystem.
    /// </remarks>
    /// <exception cref="QuackInteropException">Failed to initialize a subsystem.</exception>
    internal static void DangerousAddRef(Subsystem subsystem)
    {
        lock (_lock)
        {
            Subsystem missing = subsystem & ~_subsystems;
            if (missing == Subsystem.None)
                return;

            Initialize(missing);
            _subsystems |= missing;
        }
    }

    /// <summary>
    /// Decrements the reference count and shuts down every subsystem once it reaches zero.
    /// </summary>
    internal static void Release()
    {
        lock (_lock)
        {
            if (_refCount == 0)
                return;

            if (--_refCount > 0)
                return;

            if ((_subsystems & Subsystem.Mixer) == Subsystem.Mixer)
                SDL3_mixer.Quit();

            if ((_subsystems & Subsystem.TTF) == Subsystem.TTF)
                SDL3_ttf.Quit();

            SDL3.Quit();

            _subsystems = Subsystem.None;
        }
    }

    private static void Initialize(Subsystem subsystem)
    {
        Subsystem core = subsystem & ~(Subsystem.TTF | Subsystem.Mixer);

        if (core != Subsystem.None)
            SDLThrowHelper.ThrowIfFailed(SDL3.InitSubSystem(core));

        if ((subsystem & Subsystem.TTF) == Subsystem.TTF)
            SDLThrowHelper.ThrowIfFailed(SDL3_ttf.Init());

        if ((subsystem & Subsystem.Mixer) == Subsystem.Mixer)
            SDLThrowHelper.ThrowIfFailed(SDL3_mixer.Init());
    }
}
