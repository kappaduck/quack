// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.System.UI.Dialogs;

namespace KappaDuck.Quack.Interop.SDL.Marshalling;

[CustomMarshaller(typeof(MessageBoxOptions), MarshalMode.ManagedToUnmanagedIn, typeof(ManagedToUnmanagedIn))]
internal static unsafe class SDL_MessageBoxDataMarshaller
{
    internal ref struct ManagedToUnmanagedIn
    {
        private SDL_MessageBoxData _unmanaged;

        private byte* _title;
        private byte* _message;
        private SDL_MessageBoxButtonData* _buttons;
        private SafeHandleMarshaller<SDL_Window>.ManagedToUnmanagedIn _handleMarshaller;

        public void FromManaged(MessageBoxOptions managed)
        {
            _title = Utf8StringMarshaller.ConvertToUnmanaged(managed.Title);
            _message = Utf8StringMarshaller.ConvertToUnmanaged(managed.Message);
            _buttons = ConvertButtonsToUnmanaged(managed.Buttons);
            _handleMarshaller = new();

            nint window = nint.Zero;
            if (managed.Parent is not null)
            {
                _handleMarshaller.FromManaged(managed.Parent.NativeHandle);
                window = _handleMarshaller.ToUnmanaged();
            }

            _unmanaged = new SDL_MessageBoxData
            {
                Flags = (uint)managed.Level | (uint)managed.Alignment,
                Window = window,
                Title = _title,
                Message = _message,
                NumButtons = managed.Buttons.Count,
                Buttons = _buttons
            };
        }

        public SDL_MessageBoxData* ToUnmanaged() => (SDL_MessageBoxData*)Unsafe.AsPointer(ref _unmanaged);

        public void Free()
        {
            FreeButtons();
            Utf8StringMarshaller.Free(_title);
            Utf8StringMarshaller.Free(_message);
            _handleMarshaller.Free();
        }

        private static SDL_MessageBoxButtonData* ConvertButtonsToUnmanaged(ICollection<MessageBoxButton> buttons)
        {
            QuackException.ThrowIf(buttons.Count == 0, "The message box should have at least one button.");

            SDL_MessageBoxButtonData* unmanaged = (SDL_MessageBoxButtonData*)Marshal.AllocHGlobal(buttons.Count * sizeof(SDL_MessageBoxButtonData));

            int i = 0;
            foreach (MessageBoxButton button in buttons)
            {
                unmanaged[i++] = new SDL_MessageBoxButtonData
                {
                    Flags = (uint)button.DefaultKey,
                    ButtonId = button.Id,
                    Text = Utf8StringMarshaller.ConvertToUnmanaged(button.Text)
                };
            }

            return unmanaged;
        }

        private void FreeButtons()
        {
            for (int i = 0; i < _unmanaged.NumButtons; i++)
                Utf8StringMarshaller.Free(_buttons[i].Text);

            Marshal.FreeHGlobal((nint)_buttons);
        }
    }
}
