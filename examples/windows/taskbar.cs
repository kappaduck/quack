#!/usr/bin/env dotnet

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Events;
using KappaDuck.Quack.Graphics.Rendering;
using KappaDuck.Quack.Inputs;
using KappaDuck.Quack.Windows.Progress;

using RenderWindow window = new("Quack! Playground", 1080, 720);

TaskbarProgress taskbar = window.TaskbarProgress;

taskbar.Completed += () => Console.WriteLine("Taskbar progress completed!");
taskbar.Canceled += () => Console.WriteLine("Taskbar progress canceled!");
taskbar.ErrorOccurred += (ex) => Console.WriteLine("Taskbar progress error: " + ex.Message);
taskbar.ProgressChanged += (progress) => Console.WriteLine($"Taskbar progress changed: {progress:P0}");

while (window.IsOpen)
{
    while (window.Poll(out Event e))
    {
        if (e.RequestQuit())
        {
            return;
        }

        if (e.IsKeyDown(Scancode.Space))
        {
            _ = SimulateProgressAsync(taskbar);
        }

        if (e.IsKeyDown(Scancode.I))
        {
            _ = SimulateIndeterminateAsync(taskbar);
        }

        if (e.IsKeyDown(Scancode.C))
        {
            taskbar.Cancel();
        }

        if (e.IsKeyDown(Scancode.X))
        {
            _ = SimulateCancelableAsync(taskbar);
        }

        if (e.IsKeyDown(Scancode.S))
        {
            using (taskbar.CreateIndeterminateScope())
            {
                for (int i = 0; i < 5; i++)
                {
                    Thread.Sleep(1000);
                }
            }
        }

        if (e.IsKeyDown(Scancode.W))
        {
            RunWeighted(taskbar);
        }

        if (e.IsKeyDown(Scancode.A))
        {
            RunSimple(taskbar);
        }
    }
}

static async Task SimulateProgressAsync(TaskbarProgress taskbar)
{
    await taskbar.CreateScopeAsync(async progress =>
    {
        for (int i = 0; i <= 100; i++)
        {
            progress.Report(i);
            await Task.Delay(100);
        }
    });
}

static async Task SimulateIndeterminateAsync(TaskbarProgress taskbar) => await taskbar.CreateScopeAsync(action: async _ => await Task.Delay(5000), indeterminate: true);

static void RunSimple(TaskbarProgress taskbar)
{
    using TaskbarProgressScope scope = taskbar.CreateScope();

    for (int i = 0; i < 100; i++)
    {
        scope.Report(i + 1);
        Thread.Sleep(50);
    }
}

static async Task SimulateCancelableAsync(TaskbarProgress taskbar)
{
    CancellationTokenSource cts = new();

    await taskbar.CreateScopeAsync(async (progress) =>
    {
        for (int i = 0; i <= 50; i++)
        {
            progress.Report(i);
            await Task.Delay(100, progress.CancellationToken);
        }

        await cts.CancelAsync();

        for (int i = 0; i <= 50; i++)
        {
            progress.Report(i);
            await Task.Delay(100, progress.CancellationToken);
        }
    }, cancellationToken: cts.Token);
}

static void RunWeighted(TaskbarProgress taskbar)
{
    IWeightedProgress root = taskbar.ToWeightedRoot();

    // Stage 1 (50%)
    IWeightedProgress stage1 = root.Create(0.5f);
    for (int i = 0; i <= 10; i++)
    {
        stage1.Report(i / 10f);
        Thread.Sleep(30);
    }

    // Stage 2 (30%)
    IWeightedProgress stage2 = root.Create(0.3f);
    for (int i = 0; i <= 6; i++)
    {
        stage2.Report(i / 6f);
        Thread.Sleep(40);
    }

    // Stage 3 (20%)
    IWeightedProgress stage3 = root.Create(0.2f);
    for (int i = 0; i <= 4; i++)
    {
        stage3.Report(i / 4f);
        Thread.Sleep(600);
    }
}
