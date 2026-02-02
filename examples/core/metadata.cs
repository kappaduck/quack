#!/usr/bin/env dotnet

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Core;

// Set application metadata
// This is optional but recommended to identify your application
// Important to call this before any module initialization
QuackEngine.SetMetadata(new ApplicationMetadata
{
    Name = "Quack Example - Metadata",
    Author = "KappaDuck",
    Version = "1.0.0",
    Identifier = "com.kappaduck.quack.examples.metadata",
    Copyright = "© KappaDuck 2025",
    Url = new Uri("https://kappaduck.com/quack"),
    Type = ApplicationType.Application
});

Console.WriteLine("Application metadata has been set.");

// You can retrieve and display the metadata later
// You can use this information for logging, about dialogs, etc.
Console.WriteLine($"Application Name: {QuackEngine.Metadata.Name}");
Console.WriteLine($"Application Author: {QuackEngine.Metadata.Author}");
Console.WriteLine($"Application Version: {QuackEngine.Metadata.Version}");
Console.WriteLine($"Application Identifier: {QuackEngine.Metadata.Identifier}");
Console.WriteLine($"Application Copyright: {QuackEngine.Metadata.Copyright}");
Console.WriteLine($"Application URL: {QuackEngine.Metadata.Url}");
Console.WriteLine($"Application Type: {QuackEngine.Metadata.Type.Name}");
