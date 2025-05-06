using Network;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Threading;

public abstract class BaseUiRequest : BasePoolable
{
    protected SendInfo Send;

    protected void Init(SendInfo send)
    {
        Send = send;
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        if (Send.connections != null)
        {
            ListPool<Connection>.Instance.Free(Send.connections);
        }
        Send = default;
    }
}