// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Core;
using KappaDuck.Quack.Interop.SDL;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace KappaDuck.Quack;

/// <summary>
/// The main engine to manage <see cref="SubSystem"/> and provide core functionalities.
/// </summary>
public sealed partial class QuackEngine : IDisposable
{
    private static readonly Lock _lock = new();

    private static QuackEngine? _engine;
    private static SubSystem _subSystems = SubSystem.None;
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
    /// Gets the current version of SDL used by the engine.
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

            SDL_QuitSubSystem(_subSystems);
            SDL_Quit();

            Cleanup();
        }
    }

    /// <summary>
    /// Determines whether the specified <see cref="SubSystem"/> is initialized.
    /// </summary>
    /// <param name="subsystem">The subsystem to check.</param>
    /// <returns><see langword="true"/> if the subsystem is initialized; otherwise, <see langword="false"/>.</returns>
    public static bool Has(SubSystem subsystem) => (_subSystems & subsystem) == subsystem;

    /// <summary>
    /// Initializes the engine with the specified <see cref="SubSystem"/>.
    /// </summary>
    /// <remarks>
    /// Initialized subsystems are stored and will be automatically shut down when the engine is disposed
    /// or calling <see cref="QuitSubSystem(SubSystem)"/> You can initialize the same subsystem multiple times.
    /// It will only be initialized once.
    /// </remarks>
    /// <param name="subSystem">The subsystem to initialize.</param>
    /// <returns>An instance of <see cref="QuackEngine"/>.</returns>
    /// <exception cref="QuackNativeException">Failed to initialize the subsystem.</exception>
    public static QuackEngine Init(SubSystem subSystem)
    {
        lock (_lock)
        {
            _engine ??= new QuackEngine();

            InitializeEngine(subSystem);

            return _engine;
        }
    }

    public static void InitSubsystem(SubSystem subSystem)
    {
        ThrowIfInstanceNull();

        lock (_lock)
        {
            if (Has(subSystem))
                return;

            QuackNativeException.ThrowIfFailed(SDL_InitSubSystem(subSystem));
            _subSystems |= subSystem;
        }
    }

    /// <summary>
    /// Quits the specified <see cref="SubSystem"/>.
    /// </summary>
    /// <remarks>
    /// <para>You should call <see cref="Init(SubSystem)"/> before using this method to initialize the engine.</para>
    /// <para>You can shut down the same subsystem multiple times. It will only be shut down once.</para>
    /// <para>You still need to call <see cref="Dispose"/> or <see langword="using"/> even if you shut down all subsystems.</para>
    /// </remarks>
    /// <param name="subSystem">The subsystem to quit.</param>
    /// <exception cref="QuackNativeException">The engine is not initialized.</exception>
    public static void QuitSubSystem(SubSystem subSystem)
    {
        ThrowIfInstanceNull();

        lock (_lock)
        {
            if (!Has(subSystem))
                return;

            SDL_QuitSubSystem(subSystem);

            _subSystems &= ~subSystem;
        }
    }

    private static void Cleanup()
    {
        _engine = null;
        _subSystems = SubSystem.None;
    }

    private static void InitializeEngine(SubSystem subSystem)
    {
        if (Has(subSystem))
        {
            Interlocked.Increment(ref _refCount);
            return;
        }

        QuackNativeException.ThrowIfFailed(SDL_InitSubSystem(subSystem));

        Interlocked.Increment(ref _refCount);

        _subSystems |= subSystem;
    }

    private static void ThrowIfInstanceNull()
        => QuackNativeException.ThrowIfNull(_engine, "The engine is not initialized.");

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ulong SDL_GetTicks();

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetVersion();

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_InitSubSystem(SubSystem subsystems);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_QuitSubSystem(SubSystem subsystems);

    [LibraryImport(SDLNative.Library), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_Quit();
}
