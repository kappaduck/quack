# Quack! framework examples :duck:

This guide shows you how to run and explore the Quack! framework examples. Each is a self-contained single C# file designed
to showcase one specific feature of the framework.

## Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
> The SDK includes everything needed to build and run .NET applications.

- Install SDL and its extensions

#### Windows
```bash
dotnet ./SDL3/deps.cs
```

#### Linux
```bash
chmod +x ./SDL3/deps.cs # only needed once
./SDL3/deps.cs
```

## Running an example

- Open the solution in your preferred IDE (e.g., Visual Studio, Rider, VS Code).
> :warning: Most IDEs do not support running [single-file scripts](https://devblogs.microsoft.com/dotnet/announcing-dotnet-run-app/) directly, so you'll need to run the example file from the command line.
>
> VS Code provides intellisense but cannot run the file directly.

### Windows

```bash
dotnet ./examples/windows/window.cs
```

### Linux

```bash
chmod +x ./examples/windows/window.cs # only needed once
./examples/windows/window.cs
```

## Exploring examples

Each example is located in the `examples` folder and is organized by module. You can open and explore the code in your IDE to understand how different features of the Quack! framework work.

### Audio

_**no examples**_

### Core

_**no examples**_

### Events

_**no examples**_

### Geometry

_**no examples**_

### Graphics

_**no examples**_

### Inputs

_**no examples**_

### System

_**no examples**_

### UI

_**no examples**_

### Video

_**no examples**_

### Windows

_**no examples**_
