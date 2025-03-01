using System;
using System.Collections.Concurrent;
using System.Threading;
using Oxide.Core;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Threading;

internal class SendHandler : ISingleton
{
    private readonly ConcurrentQueue<IUiRequest> _queue = new();
    private readonly AutoResetEvent _reset = new(false);
    private readonly Thread _thread;
    private readonly CancellationTokenSource _source = new();
    private readonly ILogger<SendHandler> _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<SendHandler>();
    
    private SendHandler()
    {
        _thread = new Thread(Send)
        {
            IsBackground = true,
            Name = $"UiFramework.{nameof(SendHandler)}",
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
            SendInternal();
            _reset.WaitOne();
        }
    }

    private void SendInternal()
    {
        while (_queue.TryDequeue(out IUiRequest request))
        {
            try
            {
#if !BENCHMARKS
                request.SendUi();
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
    }

    internal void OnServerShutdown()
    {
        _source.Cancel();
        _reset.Set();
    }
}