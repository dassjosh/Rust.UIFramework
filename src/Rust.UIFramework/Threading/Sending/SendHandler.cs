using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Threading;

internal class SendHandler : ISingleton
{
    internal readonly BaseUiChannel<IUiRequest> Channel;

    private SendHandler()
    {
        if (ThreadingHelper.UiMultiThreaded)
        {
            Channel = new ThreadedSendChannel();
        }
        else
        {
            Channel = new InstantSendChannel();
        }
    }

    private sealed class ThreadedSendChannel() : BaseThreadedUiChannel<IUiRequest>(1)
    {
        protected override UniTask ProcessItem(IUiRequest item)
        {
#if SERVER
            item.SendRequest();
#endif
            return UniTask.CompletedTask;
        }
    }

    private sealed class InstantSendChannel : BaseInstantUiChannel<IUiRequest>
    {
        protected override void ProcessItem(IUiRequest item)
        {
#if SERVER
            item.SendRequest();
#endif
        }
    }
    
    internal void OnServerShutdown()
    {
        Channel.OnServerShutdown();
    }
}