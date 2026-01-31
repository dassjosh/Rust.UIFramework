using Network;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Threading;

internal abstract class BaseUiRequest : BasePoolable, IUiRequest
{
    public SendInfo Send;
    private static readonly IUiChannel<IUiRequest> Channel = Singleton<SendHandler>.Instance.Channel;

    protected void Init(SendInfo send)
    {
        Send = send;
    }

    public abstract void SendRequest();

    protected override void EnterPool()
    {
        if (Send.connections != null)
        {
            UiPool.Internal.FreeList(Send.connections);
        }
        Send = default;
    }

    public void Enqueue()
    {
        Channel.Enqueue(this);
    }

    public virtual void OnCompleted()
    {
        Dispose();
    }
}