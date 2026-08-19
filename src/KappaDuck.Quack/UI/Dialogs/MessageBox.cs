// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Interop.SDL.Primitives;
using KappaDuck.Quack.Video.Imaging;
using KappaDuck.Quack.Windowing;

namespace KappaDuck.Quack.UI.Dialogs;

/// <summary>
/// Displays native, modal message boxes.
/// </summary>
/// <remarks>
/// A message box blocks the calling thread until the user dismisses it and is drawn by the operating
/// system where possible. It can be shown even before the engine is initialized, which makes it useful
/// for reporting startup failures. Show a message box only from the thread that created its parent window,
/// or from the main thread when it has no parent.
/// </remarks>
public static class MessageBox
{
    /// <summary>
    /// Displays a simple message box with a single confirmation button.
    /// </summary>
    /// <param name="title">The title shown in the message box.</param>
    /// <param name="message">The message shown below the title.</param>
    /// <param name="severity">The severity of the message box.</param>
    /// <param name="parent">The window the message box is modal for, or <see langword="null"/> for no parent.</param>
    /// <exception cref="QuackInteropException">Failed to show the message box.</exception>
    public static void Show(string title, string message, MessageBoxSeverity severity = MessageBoxSeverity.Information, Window? parent = null)
    {
        unsafe
        {
            SDL_Window* window = parent?.NativeHandle;
            SDLThrowHelper.ThrowIfFailed(SDL3.ShowSimpleMessageBox(ToFlags(severity), title, message, window));
        }
    }

    /// <summary>
    /// Displays a customizable message box and returns the button the user activated.
    /// </summary>
    /// <param name="options">The options that configure the message box.</param>
    /// <returns>
    /// The button the user activated, or <see langword="null"/> if the message box was closed
    /// without a selection.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException"><see cref="MessageBoxOptions.Buttons"/> is empty.</exception>
    /// <exception cref="QuackInteropException">Failed to show the message box.</exception>
    public static MessageBoxButton? Show(MessageBoxOptions options)
    {
        ArgumentOutOfRangeException.ThrowIfZero(options.Buttons.Count);

        unsafe
        {
            int count = options.Buttons.Count;

            byte* title = Utf8StringMarshaller.ConvertToUnmanaged(options.Title);
            byte* message = Utf8StringMarshaller.ConvertToUnmanaged(options.Message);
            SDL_MessageBoxButtonData* buttons = (SDL_MessageBoxButtonData*)NativeMemory.AllocZeroed((nuint)count, (nuint)sizeof(SDL_MessageBoxButtonData));

            try
            {
                for (int i = 0; i < count; i++)
                {
                    MessageBoxButton button = options.Buttons[i];

                    buttons[i] = new SDL_MessageBoxButtonData
                    {
                        State = ToFlags(button),
                        ButtonId = button.Id,
                        Text = Utf8StringMarshaller.ConvertToUnmanaged(button.Text)
                    };
                }

                SDL_MessageBoxColorScheme scheme = default;
                SDL_MessageBoxColorScheme* colorScheme = null;

                if (options.ColorScheme is { } colors)
                {
                    scheme = ToColorScheme(colors);
                    colorScheme = &scheme;
                }

                SDL_MessageBoxData data = new()
                {
                    State = ToFlags(options.Severity) | ToFlags(options.ButtonOrder),
                    Window = options.Parent?.NativeHandle,
                    Title = title,
                    Message = message,
                    ButtonCount = count,
                    Buttons = buttons,
                    ColorScheme = colorScheme
                };

                SDLThrowHelper.ThrowIfFailed(SDL3.ShowMessageBox(in data, out int buttonId));

                foreach (MessageBoxButton button in options.Buttons)
                {
                    if (button.Id == buttonId)
                        return button;
                }

                return null;
            }
            finally
            {
                for (int i = 0; i < count; i++)
                    Utf8StringMarshaller.Free(buttons[i].Text);

                NativeMemory.Free(buttons);

                Utf8StringMarshaller.Free(title);
                Utf8StringMarshaller.Free(message);
            }
        }
    }

    private static SDL_MessageBoxColor ToColor(Color color) => new()
    {
        R = color.R,
        G = color.G,
        B = color.B
    };

    private static SDL_MessageBoxColorScheme ToColorScheme(MessageBoxColorScheme scheme) => new()
    {
        Background = ToColor(scheme.Background),
        Text = ToColor(scheme.Text),
        ButtonBorder = ToColor(scheme.ButtonBorder),
        ButtonBackground = ToColor(scheme.ButtonBackground),
        ButtonSelected = ToColor(scheme.ButtonSelected)
    };

    private static SDL_MessageBoxButtonState ToFlags(MessageBoxButton button)
    {
        SDL_MessageBoxButtonState flags = SDL_MessageBoxButtonState.None;

        if (button.IsReturnDefault)
            flags |= SDL_MessageBoxButtonState.ReturnKeyDefault;

        if (button.IsEscapeDefault)
            flags |= SDL_MessageBoxButtonState.EscapeKeyDefault;

        return flags;
    }

    private static SDL_MessageBoxState ToFlags(MessageBoxButtonOrder order) => order switch
    {
        MessageBoxButtonOrder.RightToLeft => SDL_MessageBoxState.ButtonsRightToLeft,
        MessageBoxButtonOrder.LeftToRight => SDL_MessageBoxState.ButtonsLeftToRight,
        _ => SDL_MessageBoxState.ButtonsLeftToRight
    };

    private static SDL_MessageBoxState ToFlags(MessageBoxSeverity severity) => severity switch
    {
        MessageBoxSeverity.Information => SDL_MessageBoxState.Information,
        MessageBoxSeverity.Warning => SDL_MessageBoxState.Warning,
        MessageBoxSeverity.Error => SDL_MessageBoxState.Error,
        MessageBoxSeverity.None => 0,
        _ => 0
    };
}
