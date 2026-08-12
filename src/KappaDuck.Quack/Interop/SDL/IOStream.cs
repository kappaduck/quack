// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Exceptions;
using KappaDuck.Quack.Interop.SDL.Primitives;

namespace KappaDuck.Quack.Interop.SDL;

internal sealed unsafe class IOStream(SDL_IOStream* stream) : IDisposable
{
    public SDL_IOStream* Handle { get; private set; } = stream;

    public void Dispose()
    {
        if (Handle is null)
            return;

        SDLThrowHelper.ThrowIfFailed(SDL3.CloseIO(Handle));
        Handle = null;
    }

    internal static IOStream FromStream(Stream stream)
    {
        GCHandle handle = GCHandle.Alloc(stream);

        SDL_IOStreamInterface callbacks = new()
        {
            Version = (uint)sizeof(SDL_IOStreamInterface),
            Size = &SizeCallback,
            Seek = &SeekCallback,
            Read = &ReadCallback,
            Write = &WriteCallback,
            Flush = &FlushCallback,
            Close = &CloseCallback
        };

        SDL_IOStream* io;

        try
        {
            io = SDL3.OpenIO(&callbacks, (void*)GCHandle.ToIntPtr(handle));
            SDLThrowHelper.ThrowIfNull(io);
        }
        catch
        {
            handle.Free();
            throw;
        }

        return new IOStream(io);
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static long SizeCallback(void* data)
    {
        try
        {
            Stream stream = GetStream(data);
            return stream.CanSeek ? stream.Length : -1;
        }
        catch
        {
            return -1;
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static long SeekCallback(void* data, long offset, SDL_IOWhence whence)
    {
        try
        {
            SeekOrigin origin = whence switch
            {
                SDL_IOWhence.Set => SeekOrigin.Begin,
                SDL_IOWhence.Current => SeekOrigin.Current,
                SDL_IOWhence.End => SeekOrigin.End,
                _ => SeekOrigin.Begin
            };

            return GetStream(data).Seek(offset, origin);
        }
        catch
        {
            return -1;
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static nuint ReadCallback(void* data, void* pointer, nuint size, SDL_IOStatus* status)
    {
        try
        {
            int count = (int)Math.Min(size, int.MaxValue);
            int read = GetStream(data).Read(new Span<byte>(pointer, count));

            if (read == 0 && count > 0)
                *status = SDL_IOStatus.EndOfFile;

            return (nuint)read;
        }
        catch
        {
            *status = SDL_IOStatus.Error;
            return 0;
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static nuint WriteCallback(void* data, void* pointer, nuint size, SDL_IOStatus* status)
    {
        try
        {
            int count = (int)Math.Min(size, int.MaxValue);
            GetStream(data).Write(new ReadOnlySpan<byte>(pointer, count));

            return (nuint)count;
        }
        catch
        {
            *status = SDL_IOStatus.Error;
            return 0;
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte FlushCallback(void* data, SDL_IOStatus* status)
    {
        try
        {
            GetStream(data).Flush();
            return 1;
        }
        catch
        {
            *status = SDL_IOStatus.Error;
            return 0;
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte CloseCallback(void* data)
    {
        GCHandle handle = GCHandle.FromIntPtr((nint)data);
        handle.Free();

        return 1;
    }

    private static Stream GetStream(void* data) => (Stream)GCHandle.FromIntPtr((nint)data).Target!;
}
