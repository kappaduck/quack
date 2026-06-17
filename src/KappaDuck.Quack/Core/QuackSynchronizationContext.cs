// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Exceptions;
using System.Runtime.ExceptionServices;

namespace KappaDuck.Quack.Core;

/// <summary>
/// A <see cref="SynchronizationContext"/> that resumes asynchronous continuations on the engine's main thread.
/// </summary>
/// <remarks>
/// <para>
/// When installed on the main thread, awaiting a task from main-thread code resumes on the main thread,
/// so you can call SDL's main-thread-only APIs after an <see langword="await"/> without explicitly going
/// through <see cref="MainThreadDispatcher"/>.
/// </para>
/// </remarks>
internal sealed class QuackSynchronizationContext : SynchronizationContext
{
    /// <summary>
    /// Installs a <see cref="QuackSynchronizationContext"/> as the current context until the returned
    /// scope is disposed, which restores the previous context.
    /// </summary>
    /// <returns>A scope that restores the previous synchronization context when disposed.</returns>
    /// <exception cref="InvalidOperationException">The caller is not on the main thread.</exception>
    public static IDisposable Enter()
    {
        if (!QuackEngine.IsMainThread)
            ThrowHelper.ThrowInvalidOperation("The Quack! synchronization context must be installed on the main thread.");

        return new Scope();
    }

    /// <inheritdoc/>
    public override void Post(SendOrPostCallback d, object? state) => MainThreadDispatcher.Post(() => d(state));

    /// <inheritdoc/>
    public override void Send(SendOrPostCallback d, object? state)
    {
        if (QuackEngine.IsMainThread)
        {
            d(state);
            return;
        }

        using ManualResetEventSlim completed = new(initialState: false);
        ExceptionDispatchInfo? error = null;

        MainThreadDispatcher.Post(() =>
        {
            try
            {
                d(state);
            }
            catch (Exception exception)
            {
                error = ExceptionDispatchInfo.Capture(exception);
            }
            finally
            {
                completed.Set();
            }
        });

        completed.Wait();
        error?.Throw();
    }

    /// <inheritdoc/>
    public override SynchronizationContext CreateCopy() => this;

    private sealed class Scope : IDisposable
    {
        private readonly SynchronizationContext? _previous;

        internal Scope()
        {
            _previous = Current;
            SetSynchronizationContext(new QuackSynchronizationContext());
        }

        public void Dispose() => SetSynchronizationContext(_previous);
    }
}
