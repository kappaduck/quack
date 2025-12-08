# Quack! 🦆 ![NuGet Version](https://img.shields.io/nuget/vpre/KappaDuck.Quack?style=flat&label=stable)

Quack! is a fast, lightweight, and user-friendly 2D/3D game engine built on [SDL] and its extensions ([SDL_image], [SDL_mixer], [SDL_ttf]).
It is designed for modern .NET 9+ games and applications. It provides a clean, flexible and intuitive API that simplifies game development by hiding low-level complexities.
It offers a range of features such as rendering ([SDL_renderer] and [SDL_gpu]), input handling, audio playback, resource management, window management, and more.

## Quack & SDL compatibility

Below is a list of Quack! versions and their compatible SDL versions:

| Quack! version | SDL version | SDL_image version | SDL_mixer version | SDL_ttf version |
|:--------------:|:-----------:|:-----------------:|:-----------------:|:---------------:|
|    `source`    |  `3.2.28`   |      `3.2.4`      |       `N/A`       |     `3.2.2`     |
|    `0.2.0`     |  `3.2.28`   |      `3.2.4`      |       `N/A`       |     `3.2.2`     |
|    `0.1.0`     |  `3.2.18`   |       `N/A`       |       `N/A`       |      `N/A`      |

> Support for SDL_mixer is planned for future releases. It needs SDL3 3.4.0 which is not yet released.
>
> The current Quack! development can update the SDL dependencies many times before the release.

## Cross-platform support

Quack! currently supports Windows, with Linux support planned for future releases.
> The API is designed to be cross-platform, so porting to other platforms should be straightforward.
>
> Other platforms may be considered in the future, but there are no current plans for them.

## Installation

Quack! is available as a NuGet package. You can install it using the following command:

```bash
dotnet add package KappaDuck.Quack -v 0.2.0
```

or by adding the following line to your `.csproj` file:

```xml
<PackageReference Include="KappaDuck.Quack" Version="0.2.0" />
```

or by using the NuGet Package Manager in Visual Studio or JetBrains Rider.

## Usage

A simple example of how to use Quack! to create a window:

```csharp
using KappaDuck.Quack.Events;
using KappaDuck.Quack.Windows;

using Window window = new("Minimal window", 800, 600)
{
    Resizable = true
};

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

You can find more examples in the [Examples] directory.

## Development

To build Quack! from source, you will need the following tools installed:

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

> The SDK includes everything you need to build and run .NET applications on your machine.

- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

> Docker is used to create SDL binaries for different platforms in a consistent environment.

### Setup

After installing the prerequisites, you can set up the project by following these steps:

Clone the repository
```bash
git clone https://github.com/KappaDuck/quack.git
```
Navigate to the project directory
```bash
cd quack
```

Install SDL and its extensions using the provided script
> Make sure you have .NET 10.0 SDK and Docker installed, to do [dotnet run app.cs].

```bash
# On Windows:
dotnet ./SDL3/build.cs

# On Linux:
chmod +x ./SDL3/build.cs # Make the script executable. No need to do this every time.
./SDL3/build.cs
```

Open the solution file in your preferred IDE (e.g., Visual Studio, Rider, etc.):
> Any IDE doesn't support single-file execution, so you will need to run as cli.
>
> Visual Code have intellisense support for single-file but can't run it directly.

To test any features, you can run the examples provided in the [Examples] directory or create a file named `quack.playground.cs` at the root and run it directly.
> The `quack.playground.cs` file is ignored by git, so you can use it to test your code without affecting the repository.

You can use the following code snippet as a starting point for your `quack.playground.cs` file:

```csharp
#!/usr/bin/env dotnet

// Ignore the warning about missing copyright header in this file
#:property NoWarn=IDE0073
#:property TargetFramework=net10.0
#:property IncludeNativeLibraries=true
#:project KappaDuck.Quack

using KappaDuck.Quack.Events;
using KappaDuck.Quack.Windows;

using Window window = new("Quack! Playground", 800, 600)
{
    Resizable = true
};

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

```bash
# On Windows:
dotnet ./quack.playground.cs

# On Linux:
chmod +x ./quack.playground.cs # Make the script executable. No need to do this every time.
./quack.playground.cs
```

## Credits

Quack! leverages and draws inspiration from the following projects:

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
