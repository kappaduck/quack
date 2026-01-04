// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

namespace KappaDuck.Quack.Windows.Progress;

internal readonly struct IndeterminateScope(TaskbarProgress progress) : IDisposable
{
    public void Dispose()
    {
        if (progress.IsCompleted || progress.IsCancelled)
            return;

        progress.Complete(forceReset: true);
    }
}
