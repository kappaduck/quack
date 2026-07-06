# How to use Quack! Engine examples 🦆

This document provides an overview of how to run and explore the Quack! game engine examples.
> You need to have **.NET 11.0 SDK** installed to run theses examples via [dotnet run app.cs].

## Prerequisites

- [.NET 11.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)

> Quack! examples are single-file C# scripts, so most IDEs cannot run them directly.
>
> VS Code provides intellisense but requires running via the terminal.

## Running Examples

Run the example directly in your terminal:

#### Window
```bash
dotnet ./examples/windows/window.cs
```

#### Linux
```bash
chmod +x ./examples/windows/window.cs # only needed once
./examples/windows/window.cs
```

Alternatively, you can open the example in VS Code for editing and exploring the code, but
execution must be done via the terminal as shown above.

> For more details on single-file, see [dotnet run app.cs]

## Examples

Each example demonstrates a specific feature or capability of the Quack! engine.

## Graphics

- [Clear](./graphics/clear.cs) - Clear the screen with a sine wave pattern.


[dotnet run app.cs]: https://devblogs.microsoft.com/dotnet/announcing-dotnet-run-app/
