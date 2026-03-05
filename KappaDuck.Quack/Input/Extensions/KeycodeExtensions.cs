// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Input.Keyboard;

namespace KappaDuck.Quack.Input.Extensions;

/// <summary>
/// Provides extension methods for <see cref="Keycode"/>.
/// </summary>
public static class KeycodeExtensions
{
    extension(Keycode key)
    {
        /// <summary>
        /// Gets the name of a code.
        /// </summary>
        /// <remarks>
        /// Letters will be presented in their uppercase form, if applicable.
        /// </remarks>
        public string Name => SDL3.Input.GetKeyName(key);

        /// <summary>
        /// Converts a <see cref="Keycode"/> to its corresponding <see cref="Scancode"/> and optional <see cref="Modifier"/> according to the current keyboard layout.
        /// </summary>
        /// <returns>The corresponding code and modifier or <see cref="Scancode.Unknown"/> and <see langword="null"/> if the key does not have a corresponding code.</returns>
        public unsafe (Scancode Code, Modifier? Modifier) ToScancode()
        {
            Modifier* modifier = null;

            Scancode scancode = SDL3.Input.GetScancodeFromKey(key, modifier);
            return (scancode, *modifier);
        }
    }
}
