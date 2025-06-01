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
        if (Send.connections != null)
        {
            UiFrameworkPool.FreeList(Send.connections);
        }
        Send = default;
    }
}