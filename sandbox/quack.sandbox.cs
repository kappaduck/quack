#!/usr/bin/env dotnet

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

#:property TargetFramework=net10.0
#:package KappaDuck.Quack

using KappaDuck.Quack;
using KappaDuck.Quack.Core;
using KappaDuck.Quack.Events;
using KappaDuck.Quack.Graphics.Rendering;

using QuackEngine _ = QuackEngine.Init(Subsystem.Video);

using RenderWindow window = new("Quack! Sandbox", 1080, 720);

while (window.IsOpen)
{
    while (window.Poll(out Event e))
    {
        if (e.RequestQuit())
        {
            window.Close();
            return;
        }
    }
}
