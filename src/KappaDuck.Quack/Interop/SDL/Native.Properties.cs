// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Interop.SDL.Marshalling;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class Native
{
    internal class Properties : IDisposable
    {
        private readonly uint _propertiesId;
        private bool _disposed;

        internal Properties()
        {
            _propertiesId = SDL_CreateProperties();
            QuackNativeException.ThrowIfZero(_propertiesId);
        }

        public void Dispose()
        {
            Dispose(true);
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

        protected void Set(string name, bool value) => SetBooleanProperty(_propertiesId, name, value);

        protected void Set(string name, float value) => SetFloatProperty(_propertiesId, name, value);

        protected void Set(string name, string value) => SetStringProperty(_propertiesId, name, value);

        protected void Set(string name, int value) => SetNumberProperty(_propertiesId, name, value);
    }

    internal static bool GetBooleanProperty(uint propertiesId, string name, bool defaultValue) => SDL_GetBooleanProperty(propertiesId, name, defaultValue);

    internal static float GetFloatProperty(uint propertiesId, string name, float defaultValue) => SDL_GetFloatProperty(propertiesId, name, defaultValue);

    internal static string GetStringProperty(uint propertiesId, string name, string defaultValue) => SDL_GetStringProperty(propertiesId, name, defaultValue);

    internal static nint GetPointerProperty(uint propertiesId, string name, nint defaultValue) => SDL_GetPointerProperty(propertiesId, name, defaultValue);

    internal static int GetNumberProperty(uint propertiesId, string name, int defaultValue)
    {
        long value = SDL_GetNumberProperty(propertiesId, name, long.CreateChecked(defaultValue));
        return int.CreateChecked(value);
    }

    internal static T GetEnumProperty<T>(uint propertiesId, string name, T defaultValue) where T : struct, Enum
    {
        object obj = Convert.ChangeType(defaultValue, defaultValue.GetTypeCode());

        long value = SDL_GetNumberProperty(propertiesId, name, Convert.ToInt64(obj));
        return Enum.Parse<T>(value.ToString());
    }

    internal static void SetBooleanProperty(uint propertiesId, string name, bool value)
    {
        bool success = SDL_SetBooleanProperty(propertiesId, name, value);
        QuackNativeException.ThrowIfFailed(success);
    }

    internal static void SetFloatProperty(uint propertiesId, string name, float value)
    {
        bool success = SDL_SetFloatProperty(propertiesId, name, value);
        QuackNativeException.ThrowIfFailed(success);
    }

    internal static void SetStringProperty(uint propertiesId, string name, string value)
    {
        bool success = SDL_SetStringProperty(propertiesId, name, value);
        QuackNativeException.ThrowIfFailed(success);
    }

    internal static void SetNumberProperty(uint propertiesId, string name, int value)
    {
        bool success = SDL_SetNumberProperty(propertiesId, name, long.CreateChecked(value));
        QuackNativeException.ThrowIfFailed(success);
    }

    [LibraryImport(SDL, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    [return: MarshalAs(UnmanagedType.U1)]
    internal static partial bool SDL_SetAppMetadataProperty(string name, string value);

    [LibraryImport(SDL), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    private static partial uint SDL_CreateProperties();

    [LibraryImport(SDL), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    private static partial void SDL_DestroyProperties(uint properties);

    [LibraryImport(SDL, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_GetBooleanProperty(uint propertiesId, string name, [MarshalAs(UnmanagedType.U1)] bool defaultValue);

    [LibraryImport(SDL, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    private static partial float SDL_GetFloatProperty(uint propertiesId, string name, float defaultValue);

    [LibraryImport(SDL, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    private static partial long SDL_GetNumberProperty(uint propertiesId, string name, long defaultValue);

    [LibraryImport(SDL, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    private static partial nint SDL_GetPointerProperty(uint propertiesId, string name, nint defaultValue);

    [LibraryImport(SDL, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    [return: MarshalUsing(typeof(SDLOwnedStringMarshaller))]
    private static partial string SDL_GetStringProperty(uint propertiesId, string name, string defaultValue);

    [LibraryImport(SDL, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_SetBooleanProperty(uint propertiesId, string name, [MarshalAs(UnmanagedType.U1)] bool value);

    [LibraryImport(SDL, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_SetFloatProperty(uint propertiesId, string name, float value);

    [LibraryImport(SDL, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_SetNumberProperty(uint propertiesId, string name, long value);

    [LibraryImport(SDL, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
    [return: MarshalAs(UnmanagedType.U1)]
    private static partial bool SDL_SetStringProperty(uint propertiesId, string name, string value);
}
