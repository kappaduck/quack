// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Exceptions;
using System.Text;

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
    [OverloadResolutionPriority(1)]
    public static unsafe void Open(FolderDialogOptions options, Action<DirectoryInfo?> callback)
    {
        using Properties properties = new(options, multiSelect: false);
        Native.SDL_ShowFileDialogWithProperties(SDL_FileDialogType.OpenFolder, (_, folders, _) => callback(GetFolder(folders)), nint.Zero, properties.Id);
    }

    /// <summary>
    /// Opens the folder dialog and selects a folder.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with <see langword="null"/>.
    /// </remarks>
    /// <param name="options">The folder dialog options to configure the dialog.</param>
    /// <param name="callback">The callback to invoke when the user selects a folder or cancels the dialog.</param>
    public static unsafe void Open(FolderDialogOptions options, Action<string?> callback)
    {
        using Properties properties = new(options, multiSelect: false);
        Native.SDL_ShowFileDialogWithProperties(SDL_FileDialogType.OpenFolder, (_, folders, _) => callback(GetFolderPath(folders)), nint.Zero, properties.Id);
    }

    /// <summary>
    /// Opens the folder dialog and selects a folder.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with <see langword="null"/>.
    /// </remarks>
    /// <param name="callback">The callback to invoke when the user selects a folder or cancels the dialog.</param>
    [OverloadResolutionPriority(1)]
    public static void Open(Action<DirectoryInfo?> callback) => Open(Default, callback);

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
    public static unsafe void OpenMultiple(FolderDialogOptions options, Action<ReadOnlySpan<DirectoryInfo>> callback)
    {
        using Properties properties = new(options, multiSelect: true);
        Native.SDL_ShowFileDialogWithProperties(SDL_FileDialogType.OpenFolder, (_, folders, _) => callback(GetFolders(folders)), nint.Zero, properties.Id);
    }

    /// <summary>
    /// Opens the folder dialog and selects multiple folders.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with an empty array.
    /// </remarks>
    /// <param name="options">The folder dialog options to configure the dialog.</param>
    /// <param name="callback">The callback to invoke when the user selects multiple folders or cancels the dialog.</param>
    public static unsafe void OpenMultiple(FolderDialogOptions options, Action<ReadOnlySpan<string>> callback)
    {
        using Properties properties = new(options, multiSelect: true);
        Native.SDL_ShowFileDialogWithProperties(SDL_FileDialogType.OpenFolder, (_, folders, _) => callback(GetFolderPaths(folders)), nint.Zero, properties.Id);
    }

    /// <summary>
    /// Opens the folder dialog and selects multiple folders.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with an empty array.
    /// </remarks>
    /// <param name="callback">The callback to invoke when the user selects multiple folders or cancels the dialog.</param>
    [OverloadResolutionPriority(1)]
    public static void OpenMultiple(Action<ReadOnlySpan<DirectoryInfo>> callback) => OpenMultiple(Default, callback);

    /// <summary>
    /// Opens the folder dialog and selects multiple folders.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with an empty array.
    /// </remarks>
    /// <param name="callback">The callback to invoke when the user selects multiple folders or cancels the dialog.</param>
    public static void OpenMultiple(Action<ReadOnlySpan<string>> callback) => OpenMultiple(Default, callback);

    private static unsafe DirectoryInfo? GetFolder(byte** folders)
    {
        QuackNativeException.ThrowIfNull(folders);

        if (*folders is null)
            return null;

        byte* file = *folders;
        string path = Encoding.UTF8.GetString(file, GetLength(file));

        return new DirectoryInfo(path);
    }

    private static unsafe ReadOnlySpan<DirectoryInfo> GetFolders(byte** files)
    {
        QuackNativeException.ThrowIfNull(files);

        if (*files is null)
            return [];

        List<DirectoryInfo> info = [];

        while (*files is not null)
        {
            byte* file = *files++;

            string path = Encoding.UTF8.GetString(file, GetLength(file));
            info.Add(new DirectoryInfo(path));
        }

        return CollectionsMarshal.AsSpan(info);
    }

    private static unsafe ReadOnlySpan<string> GetFolderPaths(byte** files)
    {
        QuackNativeException.ThrowIfNull(files);

        if (*files is null)
            return [];

        List<string> paths = [];

        while (*files is not null)
        {
            byte* file = *files++;

            string path = Encoding.UTF8.GetString(file, GetLength(file));
            paths.Add(path);
        }

        return CollectionsMarshal.AsSpan(paths);
    }

    private static unsafe string? GetFolderPath(byte** files)
    {
        QuackNativeException.ThrowIfNull(files);

        if (*files is null)
            return null;

        byte* path = *files;
        return Encoding.UTF8.GetString(path, GetLength(path));
    }

    private static unsafe int GetLength(byte* file)
    {
        int length = 0;

        while (*file++ != 0)
            length++;

        return length;
    }

    private sealed class Properties : Native.Properties
    {
        internal Properties(FolderDialogOptions options, bool multiSelect)
        {
            if (options.Title is not null)
                Set("SDL.filedialog.title", options.Title);

            if (options.AcceptLabel is not null)
                Set("SDL.filedialog.accept", options.AcceptLabel);

            if (options.CancelLabel is not null)
                Set("SDL.filedialog.cancel", options.CancelLabel);

            if (options.Location is not null)
                Set("SDL.filedialog.location", options.Location);

            if (options.Window is not null)
                Set("SDL.filedialog.window", options.Window.NativeHandle);

            if (multiSelect)
                Set("SDL.filedialog.many", true);
        }
    }
}

//#define SDL_PROP_FILE_DIALOG_FILTERS_POINTER     "SDL.filedialog.filters"
//#define SDL_PROP_FILE_DIALOG_NFILTERS_NUMBER     "SDL.filedialog.nfilters"
