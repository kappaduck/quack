#!/usr/bin/env dotnet

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

#:property TargetFramework=net10.0
#:property GenerateDocumentationFile=false
#:property IncludeNativeLibraries=true
#:project KappaDuck.Quack

using KappaDuck.Quack;
using KappaDuck.Quack.Core;
using KappaDuck.Quack.Events;
using KappaDuck.Quack.Windows;

using QuackEngine _ = QuackEngine.Init(Subsystem.Video);

using Window window = new("Quack! Sandbox", 800, 600, WindowState.Resizable);

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
