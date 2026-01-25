// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Windows;

namespace KappaDuck.Quack.UI.System.Dialog;

internal interface IDialogOptions
{
    string? Title { get; }

    string? AcceptLabel { get; }

    string? CancelLabel { get; }

    string? Location { get; }

    WindowBase? Window { get; }
}
