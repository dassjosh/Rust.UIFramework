using System;
using System.Collections.Concurrent;
using System.Threading;
using Cysharp.Threading.Tasks;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

public sealed class UiChannel<T> : IUiChannel where T : IBaseUiChannelObject
{
    private readonly UiChannelOptions _options;
    private readonly IUiChannelHandler<T> _handler;
    private readonly ConcurrentQueue<T> _queue = new();
    private readonly CancellationTokenSource _cancellationTokenSource = new();
    private readonly object _taskLock = new();
    private readonly IUiLogger _logger;
    private int _activeWorkerCount;

    internal UiChannel(UiChannelOptions options, IUiChannelHandler<T> handler = null)
    {
        _options = options;
        _handler = handler;
        _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger(GetType());
    }

    public void Enqueue(T item)
    {
        if (_cancellationTokenSource.IsCancellationRequested) return;
        _queue.Enqueue(item);
        StartWorkers();
    }

    private void StartWorkers()
    {
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
            int workersToStart = Math.Min(_options.MaxConcurrency - _activeWorkerCount, _queue.Count);
            for (int i = 0; i < workersToStart; i++)
            {
                Interlocked.Increment(ref _activeWorkerCount);
                _logger.Debug("Started new worker task. Active workers: {0}/{1}", _activeWorkerCount, _options.MaxConcurrency);
                Run().Forget();
            }
        }
    }

    private bool CanStartNewWorkers()
    {
        // Only start new workers if we're below the maximum and queue has items
        return _activeWorkerCount < _options.MaxConcurrency && !_queue.IsEmpty;
    }

    private async UniTaskVoid Run()
    {
        if (_options.EnableMultithreading)
        {
            await UniTask.SwitchToThreadPool();
        }
        else
        {
#if SERVER
            await UniTask.SwitchToMainThread();
#endif
        }

        CancellationToken cancellationToken = _cancellationTokenSource.Token;
        try
        {
            while (!cancellationToken.IsCancellationRequested && _queue.TryDequeue(out T request))
            {
               await ProcessInternal(request);
            }
        }
        finally
        {
            Interlocked.Decrement(ref _activeWorkerCount);
            _logger.Debug("Worker task shutting down due to empty queue. Active workers: {0}", _activeWorkerCount);
        }
    }

    private async UniTask ProcessInternal(T item)
    {
        try
        {
            ProcessResult result;
            try
            {
                result = await Process(item);
            }
            catch (Exception ex)
            {
                _logger.Error("An error occured processing channel item", ex);
                result = ProcessResult.Failed;
            }

            await HandleProcessResult(item, result);
        }
        catch (Exception ex)
        {
            _logger.Error("An error occured processing channel item", ex);
            await OnException(item, ex);
        }
        finally
        {
            await Complete(item);
        }
    }

    private async UniTask<ProcessResult> Process(T item)
    {
        return _handler switch
        {
            IUiChannelProcess<T> handlerProcess => handlerProcess.Process(item),
            IUiChannelAsyncProcess<T> handlerAsyncProcess => await handlerAsyncProcess.Process(item),
            _ => item switch
            {
                IUiChannelProcess process => process.Process(),
                IUiChannelAsyncProcess asyncProcess => await asyncProcess.Process(),
                _ => throw new Exception($"Channel object {item.GetType().GetRealTypeName()} does not inherit from {nameof(IUiChannelProcess)} or {nameof(IUiChannelAsyncProcess)}")
            }
        };
    }

    private async UniTask HandleProcessResult(T item, ProcessResult result)
    {
        switch (_handler)
        {
            case IUiChannelProcessResult<T> handlerProcess:
                if (result == ProcessResult.Success)
                {
                    handlerProcess.OnSuccess(item);
                }
                else
                {
                    handlerProcess.OnFailed(item);
                }
                break;
            case IUiChannelProcessAsyncResult<T> handlerAsyncProcess:
                if (result == ProcessResult.Success)
                {
                    await handlerAsyncProcess.OnSuccess(item);
                }
                else
                {
                    await handlerAsyncProcess.OnFailed(item);
                }
                break;
        }

        switch (item)
        {
            case IUiChannelProcessResult process:
                if (result == ProcessResult.Success)
                {
                    process.OnSuccess();
                }
                else
                {
                    process.OnFailed();
                }
                break;
            case IUiChannelProcessAsyncResult asyncProcess:
                if (result == ProcessResult.Success)
                {
                    await asyncProcess.OnSuccess();
                }
                else
                {
                    await asyncProcess.OnFailed();
                }
                break;
        }
    }

    private async UniTask OnException(T item, Exception ex)
    {
        try
        {
            switch (_handler)
            {
                case IUiChannelException<T> handlerProcess:
                    handlerProcess.OnException(item, ex);
                    break;
                case IUiChannelExceptionAsync<T> handlerAsyncProcess:
                    await handlerAsyncProcess.OnException(item, ex);
                    break;
            }

            switch (item)
            {
                case IUiChannelException process:
                    process.OnException(ex);
                    break;
                case IUiChannelExceptionAsync asyncProcess:
                    await asyncProcess.OnException(ex);
                    break;
            }
        }
        catch (Exception inner)
        {
            _logger.Error("An error occured processing channel item exception", inner);
        }
    }

    private async UniTask Complete(T item)
    {
        switch (_handler)
        {
            case IUiChannelComplete<T> handlerProcess:
                handlerProcess.OnCompleted(item);
                break;
            case IUiChannelAsyncComplete<T> handlerAsyncProcess:
                await handlerAsyncProcess.OnCompleted(item);
                break;
        }

        switch (item)
        {
            case IUiChannelComplete process:
                process.OnCompleted();
                break;
            case IUiChannelAsyncComplete asyncProcess:
                await asyncProcess.OnCompleted();
                break;
        }
    }

    public void Stop()
    {
        _cancellationTokenSource.Cancel();
    }

#if UNIT_TESTS || BENCHMARKS
    public void WaitUntilFinished()
    {
        while (!_queue.IsEmpty)
        {
            Thread.Sleep(10);
        }
    }
#endif

}