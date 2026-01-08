// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Windows;

namespace KappaDuck.Quack.System.UI;

/// <summary>
/// Represents a native message box that can be displayed to the user.
/// </summary>
public static class MessageBox
{
    /// <summary>
    /// Displays an error message box.
    /// </summary>
    /// <param name="title">The message box's title.</param>
    /// <param name="message">The message box's message.</param>
    /// <param name="parent">The parent window, or <see langword="null"/> for no parent.</param>
    public static void ShowError(string title, string message, Window? parent = null)
        => Show(title, message, Level.Error, parent);

    /// <summary>
    /// Displays a warning message box.
    /// </summary>
    /// <param name="title">The message box's title.</param>
    /// <param name="message">The message box's message.</param>
    /// <param name="parent">The parent window, or <see langword="null"/> for no parent.</param>
    public static void ShowWarning(string title, string message, Window? parent = null)
        => Show(title, message, Level.Warning, parent);

    /// <summary>
    /// Displays an information message box.
    /// </summary>
    /// <param name="title">The message box's title.</param>
    /// <param name="message">The message box's message.</param>
    /// <param name="parent">The parent window, or <see langword="null"/> for no parent.</param>
    public static void ShowInformation(string title, string message, Window? parent = null)
        => Show(title, message, Level.Information, parent);

    private static void Show(string title, string message, Level level, Window? parent = null)
        => Native.SDL_ShowSimpleMessageBox((uint)level, title, message, parent?.NativeHandle ?? SDL_Window.Null);

    private enum Level : uint
    {
        Error = 0x00000010u,
        Warning = 0x00000020u,
        Information = 0x00000040u
    }
}
