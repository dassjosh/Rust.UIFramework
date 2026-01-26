using System;
using System.Threading;
using System.Threading.Tasks;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Logging;

namespace Oxide.Ext.UiFramework.Threading;

internal abstract class BaseThreadedUiChannel<T>(int maxConcurrency) : BaseUiChannel<T> where T : IChannelObject<T>
{
    private int _activeWorkerCount;
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private readonly object _taskLock = new();

    public override void Enqueue(IUiChannelObject<T> item)
    {
        base.Enqueue(item);
        OnEnqueue();
    }
    
    private void OnEnqueue()
    {
        if (_cancellationTokenSource.IsCancellationRequested) return;
        
        if (!CanStartNewWorkers())
        {
            return;
        }
        
        lock (_taskLock)
        {
            if (!CanStartNewWorkers())
            {
                return;
            }

            // Calculate how many new workers we need
            int workersToStart = Math.Min(UiFrameworkConfig.Instance.ImageStorage.MaxConcurrentDownloads - _activeWorkerCount, Queue.Count);
            for (int i = 0; i < workersToStart; i++)
            {
#pragma warning disable EPC13
                Task.Factory.StartNew(
                    () => ProcessQueue(_cancellationTokenSource.Token),
                    _cancellationTokenSource.Token,
                    TaskCreationOptions.None,
                    TaskScheduler.Default);
#pragma warning restore EPC13
                    
                Interlocked.Increment(ref _activeWorkerCount);
                Logger.Debug("Started new worker task. Active workers: {0}/{1}", _activeWorkerCount, UiFrameworkConfig.Instance.ImageStorage.MaxConcurrentDownloads);
            }
        }
    }

    private bool CanStartNewWorkers()
    {
        // Only start new workers if we're below the maximum and queue has items
        return _activeWorkerCount < maxConcurrency && !Queue.IsEmpty;
    }

    private async ValueTask ProcessQueue(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested && Queue.TryDequeue(out IUiChannelObject<T> request))
            {
                await ProcessItemInternal(request).ConfigureAwait(false);
            }
        }
        finally
        {
            Logger.Debug("Worker task shutting down due to empty queue. Active workers: {0}", _activeWorkerCount);
            Interlocked.Decrement(ref _activeWorkerCount);
        }
    }

    private async ValueTask ProcessItemInternal(IUiChannelObject<T> request)
    {
        try
        {
            await ProcessItem(request.Item).ConfigureAwait(false);
        }
        catch (Exception ex) when (ex is not ObjectDisposedException)
        {
            Logger.Exception("An error occured in channel", ex);
        }
        finally
        {
            request.EnqueueNext();
        }
    }
    
    protected abstract ValueTask ProcessItem(T item);

    internal override void OnServerShutdown()
    {
        base.OnServerShutdown();
        _cancellationTokenSource.Cancel();
    }
}