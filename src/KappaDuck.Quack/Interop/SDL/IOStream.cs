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
            io = SDL3.OpenIO(&callbacks, GCHandle.ToIntPtr(handle));
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
    private static long SizeCallback(nint data)
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
    private static long SeekCallback(nint data, long offset, SDL_IOWhence whence)
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
    private static nuint ReadCallback(nint data, void* ptr, nuint size, SDL_IOStatus* status)
    {
        try
        {
            int count = (int)Math.Min(size, int.MaxValue);
            int read = GetStream(data).Read(new Span<byte>(ptr, count));

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
    private static nuint WriteCallback(nint data, void* ptr, nuint size, SDL_IOStatus* status)
    {
        try
        {
            int count = (int)Math.Min(size, int.MaxValue);
            GetStream(data).Write(new ReadOnlySpan<byte>(ptr, count));

            return (nuint)count;
        }
        catch
        {
            *status = SDL_IOStatus.Error;
            return 0;
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static byte FlushCallback(nint data, SDL_IOStatus* status)
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
    private static byte CloseCallback(nint data)
    {
        GCHandle handle = GCHandle.FromIntPtr(data);
        handle.Free();

        return 1;
    }

    private static Stream GetStream(nint data) => (Stream)GCHandle.FromIntPtr(data).Target!;
}
