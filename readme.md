# Quack! 🦆

Quack! is a fast, lightweight and user-friendly game engine built on [SDL] and its extensions ([SDL_image], [SDL_mixer], [SDL_ttf]). Designed for modern .NET 9+ applications, it provides a clean, flexible and intuitive API that simplifies game development by hiding low-level complexities. It uses SDL for 2D ([Renderer]) and 3D ([GPU]) rendering, window management, input handling and audio.

## Quack & SDL compatibility

Below is a list of Quack! versions and their corresponding SDL versions:

| Quack! version | SDL version | SDL_image version | SDL_mixer version | SDL_ttf version |
| :------------: | :---------: | :---------------: | :---------------: | :-------------: |
|    `0.2.0`     |  `3.2.20`   |      `3.2.4`      |       `N/A`       |      `N/A`      |
|    `0.1.0`     |  `3.2.18`   |       `N/A`       |       `N/A`       |      `N/A`      |

> Support for SDL_mixer is planned for future releases. It need SDL3 3.4.0 which is not yet released.
>
> The current Quack! development can update the SDL dependencies many times before the release.
>
> When a new Quack! version is released, it will use the current SDL dependencies available at that time.

## Cross-platform support

Quack! currently supports Windows, with Linux support planned for future releases.

> Other platforms may be considered in the future, but there are no current plans for them.

## Installation

Quack! is available as a NuGet package. You can install it using the following command:

```bash
dotnet add package KappaDuck.Quack -v 0.1.0
```

or by adding the following line to your `.csproj` file:

```xml
<PackageReference Include="KappaDuck.Quack" Version="0.1.0" />
```

or by using the Visual Studio NuGet Package Manager.

## Usage

A simple example of how to use Quack! to create a window:

```csharp
using KappaDuck.Quack;
using KappaDuck.Quack.Core;
using KappaDuck.Quack.Events;
using KappaDuck.Quack.Windows;

using QuackEngine _ = QuackEngine.Init(Subsystem.Video);

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

### Setup

After installing the prerequisites, you can set up the project by following these steps:

1. Clone the repository:
```bash
git clone https://github.com/KappaDuck/quack.git
```
2. Navigate to the project directory:
```bash
cd quack
```

3. Install SDL and its extensions using the provided script:
> Make sure you have .NET 10.0 SDK installed, to do [dotnet run app.cs].

```bash
# On Windows:
dotnet ./src/install.sdl.cs

# On Linux:
chmod +x ./src/install.sdl.cs
./src/install.sdl.cs
```

4. Open the solution file in your preferred IDE (e.g., Visual Studio, Rider, etc.):
> Any IDE doesn't support single-file execution, so you will need to run as cli.

> Visual Code have intellisense support for single-file but can't run it directly.

5. To test any features, you can run the examples provided in the [Examples] directory or create a file named `quack.playground.cs` in the `src` directory and run it directly.
> The `quack.playground.cs` file is ignored by git, so you can use it to test your code without affecting the repository.

You can use the following code snippet as a starting point for your `quack.playground.cs` file:

```csharp
#!/usr/bin/env dotnet

// Ignore the warning about missing copyright header in this file
#:property NoWarn=IDE0073
#:property TargetFramework=net10.0
#:property IncludeNativeLibraries=true
#:project KappaDuck.Quack

using KappaDuck.Quack;
using KappaDuck.Quack.Core;
using KappaDuck.Quack.Events;
using KappaDuck.Quack.Windows;

using QuackEngine _ = QuackEngine.Init(Subsystem.Video);

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
dotnet ./src/quack.playground.cs

# On Linux:
chmod +x ./src/quack.playground.cs
./src/quack.playground.cs
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
[Renderer]: https://wiki.libsdl.org/CategoryRender
[GPU]: https://wiki.libsdl.org/CategoryGPU
[Examples]: ./examples/
[dotnet run app.cs]: https://devblogs.microsoft.com/dotnet/announcing-dotnet-run-app/
