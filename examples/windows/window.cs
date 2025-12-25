#!/usr/bin/env dotnet

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Events;
using KappaDuck.Quack.Graphics.Rendering;
using KappaDuck.Quack.Inputs;

// Create a resizable window with the title "Minimal window" and dimensions 1080x720
using RenderWindow window = new("Minimal window", 1080, 720)
{
    Resizable = true
};

// Run the main loop while the window is open
while (window.IsOpen)
{
    // Poll events that are associated with the window
    while (window.Poll(out Event e))
    {
        // If the user requests to quit the application, it will automatically close the window and exit the loop.
        // You can close the window by clicking the close button or pressing ESC key
        if (e.RequestQuit())
        {
            return;
        }

        // You can also manually close the window by pressing the Q key
        if (e.IsKeyDown(Scancode.Q))
        {
            window.Close();
            return;
        }
    }
}
