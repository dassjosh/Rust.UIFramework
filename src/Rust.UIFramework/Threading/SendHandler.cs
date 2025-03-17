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
    private static int _sendAttempts;
    
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
        while (true)
        {
            if(SendInternal())
            {
                _sendAttempts = 0;
            }
            Reset.WaitOne(GetTimeout(_sendAttempts++));
        }
    }

    private static bool SendInternal()
    {
        bool didSend = false;
        while (Queue.TryDequeue(out IUiRequest request))
        {
            didSend = true;
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

        return didSend;
    }

    private static int GetTimeout(int attempts)
    {
        if (attempts > 10)
        {
            return -1;
        }

        return 1 << attempts;
    }
}