// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.System.UI.Dialogs;

/// <summary>
/// Represents a button displayed in a message box.
/// </summary>
/// <param name="Id"> Gets or sets the unique identifier for the button. </param>
/// <param name="Text"> Gets or sets the button text. </param>
public sealed record MessageBoxButton(int Id, string Text)
{
    /// <summary>
    /// Gets or sets the default key that will trigger the default button.
    /// </summary>
    public MessageBoxDefaultKey DefaultKey { get; set; } = MessageBoxDefaultKey.Return;
}
