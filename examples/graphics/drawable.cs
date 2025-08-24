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

// Demonstrates how to draw a triangle using IDrawable
// Initialize the engine with the Video subsystem
using QuackEngine _ = QuackEngine.Init(Subsystem.Video);

const int Width = 1080;
const int Height = 720;

// Create the rectangle
Rectangle smallRectangle = new(Color.Blue, new((Width / 2) - 100, (Height / 2) - 100), new(100, 100));

// Create another rectangle
Rectangle largeRectangle = new(Color.Red, new((Width / 2) + 100, (Height / 2) - 100), new(300, 300));

// Create the window
using RenderWindow window = new("Drawable example", Width, Height);

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
    window.Clear();

    // Draw the rectangle
    window.Draw(smallRectangle);

    // You can draw from the drawable
    largeRectangle.Draw(window);

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
