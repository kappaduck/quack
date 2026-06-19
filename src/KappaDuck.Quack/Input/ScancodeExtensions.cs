// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Exceptions;
using System.Text;

namespace KappaDuck.Quack.Input;

/// <summary>
/// Provides extension for <see cref="Scancode"/>
/// </summary>
public static class ScancodeExtensions
{
    extension(Scancode code)
    {
        /// <summary>
        /// Gets the name of a code.
        /// </summary>
        /// <remarks>
        /// The name is by design and not stable across platforms, e.g. the name for <see cref="Scancode.LeftGui"/>
        /// is "Left GUI" on Linux, but "Left Windows" on Windows. Some codes may not have a name, in which case
        /// an empty string is returned.
        /// </remarks>
        public string Name
        {
            get => SDL3.GetScancodeName(code);
            set => Scancode.SetName(code, value);
        }

        /// <summary>
        /// Converts a <see cref="Key"/> to its corresponding <see cref="Scancode"/> according to the current keyboard layout.
        /// </summary>
        /// <param name="modifier">The modifier to apply when translating the code to a key.</param>
        /// <returns>The corresponding <see cref="Key"/> from the given code or <see cref="Key.Unknown"/> if the code does not have a corresponding key.</returns>
        public Key ToKey(Keymod modifier = Keymod.None)
            => SDL3.GetKeyFromScancode(code, modifier, keyEvents: false);
    }

    extension(Scancode)
    {
        /// <summary>
        /// Gets the <see cref="Scancode"/> corresponding to the given name.
        /// </summary>
        /// <param name="name">The name of the scancode.</param>
        /// <returns>The corresponding <see cref="Scancode"/> from the given name or <see cref="Scancode.Unknown"/> if the name does not correspond to a scancode.</returns>
        public static Scancode FromName(string name) => SDL3.GetScancodeFromName(name);

        /// <summary>
        /// Sets the name of a code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="name">The name to set.</param>
        /// <exception cref="QuackInteropException">Thrown when failed to set the code name.</exception>
        public static void SetName(Scancode code, string name)
            => SDLThrowHelper.ThrowIfFailed(SDL3.SetScancodeName(code, Encoding.UTF8.GetBytes(name)));
    }
}
