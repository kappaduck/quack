#!/usr/bin/env dotnet

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack;
using KappaDuck.Quack.Core;
using KappaDuck.Quack.Events;
using KappaDuck.Quack.Inputs;
using KappaDuck.Quack.Windows;

// Demonstrates how to handle mouse input
// Initialize the engine with the Video subsystem
using QuackEngine _ = QuackEngine.Init(Subsystem.Video);

// Create the window
using Window window = new("Mouse example", 800, 600, WindowState.Resizable);

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

        // Detect if the mouse button is pressed
        if (e.Type is EventType.MouseButtonDown && e.Mouse.Button == Mouse.Button.Left)
        {
            Console.WriteLine("Mouse button is pressed");
        }

        // You can use the extension methods to check if the mouse button is pressed
        if (e.IsMouseButtonDown(Mouse.Button.Right))
        {
            Console.WriteLine("Right mouse button is pressed");
        }

        // Detect if the mouse button is released
        if (e.IsMouseButtonUp(Mouse.Button.Left))
        {
            Console.WriteLine("Mouse button is released");
        }

        // Detect if the mouse is moved
        if (e.Type is EventType.MouseMotion)
        {
            Console.WriteLine($"Mouse moved to {e.Mouse.Position}");
        }

        // Detect if the mouse wheel is scrolled
        if (e.Type is EventType.MouseWheel)
        {
            Console.WriteLine($"Mouse wheel scrolled {e.Wheel.Direction} ({e.Wheel.X}, {e.Wheel.Y})");
        }
    }

    // You can also use the Mouse class to check if the mouse button is pressed outside the event loop
    if (Mouse.IsPressed(Mouse.Button.Middle))
    {
        Console.WriteLine("Middle mouse button is pressed");
    }
}
