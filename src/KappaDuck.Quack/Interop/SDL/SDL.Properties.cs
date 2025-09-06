// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Interop.Marshalling;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace KappaDuck.Quack.Interop.SDL;

internal static partial class SDL
{
    internal static partial class Properties
    {
        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial uint SDL_CreateProperties();

        [LibraryImport(Core), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial void SDL_DestroyProperties(uint propertiesId);

        [LibraryImport(Core, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_GetBooleanProperty(uint propertiesId, string name, [MarshalAs(UnmanagedType.U1)] bool defaultValue);

        [LibraryImport(Core, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial float SDL_GetFloatProperty(uint propertiesId, string name, float defaultValue);

        [LibraryImport(Core, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial long SDL_GetNumberProperty(uint propertiesId, string name, long defaultValue);

        [LibraryImport(Core, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        internal static partial nint SDL_GetPointerProperty(uint propertiesId, string name, nint defaultValue);

        [LibraryImport(Core, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalUsing(typeof(SDLOwnedStringMarshaller))]
        internal static partial string SDL_GetStringProperty(uint propertiesId, string name, string defaultValue);

        [LibraryImport(Core, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_SetBooleanProperty(uint propertiesId, string name, [MarshalAs(UnmanagedType.U1)] bool value);

        [LibraryImport(Core, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_SetFloatProperty(uint propertiesId, string name, float value);

        [LibraryImport(Core, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_SetNumberProperty(uint propertiesId, string name, long value);

        [LibraryImport(Core, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
        [return: MarshalAs(UnmanagedType.U1)]
        internal static partial bool SDL_SetStringProperty(uint propertiesId, string name, string value);
    }
}
