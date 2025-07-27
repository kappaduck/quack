# Quack! 🦆

Quack! is a fast, lightweight and user-friendly game engine built on [SDL] and its extensions ([SDL_image], [SDL_mixer], [SDL_ttf]). Designed for modern .NET 9+ applications, it provides a clean, flexible and intuitive API that simplifies game development by hiding low-level complexities. It uses SDL for 2D ([Renderer]) and 3D ([GPU]) rendering, window management, input handling and audio.

## Quack & SDL compatibility

Below is a list of Quack! versions and their corresponding SDL versions:

| Quack! version | SDL version  | SDL_image version | SDL_mixer version | SDL_ttf version |
| :------------: | :----------: | :---------------: | :-------------: | :---------------: |
|   `>= 0.1.0`   |   `3.2.18`   |   `unsupported`   |  `unsupported`    |   `unsupported` |

> Support for SDL_image, SDL_mixer and SDL_ttf is planned for future releases. Stay tuned!

## Cross-platform support

Quack! currently supports Windows, with Linux support pllaned for future releases.

> Other platforms may be considered in the future, but there are no current plans for them.

## Installation

*Work in progress...*

## Usage

*Work in progress...*

## Development

To build Quack! from source, you will need the following tools installed:

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

> The SDK includes everything you need to build and run .NET applications on your machine.

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
