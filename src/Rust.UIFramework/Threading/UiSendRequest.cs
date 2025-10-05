using Network;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Libraries;

namespace Oxide.Ext.UiFramework.Threading;

internal class UiSendRequest : BaseUiRequest, IUiRequest
{
    public BaseBuilder Builder;

    public static UiSendRequest Create(BaseBuilder builder, SendInfo send)
    {
        UiSendRequest request = builder.PluginPool?.Get<UiSendRequest>() ?? UiPool.Internal.Get<UiSendRequest>();
        request.Init(builder, send);
        return request;
    }
    
    protected void Init(BaseBuilder builder, SendInfo send)
    {
        base.Init(send);
        Builder = builder;
    }
    
    public virtual void SendRequest()
    {
        Builder.SendUi(Send, null);
    }
    
    protected override void EnterPool()
    {
        base.EnterPool();
        Builder.TryDispose();
        Builder = null;
    }
}