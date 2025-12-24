// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Core.Extensions;
using KappaDuck.Quack.Exceptions;

namespace KappaDuck.Quack.Core;

/// <summary>
/// The engine to manage subsystems and offers some utility functionalities.
/// </summary>
public static class QuackEngine
{
    private static readonly Lock _lock = new();
    private static int _refCount;

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
    /// <param name="metadata">The application metadata</param>
    /// <remarks>
    /// <para>
    /// You should call this method at the very start of your application.
    /// After calling this method, the metadata cannot be changed.
    /// </para>
    /// <para>Setting the metadata is not required, but strongly recommended for better identification of the application.</para>
    /// </remarks>
    /// <exception cref="QuackException">Cannot set metadata after engine initialization.</exception>
    /// <exception cref="QuackNativeException">Fails to set metadata properties.</exception>
    public static void SetMetadata(ApplicationMetadata metadata)
    {
        QuackException.ThrowIf(_refCount > 0 || Metadata is not null, "Cannot set metadata after engine initialization.");
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

    internal static void Acquire(Subsystem subsystem)
    {
        lock (_lock)
        {
            if ((subsystem & Subsystem.TTF) == Subsystem.TTF)
                QuackNativeException.ThrowIfFailed(Native.TTF_Init());

            if ((subsystem & ~Subsystem.TTF) != Subsystem.None)
                QuackNativeException.ThrowIfFailed(Native.SDL_InitSubSystem(subsystem));

            _refCount++;
        }
    }

    internal static void Release()
    {
        lock (_lock)
        {
            if (--_refCount == 0)
            {
                Native.TTF_Quit();
                Native.SDL_Quit();
            }
        }
    }
}
