// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using System.Collections.Concurrent;

namespace KappaDuck.Quack.Core;

/// <summary>
/// Marshals work back onto the engine's main thread.
/// </summary>
/// <remarks>
/// <para>
/// SDL's video, rendering and event APIs must only be used from the main thread.
/// Use this dispatcher to hop back to the main thread from
/// background work, e.g. after loading an asset on a thread pool.
/// </para>
/// <para>
/// The engine drains queued callbacks once per frame on the main thread, typically right after
/// pumping SDL events. Callbacks queued from within a callback run on the next drain rather than
/// the current one, so a misbehaving callback cannot starve the frame.
/// </para>
/// </remarks>
internal static class MainThreadDispatcher
{
    private static readonly ConcurrentQueue<Action> _queue = [];

    /// <summary>
    /// Queues <paramref name="work"/> to run on the main thread, without waiting for it to complete.
    /// </summary>
    /// <param name="work">The callback to run on the main thread.</param>
    internal static void Post(Action work) => _queue.Enqueue(work);

    /// <summary>
    /// Runs <paramref name="work"/> on the main thread and asynchronously waits for it to complete.
    /// </summary>
    /// <param name="work">The callback to run on the main thread.</param>
    /// <returns>A task that completes once the callback has run on the main thread.</returns>
    /// <remarks>
    /// If the caller is already on the main thread, the callback runs inline and the returned task is
    /// already completed. Otherwise it is queued and the task completes on the next drain.
    /// </remarks>
    internal static Task InvokeAsync(Action work)
    {
        if (QuackEngine.IsMainThread)
        {
            work();
            return Task.CompletedTask;
        }

        TaskCompletionSource source = new(TaskCreationOptions.RunContinuationsAsynchronously);

        _queue.Enqueue(() =>
        {
            try
            {
                work();
                source.SetResult();
            }
            catch (Exception exception)
            {
                source.SetException(exception);
            }
        });

        return source.Task;
    }

    /// <summary>
    /// Runs <paramref name="work"/> on the main thread and asynchronously waits for its result.
    /// </summary>
    /// <typeparam name="T">The type of the result produced by <paramref name="work"/>.</typeparam>
    /// <param name="work">The callback to run on the main thread.</param>
    /// <returns>A task that completes with the callback's result once it has run on the main thread.</returns>
    internal static Task<T> InvokeAsync<T>(Func<T> work)
    {
        if (QuackEngine.IsMainThread)
            return Task.FromResult(work());

        TaskCompletionSource<T> source = new(TaskCreationOptions.RunContinuationsAsynchronously);

        _queue.Enqueue(() =>
        {
            try
            {
                source.SetResult(work());
            }
            catch (Exception exception)
            {
                source.SetException(exception);
            }
        });

        return source.Task;
    }

    /// <summary>
    /// Executes every callback queued so far. Must be called on the main thread.
    /// </summary>
    internal static void Drain()
    {
        int count = _queue.Count;

        while (count-- > 0 && _queue.TryDequeue(out Action? work))
            work();
    }
}
