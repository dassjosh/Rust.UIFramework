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
    
    private void Init(BaseBuilder builder, SendInfo send)
    {
        base.Init(send);
        Builder = builder;
    }
    
    public void SendRequest()
    {
        Builder.SendUi(Send);
    }
    
    protected override void EnterPool()
    {
        base.EnterPool();
        Builder = null;
    }
}