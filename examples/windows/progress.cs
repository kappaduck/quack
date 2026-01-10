#!/usr/bin/env dotnet

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Events;
using KappaDuck.Quack.Graphics.Rendering;
using KappaDuck.Quack.Inputs;
using KappaDuck.Quack.Windows.Progress;

// Create a render window
using RenderWindow window = new("Window progress bar examples", 1080, 720)
{
    Resizable = true
};

// This example demonstrates various ways to use the window's taskbar icon progress bar
WindowProgressBar progressBar = window.ProgressBar;

// Subscribe to progress bar events
// These events can be used for logging or updating UI elements
// They are optional and can be omitted if not needed
// Completed event is raised when the progress bar completes successfully
progressBar.Completed += () => Console.WriteLine("Progress bar completed!");

// Cancelled event is raised when the progress bar is cancelled
progressBar.Cancelled += () => Console.WriteLine("Progress bar cancelled!");

// ErrorOccurred event is raised when an error occurs during the progress operation
progressBar.ErrorOccurred += (ex) => Console.WriteLine("Progress bar error: " + ex.Message);

// ProgressChanged event is raised whenever the progress value changes
progressBar.ProgressChanged += (progress) => Console.WriteLine($"Progress bar changed: {progress:P0}");

// Main event loop
while (window.IsOpen)
{
    // Poll events
    while (window.Poll(out Event e))
    {
        // Handle window close request
        if (e.RequestQuit())
        {
            return;
        }

        // Handle key presses to run different progress bar examples
        // Press Q for simple progress bar
        if (e.IsKeyDown(Scancode.Q))
        {
            RunSimple(window.ProgressBar);
        }

        // Press W for indeterminate progress bar
        if (e.IsKeyDown(Scancode.W))
        {
            RunIndeterminate(window.ProgressBar);
        }

        // Press E for asynchronous progress bar
        // Important: Do not await this method here to avoid blocking the main thread
        if (e.IsKeyDown(Scancode.E))
        {
            _ = RunAsync(window.ProgressBar);
        }

        // Press R for asynchronous indeterminate progress bar
        // Important: Do not await this method here to avoid blocking the main thread
        if (e.IsKeyDown(Scancode.R))
        {
            _ = RunIndeterminateAsync(window.ProgressBar);
        }

        // Press A for cancelable progress bar
        if (e.IsKeyDown(Scancode.A))
        {
            RunCancelable(window.ProgressBar);
        }

        // Press S for cancelable indeterminate progress bar
        if (e.IsKeyDown(Scancode.S))
        {
            RunIndeterminateCancelable(window.ProgressBar);
        }

        // Press D for asynchronous cancelable progress bar
        // Important: Do not await this method here to avoid blocking the main thread
        if (e.IsKeyDown(Scancode.D))
        {
            _ = RunCancelableAsync(window.ProgressBar);
        }

        // Press F for asynchronous cancelable indeterminate progress bar
        // Important: Do not await this method here to avoid blocking the main thread
        if (e.IsKeyDown(Scancode.F))
        {
            _ = RunIndeterminateCancelableAsync(window.ProgressBar);
        }

        // Press Z for progress bar that fails with an error
        if (e.IsKeyDown(Scancode.Z))
        {
            RunError(window.ProgressBar);
        }

        // Press X for asynchronous progress bar that fails with an error
        // Important: Do not await this method here to avoid blocking the main thread
        if (e.IsKeyDown(Scancode.X))
        {
            _ = RunErrorAsync(window.ProgressBar);
        }
    }
}

// Run a simple progress bar that completes successfully
// Important: This method blocks the calling thread
static void RunSimple(WindowProgressBar progressBar)
{
    Console.WriteLine("Starting simple progress bar...");
    progressBar.Start(static progress =>
    {
        for (int i = 0; i < 100; i++)
        {
            progress.Advance();
            Thread.Sleep(100);
        }
    });
}

// Run an indeterminate progress bar that completes successfully
// Important: This method blocks the calling thread
static void RunIndeterminate(WindowProgressBar progressBar)
{
    Console.WriteLine("Starting indeterminate progress bar...");
    progressBar.StartIndeterminate(static _ => Thread.Sleep(5000));
}

// Run an asynchronous progress bar that completes successfully
// This method does not block the calling thread so you can move the window as you like
static async Task RunAsync(WindowProgressBar progressBar)
{
    Console.WriteLine("Starting asynchronous progress bar...");
    await progressBar.StartAsync(async progress =>
    {
        for (int i = 0; i < 100; i++)
        {
            progress.Advance();
            await Task.Delay(100, progress.CancellationToken);
        }
    });
}

// Run an asynchronous indeterminate progress bar that completes successfully
// This method does not block the calling thread so you can move the window as you like
static async Task RunIndeterminateAsync(WindowProgressBar progressBar)
{
    Console.WriteLine("Starting asynchronous indeterminate progress bar...");
    await progressBar.StartIndeterminateAsync(async _ => await Task.Delay(TimeSpan.FromSeconds(5)));
}

// Run a cancelable progress bar that gets canceled at 70%
// Important: This method blocks the calling thread
static void RunCancelable(WindowProgressBar progressBar)
{
    Console.WriteLine("Starting cancelable progress bar...");
    progressBar.Start(static progress =>
    {
        for (int i = 0; i < 100; i++)
        {
            if (i == 70)
            {
                progress.Cancel();
            }

            progress.Advance();
            Thread.Sleep(100);
        }
    });
}

// Run a cancelable indeterminate progress bar that gets canceled.
// Important: This method blocks the calling thread
static void RunIndeterminateCancelable(WindowProgressBar progressBar)
{
    Console.WriteLine("Starting cancelable indeterminate progress bar...");
    progressBar.StartIndeterminate(static progress =>
    {
        Thread.Sleep(TimeSpan.FromSeconds(5));
        progress.Cancel();

        // This will not be reached because the progress is canceled above
        Thread.Sleep(TimeSpan.FromSeconds(5));
    });
}

// Run an asynchronous cancelable progress bar that gets canceled at 70%
// This method does not block the calling thread so you can move the window as you like
static async Task RunCancelableAsync(WindowProgressBar progressBar)
{
    Console.WriteLine("Starting asynchronous cancelable progress bar...");
    await progressBar.StartAsync(async progress =>
    {
        for (int i = 0; i < 100; i++)
        {
            if (i == 70)
            {
                await progress.CancelAsync();
            }

            progress.Advance();
            await Task.Delay(100, progress.CancellationToken);
        }
    });
}

// Run an asynchronous cancelable indeterminate progress bar that gets canceled.
// This method does not block the calling thread so you can move the window as you like
static async Task RunIndeterminateCancelableAsync(WindowProgressBar progressBar)
{
    Console.WriteLine("Starting asynchronous cancelable indeterminate progress bar...");
    await progressBar.StartIndeterminateAsync(async progress =>
    {
        progress.CancelAfter(TimeSpan.FromSeconds(7));
        await Task.Delay(TimeSpan.FromSeconds(10), progress.CancellationToken);
    });
}


// Run a simple progress bar that fails with an error
// Important: This method blocks the calling thread
static void RunError(WindowProgressBar progressBar)
{
    Console.WriteLine("Starting error progress bar...");
    progressBar.Start(static progress =>
    {
        for (int i = 0; i < 50; i++)
        {
            progress.Advance();
            Thread.Sleep(100);
        }

        throw new InvalidOperationException("An error occurred during the operation.");
    });
}


// Run an asynchronous progress bar that fails with an error
// This method does not block the calling thread so you can move the window as you like
static async Task RunErrorAsync(WindowProgressBar progressBar)
{
    Console.WriteLine("Starting asynchronous error progress bar...");
    await progressBar.StartAsync(async progress =>
    {
        for (int i = 0; i < 50; i++)
        {
            progress.Advance();
            await Task.Delay(100, progress.CancellationToken);
        }

        throw new InvalidOperationException("An error occurred during the operation.");
    });
}
