#!/usr/bin/env dotnet

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Diagnostics;

const string image = "sdl3-build-image";
const string source = "SDL3";

BuildImage(image, source);
CopyLibraries(image, source);

void BuildImage(string image, string source)
{
    Console.WriteLine("Building Docker image...");

    ProcessStartInfo info = new()
    {
        FileName = "docker",
        Arguments = $"build {source} -t {image} --platform linux/amd64",
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        UseShellExecute = false
    };

    using Process process = Process.Start(info)!;

    process.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);
    process.ErrorDataReceived += (sender, e) => Console.Error.WriteLine(e.Data);

    process.BeginOutputReadLine();
    process.BeginErrorReadLine();

    process.WaitForExit();
}

void CopyLibraries(string image, string destination)
{
    string containerId = CreateContainer(image);

    Console.WriteLine("Copying libraries from container...");

    ProcessStartInfo info = new()
    {
        FileName = "docker",
        Arguments = $"cp {containerId}:/src/runtimes {destination}",
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        UseShellExecute = false
    };

    using Process process = Process.Start(info)!;
    process.WaitForExit();

    RemoveContainer(containerId);
}

string CreateContainer(string image)
{
    Console.WriteLine("Creating temporary container...");

    ProcessStartInfo info = new()
    {
        FileName = "docker",
        Arguments = $"create {image}",
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        UseShellExecute = false
    };

    using Process process = Process.Start(info)!;

    string containerId = process.StandardOutput.ReadToEnd().Trim();
    process.WaitForExit();

    return containerId;
}

void RemoveContainer(string containerId)
{
    Console.WriteLine("Removing temporary container...");

    ProcessStartInfo info = new()
    {
        FileName = "docker",
        Arguments = $"rm {containerId}",
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        UseShellExecute = false
    };

    using Process process = Process.Start(info)!;
    process.WaitForExit();
}
