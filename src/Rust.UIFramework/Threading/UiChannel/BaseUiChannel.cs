using System;
using System.Collections.Concurrent;
using System.Threading;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Threading.UiChannel;

internal abstract class BaseUiChannel<T> : IUiChannel where T : IChannelObject
{
    private readonly ConcurrentQueue<IUiChannelObject<T>> _queue = new();
    private readonly AutoResetEvent _reset = new(false);
    private readonly Thread _thread;
    private readonly CancellationTokenSource _source = new();
    private int _sendAttempts;
    private readonly IUiLogger _logger;
    protected readonly bool EnableThreading;
    
    protected BaseUiChannel(bool enableThreading)
    {
        EnableThreading = enableThreading;
        _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger(GetType());
        if (enableThreading)
        {
            _thread = new Thread(Run)
            {
                IsBackground = true,
                Name = $"{UiFrameworkExtension.Instance.Name}.{GetType().Name}"
            };
            _thread.Start();
        }
    }
    
    public void Enqueue(IUiChannelObject item)
    {
        if (item is not IUiChannelObject<T> obj)
        {
            throw new Exception($"Invalid item type: {item.GetType().FullName}");
        }

        if (!EnableThreading)
        {
            ProcessRequestInternal(obj);
            return;
        }
            
        _queue.Enqueue(obj);
        _reset.Set();
    }
    
    private void Run()
    {
        while (!_source.IsCancellationRequested)
        {
            if(RunInternal())
            {
                _sendAttempts = 0;
            }
            _reset.WaitOne(GetTimeout(_sendAttempts++));
        }
    }
    
    private bool RunInternal()
    {
        bool didRun = false;
        while (_queue.TryDequeue(out IUiChannelObject<T> request))
        {
            didRun = true;
            ProcessRequestInternal(request);
        }

        return didRun;
    }

    private void ProcessRequestInternal(IUiChannelObject<T> request)
    {
        try
        {
            ProcessItem(request.Item);
        }
        catch (Exception ex) when (ex is not ObjectDisposedException)
        {
            _logger.Exception("An error occured in channel", ex);
        }
        finally
        {
            request.EnqueueNext();
        }
    }

    protected abstract void ProcessItem(T item);
    
    private static int GetTimeout(int attempts)
    {
        if (attempts > 4)
        {
            return -1;
        }

        return 1 << attempts;
    }
    
#if !SERVER
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