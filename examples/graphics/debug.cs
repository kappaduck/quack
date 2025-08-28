#!/usr/bin/env dotnet

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack;
using KappaDuck.Quack.Core;
using KappaDuck.Quack.Events;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Drawing;
using KappaDuck.Quack.Graphics.Primitives;
using KappaDuck.Quack.Graphics.Rendering;
using System.Drawing;

// Demonstrates how to draw text for debugging purposes
// Initialize the engine with the Video subsystem
using QuackEngine _ = QuackEngine.Init(Subsystem.Video);

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
        // If the user requests to quit the application, close the window and exit the loop
        // You can close the window by clicking the close button or pressing Esc key
        if (e.RequestQuit())
        {
            window.Close();
            return;
        }
    }

    // Clear the window with black color
    // Be sure to call this before drawing the debug text otherwise it may not appear
    window.Clear();

    // Draw the debug text
    window.DrawDebugText(position, "Demonstrates how to draw text for debugging purposes", Color.Red);

    // Render the graphics to the window since the last call
    window.Render();
}

file sealed class Rectangle : IDrawable
{
    private const int Points = 4;
    private static readonly int[] _indices = [0, 1, 2, 2, 3, 0];
    private readonly Vertex[] _vertices = new Vertex[Points];

    public Rectangle(Color color, Vector2 position, Vector2 size)
    {
        _vertices[0].Position = position;
        _vertices[0].Color = color;

        _vertices[1].Position = position + new Vector2(size.X, 0);
        _vertices[1].Color = color;

        _vertices[2].Position = position + size;
        _vertices[2].Color = color;

        _vertices[3].Position = position + new Vector2(0, size.Y);
        _vertices[3].Color = color;
    }

    public void Draw(IRenderTarget target) => target.Draw(_vertices, _indices);
}
