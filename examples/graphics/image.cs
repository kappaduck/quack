#!/usr/bin/env dotnet

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Events;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Drawing;
using KappaDuck.Quack.Graphics.Rendering;

const int Width = 1080;
const int Height = 720;

// Create the window
using RenderWindow window = new("Hello triangle!", Width, Height);

// Create the texture and the sprite so we can draw the image on the window
using Texture texture = window.LoadTexture("icon.png");
Sprite duck = new(texture, new Vector2((Width / 2) - 64, (Height / 2) - 64));

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
    window.Clear();

    // Draw the image
    window.Draw(duck);

    // Presents all the drawn content on the window
    window.Present();
}
