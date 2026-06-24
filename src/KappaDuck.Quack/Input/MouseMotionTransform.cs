// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Windows;

namespace KappaDuck.Quack.Input;

/// <summary>
/// A callback used to transform relative mouse motion before it is delivered to the application.
/// </summary>
/// <remarks>
/// The callback runs on the thread that processes mouse events. Adjust <paramref name="x"/> and
/// <paramref name="y"/> in place to change the reported motion.
/// </remarks>
/// <param name="window">The window the motion is targeting, or <see langword="null"/> if it cannot be resolved.</param>
/// <param name="mouseId">The id of the mouse that generated the motion.</param>
/// <param name="x">The horizontal relative motion, which can be modified in place.</param>
/// <param name="y">The vertical relative motion, which can be modified in place.</param>
public delegate void MouseMotionTransform(Window? window, uint mouseId, ref float x, ref float y);
