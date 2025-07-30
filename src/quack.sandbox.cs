#!/usr/bin/env dotnet

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

#:property TargetFramework=net10.0
#:property GenerateDocumentationFile=false
#:property IncludeNativeLibraries=true
#:project KappaDuck.Quack

using KappaDuck.Quack;
using KappaDuck.Quack.Core;

using QuackEngine _ = QuackEngine.Init(Subsystem.Video, new AppMetadata
{
    Id = "com.kappaduck.quack.sandbox",
    Name = "Quack! Sandbox",
    Version = "0.1.0",
    Author = "KappaDuck",
    Copyright = "Copyright (c) KappaDuck. All rights reserved.",
    Type = AppMetadata.AppType.Application
});

Console.WriteLine($"Quack Engine using SDL {QuackEngine.Version}");
