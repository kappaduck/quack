// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Interop.SDL.Primitives;
using KappaDuck.Quack.Windowing;

namespace KappaDuck.Quack.UI.Dialogs;

/// <summary>
/// Displays native file and folder selection dialogs.
/// </summary>
/// <remarks>
/// Every method returns immediately and completes its task once the user makes a choice. A canceled dialog
/// completes with <see langword="null"/> or an empty list rather than throwing. Show a dialog only from the
/// main thread.
/// <para>
/// A dialog is driven by your event loop, so keep pumping events (for example by polling window events) while
/// it is open. On Linux the task never completes otherwise.
/// </para>
/// <para>
/// For that reason, do not <see langword="await"/> a dialog directly inside your event loop except you using an async loop managed by the engine.
/// The loop is what pumps the events the dialog waits on, so awaiting it there freezes the application. Instead, start the dialog
/// by doing a fire and forget. The started task runs on its own, so any failure goes unobserved. Wrap the body in a
/// <see langword="try"/>/<see langword="catch"/> if you need to react to errors.
/// </para>
/// </remarks>
public static class FileDialog
{
    private const string FileDialogAcceptString = "SDL.filedialog.accept";
    private const string FileDialogCancelString = "SDL.filedialog.cancel";
    private const string FileDialogFiltersPointer = "SDL.filedialog.filters";
    private const string FileDialogLocationString = "SDL.filedialog.location";
    private const string FileDialogManyBoolean = "SDL.filedialog.many";
    private const string FileDialogNFiltersNumber = "SDL.filedialog.nfilters";
    private const string FileDialogTitleString = "SDL.filedialog.title";
    private const string FileDialogWindowPointer = "SDL.filedialog.window";

    /// <summary>
    /// Displays a dialog that lets the user select a single existing file, delivering the result to a callback.
    /// </summary>
    /// <param name="options">The options that configure the dialog, or <see langword="null"/> for the defaults.</param>
    /// <param name="callback">Invoked with the chosen file, or <see langword="null"/> if the user canceled or the dialog could not be shown.</param>
    /// <remarks>
    /// The callback may run on a background thread, so marshal back to the main thread before touching the window or renderer.
    /// Use <see cref="OpenFileAsync(OpenFileDialogOptions?)"/> instead if you need to handle failures explicitly.
    /// </remarks>
    public static void OpenFile(OpenFileDialogOptions? options, Action<string?> callback)
        => Show(SDL_FileDialogType.OpenFile, options?.Filters ?? [], options?.Location, options?.Parent, options?.Title, options?.AcceptLabel, options?.CancelLabel, false, files => callback(files is { Length: 1 } ? files[0] : null), _ => callback(null));

    /// <summary>
    /// Displays a dialog that lets the user select a single existing file.
    /// </summary>
    /// <param name="options">The options that configure the dialog.</param>
    /// <returns>The chosen file, or <see langword="null"/> if the user canceled.</returns>
    /// <exception cref="QuackInteropException">The dialog failed to open or complete.</exception>
    public static async Task<string?> OpenFileAsync(OpenFileDialogOptions? options = null)
    {
        string[] files = await ShowAsync(SDL_FileDialogType.OpenFile, options?.Filters ?? [], options?.Location, options?.Parent, options?.Title, options?.AcceptLabel, options?.CancelLabel, false).ConfigureAwait(false);
        return files is { Length: 1 } ? files[0] : null;
    }

    /// <summary>
    /// Displays a dialog that lets the user select one or more existing files, delivering the result to a callback.
    /// </summary>
    /// <param name="options">The options that configure the dialog, or <see langword="null"/> for the defaults.</param>
    /// <param name="callback">Invoked with the chosen files, or an empty list if the user canceled or the dialog could not be shown.</param>
    /// <remarks>
    /// The callback may run on a background thread, so marshal back to the main thread before touching the window or renderer.
    /// Use <see cref="OpenFilesAsync(OpenFileDialogOptions?)"/> instead if you need to handle failures explicitly.
    /// </remarks>
    public static void OpenFiles(OpenFileDialogOptions? options, Action<IReadOnlyList<string>> callback)
        => Show(SDL_FileDialogType.OpenFile, options?.Filters ?? [], options?.Location, options?.Parent, options?.Title, options?.AcceptLabel, options?.CancelLabel, true, callback, _ => callback([]));

    /// <summary>
    /// Displays a dialog that lets the user select one or more existing files.
    /// </summary>
    /// <param name="options">The options that configure the dialog.</param>
    /// <returns>The chosen files, or an empty list if the user canceled.</returns>
    /// <exception cref="QuackInteropException">The dialog failed to open or complete.</exception>
    public static async Task<IReadOnlyList<string>> OpenFilesAsync(OpenFileDialogOptions? options = null)
        => await ShowAsync(SDL_FileDialogType.OpenFile, options?.Filters ?? [], options?.Location, options?.Parent, options?.Title, options?.AcceptLabel, options?.CancelLabel, true).ConfigureAwait(false);

    /// <summary>
    /// Displays a dialog that lets the user select a single folder, delivering the result to a callback.
    /// </summary>
    /// <param name="options">The options that configure the dialog, or <see langword="null"/> for the defaults.</param>
    /// <param name="callback">Invoked with the chosen folder, or <see langword="null"/> if the user canceled or the dialog could not be shown.</param>
    /// <remarks>
    /// The callback may run on a background thread, so marshal back to the main thread before touching the window or renderer.
    /// Use <see cref="OpenFolderAsync(OpenFolderDialogOptions?)"/> instead if you need to handle failures explicitly.
    /// </remarks>
    public static void OpenFolder(OpenFolderDialogOptions? options, Action<string?> callback)
        => Show(SDL_FileDialogType.OpenFolder, [], options?.Location, options?.Parent, options?.Title, options?.AcceptLabel, options?.CancelLabel, false, files => callback(files is { Length: 1 } ? files[0] : null), _ => callback(null));

    /// <summary>
    /// Displays a dialog that lets the user select a single folder.
    /// </summary>
    /// <param name="options">The options that configure the dialog.</param>
    /// <returns>The chosen folder, or <see langword="null"/> if the user canceled.</returns>
    /// <exception cref="QuackInteropException">The dialog failed to open or complete.</exception>
    public static async Task<string?> OpenFolderAsync(OpenFolderDialogOptions? options = null)
    {
        string[] folders = await ShowAsync(SDL_FileDialogType.OpenFolder, [], options?.Location, options?.Parent, options?.Title, options?.AcceptLabel, options?.CancelLabel, false).ConfigureAwait(false);
        return folders is { Length: 1 } ? folders[0] : null;
    }

    /// <summary>
    /// Displays a dialog that lets the user select one or more folders, delivering the result to a callback.
    /// </summary>
    /// <param name="options">The options that configure the dialog, or <see langword="null"/> for the defaults.</param>
    /// <param name="callback">Invoked with the chosen folders, or an empty list if the user canceled or the dialog could not be shown.</param>
    /// <remarks>
    /// The callback may run on a background thread, so marshal back to the main thread before touching the window or renderer.
    /// Use <see cref="OpenFoldersAsync(OpenFolderDialogOptions?)"/> instead if you need to handle failures explicitly.
    /// </remarks>
    public static void OpenFolders(OpenFolderDialogOptions? options, Action<IReadOnlyList<string>> callback)
        => Show(SDL_FileDialogType.OpenFolder, [], options?.Location, options?.Parent, options?.Title, options?.AcceptLabel, options?.CancelLabel, true, callback, _ => callback([]));

    /// <summary>
    /// Displays a dialog that lets the user select one or more folders.
    /// </summary>
    /// <param name="options">The options that configure the dialog.</param>
    /// <returns>The chosen folders, or an empty list if the user canceled.</returns>
    /// <exception cref="QuackInteropException">The dialog failed to open or complete.</exception>
    public static async Task<IReadOnlyList<string>> OpenFoldersAsync(OpenFolderDialogOptions? options = null)
        => await ShowAsync(SDL_FileDialogType.OpenFolder, [], options?.Location, options?.Parent, options?.Title, options?.AcceptLabel, options?.CancelLabel, true).ConfigureAwait(false);

    /// <summary>
    /// Displays a dialog that lets the user choose a new or existing file to save to, delivering the result to a callback.
    /// </summary>
    /// <param name="options">The options that configure the dialog, or <see langword="null"/> for the defaults.</param>
    /// <param name="callback">Invoked with the chosen file, or <see langword="null"/> if the user canceled or the dialog could not be shown.</param>
    /// <remarks>
    /// The callback may run on a background thread, so marshal back to the main thread before touching the window or renderer.
    /// Use <see cref="SaveFileAsync(SaveFileDialogOptions?)"/> instead if you need to handle failures explicitly.
    /// </remarks>
    public static void SaveFile(SaveFileDialogOptions? options, Action<string?> callback)
        => Show(SDL_FileDialogType.SaveFile, options?.Filters ?? [], options?.Location, options?.Parent, options?.Title, options?.AcceptLabel, options?.CancelLabel, false, files => callback(files is { Length: 1 } ? files[0] : null), _ => callback(null));

    /// <summary>
    /// Displays a dialog that lets the user choose a new or existing file to save to.
    /// </summary>
    /// <param name="options">The options that configure the dialog.</param>
    /// <returns>The chosen file, or <see langword="null"/> if the user canceled.</returns>
    /// <exception cref="QuackInteropException">The dialog failed to open or complete.</exception>
    public static async Task<string?> SaveFileAsync(SaveFileDialogOptions? options = null)
    {
        string[] files = await ShowAsync(SDL_FileDialogType.SaveFile, options?.Filters ?? [], options?.Location, options?.Parent, options?.Title, options?.AcceptLabel, options?.CancelLabel, false).ConfigureAwait(false);
        return files is { Length: 1 } ? files[0] : null;
    }

    private static Task<string[]> ShowAsync(SDL_FileDialogType type, IReadOnlyList<FileDialogFilter> filters, string? location, Window? parent, string? title, string? accept, string? cancel, bool allowMany)
    {
        TaskCompletionSource<string[]> source = new(TaskCreationOptions.RunContinuationsAsynchronously);

        Show(type, filters, location, parent, title, accept, cancel, allowMany, source.SetResult, source.SetException);
        return source.Task;
    }

    private static void Show(SDL_FileDialogType type, IReadOnlyList<FileDialogFilter> filters, string? location, Window? parent, string? title, string? accept, string? cancel, bool allowMany, Action<string[]> success, Action<Exception> error)
    {
        Request request = new(success, error);

        request.SetFilters(filters);
        GCHandle handle = GCHandle.Alloc(request);

        unsafe
        {
            try
            {
                using Properties properties = new();

                if (request.Filters is not null)
                {
                    properties.Set(FileDialogFiltersPointer, request.Filters);
                    properties.Set(FileDialogNFiltersNumber, request.Count);
                }

                if (parent is { NativeHandle: var native })
                    properties.Set(FileDialogWindowPointer, native);

                if (!string.IsNullOrEmpty(location))
                    properties.Set(FileDialogLocationString, location);

                if (!string.IsNullOrEmpty(title))
                    properties.Set(FileDialogTitleString, title);

                if (!string.IsNullOrEmpty(accept))
                    properties.Set(FileDialogAcceptString, accept);

                if (!string.IsNullOrEmpty(cancel))
                    properties.Set(FileDialogCancelString, cancel);

                if (allowMany)
                    properties.Set(FileDialogManyBoolean, true);

                SDL3.ShowFileDialogWithProperties(type, &OnResult, GCHandle.ToIntPtr(handle), properties);
            }
            catch (Exception ex)
            {
                request.Free();
                handle.Free();

                error(ex);
            }
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void OnResult(nint data, byte** fileList, int filter)
    {
        GCHandle handle = GCHandle.FromIntPtr(data);
        Request request = (Request)handle.Target!;

        unsafe
        {
            try
            {
                if (fileList is null)
                {
                    string error = SDL3.GetError() ?? "unknown SDL error";
                    SDL3.ClearError();

                    request.OnError(new QuackInteropException($"[{nameof(SDL3)}] {error} (file dialog)"));
                    return;
                }

                List<string> files = [];

                for (byte** current = fileList; *current is not null; current++)
                {
                    string? path = Marshal.PtrToStringUTF8(*current);

                    if (path is not null)
                        files.Add(path);
                }

                request.OnSuccess([.. files]);
            }
            finally
            {
                request.Free();
                handle.Free();
            }
        }
    }
}

file sealed class Request(Action<string[]> success, Action<Exception> error)
{
    internal Action<string[]> OnSuccess { get; } = success;

    internal Action<Exception> OnError { get; } = error;

    internal SDL_DialogFileFilter* Filters { get; private set; } = null;

    internal int Count { get; private set; }

    internal void SetFilters(IReadOnlyList<FileDialogFilter> filters)
    {
        if (filters.Count == 0)
            return;

        unsafe
        {
            Filters = (SDL_DialogFileFilter*)NativeMemory.AllocZeroed((nuint)filters.Count, (nuint)sizeof(SDL_DialogFileFilter));

            foreach ((int i, FileDialogFilter filter) in filters.Index())
            {
                Filters[i] = new SDL_DialogFileFilter
                {
                    Name = Utf8StringMarshaller.ConvertToUnmanaged(filter.Name),
                    Pattern = Utf8StringMarshaller.ConvertToUnmanaged(filter.Pattern)
                };
            }

            Count = filters.Count;
        }
    }

    internal void Free()
    {
        unsafe
        {
            if (Filters is null)
                return;

            for (int i = 0; i < Count; i++)
            {
                Utf8StringMarshaller.Free(Filters[i].Name);
                Utf8StringMarshaller.Free(Filters[i].Pattern);
            }

            NativeMemory.Free(Filters);
            Filters = null;
        }
    }
}
