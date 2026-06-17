// Copyright (c) KappaDuck.
// Licensed under the MIT license.

namespace KappaDuck.Quack.Core;

/// <summary>
/// Represents the lifetime of the initialized <see cref="QuackEngine"/>.
/// Dispose it on the main thread to shut the engine down.
/// </summary>
public sealed class EngineScope : IDisposable
{
    private readonly IDisposable _context;
    private bool _disposed;

    internal EngineScope() => _context = QuackSynchronizationContext.Enter();

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_disposed)
            return;

        _disposed = true;

        _context.Dispose();
        QuackEngine.Release();
    }
}
