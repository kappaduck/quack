#!/usr/bin/env dotnet

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack;
using KappaDuck.Quack.Core;
using KappaDuck.Quack.Events;
using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Primitives;
using KappaDuck.Quack.Graphics.Rendering;
using System.Drawing;

// Demonstrates how to draw a triangle using vertices
// Initialize the engine with the Video subsystem
using QuackEngine _ = QuackEngine.Init(Subsystem.Video);

const int Width = 1080;
const int Height = 720;

// Create the triangle using vertices
Vertex[] vertices = CreateTriangle(Width, Height);

// Create the window
using RenderWindow window = new("Hello triangle!", Width, Height);

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

    // Draw the triangle
    window.Draw(vertices);

    // Render the graphics to the window since the last call
    window.Render();
}

/// <summary>
/// Creates a triangle with red, green and blue vertices.
/// </summary>
/// <returns>The vertices of the triangle.</returns>
static Vertex[] CreateTriangle(int width, int height)
{
    // Create the vertices with red, green and blue colors
    Vertex[] vertices =
    [
        new Vertex(Color.Red),
        new Vertex(Color.Green),
        new Vertex(Color.Blue)
    ];

    const float radius = 200.0f;

    // Calculate the position of the vertices
    for (int i = 0; i < vertices.Length; i++)
    {
        float angle = i * (2 * MathF.PI / vertices.Length);

        float x = (width / 2) + (MathF.Cos(angle) * radius);
        float y = (height / 2) + (MathF.Sin(angle) * radius);

        vertices[i].Position = new Vector2(x, y);
    }

    return vertices;
}
