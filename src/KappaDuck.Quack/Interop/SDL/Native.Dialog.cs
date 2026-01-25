// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.UI.System.Dialog;
using System.Text;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class Native
{
    internal static unsafe partial class Dialog
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void Callback(void* data, byte** files, int filter);

        internal static void OpenFile(FileDialogOptions options, Action<FileInfo?> callback)
        {
            UnmanagedFilters filters = new(options.Filters);
            using Properties properties = new(options, filters, false);

            SDL_ShowFileDialogWithProperties(Type.OpenFile, InternalCallback, nint.Zero, properties.Id);

            void InternalCallback(void* _, byte** files, int _1)
            {
                callback(GetFileInfo(files));
                filters.Dispose();
            }
        }

        internal static void OpenFile(Action<FileInfo?> callback)
        {
            using Properties properties = new(new FileDialogOptions(), false);
            SDL_ShowFileDialogWithProperties(Type.OpenFile, (_, files, _) => callback(GetFileInfo(files)), nint.Zero, properties.Id);
        }

        internal static void OpenFile(FileDialogOptions options, Action<string?> callback)
        {
            UnmanagedFilters filters = new(options.Filters);
            using Properties properties = new(options, filters, false);

            SDL_ShowFileDialogWithProperties(Type.OpenFile, InternalCallback, nint.Zero, properties.Id);

            void InternalCallback(void* _, byte** files, int _1)
            {
                callback(GetPath(files));
                filters.Dispose();
            }
        }

        internal static void OpenFile(Action<string?> callback)
        {
            using Properties properties = new(new FileDialogOptions(), false);
            SDL_ShowFileDialogWithProperties(Type.OpenFile, (_, files, _) => callback(GetPath(files)), nint.Zero, properties.Id);
        }

        internal static void OpenFiles(FileDialogOptions options, Action<ReadOnlySpan<FileInfo>> callback)
        {
            UnmanagedFilters filters = new(options.Filters);
            using Properties properties = new(options, filters, true);

            SDL_ShowFileDialogWithProperties(Type.OpenFile, InternalCallback, nint.Zero, properties.Id);

            void InternalCallback(void* _, byte** files, int _1)
            {
                callback(GetFileInfos(files));
                filters.Dispose();
            }
        }

        internal static void OpenFiles(Action<ReadOnlySpan<FileInfo>> callback)
        {
            using Properties properties = new(new FileDialogOptions(), true);
            SDL_ShowFileDialogWithProperties(Type.OpenFile, (_, files, _) => callback(GetFileInfos(files)), nint.Zero, properties.Id);
        }

        internal static void OpenFiles(FileDialogOptions options, Action<ReadOnlySpan<string>> callback)
        {
            UnmanagedFilters filters = new(options.Filters);
            using Properties properties = new(options, filters, true);

            SDL_ShowFileDialogWithProperties(Type.OpenFile, InternalCallback, nint.Zero, properties.Id);

            void InternalCallback(void* _, byte** files, int _1)
            {
                callback(GetPaths(files));
                filters.Dispose();
            }
        }

        internal static void OpenFiles(Action<ReadOnlySpan<string>> callback)
        {
            using Properties properties = new(new FileDialogOptions(), true);
            SDL_ShowFileDialogWithProperties(Type.OpenFile, (_, files, _) => callback(GetPaths(files)), nint.Zero, properties.Id);
        }

        internal static void OpenFolder(FolderDialogOptions options, Action<DirectoryInfo?> callback)
        {
            using Properties properties = new(options, false);
            SDL_ShowFileDialogWithProperties(Type.OpenFolder, (_, folders, _) => callback(GetFolder(folders)), nint.Zero, properties.Id);

            static DirectoryInfo? GetFolder(byte** paths)
            {
                QuackNativeException.ThrowIfNull(paths);

                if (*paths is null)
                    return null;

                string path = ConvertToUnmanaged(*paths);
                return new DirectoryInfo(path);
            }
        }

        internal static void OpenFolder(FolderDialogOptions options, Action<string?> callback)
        {
            using Properties properties = new(options, false);
            SDL_ShowFileDialogWithProperties(Type.OpenFolder, (_, folders, _) => callback(GetPath(folders)), nint.Zero, properties.Id);
        }

        internal static void OpenFolders(FolderDialogOptions options, Action<ReadOnlySpan<DirectoryInfo>> callback)
        {
            using Properties properties = new(options, true);
            SDL_ShowFileDialogWithProperties(Type.OpenFolder, (_, folders, _) => callback(GetFolders(folders)), nint.Zero, properties.Id);

            static ReadOnlySpan<DirectoryInfo> GetFolders(byte** paths)
            {
                QuackNativeException.ThrowIfNull(paths);

                if (*paths is null)
                    return [];

                List<DirectoryInfo> result = [];

                while (*paths is not null)
                    result.Add(new DirectoryInfo(ConvertToUnmanaged(*paths++)));

                return CollectionsMarshal.AsSpan(result);
            }
        }

        internal static void OpenFolders(FolderDialogOptions options, Action<ReadOnlySpan<string>> callback)
        {
            using Properties properties = new(options, true);
            SDL_ShowFileDialogWithProperties(Type.OpenFolder, (_, folders, _) => callback(GetPaths(folders)), nint.Zero, properties.Id);
        }

        internal static void SaveFile(FileDialogOptions options, Action<FileInfo?, FileDialogFilter?> callback)
        {
            UnmanagedFilters filters = new(options.Filters);
            using Properties properties = new(options, filters, false);

            SDL_ShowFileDialogWithProperties(Type.SaveFile, InternalCallback, nint.Zero, properties.Id);

            void InternalCallback(void* _, byte** files, int index)
            {
                callback(GetFileInfo(files), filters.Get(index));
                filters.Dispose();
            }
        }

        internal static void SaveFile(Action<FileInfo?> callback)
        {
            using Properties properties = new(new FileDialogOptions(), false);
            SDL_ShowFileDialogWithProperties(Type.SaveFile, (_, file, _) => callback(GetFileInfo(file)), nint.Zero, properties.Id);
        }

        internal static void SaveFile(FileDialogOptions options, Action<string?, FileDialogFilter?> callback)
        {
            UnmanagedFilters filters = new(options.Filters);
            using Properties properties = new(options, filters, false);

            SDL_ShowFileDialogWithProperties(Type.SaveFile, InternalCallback, nint.Zero, properties.Id);

            void InternalCallback(void* _, byte** files, int index)
            {
                callback(GetPath(files), filters.Get(index));
                filters.Dispose();
            }
        }

        internal static void SaveFile(Action<string?> callback)
        {
            using Properties properties = new(new FileDialogOptions(), false);
            SDL_ShowFileDialogWithProperties(Type.SaveFile, (_, file, _) => callback(GetPath(file)), nint.Zero, properties.Id);
        }

        internal static void SaveFiles(FileDialogOptions options, Action<ReadOnlySpan<FileInfo>, FileDialogFilter?> callback)
        {
            UnmanagedFilters filters = new(options.Filters);
            using Properties properties = new(options, filters, true);

            SDL_ShowFileDialogWithProperties(Type.SaveFile, InternalCallback, nint.Zero, properties.Id);

            void InternalCallback(void* _, byte** files, int index)
            {
                callback(GetFileInfos(files), filters.Get(index));
                filters.Dispose();
            }
        }

        internal static void SaveFiles(Action<ReadOnlySpan<FileInfo>> callback)
        {
            using Properties properties = new(new FileDialogOptions(), true);
            SDL_ShowFileDialogWithProperties(Type.SaveFile, (_, files, _) => callback(GetFileInfos(files)), nint.Zero, properties.Id);
        }

        internal static void SaveFiles(FileDialogOptions options, Action<ReadOnlySpan<string>, FileDialogFilter?> callback)
        {
            UnmanagedFilters filters = new(options.Filters);
            using Properties properties = new(options, filters, true);

            SDL_ShowFileDialogWithProperties(Type.SaveFile, InternalCallback, nint.Zero, properties.Id);

            void InternalCallback(void* _, byte** files, int index)
            {
                callback(GetPaths(files), filters.Get(index));
                filters.Dispose();
            }
        }

        internal static void SaveFiles(Action<ReadOnlySpan<string>> callback)
        {
            using Properties properties = new(new FileDialogOptions(), true);
            SDL_ShowFileDialogWithProperties(Type.SaveFile, (_, files, _) => callback(GetPaths(files)), nint.Zero, properties.Id);
        }

        private static FileInfo? GetFileInfo(byte** files)
        {
            QuackNativeException.ThrowIfNull(files);

            if (*files is null)
                return null;

            string path = ConvertToUnmanaged(*files);
            return new FileInfo(path);
        }

        private static ReadOnlySpan<FileInfo> GetFileInfos(byte** files)
        {
            QuackNativeException.ThrowIfNull(files);

            if (*files is null)
                return [];

            List<FileInfo> result = [];

            while (*files is not null)
                result.Add(new FileInfo(ConvertToUnmanaged(*files++)));

            return CollectionsMarshal.AsSpan(result);
        }

        private static string? GetPath(byte** paths)
        {
            QuackNativeException.ThrowIfNull(paths);

            if (*paths is null)
                return null;

            return ConvertToUnmanaged(*paths);
        }

        private static ReadOnlySpan<string> GetPaths(byte** paths)
        {
            QuackNativeException.ThrowIfNull(paths);

            if (*paths is null)
                return [];

            List<string> result = [];

            while (*paths is not null)
                result.Add(ConvertToUnmanaged(*paths++));

            return CollectionsMarshal.AsSpan(result);
        }

        private static string ConvertToUnmanaged(byte* str)
        {
            return Encoding.UTF8.GetString(str, GetLength(str));

            static int GetLength(byte* str)
            {
                int length = 0;
                while (*str++ != 0)
                    length++;

                return length;
            }
        }

        private sealed class Properties : Native.Properties
        {
            internal Properties(FileDialogOptions options, UnmanagedFilters filters, bool multiSelect)
            {
                SetDialogOptions(options, multiSelect);

                if (filters.Length > 0)
                {
                    Set("SDL.filedialog.filters", filters.ToUnmanaged());
                    Set("SDL.filedialog.nfilters", filters.Length);
                }
            }

            internal Properties(IDialogOptions options, bool multiSelect) => SetDialogOptions(options, multiSelect);

            private void SetDialogOptions(IDialogOptions options, bool multiSelect)
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

        private struct UnmanagedFilters : IDisposable
        {
            private readonly FileDialogFilter[] _filters;
            private SDL_DialogFileFilter* _unmanaged;

            internal UnmanagedFilters(FileDialogFilter[] filters)
            {
                if (filters.Length == 0)
                {
                    _filters = [];
                    _unmanaged = null;

                    return;
                }

                int buffer = sizeof(SDL_DialogFileFilter) * filters.Length;

                _unmanaged = (SDL_DialogFileFilter*)NativeMemory.Alloc((nuint)buffer);
                _filters = filters;
            }

            internal readonly int Length => _filters.Length;

            public void Dispose()
            {
                if (_unmanaged is null)
                    return;

                for (int i = 0; i < _filters.Length; i++)
                    _unmanaged[i].Dispose();

                NativeMemory.Free(_unmanaged);
                _unmanaged = null;
            }

            public readonly FileDialogFilter? Get(int index)
            {
                if (index < 0 || index >= _filters.Length)
                    return null;

                return _filters[index];
            }

            internal readonly SDL_DialogFileFilter* ToUnmanaged()
            {
                if (_filters.Length == 0)
                    return null;

                Span<SDL_DialogFileFilter> span = new(_unmanaged, _filters.Length);

                for (int i = 0; i < _filters.Length; i++)
                    span[i] = new SDL_DialogFileFilter(_filters[i].Name, string.Join(';', _filters[i].Patterns));

                return (SDL_DialogFileFilter*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(span));
            }
        }

        [LibraryImport(SDL), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        private static partial void SDL_ShowFileDialogWithProperties(Type type, Callback callback, nint data, uint propertiesId);

        private enum Type
        {
            OpenFile = 0,
            SaveFile = 1,
            OpenFolder = 2
        }
    }
}
