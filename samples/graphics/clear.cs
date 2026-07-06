#!/usr/bin/env dotnet

// Copyright (c) KappaDuck.
// Licensed under the MIT license.

// This example code creates a window and renderer, and then clears the
// window to a different color every frame, so you'll effectively get a window
// that's smoothly fading between colors.

using KappaDuck.Quack.Core;
using KappaDuck.Quack.Events;
using KappaDuck.Quack.Graphics.Rendering;
using KappaDuck.Quack.Video.Pixels;
using KappaDuck.Quack.Windows;
using System.Diagnostics;

// Initialize the engine with metadata and the Video Subsystem
QuackEngine.SetMetadata(new ApplicationMetadata("Clear example", "1.0.0", "com.example.clear"));
using EngineScope _ = QuackEngine.Init(Subsystem.Video);

// Create a resizable window and a renderer
using Window window = new("Clear example", 640, 480) { Resizable = true };
using Renderer renderer = new(window)
{
    Presentation = (640, 480, LogicalPresentation.Letterbox)
};

// Start a stopwatch for the sine wave
Stopwatch stopwatch = Stopwatch.StartNew();

// Run the main loop
while (window.IsOpen)
{
    // Poll the events
    while (window.Poll(out Event e))
    {
        // If the user requests to quit the application, it will automatically close the window and exit the loop.
        if (e is QuitRequestedEvent)
            return;
    }

    // Clear the window with a sine wave color
    renderer.Clear(SineWave(stopwatch.Elapsed.TotalSeconds));

    // Presents all the drawn content on the window
    renderer.Present();
}

static ColorF SineWave(double seconds)
{
    float r = (float)(0.5 + (0.5 * Math.Sin(seconds)));
    float g = (float)(0.5 + (0.5 * Math.Sin(seconds + (Math.PI * 2 / 3))));
    float b = (float)(0.5 + (0.5 * Math.Sin(seconds + (Math.PI * 4 / 3))));

    return new ColorF(r, g, b);
}
