# Quack! :duck: ![Static Badge](https://img.shields.io/badge/.NET-9.0%2C%2010.0-512BD4) [![NuGet Version](https://img.shields.io/nuget/vpre/KappaDuck.Quack?style=flat&label=stable)][NuGet]


A lightweight, modern .NET game framework built on SDL
---

## Overview

Quack! is a fast and lightweight game framework built on top of [SDL] and its extensions ([SDL_image], [SDL_mixer], [SDL_ttf]).
It targets modern .NET 9+ desktop apps and games, providing a clean and flexible API that abstracts away low-level native and platform-specific details.

## Features

- 2D/3D rendering using the [GPU API]
- Window and display management
- Input handling (keyboard, mouse, gamepads, etc...)
- Event system
- Audio playback and management
- System utilities (power management, clipboard, etc...)
- Native UI integration (Context menus, dialogs, etc...)
- Cross-platform support (Windows, Linux)

## Installation

You can install Quack! via [NuGet]

```bash
dotnet add package KappaDuck.Quack -v 0.5.0
```

or via the `.csproj` file:

```xml
<PackageReference Include="KappaDuck.Quack" Version="0.5.0" />
```

You can also install via the NuGet Package Manager in Visual Studio or JetBrains Rider.

## Usage

```csharp
using KappaDuck.Quack.Events;
using KappaDuck.Quack.Windows;

using Window window = new Window("Quack!", 1080, 720);

while (window.IsOpen)
{
    while (window.Poll(out Event e))
    {
        if (e.RequestQuit())
        {
            window.Close();
            return;
        }
    }
}
```

More examples can be found in the [Examples] directory.

## Cross-Platform Support

Quack! currently supports Windows and Linux platforms thanks to SDL's abstraction layer, making porting to other platforms easier in the future.

The framework may have platform-specific implementations or limitations depending on the underlying SDL support. Using theses platform-specific features will have a warning in the editors saying that the code may not be portable.

> :information_source: Other platforms such as Android or WebAssembly may be supported in the future, but there are no immediate plans.

## SDL compatibility

Quack! is shipped with precompiled SDL binaries for Windows and Linux which are built from [quack.runtimes].

Below is a compatibility table for the SDL libraries used in each Quack! release.

| Quack! version | SDL version | SDL_image version | SDL_ttf version | SDL_mixer version |
| :------------: | :---------: | :---------------: | :-------------: | :---------------: |
|    `0.5.0`     |   `3.4.0`   |      `3.2.6`      |     `3.2.2`     |       `N/A`       |
|    `0.4.0`     |   `3.4.0`   |      `3.2.6`      |     `3.2.2`     |       `N/A`       |
|    `0.3.0`     |  `3.2.30`   |      `3.2.6`      |     `3.2.2`     |       `N/A`       |
|    `0.2.0`     |  `3.2.28`   |      `3.2.4`      |     `3.2.2`     |       `N/A`       |
|    `0.1.0`     |  `3.2.18`   |       `N/A`       |      `N/A`      |       `N/A`       |

> :warning: During active development, SDL dependencies may be updated frequently.

## Development & Playground

You can build Quack! from source and running quick experiments using the playground file or [examples] provided in the repository.

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
dotnet ./SDL3/dependencies.cs
```

#### Linux
```bas
chmod +x ./SDL3/dependencies.cs
./SDL3/dependencies.cs
```

> The dependencies script installs SDL and all required extensions. On linux, you only need to make it executable once.

### Build & Run

Open the solution in your preferred IDE (e.g., Visual Studio, Rider, VS Code).
> :warning: Most IDEs do not support running single-file scripts directly, so you'll need to run the playground file from the command line.
>
> VS Code provides intellisense but cannot run the playground file directly.

### Playground file (quack.playground.cs)

The playground allows you to experiment with windows, input, rendering, and more without modifying the main source code.

Create a file named `quack.playground.cs` at the root of the repository with the following content:
> This file is ignored by git, so it's safe to use for your experiments.

```csharp
#!/usr/bin/env dotnet

#:property NoWarn=IDE0073
#:property TargetFramework=net10.0-windows
#:property IncludeBinaries=true
#:project KappaDuck.Quack

using KappaDuck.Quack.Events;
using KappaDuck.Quack.Windows;

using Window window = new Window("Quack!", 1080, 720);

while (window.IsOpen)
{
    while (window.Poll(out Event e))
    {
        if (e.RequestQuit())
        {
            window.Close();
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
## :raised_hands: Credits

Built with inspiration from

- [SDL]
- [SDL_image]
- [SDL_ttf]
- [SDL_mixer]
- [SFML](https://www.sfml-dev.org/)
- [LazyFoo](https://lazyfoo.net/index.php)
- [Sayers.SDL2.Core](https://github.com/JeremySayers/Sayers.SDL2.Core)
- [SDL3-CS](https://github.com/flibitijibibo/SDL3-CS)

[Examples]: examples
[NuGet]: https://www.nuget.org/packages/KappaDuck.Quack/
[SDL]: https://www.libsdl.org/
[GPU API]: https://wiki.libsdl.org/CategoryGPU
[SDL_image]: https://www.libsdl.org/projects/SDL_image/
[SDL_mixer]: https://www.libsdl.org/projects/SDL_mixer/
[SDL_ttf]: https://www.libsdl.org/projects/SDL_ttf/
[quack.runtimes]: https://github.com/kappaduck/quack.runtimes
