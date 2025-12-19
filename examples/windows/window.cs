#!/usr/bin/env dotnet

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Events;
using KappaDuck.Quack.Windows;

// Create a resizable window with the title "Minimal window" and dimensions 1080x720
using Window window = new("Minimal window", 1080, 720)
{
    Resizable = true
};

// Run the main loop while the window is open
while (window.IsOpen)
{
    // Poll events that are associated with the window
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
}
