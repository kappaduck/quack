// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Core;
using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Interop.SDL.Primitives;
using KappaDuck.Quack.UI.Progress;

namespace KappaDuck.Quack.Windows;

/// <summary>
/// Controls the progress indicator shown on a window's taskbar icon.
/// </summary>
public sealed class TaskbarProgressBar : ProgressBar
{
    private const int ThrottleInMilliseconds = 35;

    private readonly Window _window;

    private double _lastAppliedMilliseconds;

    internal TaskbarProgressBar(Window window) => _window = window;

    /// <inheritdoc/>
    protected override void SetState(ProgressState state)
        => MainThreadDispatcher.Invoke(() => SDLThrowHelper.ThrowIfFailed(SDL3.SetWindowProgressState(_window.NativeHandle, Map(state))));

    /// <inheritdoc/>
    protected override void SetValue(float value)
    {
        MainThreadDispatcher.Invoke(() =>
        {
            double now = QuackEngine.ElapsedTime.TotalMilliseconds;

            if (value < 1f && now - _lastAppliedMilliseconds < ThrottleInMilliseconds)
                return;

            _lastAppliedMilliseconds = now;

            SDL3.SetWindowProgressValue(_window.NativeHandle, value);
        });
    }

    private static SDL_ProgressState Map(ProgressState state) => state switch
    {
        ProgressState.None => SDL_ProgressState.None,
        ProgressState.Normal => SDL_ProgressState.Normal,
        ProgressState.Indeterminate => SDL_ProgressState.Indeterminate,
        ProgressState.Error => SDL_ProgressState.Error,
        ProgressState.Paused => SDL_ProgressState.Paused,
        _ => SDL_ProgressState.None
    };
}
