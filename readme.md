# Quack! 🦆 ![Static Badge](https://img.shields.io/badge/.NET-10.0,%2011.0-512BD4) [![NuGet Version](https://img.shields.io/nuget/v/KappaDuck.Quack?style=flat&label=NuGet)][NuGet]

A modern .NET multimedia framework for building games and interactive apps, built on SDL3

---

## Overview

Quack! is a modern, simple and fast multimedia framework for building games and interactive applications, built on top of SDL3 and its extensions ([SDL_image], [SDL_mixer], [SDL_ttf]).
It targets .NET 10+ desktop and web apps, providing a clean and flexible API that hides the complexity of SDL.

## Features

- 2D rendering via the Renderer API and 3D rendering via the GPU API
- Window and display management
- Audio management
- Input & events
- Image and font loading
- Native UI integration, system utilities, and cross-platform support

## Usage

```csharp
using KappaDuck.Quack.Core;
using KappaDuck.Quack.Events;
using KappaDuck.Quack.Graphics.Rendering;
using KappaDuck.Quack.Input;
using KappaDuck.Quack.Video.Pixels;
using KappaDuck.Quack.Windows;

using EngineScope _ = QuackEngine.Init(Subsystem.Video);

using Window window = new("Quack!", 1920, 1080) { Resizable = true };
using Renderer renderer = new(window);

while (window.IsOpen)
{
    while (window.Poll(out Event e))
    {
        if (e is QuitRequestedEvent or KeyPressedEvent { Key: Key.Escape })
            return;
    }

    renderer.Clear(Colors.Black);
    renderer.Present();
}
```

More examples and documentation can be found at [Documentation](#documentation)

## Installation

Install Quack! via [NuGet]:

```bash
dotnet package add KappaDuck.Quack -v 0.5.0
```

or via your `.csproj`:

```xml
<PackageReference Include="KappaDuck.Quack" Version="0.5.0">
```

You can also install via the NuGet Package Manager in Visual Studio or JetBrains Rider.

> [!WARNING]
> Quack! is still in early development. Expect breaking changes and frequent updates. Always use the latest version for the best experience.

### Beta packages

Pre-release versions are published to NuGet.org alongside stable releases. To install the latest beta:

```bash
dotnet package add KappaDuck.Quack --prerelease
```

## Documentation

Full API documentation and samples are available:

- [Full API reference][quack.kappaduck.com]
- **[`samples/`][samples]** for runnable code covering common uses cases

## Contributing

Contributions are welcome! Please read [CONTRIBUTING.md](contributing.md) before opening a pull request.

## Cross-platform support

Quack! supports Windows and Linux thanks to SDL3's abstraction layer.

The framework may have platform-specific implementations or limitations depending on the underlying SDL support. Using platform-specific features will surface a compiler warning indicating the code may not be portable across all targets.

> [!NOTE]
> Android and WebAssembly (WASM) support is planned for a future milestone. macOS and iOS are not in scope.

## SDL compatibility

SDL3 native libraries are bundled via `KappaDuck.Quack.Runtimes`. The table below shows the SDL versions included in each release of both packages.

During active development, `KappaDuck.Quack` references the **pre-release** version of `KappaDuck.Quack.Runtimes`. When a stable release of `KappaDuck.Quack` is published, it switches to the corresponding **production** version of `KappaDuck.Quack.Runtimes`.

|  Quack!  |    Runtimes    |   SDL3   | SDL_image | SDL_ttf | SDL_mixer |
| :------: | :------------: | :------: | :-------: | :-----: | :-------: |
| `source` | `0.1.0-beta.4` | `3.4.12` |  `3.4.4`  | `3.2.2` |  `3.2.4`  |

## Development & Sandbox

You can build Quack! from source and experiment quickly using the included sandbox project.

### Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [.NET 11.0 SDK](https://dotnet.microsoft.com/download/dotnet/11.0)

### Setup

**1. Clone the repository**

```bash
git clone https://github.com/KappaDuck/quack.git
cd quack
```

**2. Create an entry point in the sandbox**

The sandbox project requires at least one `.cs` file to compile. All `.cs` files inside `src/Quack.Sandbox/` are listed in `.gitignore`, so create any file you like there. A simple starting point:

```csharp
using KappaDuck.Quack.Core;
using KappaDuck.Quack.Events;
using KappaDuck.Quack.Input;
using KappaDuck.Quack.Windows;

using EngineScope _ = QuackEngine.Init(Subsystem.Video);

using Window window = new("Quack!", 1920, 1080) { Resizable = true };

while (window.IsOpen)
{
    while (window.Poll(out Event e))
    {
        if (e is QuitRequestedEvent or KeyPressedEvent { Key: Key.Escape })
            return;
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

## AI disclosure

AI tools assisted with two things in this project: **documentation** (XML doc comments, README, CONTRIBUTING guidelines) and **design exploration** (prototyping API shapes, exploring implementation approaches, and thinking through architecture decisions).

## Credits

Built with inspiration from

- [SDL3]
- [SDL_image]
- [SDL_ttf]
- [SDL_mixer]
- [SFML](https://www.sfml-dev.org/)
- [LazyFoo](https://lazyfoo.net/index.php)
- [Sayers.SDL2.Core](https://github.com/JeremySayers/Sayers.SDL2.Core)
- [SDL3-CS](https://github.com/flibitijibibo/SDL3-CS)

[samples]: samples
[NuGet]: https://www.nuget.org/packages/KappaDuck.Quack/
[SDL3]: https://www.libsdl.org/
[SDL_image]: https://github.com/libsdl-org/SDL_image
[SDL_mixer]: https://github.com/libsdl-org/SDL_mixer
[SDL_ttf]: https://github.com/libsdl-org/SDL_ttf
[quack.kappaduck.com]: https://quack.kappaduck.com
