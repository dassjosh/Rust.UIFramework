using System;
using System.Collections.Concurrent;
using System.Threading;
using Oxide.Core;

namespace Oxide.Ext.UiFramework.Threading;

internal static class SendHandler
{
    private static readonly ConcurrentQueue<IUiRequest> Queue = new();
    private static readonly AutoResetEvent Reset = new(false);
    private static readonly Thread _thread;
    private static readonly CancellationTokenSource _source = new();
    
    static SendHandler()
    {
        _thread = new Thread(Send)
        {
            IsBackground = true,
            Name = $"UiFramework.{nameof(SendHandler)}",
        };
        _thread.Start();
    }
    
    internal static void Enqueue(IUiRequest request)
    {
        Queue.Enqueue(request);
        Reset.Set();
    }

    private static void Send()
    {
        while (!_source.IsCancellationRequested)
        {
            SendInternal();
            Reset.WaitOne();
        }
    }

    private static void SendInternal()
    {
        while (Queue.TryDequeue(out IUiRequest request))
        {
            try
            {
#if !BENCHMARKS
                request.SendUi();
#endif
            }
            catch (Exception ex)
            {
                Interface.Oxide.LogException("An error occured during UI Send", ex);
            }
            finally
            {
                request.Dispose();
            }
        }
    }

    internal static void OnServerShutdown()
    {
        _source.Cancel();
        Reset.Set();
    }
}