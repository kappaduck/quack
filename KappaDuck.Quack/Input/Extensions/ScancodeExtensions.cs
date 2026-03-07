// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Input.Keyboard;
using System.Text;

namespace KappaDuck.Quack.Input.Extensions;

/// <summary>
/// Provides extension methods for <see cref="Scancode"/>.
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
            get => SDL3.Input.GetScancodeName(code);
            set => Scancode.SetName(code, value);
        }

        /// <summary>
        /// Converts a <see cref="Keycode"/> to its corresponding <see cref="Scancode"/> according to the current keyboard layout.
        /// </summary>
        /// <param name="modifier">The modifier to apply when translating the code to a key.</param>
        /// <returns>The corresponding <see cref="Keycode"/> from the given code or <see cref="Keycode.Unknown"/> if the code does not have a corresponding key.</returns>
        public Keycode ToKey(Modifier modifier = Modifier.None)
            => SDL3.Input.GetKeyFromScancode(code, modifier, keyEvents: false);
    }

    extension(Scancode)
    {
        /// <summary>
        /// Gets the <see cref="Scancode"/> corresponding to the given name.
        /// </summary>
        /// <param name="name">The name of the scancode.</param>
        /// <returns>The corresponding <see cref="Scancode"/> from the given name or <see cref="Scancode.Unknown"/> if the name does not correspond to a scancode.</returns>
        public static Scancode FromName(string name) => SDL3.Input.GetScancodeFromName(name);

        /// <summary>
        /// Sets the name of a code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="name">The name to set.</param>
        /// <exception cref="QuackInteropException">Thrown when failed to set the code name.</exception>
        public static void SetName(Scancode code, string name)
            => QuackInteropException.ThrowIfFailed(SDL3.Input.SetScancodeName(code, Encoding.UTF8.GetBytes(name)));
    }
}
