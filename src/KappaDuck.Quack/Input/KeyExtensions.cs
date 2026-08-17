// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Exceptions;

namespace KappaDuck.Quack.Input;

/// <summary>
/// Provides extension for <see cref="Key"/>
/// </summary>
public static class KeyExtensions
{
    extension(Key)
    {
        /// <summary>
        /// Gets the key from a name.
        /// </summary>
        /// <param name="name">The key name.</param>
        /// <returns>The key based on the name</returns>
        /// <exception cref="QuackInteropException">Failed to recognize the name for the key.</exception>
        public static Key FromName(string name)
        {
            Key key = SDL3.GetKeyFromName(name);
            SDLThrowHelper.ThrowIf(key == Key.Unknown);

            return key;
        }
    }

    extension(Key key)
    {
        /// <summary>
        /// Gets the name of a key.
        /// </summary>
        /// <remarks>
        /// Letters will be presented in their uppercase form, if applicable.
        /// </remarks>
        public string Name => SDL3.GetKeyName(key);

        /// <summary>
        /// Converts a <see cref="Key"/> to its corresponding <see cref="Scancode"/> and optional <see cref="KeyModifiers"/> according to the current keyboard layout.
        /// </summary>
        /// <returns>The corresponding code and modifier or <see cref="Scancode.Unknown"/> and <see langword="null"/> if the key does not have a corresponding code.</returns>
        public (Scancode Code, KeyModifiers? Modifiers) ToScancode()
        {
            KeyModifiers modifiers;

            Scancode scancode = unsafe (SDL3.GetScancodeFromKey(key, &modifiers));
            return (scancode, modifiers);
        }
    }
}
