# Contributing to Quack! 🦆

Thank you for your interest in contributing to **KappaDuck.Quack**!

This document covers everything you need to make a quality contribution: the project structure, P/Invoke patterns, architecture rules, naming conventions, and the PR checklist. Please read it before opening a pull request.

> [!WARNING]
> Contributing is in a work in progress. It is possible some changes are made during the active development

## Table of contents

- [Contributing to Quack! 🦆](#contributing-to-quack-)
  - [Table of contents](#table-of-contents)
  - [Project structure](#project-structure)
  - [Getting started](#getting-started)
    - [Setup](#setup)
    - [Quack.Sandbox](#quacksandbox)
  - [Architecture](#architecture)
    - [1. Interop is always `internal`](#1-interop-is-always-internal)
    - [2. Use `[LibraryImport]`, never `[DllImport]`](#2-use-libraryimport-never-dllimport)
    - [3. Always specify `[UnmanagedCallConv]`](#3-always-specify-unmanagedcallconv)
    - [4. SDL `bool` is one byte](#4-sdl-bool-is-one-byte)
    - [5. All native handles inherit `SafeHandleZeroInvalid`](#5-all-native-handles-inherit-safehandlezeroinvalid)
    - [6. Public types own their lifetime via `IDisposable`](#6-public-types-own-their-lifetime-via-idisposable)

## Project structure

```
quack/
├── docs
├── src/
│   ├── KappaDuck.Quack/
│   │   ├── Audio
│   │   ├── Events
│   │   ├── Fonts
│   │   ├── Geometry
│   │   ├── Graphics
│   │   ├── Image
│   │   ├── Input
│   │   ├── Interop
│   │   ├── Physics
│   │   ├── System
│   │   ├── UI
│   │   ├── Video
│   │   └── Windows
│   └── Quack.Sandbox
├── samples
└── tests/
    ├── Quack.Unit.Tests
    ├── Quack.Integration.Tests
    └── Quack.Benchmarks
```

---

## Getting started

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [.NET 11.0 SDK](https://dotnet.microsoft.com/download/dotnet/11.0)
- A C# IDE with Roslyn support (Visual Studio 2022+, Visual Code, Rider)

### Setup

**1. Clone the repository**

```bash
git clone https://github.com/KappaDuck/quack.git
cd quack
```

**2. Create an entry point in the sandbox**

The sandbox project requires at least one `.cs` file to compile. All `.cs` files inside `src/Quack.Sandbox/` are listed in `.gitignore`, so create any file you like there. A simple starting point:

```csharp
using KappaDuck.Quack.Events;
using KappaDuck.Quack.Windows;

using Window window = new("Quack! Sandbox", 1280, 720);

while (window.IsOpen)
{
    while (window.Poll(out Event e))
    {
        if (e is QuitEvent || e is KeyEvent { Key: Key.Escape })
        {
            window.Close();
            return;
        }
    }
}
```

**3. Build**

```bash
dotnet build
```

**4. Run the tests**

```bash
dotnet test
```

### Quack.Sandbox

The repository includes a dedicated sandbox project at `src/Quack.Sandbox/` for experimenting without touching the main source. It references `KappaDuck.Quack` directly so changes are reflected immediately.

To run your sandbox:

```bash
cd sandbox/Quack.Sandbox
dotnet run
```
or simply run in your IDE

---

## Architecture

Theses rules need to respected. PRs that violate them will not be merged.

### 1. Interop is always `internal`

`KappaDuck.Quack.Interop` must never expose a public type. If a user needs access to something currently in Interop, the right fix is to add a wrapper in the appropriate public module.

```csharp
// ✅ Correct
internal static partial class SDL3
{
    [LibraryImport(nameof(SDL3), EntryPoint = "SDL_Init")]
    internal static partial bool Init(SDL_InitFlags flags);
}

// ❌ Wrong — Interop is never public
public static partial class SDL3 { ... }
```

### 2. Use `[LibraryImport]`, never `[DllImport]`

`[LibraryImport]` is source-generated, trimmer-friendly, and NativeAOT-compatible.

```csharp
// ✅ Correct
[LibraryImport(nameof(SDL3), EntryPoint = "SDL_init")]
[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
[return: MarshalAs(UnmanagedType.I1)]
internal static partial bool Init(Subsystem subsystem);

// ❌ Wrong
[DllImport(nameof(SDL3), EntryPoint = "SDL_init")]
[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
internal static extern byte Init(Subsystem subsystem);
```

### 3. Always specify `[UnmanagedCallConv]`

Every `[LibraryImport]` declaration must be decorated with `[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]`. SDL3 uses the C calling convention on all platforms. Omitting this attribute lets the runtime guess, which can cause subtle stack corruption on certain platforms or runtimes.

```csharp
// ✅ Correct
[LibraryImport(nameof(SDL3), EntryPoint = "SDL_Init")]
[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
[return: MarshalAs(UnmanagedType.I1)]
internal static partial bool Init(Subsystem subsystem);

// ❌ Wrong — calling convention is unspecified, behaviour is platform-dependent
[LibraryImport(nameof(SDL3), EntryPoint = "SDL_Init")]
[return: MarshalAs(UnmanagedType.I1)]
internal static partial bool Init(Subsystem subsystem);
```

### 4. SDL `bool` is one byte

SDL3 uses C99 `bool` (1 byte). Always use `UnmanagedType.I1` — never `UnmanagedType.Bool` (4 bytes, Windows `BOOL`).

```csharp
// ✅ Correct
[return: MarshalAs(UnmanagedType.I1)]
internal static partial bool Init(Subsystem subsystem);

// ❌ Wrong — silent data corruption on non-Windows
[return: MarshalAs(UnmanagedType.Bool)]
internal static partial bool Init(Subsystem subsystem);
```

### 5. All native handles inherit `SafeHandleZeroInvalid`

Never store a raw `IntPtr` or `nint` for an SDL object or using an opaque struct. `SafeHandleZeroInvalid` ensures deterministic release and GC-finaliser safety if `Dispose()` is missed.

```csharp
// ✅ Correct
internal sealed partial class SDL_Texture() : SafeHandleZeroInvalid(ownsHandle: true)
{
    protected override bool ReleaseHandle()
    {
        SDL_DestroyTexture(handle);
        return true;
    }

    [LibraryImport(nameof(SDL3)), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyTexture(nint texture);
}


// ❌ Wrong — leaks if Dispose() is not called
private IntPtr _texture;
```

### 6. Public types own their lifetime via `IDisposable`

Every public class that holds a `SafeHandleZeroInvalid` must implement `IDisposable` so users can write `using`.

```csharp
// ✅ Correct
public sealed class Window : IDisposable
{
    private readonly SDL_Window _handle;

    public void Dispose() => _handle.Dispose();
}
```
