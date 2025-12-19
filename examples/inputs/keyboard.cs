#!/usr/bin/env dotnet

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Events;
using KappaDuck.Quack.Inputs;
using KappaDuck.Quack.Windows;

// Create the window
using Window window = new("Keyboard example", 1080, 720);

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

        // Detect if a key is pressed
        if (e.Type is EventType.KeyDown && e.Keyboard.Code is Keyboard.Scancode.A)
        {
            Console.WriteLine("A key is pressed");
        }

        // You can use the extension methods to check if a key is pressed
        if (e.IsKeyDown(Keyboard.Scancode.D))
        {
            Console.WriteLine("D key is pressed");
        }

        // Detect if a key is released
        if (e.IsKeyUp(Keyboard.Scancode.A))
        {
            Console.WriteLine("A key is released");
        }

        // Detect if a modifier key is pressed
        if (e.Type == EventType.KeyDown && e.Keyboard.Modifiers == Keyboard.Modifier.LeftShift)
        {
            Console.WriteLine("Shift key is pressed");
        }
    }

    // You can also check if a key is pressed outside of the event loop
    if (Keyboard.IsDown(Keyboard.Scancode.S))
    {
        Console.WriteLine("S key is pressed");
    }
}
