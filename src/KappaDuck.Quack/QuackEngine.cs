// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Core;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Interop.SDL;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace KappaDuck.Quack;

/// <summary>
/// The main engine to manage <see cref="Subsystem"/> and provide core functionalities.
/// </summary>
public sealed partial class QuackEngine : IDisposable
{
    private const string IdentifierProperty = "SDL.app.metadata.identifier";
    private const string NameProperty = "SDL.app.metadata.name";
    private const string VersionProperty = "SDL.app.metadata.version";
    private const string AuthorProperty = "SDL.app.metadata.creator";
    private const string CopyrightProperty = "SDL.app.metadata.copyright";
    private const string UrlProperty = "SDL.app.metadata.url";
    private const string TypeProperty = "SDL.app.metadata.type";

    private static readonly Lock _lock = new();

    private static QuackEngine? _engine;
    private static Subsystem _subsystems = Subsystem.None;
    private static int _refCount;

    private QuackEngine()
    {
    }

    /// <summary>
    /// Gets the number of milliseconds since the engine initialization.
    /// </summary>
    public static TimeSpan Ticks
    {
        get
        {
            ulong ticks = SDL_GetTicks();
            return TimeSpan.FromMilliseconds(ticks);
        }
    }

    /// <summary>
    /// Gets the current SDL version that the engine is using.
    /// </summary>
    public static string Version
    {
        get
        {
            int version = SDL_GetVersion();

            int major = version / 1000000;
            int minor = version / 1000 % 1000;
            int patch = version % 1000;

            return $"{major}.{minor}.{patch}";
        }
    }

    /// <summary>
    /// Shutdown the engine and all subsystems.
    /// </summary>
    public void Dispose()
    {
        lock (_lock)
        {
            if (Interlocked.Decrement(ref _refCount) > 0)
                return;

            SDL_QuitSubSystem(_subsystems);
            SDL_Quit();

            Cleanup();
        }
    }

    /// <summary>
    /// Determines whether the specified <see cref="Subsystem"/> is initialized.
    /// </summary>
    /// <param name="subsystem">The subsystem to check.</param>
    /// <returns><see langword="true"/> if the subsystem is initialized; otherwise, <see langword="false"/>.</returns>
    public static bool Has(Subsystem subsystem) => (_subsystems & subsystem) == subsystem;

    /// <summary>
    /// Initializes the engine with the specified <see cref="Subsystem"/> and optional <see cref="AppMetadata"/>.
    /// </summary>
    /// <remarks>
    /// Initialized subsystems are stored and will be automatically shut down when the engine is disposed
    /// or calling <see cref="QuitSubsystem(Subsystem)"/> You can initialize the same subsystem multiple times.
    /// It will only be initialized once.
    /// </remarks>
    /// <param name="subsystem">The subsystem to initialize.</param>
    /// <param name="metadata">Optional metadata for the application.</param>
    /// <returns>An instance of <see cref="QuackEngine"/>.</returns>
    /// <exception cref="QuackNativeException">Failed to initialize the subsystem.</exception>
    public static QuackEngine Init(Subsystem subsystem, AppMetadata? metadata = null)
    {
        lock (_lock)
        {
            _engine ??= new QuackEngine();

            SetAppMetadata(metadata);
            InitializeEngine(subsystem);

            return _engine;
        }
    }

    /// <summary>
    /// Initialize specific subsystems.
    /// </summary>
    /// <remarks>
    /// You should call <see cref="Init(Subsystem, AppMetadata?)"/> before using this method to initialize the engine.
    /// </remarks>
    /// <param name="subsystem">The subsystem to initialize.</param>
    /// <exception cref="QuackException">The engine is not initialized.</exception>
    /// <exception cref="QuackNativeException">Failed to initialize the subsystem.</exception>
    public static void InitSubsystem(Subsystem subsystem)
    {
        ThrowIfInstanceNull();

        lock (_lock)
        {
            if (Has(subsystem))
                return;

            QuackNativeException.ThrowIfFailed(SDL_InitSubSystem(subsystem));
            _subsystems |= subsystem;
        }
    }

    /// <summary>
    /// Quits the specified <see cref="Subsystem"/>.
    /// </summary>
    /// <remarks>
    /// <para>You should call <see cref="Init(Subsystem, AppMetadata?)"/> before using this method to initialize the engine.</para>
    /// <para>You can shut down the same subsystem multiple times. It will only be shut down once.</para>
    /// <para>You still need to call <see cref="Dispose"/> or <see langword="using"/> even if you shut down all subsystems.</para>
    /// </remarks>
    /// <param name="subsystem">The subsystem to quit.</param>
    /// <exception cref="QuackException">The engine is not initialized.</exception>
    public static void QuitSubsystem(Subsystem subsystem)
    {
        ThrowIfInstanceNull();

        lock (_lock)
        {
            if (!Has(subsystem))
                return;

            SDL_QuitSubSystem(subsystem);

            _subsystems &= ~subsystem;
        }
    }

    private static void Cleanup()
    {
        _engine = null;
        _subsystems = Subsystem.None;
    }

    private static void InitializeEngine(Subsystem subsystem)
    {
        if (Has(subsystem))
        {
            Interlocked.Increment(ref _refCount);
            return;
        }

        QuackNativeException.ThrowIfFailed(SDL_InitSubSystem(subsystem));

        Interlocked.Increment(ref _refCount);

        _subsystems |= subsystem;
    }

    private static void SetAppMetadata(AppMetadata? metadata)
    {
        if (metadata is null)
            return;

        SetAppMetadataProperty(IdentifierProperty, metadata.Id);
        SetAppMetadataProperty(NameProperty, metadata.Name);
        SetAppMetadataProperty(VersionProperty, metadata.Version);
        SetAppMetadataProperty(AuthorProperty, metadata.Author);
        SetAppMetadataProperty(CopyrightProperty, metadata.Copyright);
        SetAppMetadataProperty(UrlProperty, metadata.Url?.ToString());
        SetAppMetadataProperty(TypeProperty, metadata.Type);

        static void SetAppMetadataProperty(string name, string? value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            QuackNativeException.ThrowIfFailed(SDL_SetAppMetadataProperty(name, value));
        }
    }

    private static void ThrowIfInstanceNull()
        => QuackException.ThrowIfNull(_engine, "The engine is not initialized.");

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ulong SDL_GetTicks();

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetVersion();

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_InitSubSystem(Subsystem subsystems);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_QuitSubSystem(Subsystem subsystems);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_Quit();

    [LibraryImport(SDLNative.Library, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_SetAppMetadataProperty(string name, string value);
}
