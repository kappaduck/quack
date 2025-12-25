#!/usr/bin/env dotnet

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Events;
using KappaDuck.Quack.Graphics.Rendering;
using KappaDuck.Quack.Inputs;

// Create the window
using RenderWindow window = new("Keyboard example", 1080, 720);

// Run the main loop
while (window.IsOpen)
{
    // Poll events
    while (window.Poll(out Event e))
    {
        // If the user requests to quit the application, it will automatically close the window and exit the loop.
        // You can close the window by clicking the close button or pressing ESC key
        if (e.RequestQuit())
        {
            return;
        }

        // Detect if a key is pressed
        if (e.Type is EventType.KeyDown && e.Keyboard.Code is Scancode.A)
        {
            Console.WriteLine("A key is pressed");
        }

        // You can use the extension methods to check if a key is pressed
        if (e.IsKeyDown(Scancode.D))
        {
            Console.WriteLine("D key is pressed");
        }

        // Detect if a key is released
        if (e.IsKeyUp(Scancode.A))
        {
            Console.WriteLine("A key is released");
        }

        // Detect if a modifier key is pressed
        if (e.Type == EventType.KeyDown && e.Keyboard.Modifiers == Modifier.LeftShift)
        {
            Console.WriteLine("Shift key is pressed");
        }

        // Detect if a key is pressed along with a modifier key
        if (e.IsKeyDown(Scancode.W, Modifier.LeftShift))
        {
            Console.WriteLine("W key is pressed while Left Shift is held down");
        }

        // You can also check for virtual keys instead of physical codes
        if (e.IsKeyDown(Keycode.E))
        {
            Console.WriteLine("E key is pressed");
        }
    }

    // You can also check if a key is pressed outside of the event loop
    if (Keyboard.IsDown(Scancode.S))
    {
        Console.WriteLine("S key is pressed");
    }
}
