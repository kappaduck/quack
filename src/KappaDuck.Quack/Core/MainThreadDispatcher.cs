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
    /// Queues <paramref name="work"/> to run on the main thread at the next drain, without waiting.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Always asynchronous: the work is enqueued and never runs inline, even when the caller is already
    /// on the main thread. This is the primitive behind <see cref="QuackSynchronizationContext.Post"/>.
    /// </para>
    /// <para>
    /// Because it always defers to the next <see cref="Drain"/>, work posted from within a callback runs
    /// on the following drain rather than re-entrantly during the current one, so a single misbehaving
    /// callback cannot starve the frame. If you want main-thread execution that runs inline when already
    /// on the main thread, use <see cref="Invoke"/> instead.
    /// </para>
    /// <para>
    /// An exception thrown by <paramref name="work"/> surfaces on the main thread during the drain and is
    /// not observed by the caller. Use <see cref="InvokeAsync(Action)"/> when you need to observe failures.
    /// </para>
    /// </remarks>
    /// <param name="work">The callback to run on the main thread.</param>
    internal static void Post(Action work) => _queue.Enqueue(work);

    /// <summary>
    /// Runs <paramref name="work"/> on the main thread, executing it inline when the caller is already on
    /// the main thread, otherwise queuing it to run at the next drain.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Prefer this for main-thread-affine side effects that return nothing, e.g. a progress backend
    /// applying a taskbar or window change. Unlike <see cref="Post"/>, it does not force a deferral when
    /// the caller is already on the main thread, so the effect is immediate in that case. Unlike
    /// <see cref="InvokeAsync(Action)"/>, it returns nothing and cannot be awaited.
    /// </para>
    /// <para>
    /// When the caller is on the main thread, an exception thrown by <paramref name="work"/> propagates to
    /// the caller. When queued, it surfaces on the main thread during the drain and is not observed by the
    /// caller; use <see cref="InvokeAsync(Action)"/> to observe failures.
    /// </para>
    /// </remarks>
    /// <param name="work">The callback to run on the main thread.</param>
    internal static void Invoke(Action work)
    {
        if (QuackEngine.IsMainThread)
        {
            work();
            return;
        }

        _queue.Enqueue(work);
    }

    /// <summary>
    /// Runs <paramref name="work"/> on the main thread and asynchronously waits for it to complete.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The awaitable counterpart of <see cref="Invoke"/>. If the caller is already on the main thread, the
    /// callback runs inline and the returned task is already completed; otherwise it is queued and the task
    /// completes on the next drain.
    /// </para>
    /// <para>
    /// Unlike <see cref="Invoke"/> and <see cref="Post"/>, an exception thrown by <paramref name="work"/> is
    /// captured on the returned task instead of being raised at the drain site, so awaiting observes success
    /// or failure. Continuations are scheduled asynchronously so they never run inside the drain.
    /// </para>
    /// </remarks>
    /// <param name="work">The callback to run on the main thread.</param>
    /// <returns>A task that completes once the callback has run on the main thread.</returns>
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
    /// <remarks>
    /// <para>
    /// The result-producing counterpart of <see cref="InvokeAsync(Action)"/>. If the caller is already on
    /// the main thread, the callback runs inline and the returned task is already completed with its result;
    /// otherwise it is queued and the task completes on the next drain.
    /// </para>
    /// <para>
    /// An exception thrown by <paramref name="work"/> is captured on the returned task, so awaiting observes
    /// success or failure.
    /// </para>
    /// </remarks>
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
