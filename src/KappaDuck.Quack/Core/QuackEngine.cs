// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Core.Extensions;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Interop.SDL;

namespace KappaDuck.Quack.Core;

/// <summary>
/// Manages <see cref="Subsystem"/>s and provides core engine functionality.
/// </summary>
/// <remarks>
/// In most cases, you do not need to use this class directly.
/// Subsystems are automatically managed by modules such as Audio, Graphics, Window, etc.
/// Use <see cref="QuackEngine"/> only when you need to manually initialize or shut down a subsystem.
/// </remarks>
[PublicAPI]
public static class QuackEngine
{
    private static readonly Lock _lock = new();

    private static Subsystem _subsystem;
    private static int _refCount;
    private static bool _initialized;

    /// <summary>
    /// Gets the application metadata.
    /// </summary>
    /// <remarks>
    /// If you want to set the application metadata, use <see cref="SetMetadata(ApplicationMetadata)"/> before
    /// any module initialization (preferably at the very start of your application). You can only set it once.
    /// </remarks>
    public static ApplicationMetadata? Metadata { get; private set; }

    /// <summary>
    /// Gets the number of milliseconds since the engine initialization.
    /// </summary>
    public static TimeSpan Ticks => TimeSpan.FromMilliseconds(Native.SDL_GetTicks());

    /// <summary>
    /// Sets the application metadata.
    /// </summary>
    /// <param name="metadata">The application metadata to set.</param>
    /// <remarks>
    /// You should call this method at the very start of your application.
    /// After calling this method, the metadata cannot be changed.
    /// Setting the metadata is not required, but strongly recommended for better identification of the application.
    /// </remarks>
    /// <exception cref="QuackException">Cannot set metadata after engine initialization.</exception>
    /// <exception cref="QuackNativeException">Fails to set metadata properties.</exception>
    public static void SetMetadata(ApplicationMetadata metadata)
    {
        QuackException.ThrowIf(_initialized, "Cannot set metadata after engine initialization.");
        Metadata = metadata;

        SetMetadataProperty("SDL.app.metadata.identifier", metadata.Id);
        SetMetadataProperty("SDL.app.metadata.name", metadata.Name);
        SetMetadataProperty("SDL.app.metadata.version", metadata.Version);
        SetMetadataProperty("SDL.app.metadata.author", metadata.Author);
        SetMetadataProperty("SDL.app.metadata.copyright", metadata.Copyright);
        SetMetadataProperty("SDL.app.metadata.url", metadata.Url?.ToString());
        SetMetadataProperty("SDL.app.metadata.type", metadata.Type.Name);

        static void SetMetadataProperty(string name, string? value)
        {
            if (string.IsNullOrEmpty(value))
                return;

            QuackNativeException.ThrowIfFailed(Native.SDL_SetAppMetadataProperty(name, value));
        }
    }

    internal static void Init(Subsystem subsystem)
    {
        lock (_lock)
        {
            if (Has(subsystem))
            {
                Interlocked.Increment(ref _refCount);
                return;
            }

            QuackNativeException.ThrowIfFailed(Native.SDL_InitSubSystem(subsystem & ~Subsystem.TTF));
            _subsystem |= subsystem;

            if (Has(Subsystem.TTF))
                QuackNativeException.ThrowIfFailed(Native.TTF_Init());

            Interlocked.Increment(ref _refCount);
            _initialized = true;
        }
    }

    internal static void Release()
    {
        lock (_lock)
        {
            if (Interlocked.Decrement(ref _refCount) > 0)
                return;

            if (Has(Subsystem.TTF))
                QuackNativeException.ThrowIfFailed(Native.TTF_Quit());

            Native.SDL_QuitSubSystem(_subsystem & ~Subsystem.TTF);
            Native.SDL_Quit();

            _subsystem &= ~_subsystem;
            _initialized = false;
        }
    }

    private static bool Has(Subsystem subsystem) => (_subsystem & subsystem) == subsystem;
}
