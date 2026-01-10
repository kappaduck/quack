// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Windows;

namespace KappaDuck.Quack.System.UI.Dialogs;

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
    /// <exception cref="QuackNativeException">Throw when failed to display the error message box.</exception>
    public static void ShowError(string title, string message, Window? parent = null)
        => Show(title, message, MessageBoxLevel.Error, parent);

    /// <summary>
    /// Displays an error message box.
    /// </summary>
    /// <param name="options">The message box options.</param>
    /// <returns>The <see cref="MessageBoxButton.Id"/> which was pressed from the user in the message box.</returns>
    /// <exception cref="QuackNativeException">Throw when failed to display the error message box.</exception>
    public static int ShowError(MessageBoxOptions options) => Show(options, MessageBoxLevel.Error);

    /// <summary>
    /// Displays a warning message box.
    /// </summary>
    /// <param name="title">The message box's title.</param>
    /// <param name="message">The message box's message.</param>
    /// <param name="parent">The parent window, or <see langword="null"/> for no parent.</param>
    /// <exception cref="QuackNativeException">Throw when failed to display the warning message box.</exception>
    public static void ShowWarning(string title, string message, Window? parent = null)
        => Show(title, message, MessageBoxLevel.Warning, parent);

    /// <summary>
    /// Displays a warning message box.
    /// </summary>
    /// <param name="options">The message box options.</param>
    /// <returns>The <see cref="MessageBoxButton.Id"/> which was pressed from the user in the message box.</returns>
    /// <exception cref="QuackNativeException">Throw when failed to display the warning message box.</exception>
    public static int ShowWarning(MessageBoxOptions options) => Show(options, MessageBoxLevel.Warning);

    /// <summary>
    /// Displays an information message box.
    /// </summary>
    /// <param name="title">The message box's title.</param>
    /// <param name="message">The message box's message.</param>
    /// <param name="parent">The parent window, or <see langword="null"/> for no parent.</param>
    /// <exception cref="QuackNativeException">Throw when failed to display the information message box.</exception>
    public static void ShowInformation(string title, string message, Window? parent = null)
        => Show(title, message, MessageBoxLevel.Information, parent);

    /// <summary>
    /// Displays an information message box.
    /// </summary>
    /// <param name="options">The message box options.</param>
    /// <returns>The <see cref="MessageBoxButton.Id"/> which was pressed from the user in the message box.</returns>
    /// <exception cref="QuackNativeException">Throw when failed to display the information message box.</exception>
    public static int ShowInformation(MessageBoxOptions options) => Show(options, MessageBoxLevel.Information);

    private static void Show(string title, string message, MessageBoxLevel level, Window? parent = null)
        => QuackNativeException.ThrowIfFailed(Native.SDL_ShowSimpleMessageBox((uint)level, title, message, parent?.NativeHandle ?? SDL_Window.Zero));

    private static int Show(MessageBoxOptions options, MessageBoxLevel level)
    {
        options.Level = level;

        QuackNativeException.ThrowIfFailed(Native.SDL_ShowMessageBox(options, out int buttonId));
        return buttonId;
    }
}
