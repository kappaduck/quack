// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack;
using KappaDuck.Quack.Core;
using KappaDuck.Quack.Events;
using KappaDuck.Quack.Windows;

// Demonstrates how to create a minimal window
// Initialize the engine with the Video subsystem
using QuackEngine _ = QuackEngine.Init(Subsystem.Video);

// Create a resizable window with the title "Minimal window" and dimensions 800x600
using Window window = new("Minimal window", 800, 600, WindowState.Resizable);

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
