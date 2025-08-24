# How to use Quack! Engine

This document provides a overview of how to use the Quack! game engine by running or checking out various examples.
> You need to have .NET 10.0 SDK installed to run theses examples as [dotnet run app.cs].

To run a example, run the following command in the terminal:

```bash
# On Windows
dotnet ./examples/windows/minimalWindow.cs

# On Linux
chmod +x ./examples/windows/minimalWindow.cs
./examples/windows/minimalWindow.cs
```

You can also open the example in your preferred IDE (e.g., Visual Studio, Rider, etc.).
> Any IDE doesn't support single-file execution, so you will need to run as cli.

> Visual Code has intellisense support for single-file but can't run it directly.

## Examples

Here the examples are categorized by module. Each example is a C# file that demonstrates a specific feature or capability of the Quack! engine.

### Windows

- [Minimal Window](./windows/minimalWindow.cs) - Demonstrates how to create a simple window.

### Graphics

- [Drawable](./graphics/drawable.cs) - Demonstrates how to use the IDrawable interface.
- [Sine Wave](./graphics/sinewave.cs) - Demonstrates how to render a sine wave.
- [Triangle](./graphics/triangle.cs) - Demonstrates how to draw a triangle using vertices.

## Inputs

- [Keyboard Input](./inputs/keyboard.cs) - Demonstrates how to handle keyboard input.
- [Mouse Input](./inputs/mouse.cs) - Demonstrates how to handle mouse input.

[dotnet run app.cs]: https://devblogs.microsoft.com/dotnet/announcing-dotnet-run-app/
