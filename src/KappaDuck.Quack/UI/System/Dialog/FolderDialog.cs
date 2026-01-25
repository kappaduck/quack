// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.UI.System.Dialog;

/// <summary>
/// Represents a dialog that allows users to select a folder from the file system.
/// </summary>
public static class FolderDialog
{
    private static readonly FolderDialogOptions Default = new();

    /// <summary>
    /// Opens the folder dialog and selects a folder.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with <see langword="null"/>.
    /// </remarks>
    /// <param name="options">The folder dialog options to configure the dialog.</param>
    /// <param name="callback">The callback to invoke when the user selects a folder or cancels the dialog.</param>
    [OverloadResolutionPriority(2)]
    public static void Open(FolderDialogOptions options, Action<DirectoryInfo?> callback) => Native.Dialog.OpenFolder(options, callback);

    /// <summary>
    /// Opens the folder dialog and selects a folder.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with <see langword="null"/>.
    /// </remarks>
    /// <param name="callback">The callback to invoke when the user selects a folder or cancels the dialog.</param>
    [OverloadResolutionPriority(2)]
    public static void Open(Action<DirectoryInfo?> callback) => Open(Default, callback);

    /// <summary>
    /// Opens the folder dialog and selects a folder.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with <see langword="null"/>.
    /// </remarks>
    /// <param name="options">The folder dialog options to configure the dialog.</param>
    /// <param name="callback">The callback to invoke when the user selects a folder or cancels the dialog.</param>
    public static void Open(FolderDialogOptions options, Action<string?> callback) => Native.Dialog.OpenFolder(options, callback);

    /// <summary>
    /// Opens the folder dialog and selects a folder.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with <see langword="null"/>.
    /// </remarks>
    /// <param name="callback">The callback to invoke when the user selects a folder or cancels the dialog.</param>
    public static void Open(Action<string?> callback) => Open(Default, callback);

    /// <summary>
    /// Opens the folder dialog and selects multiple folders.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with an empty array.
    /// </remarks>
    /// <param name="options">The folder dialog options to configure the dialog.</param>
    /// <param name="callback">The callback to invoke when the user selects multiple folders or cancels the dialog.</param>
    [OverloadResolutionPriority(1)]
    public static void Open(FolderDialogOptions options, Action<ReadOnlySpan<DirectoryInfo>> callback) => Native.Dialog.OpenFolders(options, callback);

    /// <summary>
    /// Opens the folder dialog and selects multiple folders.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with an empty array.
    /// </remarks>
    /// <param name="callback">The callback to invoke when the user selects multiple folders or cancels the dialog.</param>
    [OverloadResolutionPriority(1)]
    public static void Open(Action<ReadOnlySpan<DirectoryInfo>> callback) => Open(Default, callback);

    /// <summary>
    /// Opens the folder dialog and selects multiple folders.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with an empty array.
    /// </remarks>
    /// <param name="options">The folder dialog options to configure the dialog.</param>
    /// <param name="callback">The callback to invoke when the user selects multiple folders or cancels the dialog.</param>
    public static void Open(FolderDialogOptions options, Action<ReadOnlySpan<string>> callback) => Native.Dialog.OpenFolders(options, callback);

    /// <summary>
    /// Opens the folder dialog and selects multiple folders.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with an empty array.
    /// </remarks>
    /// <param name="callback">The callback to invoke when the user selects multiple folders or cancels the dialog.</param>
    public static void Open(Action<ReadOnlySpan<string>> callback) => Open(Default, callback);
}
