# Quack! 🦆 ![NuGet Version](https://img.shields.io/nuget/vpre/KappaDuck.Quack?style=flat&label=stable)

Quack! is a fast, lightweight, and code-first 2D/3D game engine built on [SDL] and its extensions ([SDL_image], [SDL_mixer], [SDL_ttf]).
It is designed for modern .NET 9+ games and desktop apps, providing a clean and flexible API that abstracts low-level platform details.

Quack! offers a wide range of features, including:

- Rendering 2D/3D graphics using SDL's rendering API ([SDL_renderer] and [SDL_gpu])
- Window management and event handling
- Input handling for keyboard, mouse, and game controllers
- Audio playback and management
- And much more!

## Quack & SDL compatibility

Quack! is shipped with native binaries of SDL and its extensions.
Below is a compatibility table showing which versions of SDL are used in each Quack! release.

| Quack! version | SDL version | SDL_image version | SDL_ttf version | SDL_mixer version |
| :------------: | :---------: | :---------------: | :-------------: | :---------------: |
|    `source`    |   `3.4.0`   |      `3.2.6`      |     `3.2.2`     |       `N/A`       |
|    `0.4.0`     |   `3.4.0`   |      `3.2.6`      |     `3.2.2`     |       `N/A`       |
|    `0.3.0`     |  `3.2.30`   |      `3.2.6`      |     `3.2.2`     |       `N/A`       |
|    `0.2.0`     |  `3.2.28`   |      `3.2.4`      |     `3.2.2`     |       `N/A`       |
|    `0.1.0`     |  `3.2.18`   |       `N/A`       |      `N/A`      |       `N/A`       |

> :warning: During active development, SDL dependencies may be updated frequently. :warning:

## Cross-platform support

Quack! currently supports Windows and Linux.

The API is designed to be cross-platform thanks to SDL's abstraction layer, making porting to other platforms straightforward.

> Other platforms such as Android or WebAssembly may be supported in the future, but there are no immediate plans.

## Installation

Quack! is available on [NuGet]. Install it via the .NET CLI:

```bash
dotnet add package KappaDuck.Quack -v 0.4.0
```

or add it directly to your `.csproj` file:

```xml
<PackageReference Include="KappaDuck.Quack" Version="0.4.0" />
```

You can also install via the NuGet Package Manager in your Visual Studio or JetBrains Rider.

## Usage

A minimal example creating a resizable window and handling quit events:

```csharp
using KappaDuck.Quack.Events;
using KappaDuck.Quack.Graphics.Rendering;

using RenderWindow window = new("Quack! Playground", 1080, 720)
{
    Resizable = true
};

while (window.IsOpen)
{
    while (window.Poll(out Event e))
    {
        if (e.RequestQuit())
        {
            return;
        }
    }
}
```

More examples can be found in the [Examples] directory.

## Development & Playground

You can build Quack! from source or run quick experiments using a playground file.

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

> The SDK includes everything needed to build and run .NET applications.

### Setup

1. Clone the repository
```bash
git clone https://github.com/KappaDuck/quack.git
cd quack
```

2. Install SDL and its extensions

#### Windows
```bash
dotnet ./SDL3/setup.cs
```

#### Linux
```bas
chmod +x ./SDL3/setup.cs
./SDL3/setup.cs
```

> The setup script installs SDL and all required extensions. On linux, you only need to make it executable once.

### Build & Run

Open the solution in your preferred IDE (e.g., Visual Studio, Rider, VS Code).
> Most IDEs do not support running single-file scripts directly, so you'll need to run the playground file from the command line.
>
> VS Code provides intellisense but cannot run the playground file directly.

### Playground file (quack.playground.cs)

The playground allows you to experiment with windows, input, rendering, and more without modifying the main source code.

Create a file named `quack.playground.cs` at the root of the repository with the following content:
> This file is ignored by git, so it's safe to use for your experiments.

```csharp
#!/usr/bin/env dotnet

// Ignore the warning about missing copyright header in this file
#:property NoWarn=IDE0073
#:property TargetFramework=net10.0-windows
#:property IncludeNativeLibs=true
#:project src/KappaDuck.Quack

using KappaDuck.Quack.Events;
using KappaDuck.Quack.Graphics.Rendering;

using RenderWindow window = new("Quack! Playground", 1080, 720)
{
    Resizable = true
};

while (window.IsOpen)
{
    while (window.Poll(out Event e))
    {
        if (e.RequestQuit())
        {
            return;
        }
    }
}

```

### Run the playground

#### Windows
```bash
dotnet ./quack.playground.cs
```

#### Linux
```bash
chmod +x ./quack.playground.cs # only needed once
./quack.playground.cs
```

## Credits

Quack! draws inspiration from and leverages the following projects:

- [SDL]
- [SDL_image]
- [SDL_ttf]
- [SDL_mixer]
- [LazyFoo](https://lazyfoo.net/index.php)
- [Sayers.SDL2.Core](https://github.com/JeremySayers/Sayers.SDL2.Core)
- [SDL3-CS](https://github.com/flibitijibibo/SDL3-CS)
- [SFML](https://www.sfml-dev.org/)

[SDL]: https://www.libsdl.org/
[SDL_image]: https://www.libsdl.org/projects/SDL_image/
[SDL_ttf]: https://www.libsdl.org/projects/SDL_ttf/
[SDL_mixer]: https://www.libsdl.org/projects/SDL_mixer/
[SDL_renderer]: https://wiki.libsdl.org/CategoryRender
[SDL_gpu]: https://wiki.libsdl.org/CategoryGPU
[Examples]: examples
[dotnet run app.cs]: https://devblogs.microsoft.com/dotnet/announcing-dotnet-run-app/
[NuGet]: https://www.nuget.org/packages/KappaDuck.Quack/
