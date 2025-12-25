#!/usr/bin/env dotnet

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Events;
using KappaDuck.Quack.Graphics.Rendering;
using System.Diagnostics;
using System.Drawing;

// Create the window
using RenderWindow window = new("Sine wave", 1080, 720);

// Start a stopwatch for the sine wave
Stopwatch stopwatch = Stopwatch.StartNew();

// Run the main loop
while (window.IsOpen)
{
    // Poll events
    while (window.Poll(out Event e))
    {
        // If the user requests to quit the application, it will automatically close the window and exit the loop.
        // You can close the window by clicking the close button or pressing Esc key
        if (e.RequestQuit())
        {
            return;
        }
    }

    // Clear the window with a sine wave color
    window.Clear(SineWaveColor(stopwatch.Elapsed.TotalSeconds));

    // Presents all the drawn content on the window
    window.Present();
}

static Color SineWaveColor(double seconds)
{
    double red = 0.5 + (0.5 * Math.Sin(seconds));
    double green = 0.5 + (0.5 * Math.Sin(seconds + (Math.PI * 2 / 3)));
    double blue = 0.5 + (0.5 * Math.Sin(seconds + (Math.PI * 4 / 3)));

    return Color.FromArgb((byte)(red * 255), (byte)(green * 255), (byte)(blue * 255));
}
