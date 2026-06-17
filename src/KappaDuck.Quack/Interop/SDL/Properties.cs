// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Exceptions;

namespace KappaDuck.Quack.Interop.SDL;

internal sealed class Properties : IDisposable
{
    private readonly uint _id;

    internal Properties()
    {
        _id = SDL3.CreateProperties();
        SDLThrowHelper.ThrowIfZero(_id, nameof(SDL3.CreateProperties));
    }

    internal Properties(Properties properties) : this()
        => SDLThrowHelper.ThrowIfFailed(SDL3.CopyProperties(properties, _id), nameof(SDL3.CopyProperties));

    public static implicit operator uint(Properties properties) => properties._id;

    public void Dispose() => SDL3.DestroyProperties(_id);

    internal static bool Get(uint propertiesId, string name, bool defaultValue) => SDL3.GetBooleanProperty(propertiesId, name, defaultValue);

    internal static float Get(uint propertiesId, string name, float defaultValue) => SDL3.GetFloatProperty(propertiesId, name, defaultValue);

    internal static int Get(uint propertiesId, string name, int defaultValue)
    {
        long value = SDL3.GetNumberProperty(propertiesId, name, defaultValue);
        return int.CreateChecked(value);
    }

    internal static T Get<T>(uint propertiesId, string name, T defaultValue) where T : struct, Enum
    {
        object obj = Convert.ChangeType(defaultValue, defaultValue.GetTypeCode());

        long value = SDL3.GetNumberProperty(propertiesId, name, Convert.ToInt64(obj));
        return Enum.Parse<T>(value.ToString().AsSpan());
    }

    internal static nint Get(uint propertiesId, string name, void* defaultValue) => SDL3.GetPointerProperty(propertiesId, name, defaultValue);

    internal static string Get(uint propertiesId, string name, string defaultValue) => SDL3.GetStringProperty(propertiesId, name, defaultValue);

    internal void Set(string name, bool value) => SDLThrowHelper.ThrowIfFailed(SDL3.SetBooleanProperty(_id, name, value));

    internal void Set(string name, float value) => SDLThrowHelper.ThrowIfFailed(SDL3.SetFloatProperty(_id, name, value));

    internal void Set(string name, int value) => SDLThrowHelper.ThrowIfFailed(SDL3.SetNumberProperty(_id, name, value));

    internal void Set<T>(string name, T* value) where T : unmanaged
        => SDLThrowHelper.ThrowIfFailed(SDL3.SetPointerProperty(_id, name, value));

    internal void Set(string name, string value) => SDLThrowHelper.ThrowIfFailed(SDL3.SetStringProperty(_id, name, value));
}
