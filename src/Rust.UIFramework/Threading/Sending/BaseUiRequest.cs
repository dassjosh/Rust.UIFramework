using Network;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Threading;

public abstract class BaseUiRequest : BasePoolable
{
    public SendInfo Send;

    protected void Init(SendInfo send)
    {
        Send = send;
    }

    protected override void EnterPool()
    {
        if (Send.connections != null)
        {
            UiPool.Internal.FreeList(Send.connections);
        }
        Send = default;
    }
}