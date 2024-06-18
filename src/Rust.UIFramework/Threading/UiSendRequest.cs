using Network;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Threading;

internal class UiSendRequest : BasePoolable
{
    public BaseBuilder Builder;
    public SendInfo Send;

    public static UiSendRequest Create(BaseBuilder builder, SendInfo send)
    {
        UiSendRequest request = UiFrameworkPool.Get<UiSendRequest>();
        request.Init(builder, send);
        return request;
    }
    
    private void Init(BaseBuilder builder, SendInfo send)
    {
        Builder = builder;
        Send = send;
    }
    
    protected override void EnterPool()
    {
        Builder = null;
        if (Send.connections != null)
        {
            ListPool<Connection>.Instance.Free(Send.connections);
        }
        Send = default;
    }
}