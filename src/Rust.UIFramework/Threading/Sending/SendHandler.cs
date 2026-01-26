using System.Threading.Tasks;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Threading;

internal class SendHandler : ISingleton
{
    internal readonly BaseUiChannel<IUiRequest> Channel;

    private SendHandler()
    {
        if (UiFrameworkConfig.Instance.Threading.EnableUiSendingThread)
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
        protected override ValueTask ProcessItem(IUiRequest item)
        {
#if SERVER
            item.SendRequest();
#endif
            return ValueTask.Completed;
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