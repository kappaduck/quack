#!/usr/bin/env dotnet

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Events;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Rendering;
using System.Drawing;

const int Width = 1080;
const int Height = 720;

// Create the window
using RenderWindow window = new("Drawable example", Width, Height);

// Create the position for the debug text
Vector2 position = new(325, (Height / 2) - 25);

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

    // Clear the window with black color
    // Be sure to call this before drawing the debug text otherwise it may not appear
    window.Clear();

    // Draw the debug text
    window.DrawDebugText(position, "Demonstrates how to draw text for debugging purposes", Color.Red);

    // Presents all the drawn content on the window
    window.Present();
}
