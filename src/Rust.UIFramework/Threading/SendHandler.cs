using System;
using System.Collections.Concurrent;
using System.Threading;
using Oxide.Core;

namespace Oxide.Ext.UiFramework.Threading;

internal static class SendHandler
{
    private static readonly ConcurrentQueue<UiSendRequest> Queue = new();
    private static readonly AutoResetEvent Reset = new(false);
    private static readonly Thread _thread;
    
    static SendHandler()
    {
#if !BENCHMARKS
        _thread = new Thread(Send)
        {
            IsBackground = true,
            Name = $"UiFramework.{nameof(SendHandler)}",
        };
        _thread.Start();
#endif
    }
    
    internal static void Enqueue(UiSendRequest request)
    {
        Queue.Enqueue(request);
        Reset.Set();
    }

    private static void Send()
    {
        while (true)
        {
            SendInternal();
            Reset.WaitOne();
        }
    }

    private static void SendInternal()
    {
        while (Queue.TryDequeue(out UiSendRequest request))
        {
            try
            {
                request.Builder.SendUi(request.Send);
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
}