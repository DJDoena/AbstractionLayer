using System;

namespace DoenaSoft.AbstractionLayer.IO.Default.Tests.Mocks;

/// <summary>
/// A synchronous implementation of IProgress for testing purposes.
/// Unlike Progress{T}, this invokes callbacks immediately without posting to a synchronization context.
/// </summary>
internal sealed class SynchronousProgress<T> : IProgress<T>
{
    private readonly Action<T> _handler;

    public SynchronousProgress(Action<T> handler)
    {
        _handler = handler ?? throw new ArgumentNullException(nameof(handler));
    }

    public void Report(T value)
    {
        _handler(value);
    }
}