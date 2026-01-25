// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Interop.SDL.Primitives;

[StructLayout(LayoutKind.Sequential)]
internal unsafe struct SDL_DialogFileFilter(string name, string pattern) : IDisposable
{
    private byte* _name = Utf8StringMarshaller.ConvertToUnmanaged(name);
    private byte* _pattern = Utf8StringMarshaller.ConvertToUnmanaged(pattern);

    public void Dispose()
    {
        if (_name is not null)
        {
            Utf8StringMarshaller.Free(_name);
            _name = null;
        }

        if (_pattern is not null)
        {
            Utf8StringMarshaller.Free(_pattern);
            _pattern = null;
        }
    }
}
