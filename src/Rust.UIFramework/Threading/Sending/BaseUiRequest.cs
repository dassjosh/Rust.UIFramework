using Network;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Threading;

internal abstract class BaseUiRequest : BasePoolable, IUiRequest
{
    public SendInfo Send;

    protected void Init(SendInfo send)
    {
        Send = send;
    }
    
    public virtual IUiChannel<IUiRequest> GetChannel(int index)
    {
        return index switch
        {
            0 => Singleton<SendHandler>.Instance.Channel,
            _ => null
        };
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
}