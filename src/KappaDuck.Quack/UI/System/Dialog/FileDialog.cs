// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Exceptions;
using System.Text;

namespace KappaDuck.Quack.UI.System.Dialog;

/// <summary>
/// Represents a dialog that allows users to select or save files from the file system.
/// </summary>
public static class FileDialog
{
    private static readonly FileDialogOptions Default = new();

    /// <summary>
    /// Opens the file dialog and selects a file.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with <see langword="null"/>.
    /// </remarks>
    /// <param name="options">The file dialog options to configure the dialog.</param>
    /// <param name="callback">The callback to invoke when the user selects a file or cancels the dialog.</param>
    [OverloadResolutionPriority(1)]
    public static unsafe void Open(FileDialogOptions options, Action<FileInfo?> callback)
    {
        using Properties properties = new(options, multiSelect: false);
        Native.SDL_ShowFileDialogWithProperties(SDL_FileDialogType.OpenFile, (_, files, _) => callback(GetFileInfo(files)), nint.Zero, properties.Id);
    }

    /// <summary>
    /// Opens the file dialog and selects a file.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with <see langword="null"/>.
    /// </remarks>
    /// <param name="options">The file dialog options to configure the dialog.</param>
    /// <param name="callback">The callback to invoke when the user selects a file or cancels the dialog.</param>
    public static unsafe void Open(FileDialogOptions options, Action<string?> callback)
    {
        using Properties properties = new(options, multiSelect: false);
        Native.SDL_ShowFileDialogWithProperties(SDL_FileDialogType.OpenFile, (_, files, _) => callback(GetFilePath(files)), nint.Zero, properties.Id);
    }

    /// <summary>
    /// Opens the file dialog and selects a file.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with <see langword="null"/>.
    /// </remarks>
    /// <param name="callback">The callback to invoke when the user selects a file or cancels the dialog.</param>
    [OverloadResolutionPriority(1)]
    public static void Open(Action<FileInfo?> callback) => Open(Default, callback);

    /// <summary>
    /// Opens the file dialog and selects a file.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with <see langword="null"/>.
    /// </remarks>
    /// <param name="callback">The callback to invoke when the user selects a file or cancels the dialog.</param>
    public static void Open(Action<string?> callback) => Open(Default, callback);

    /// <summary>
    /// Opens the file dialog and selects multiple files.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with an empty array.
    /// </remarks>
    /// <param name="options">The file dialog options to configure the dialog.</param>
    /// <param name="callback">The callback to invoke when the user selects files or cancels the dialog.</param>
    [OverloadResolutionPriority(1)]
    public static unsafe void OpenMultiple(FileDialogOptions options, Action<ReadOnlySpan<FileInfo>> callback)
    {
        using Properties properties = new(options, multiSelect: true);
        Native.SDL_ShowFileDialogWithProperties(SDL_FileDialogType.OpenFile, (_, files, _) => callback(GetFileInfos(files)), nint.Zero, properties.Id);
    }

    /// <summary>
    /// Opens the file dialog and selects multiple files.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with an empty array.
    /// </remarks>
    /// <param name="options">The file dialog options to configure the dialog.</param>
    /// <param name="callback">The callback to invoke when the user selects files or cancels the dialog.</param>
    public static unsafe void OpenMultiple(FileDialogOptions options, Action<ReadOnlySpan<string>> callback)
    {
        using Properties properties = new(options, multiSelect: true);
        Native.SDL_ShowFileDialogWithProperties(SDL_FileDialogType.OpenFile, (_, files, _) => callback(GetFilePaths(files)), nint.Zero, properties.Id);
    }

    /// <summary>
    /// Opens the file dialog and selects multiple files.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with an empty array.
    /// </remarks>
    /// <param name="callback">The callback to invoke when the user selects files or cancels the dialog.</param>
    [OverloadResolutionPriority(1)]
    public static void OpenMultiple(Action<ReadOnlySpan<FileInfo>> callback) => OpenMultiple(Default, callback);

    /// <summary>
    /// Opens the file dialog and selects multiple files.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with an empty array.
    /// </remarks>
    /// <param name="callback">The callback to invoke when the user selects files or cancels the dialog.</param>
    public static void OpenMultiple(Action<ReadOnlySpan<string>> callback) => OpenMultiple(Default, callback);

    /// <summary>
    /// Opens the file dialog and saves a file.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with <see langword="null"/>.
    /// </remarks>
    /// <param name="options">The file dialog options to configure the dialog.</param>
    /// <param name="callback">The callback to invoke when the user saves a file or cancels the dialog.</param>
    [OverloadResolutionPriority(1)]
    public static unsafe void Save(FileDialogOptions options, Action<FileInfo?> callback)
    {
        using Properties properties = new(options, multiSelect: false);
        Native.SDL_ShowFileDialogWithProperties(SDL_FileDialogType.SaveFile, (_, files, _) => callback(GetFileInfo(files)), nint.Zero, properties.Id);
    }

    /// <summary>
    /// Opens the file dialog and saves a file.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with <see langword="null"/>.
    /// </remarks>
    /// <param name="options">The file dialog options to configure the dialog.</param>
    /// <param name="callback">The callback to invoke when the user saves a file or cancels the dialog.</param>
    public static unsafe void Save(FileDialogOptions options, Action<string?> callback)
    {
        using Properties properties = new(options, multiSelect: false);
        Native.SDL_ShowFileDialogWithProperties(SDL_FileDialogType.SaveFile, (_, files, _) => callback(GetFilePath(files)), nint.Zero, properties.Id);
    }

    /// <summary>
    /// Opens the file dialog and saves a file.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with <see langword="null"/>.
    /// </remarks>
    /// <param name="callback">The callback to invoke when the user saves a file or cancels the dialog.</param>
    [OverloadResolutionPriority(1)]
    public static void Save(Action<FileInfo?> callback) => Open(Default, callback);

    /// <summary>
    /// Opens the file dialog and saves a file.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with <see langword="null"/>.
    /// </remarks>
    /// <param name="callback">The callback to invoke when the user saves a file or cancels the dialog.</param>
    public static void Save(Action<string?> callback) => Open(Default, callback);

    /// <summary>
    /// Opens the file dialog and saves multiple files.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with an empty array.
    /// </remarks>
    /// <param name="options">The file dialog options to configure the dialog.</param>
    /// <param name="callback">The callback to invoke when the user saves files or cancels the dialog.</param>
    [OverloadResolutionPriority(1)]
    public static unsafe void SaveMultiple(FileDialogOptions options, Action<ReadOnlySpan<FileInfo>> callback)
    {
        using Properties properties = new(options, multiSelect: true);
        Native.SDL_ShowFileDialogWithProperties(SDL_FileDialogType.SaveFile, (_, files, _) => callback(GetFileInfos(files)), nint.Zero, properties.Id);
    }

    /// <summary>
    /// Opens the file dialog and saves multiple files.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with an empty array.
    /// </remarks>
    /// <param name="options">The file dialog options to configure the dialog.</param>
    /// <param name="callback">The callback to invoke when the user saves files or cancels the dialog.</param>
    public static unsafe void SaveMultiple(FileDialogOptions options, Action<ReadOnlySpan<string>> callback)
    {
        using Properties properties = new(options, multiSelect: true);
        Native.SDL_ShowFileDialogWithProperties(SDL_FileDialogType.SaveFile, (_, files, _) => callback(GetFilePaths(files)), nint.Zero, properties.Id);
    }

    /// <summary>
    /// Opens the file dialog and saves multiple files.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with an empty array.
    /// </remarks>
    /// <param name="callback">The callback to invoke when the user saves files or cancels the dialog.</param>
    [OverloadResolutionPriority(1)]
    public static void SaveMultiple(Action<ReadOnlySpan<FileInfo>> callback) => OpenMultiple(Default, callback);

    /// <summary>
    /// Opens the file dialog and saves multiple files.
    /// </summary>
    /// <remarks>
    /// If the user cancels the dialog, the callback will be invoked with an empty array.
    /// </remarks>
    /// <param name="callback">The callback to invoke when the user saves files or cancels the dialog.</param>
    public static void SaveMultiple(Action<ReadOnlySpan<string>> callback) => OpenMultiple(Default, callback);

    private static unsafe FileInfo? GetFileInfo(byte** files)
    {
        QuackNativeException.ThrowIfNull(files);

        if (*files is null)
            return null;

        byte* file = *files;
        string path = Encoding.UTF8.GetString(file, GetLength(file));

        return new FileInfo(path);
    }

    private static unsafe ReadOnlySpan<FileInfo> GetFileInfos(byte** files)
    {
        QuackNativeException.ThrowIfNull(files);

        if (*files is null)
            return [];

        List<FileInfo> info = [];

        while (*files is not null)
        {
            byte* file = *files++;

            string path = Encoding.UTF8.GetString(file, GetLength(file));
            info.Add(new FileInfo(path));
        }

        return CollectionsMarshal.AsSpan(info);
    }

    private static unsafe ReadOnlySpan<string> GetFilePaths(byte** files)
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

    private static unsafe string? GetFilePath(byte** files)
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
        internal Properties(FileDialogOptions options, bool multiSelect)
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
