using System;
using System.Collections.Concurrent;
using System.Threading;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Threading;

internal class SendHandler : ISingleton
{
    private readonly ConcurrentQueue<IUiRequest> _queue = new();
    private readonly AutoResetEvent _reset = new(false);
    private readonly Thread _thread;
    private readonly CancellationTokenSource _source = new();
    private int _sendAttempts;
    private readonly IUiLogger<SendHandler> _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<SendHandler>();
    
    private SendHandler()
    {
        _thread = new Thread(Send)
        {
            IsBackground = true,
            Name = $"{UiFrameworkExtension.Instance.Name}.{nameof(SendHandler)}",
        };
        _thread.Start();
    }
    
    internal void Enqueue(IUiRequest request)
    {
        _queue.Enqueue(request);
        _reset.Set();
    }

    private void Send()
    {
        while (!_source.IsCancellationRequested)
        {
            if(SendInternal())
            {
                _sendAttempts = 0;
            }
            _reset.WaitOne(GetTimeout(_sendAttempts++));
        }
    }

    private bool SendInternal()
    {
        bool didSend = false;
        while (_queue.TryDequeue(out IUiRequest request))
        {
            didSend = true;
            try
            {
#if !BENCHMARKS && !UNIT_TESTS
                request.SendRequest();
#endif
            }
            catch (Exception ex)
            {
                _logger.Exception("An error occured during UI Send", ex);
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
    
#if UNIT_TESTS || BENCHMARKS
    internal void WaitUntilFinished()
    {
        while (!_queue.IsEmpty)
        {
            Thread.Sleep(10);
        }
    }
#endif

    internal void OnServerShutdown()
    {
        _source.Cancel();
        _reset.Set();
    }
}