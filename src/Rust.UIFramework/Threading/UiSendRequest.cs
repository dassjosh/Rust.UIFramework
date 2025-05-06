using Network;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Threading;

internal class UiSendRequest : BaseUiRequest, IUiRequest
{
    public BaseBuilder Builder;

    public static UiSendRequest Create(BaseBuilder builder, SendInfo send)
    {
        UiSendRequest request = UiFrameworkPool.Get<UiSendRequest>();
        request.Init(builder, send);
        return request;
    }
    
    private void Init(BaseBuilder builder, SendInfo send)
    {
        base.Init(send);
        Builder = builder;
    }
    
    public void SendUi()
    {
        Builder.SendUi(Send);
    }
    
    protected override void EnterPool()
    {
        base.EnterPool();
        Builder = null;
    }
}