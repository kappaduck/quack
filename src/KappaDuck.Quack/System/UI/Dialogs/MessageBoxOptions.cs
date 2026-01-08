// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.SDL.Marshalling;
using KappaDuck.Quack.Windows;

namespace KappaDuck.Quack.System.UI.Dialogs;

/// <summary>
/// Represents the configuration options for a message box.
/// </summary>
/// <param name="Title">The message box's title.</param>
/// <param name="Message">The message box's message.</param>
[NativeMarshalling(typeof(SDL_MessageBoxDataMarshaller))]
public sealed record MessageBoxOptions(string Title, string Message)
{
    /// <summary>
    /// Creates a MessageBox options
    /// </summary>
    /// <param name="title">The message box's title.</param>
    /// <param name="message">The message box's message.</param>
    /// <param name="buttons">The buttons to display in the message box.</param>
    public MessageBoxOptions(string title, string message, params ReadOnlySpan<MessageBoxButton> buttons) : this(title, message)
        => Buttons = buttons.ToArray();

    /// <summary>
    /// Gets or sets the alignment of the buttons displayed in the message box.
    /// </summary>
    public MessageBoxButtonAlignment Alignment { get; set; } = MessageBoxButtonAlignment.Right;

    /// <summary>
    /// Gets the collection of buttons displayed in the message buttons.
    /// </summary>
    public ICollection<MessageBoxButton> Buttons { get; } = [];

    /// <summary>
    /// Gets or sets the parent window or <see langword="null"/> for no parent.
    /// </summary>
    public Window? Parent { get; set; }

    internal MessageBoxLevel Level { get; set; }
}
