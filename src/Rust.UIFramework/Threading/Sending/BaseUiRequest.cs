using Network;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Threading.UiChannel;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Threading;

internal abstract class BaseUiRequest : BasePoolable, IChannelObject
{
    public SendInfo Send;

    protected void Init(SendInfo send)
    {
        Send = send;
    }
    
    public virtual IUiChannel GetChannel(int index)
    {
        return index switch
        {
            0 => (IUiChannel)Singleton<SendHandler>.Instance,
            _ => null
        };
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