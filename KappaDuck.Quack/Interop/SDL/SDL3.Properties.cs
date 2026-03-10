// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Interop.SDL.Marshalling;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL3
{
    internal partial class Properties : IDisposable
    {
        private readonly uint _propertiesId;

        private bool _disposed;

        internal Properties()
        {
            _propertiesId = SDL_CreateProperties();
            QuackInteropException.ThrowIfZero(_propertiesId);
        }

        public static implicit operator uint(Properties properties) => properties._propertiesId;

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                SDL_DestroyProperties(_propertiesId);

            _disposed = true;
        }

        protected void Set(string name, bool value)
            => QuackInteropException.ThrowIfFailed(SDL_SetBooleanProperty(_propertiesId, name, value));

        protected void Set(string name, float value)
            => QuackInteropException.ThrowIfFailed(SDL_SetFloatProperty(_propertiesId, name, value));

        protected void Set(string name, int value)
           => QuackInteropException.ThrowIfFailed(SDL_SetNumberProperty(_propertiesId, name, value));

        protected void Set(string name, SafeHandle value)
            => QuackInteropException.ThrowIfFailed(SDL_SetPointerProperty(_propertiesId, name, value));

        protected unsafe void Set<T>(string name, T* value) where T : unmanaged
            => QuackInteropException.ThrowIfFailed(SDL_SetPointerProperty(_propertiesId, name, (nint)value));

        protected void Set(string name, string value)
            => QuackInteropException.ThrowIfFailed(SDL_SetStringProperty(_propertiesId, name, value));

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetAppMetadataProperty", StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SetAppMetadataProperty(string name, string value);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetBooleanProperty", StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool GetBooleanProperty(uint propertiesId, string name, [MarshalAs(UnmanagedType.U1)] bool defaultValue);

        internal static T GetEnumProperty<T>(uint propertiesId, string name, T defaultValue) where T : struct, Enum
        {
            object obj = Convert.ChangeType(defaultValue, defaultValue.GetTypeCode());

            long value = SDL_GetNumberProperty(propertiesId, name, Convert.ToInt64(obj));
            return Enum.Parse<T>(value.ToString());
        }

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetFloatProperty", StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial float GetFloatProperty(uint propertiesId, string name, float defaultValue);

        internal static int GetIntProperty(uint propertiesId, string name, int defaultValue)
        {
            long value = SDL_GetNumberProperty(propertiesId, name, defaultValue);
            return int.CreateChecked(value);
        }

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetPointerProperty", StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        internal static partial nint GetPointerProperty(uint propertiesId, string name, nint defaultValue);

        [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetStringProperty", StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalUsing(typeof(SDLStringMarshaller))]
        internal static partial string GetStringProperty(uint propertiesId, string name, string defaultValue);

        [LibraryImport(nameof(SDL3)), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        private static partial uint SDL_CreateProperties();

        [LibraryImport(nameof(SDL3)), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        private static partial void SDL_DestroyProperties(uint properties);

        [LibraryImport(nameof(SDL3), StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        private static partial long SDL_GetNumberProperty(uint propertiesId, string name, long defaultValue);

        [LibraryImport(nameof(SDL3), StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static partial bool SDL_SetBooleanProperty(uint propertiesId, string name, [MarshalAs(UnmanagedType.U1)] bool value);

        [LibraryImport(nameof(SDL3), StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static partial bool SDL_SetFloatProperty(uint propertiesId, string name, float value);

        [LibraryImport(nameof(SDL3), StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static partial bool SDL_SetNumberProperty(uint propertiesId, string name, long value);

        [LibraryImport(nameof(SDL3), StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static partial bool SDL_SetPointerProperty(uint propertiesId, string name, SafeHandle handle);

        [LibraryImport(nameof(SDL3), StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static partial bool SDL_SetPointerProperty(uint propertiesId, string name, nint pointer);

        [LibraryImport(nameof(SDL3), StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
        [return: MarshalAs(UnmanagedType.U1)]
        private static partial bool SDL_SetStringProperty(uint propertiesId, string name, string value);
    }
}
