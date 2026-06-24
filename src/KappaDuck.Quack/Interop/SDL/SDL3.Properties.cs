// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Interop.SDL.Marshalling;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL3
{
    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_CopyProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool CopyProperties(uint source, uint destination);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_CreateProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial uint CreateProperties();

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_DestroyProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void DestroyProperties(uint propertiesId);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetBooleanProperty", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool GetBooleanProperty(uint propertiesId, string name, [MarshalAs(UnmanagedType.I1)] bool defaultValue);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetFloatProperty", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial float GetFloatProperty(uint propertiesId, string name, float defaultValue);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetNumberProperty", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial long GetNumberProperty(uint propertiesId, string name, long defaultValue);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetPointerProperty", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial nint GetPointerProperty(uint propertiesId, string name, nint defaultValue);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_GetStringProperty", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SDLStringMarshaller))]
    internal static partial string GetStringProperty(uint propertiesId, string name, string defaultValue);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetAppMetadataProperty", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetAppMetadataProperty(string name, string value);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetBooleanProperty", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetBooleanProperty(uint propertiesId, string name, [MarshalAs(UnmanagedType.I1)] bool value);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetFloatProperty", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetFloatProperty(uint propertiesId, string name, float value);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetNumberProperty", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetNumberProperty(uint propertiesId, string name, long value);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetPointerProperty", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetPointerProperty(uint propertiesId, string name, void* value);

    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_SetStringProperty", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    internal static partial bool SetStringProperty(uint propertiesId, string name, string value);
}
