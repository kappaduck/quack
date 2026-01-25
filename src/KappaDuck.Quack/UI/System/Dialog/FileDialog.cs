// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.UI.System.Dialog;

/// <summary>
/// Represents a dialog that allows users to select or save files from the file system.
/// </summary>
public static class FileDialog
{
    /// <summary>
    /// Opens the file dialog and selects a file.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with <see langword="null"/>.
    /// </remarks>
    /// <param name="options">The file dialog options to configure the dialog.</param>
    /// <param name="callback">The callback to invoke when the user selects a file or cancels the dialog.</param>
    [OverloadResolutionPriority(2)]
    public static void Open(FileDialogOptions options, Action<FileInfo?> callback) => Native.Dialog.OpenFile(options, callback);

    /// <summary>
    /// Opens the file dialog and selects a file.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with <see langword="null"/>.
    /// </remarks>
    /// <param name="callback">The callback to invoke when the user selects a file or cancels the dialog.</param>
    [OverloadResolutionPriority(2)]
    public static void Open(Action<FileInfo?> callback) => Native.Dialog.OpenFile(callback);

    /// <summary>
    /// Opens the file dialog and selects a file.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with <see langword="null"/>.
    /// </remarks>
    /// <param name="options">The file dialog options to configure the dialog.</param>
    /// <param name="callback">The callback to invoke when the user selects a file or cancels the dialog.</param>
    public static void Open(FileDialogOptions options, Action<string?> callback) => Native.Dialog.OpenFile(options, callback);

    /// <summary>
    /// Opens the file dialog and selects a file.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with <see langword="null"/>.
    /// </remarks>
    /// <param name="callback">The callback to invoke when the user selects a file or cancels the dialog.</param>
    public static void Open(Action<string?> callback) => Native.Dialog.OpenFile(callback);

    /// <summary>
    /// Opens the file dialog and selects multiple files.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with an empty array.
    /// </remarks>
    /// <param name="options">The file dialog options to configure the dialog.</param>
    /// <param name="callback">The callback to invoke when the user selects multiple files or cancels the dialog.</param>
    [OverloadResolutionPriority(1)]
    public static void Open(FileDialogOptions options, Action<ReadOnlySpan<FileInfo>> callback) => Native.Dialog.OpenFiles(options, callback);

    /// <summary>
    /// Opens the file dialog and selects multiple files.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with an empty array.
    /// </remarks>
    /// <param name="callback">The callback to invoke when the user selects multiple files or cancels the dialog.</param>
    [OverloadResolutionPriority(1)]
    public static void Open(Action<ReadOnlySpan<FileInfo>> callback) => Native.Dialog.OpenFiles(callback);

    /// <summary>
    /// Opens the file dialog and selects multiple files.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with an empty array.
    /// </remarks>
    /// <param name="options">The file dialog options to configure the dialog.</param>
    /// <param name="callback">The callback to invoke when the user selects multiple files or cancels the dialog.</param>
    public static void Open(FileDialogOptions options, Action<ReadOnlySpan<string>> callback) => Native.Dialog.OpenFiles(options, callback);

    /// <summary>
    /// Opens the file dialog and selects multiple files.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with an empty array.
    /// </remarks>
    /// <param name="callback">The callback to invoke when the user selects multiple files or cancels the dialog.</param>
    public static void Open(Action<ReadOnlySpan<string>> callback) => Native.Dialog.OpenFiles(callback);

    /// <summary>
    /// Opens the file dialog and saves a file.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with <see langword="null"/>.
    /// </remarks>
    /// <param name="options">The file dialog options to configure the dialog.</param>
    /// <param name="callback">The callback to invoke when the user save a file or cancels the dialog.</param>
    [OverloadResolutionPriority(2)]
    public static void Save(FileDialogOptions options, Action<FileInfo?, FileDialogFilter?> callback) => Native.Dialog.SaveFile(options, callback);

    /// <summary>
    /// Opens the file dialog and saves a file.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with <see langword="null"/>.
    /// </remarks>
    /// <param name="callback">The callback to invoke when the user save a file or cancels the dialog.</param>
    [OverloadResolutionPriority(2)]
    public static void Save(Action<FileInfo?> callback) => Native.Dialog.SaveFile(callback);

    /// <summary>
    /// Opens the file dialog and saves a file.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with <see langword="null"/>.
    /// </remarks>
    /// <param name="options">The file dialog options to configure the dialog.</param>
    /// <param name="callback">The callback to invoke when the user save a file or cancels the dialog.</param>
    public static void Save(FileDialogOptions options, Action<string?, FileDialogFilter?> callback) => Native.Dialog.SaveFile(options, callback);

    /// <summary>
    /// Opens the file dialog and saves a file.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with <see langword="null"/>.
    /// </remarks>
    /// <param name="callback">The callback to invoke when the user save a file or cancels the dialog.</param>
    public static void Save(Action<string?> callback) => Native.Dialog.SaveFile(callback);

    /// <summary>
    /// Opens the file dialog and saves multiple files.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with an empty array.
    /// </remarks>
    /// <param name="options">The file dialog options to configure the dialog.</param>
    /// <param name="callback">The callback to invoke when the user saves files or cancels the dialog.</param>
    [OverloadResolutionPriority(1)]
    public static void Save(FileDialogOptions options, Action<ReadOnlySpan<FileInfo>, FileDialogFilter?> callback) => Native.Dialog.SaveFiles(options, callback);

    /// <summary>
    /// Opens the file dialog and saves multiple files.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with an empty array.
    /// </remarks>
    /// <param name="callback">The callback to invoke when the user saves files or cancels the dialog.</param>
    [OverloadResolutionPriority(1)]
    public static void Save(Action<ReadOnlySpan<FileInfo>> callback) => Native.Dialog.SaveFiles(callback);

    /// <summary>
    /// Opens the file dialog and saves multiple files.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with an empty array.
    /// </remarks>
    /// <param name="options">The file dialog options to configure the dialog.</param>
    /// <param name="callback">The callback to invoke when the user saves files or cancels the dialog.</param>
    public static void Save(FileDialogOptions options, Action<ReadOnlySpan<string>, FileDialogFilter?> callback) => Native.Dialog.SaveFiles(options, callback);

    /// <summary>
    /// Opens the file dialog and saves multiple files.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with an empty array.
    /// </remarks>
    /// <param name="callback">The callback to invoke when the user saves files or cancels the dialog.</param>
    public static void Save(Action<ReadOnlySpan<string>> callback) => Native.Dialog.SaveFiles(callback);
}
