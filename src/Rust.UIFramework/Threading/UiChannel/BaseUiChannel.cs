using System.Collections.Concurrent;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Threading;

internal abstract class BaseUiChannel<T> : IUiChannel<T> where T : IChannelObject<T>
{
    internal readonly ConcurrentQueue<IUiChannelObject<T>> Queue = new();
    protected readonly IUiLogger Logger;
    
    protected BaseUiChannel()
    {
        Logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger(GetType());
    }
    
    public virtual void Enqueue(IUiChannelObject<T> item)
    {
        Queue.Enqueue(item);
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

    internal virtual void OnServerShutdown() { }
}