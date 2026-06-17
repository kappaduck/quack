// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Exceptions;
using System.Diagnostics;

namespace KappaDuck.Quack.Core;

/// <summary>
/// Represents the core engine of Quack!.
/// </summary>
public static class QuackEngine
{
    private static readonly Lock _lock = new();

    private static long _startTimestamp;
    private static int? _mainThreadId;
    private static Subsystem _subsystem;

    /// <summary>
    /// Gets the elapsed time since the engine was initialized.
    /// </summary>
    /// <remarks>
    /// Using <see cref="ElapsedTime"/> before the engine is initialized will return <see cref="TimeSpan.Zero"/>.
    /// </remarks>
    public static TimeSpan ElapsedTime => IsInitialized ? Stopwatch.GetElapsedTime(_startTimestamp) : TimeSpan.Zero;

    /// <summary>
    /// Gets a value indicating whether the engine has been initialized.
    /// </summary>
    public static bool IsInitialized { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the calling thread is the application's main thread.
    /// </summary>
    public static bool IsMainThread => _mainThreadId == Environment.CurrentManagedThreadId;

    /// <summary>
    /// Gets the application metadata provided through <see cref="SetMetadata(ApplicationMetadata)"/>.
    /// </summary>
    public static ApplicationMetadata? Metadata { get; private set; }

    /// <summary>
    /// Initializes the engine and the given <paramref name="subsystem"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Call this once on the application's main thread before using any engine feature. It captures the
    /// main thread, installs the synchronization context, and brings up the requested subsystems.
    /// </para>
    /// <para>
    /// Dispose the returned <see cref="EngineScope"/> to shut the engine down and restore the previous
    /// synchronization context.
    /// </para>
    /// </remarks>
    /// <param name="subsystem">The subsystems to initialize.</param>
    /// <returns>A scope that shuts the engine down when disposed.</returns>
    /// <exception cref="QuackException">The engine is already initialized.</exception>
    /// <exception cref="QuackInteropException">Failed to initialize a subsystem.</exception>
    public static EngineScope Init(Subsystem subsystem)
    {
        lock (_lock)
        {
            ThrowHelper.ThrowIf(IsInitialized, "The engine is already initialized.");

            _mainThreadId = Environment.CurrentManagedThreadId;
            _startTimestamp = Stopwatch.GetTimestamp();

            Initialize(subsystem);

            _subsystem = subsystem | Subsystem.Events;
            IsInitialized = true;
        }

        return new EngineScope();
    }

    /// <summary>
    /// Determines whether the given <paramref name="subsystem"/> is currently initialized.
    /// </summary>
    /// <param name="subsystem">The subsystem(s) to check.</param>
    /// <returns><see langword="true"/> if every requested subsystem is initialized; otherwise <see langword="false"/>.</returns>
    public static bool HasSubsystem(Subsystem subsystem) => (_subsystem & subsystem) == subsystem;

    /// <summary>
    /// Sets the application metadata.
    /// </summary>
    /// <param name="metadata">The application metadata.</param>
    /// <remarks>
    /// You can set it only once and must do so before <see cref="Init(Subsystem)"/>; every subsequent
    /// call is ignored.
    /// </remarks>
    /// <exception cref="QuackInteropException">Failed to set an application metadata property.</exception>
    public static void SetMetadata(ApplicationMetadata metadata)
    {
        lock (_lock)
        {
            if (IsInitialized || Metadata is not null)
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
                ApplicationType.Application => nameof(ApplicationType.Application),
                ApplicationType.Game => nameof(ApplicationType.Game),
                ApplicationType.MediaPlayer => nameof(ApplicationType.MediaPlayer),
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

    internal static void EnsureInitialized(Subsystem subsystem, [CallerMemberName] string member = "")
        => ThrowHelper.ThrowIf(!HasSubsystem(subsystem), $"The {subsystem} subsystem is required. Call QuackEngine.Init({subsystem}) first.", member);

    internal static void Release()
    {
        lock (_lock)
        {
            if (!IsInitialized)
                return;

            if ((_subsystem & Subsystem.Mixer) == Subsystem.Mixer)
                SDL3_mixer.Quit();

            if ((_subsystem & Subsystem.TTF) == Subsystem.TTF)
                SDL3_ttf.Quit();

            Subsystem core = _subsystem & ~(Subsystem.TTF | Subsystem.Mixer);

            SDL3.QuitSubSystem(core);
            SDL3.Quit();

            _subsystem = Subsystem.None;
            _mainThreadId = null;
            IsInitialized = false;
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
