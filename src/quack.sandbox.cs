#!/usr/bin/env dotnet

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

#:property TargetFramework=net10.0
#:property GenerateDocumentationFile=false
#:property IncludeNativeLibraries=true
#:project KappaDuck.Quack

using KappaDuck.Quack;
using KappaDuck.Quack.Core;

using QuackEngine _ = QuackEngine.Init(SubSystem.Video);

Console.WriteLine($"Quack Engine using SDL: {QuackEngine.Version}");
