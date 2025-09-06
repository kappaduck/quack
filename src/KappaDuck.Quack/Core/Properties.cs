// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Interop.SDL;
using System.Numerics;

namespace KappaDuck.Quack.Core;

internal sealed class Properties : IDisposable
{
    internal Properties()
    {
        Id = SDL.Properties.SDL_CreateProperties();

        QuackNativeException.ThrowIfZero(Id);
    }

    internal uint Id { get; }

    public void Dispose() => SDL.Properties.SDL_DestroyProperties(Id);

    internal bool Get(string name, bool defaultValue) => Get(Id, name, defaultValue);

    internal float Get(string name, float defaultValue) => Get(Id, name, defaultValue);

    internal string Get(string name, string defaultValue) => Get(Id, name, defaultValue);

    internal nint Get(string name, nint defaultValue) => Get(Id, name, defaultValue);

    internal T Get<T>(string name, T defaultValue) where T : struct, INumber<T>
        => Get(Id, name, defaultValue);

    internal void Set(string name, bool value) => Set(Id, name, value);

    internal void Set(string name, float value) => Set(Id, name, value);

    internal void Set(string name, string value) => Set(Id, name, value);

    internal void Set<T>(string name, T value) where T : struct, INumber<T>
        => Set(Id, name, value);

    internal static bool Get(uint propertiesId, string name, bool defaultValue)
        => SDL.Properties.SDL_GetBooleanProperty(propertiesId, name, defaultValue);

    internal static float Get(uint propertiesId, string name, float defaultValue)
        => SDL.Properties.SDL_GetFloatProperty(propertiesId, name, defaultValue);

    internal static string Get(uint propertiesId, string name, string defaultValue)
        => SDL.Properties.SDL_GetStringProperty(propertiesId, name, defaultValue);

    internal static nint Get(uint propertiesId, string name, nint defaultValue)
        => SDL.Properties.SDL_GetPointerProperty(propertiesId, name, defaultValue);

    internal static T Get<T>(uint propertiesId, string name, T defaultValue) where T : struct, INumber<T>
        => T.CreateChecked(SDL.Properties.SDL_GetNumberProperty(propertiesId, name, long.CreateChecked(defaultValue)));

    internal static T GetEnum<T>(uint propertiesId, string name, T defaultValue) where T : Enum
    {
        object obj = Convert.ChangeType(defaultValue, defaultValue.GetTypeCode())!;

        long number = SDL.Properties.SDL_GetNumberProperty(propertiesId, name, Convert.ToInt64(obj));
        return (T)Enum.Parse(typeof(T), number.ToString())!;
    }

    internal static void Set(uint propertiesId, string name, bool value)
    {
        bool isSet = SDL.Properties.SDL_SetBooleanProperty(propertiesId, name, value);
        QuackNativeException.ThrowIfFailed(isSet);
    }

    internal static void Set(uint propertiesId, string name, float value)
    {
        bool isSet = SDL.Properties.SDL_SetFloatProperty(propertiesId, name, value);
        QuackNativeException.ThrowIfFailed(isSet);
    }

    internal static void Set<T>(uint propertiesId, string name, T value) where T : struct, INumber<T>
    {
        bool isSet = SDL.Properties.SDL_SetNumberProperty(propertiesId, name, long.CreateChecked(value));
        QuackNativeException.ThrowIfFailed(isSet);
    }

    internal static void Set(uint propertiesId, string name, string value)
    {
        bool isSet = SDL.Properties.SDL_SetStringProperty(propertiesId, name, value);
        QuackNativeException.ThrowIfFailed(isSet);
    }
}
