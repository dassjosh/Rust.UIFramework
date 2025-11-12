using Network;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Libraries;

namespace Oxide.Ext.UiFramework.Threading;

internal class UiSendRequest : BaseUiRequest, IUiRequest
{
    public BaseBuilder Builder;
    public UiDebugOptions? Options;

    public static UiSendRequest Create(BaseBuilder builder, SendInfo send, in UiDebugOptions? options)
    {
        UiSendRequest request = builder.PluginPool?.Get<UiSendRequest>() ?? UiPool.Internal.Get<UiSendRequest>();
        request.Init(builder, send, options);
        return request;
    }

    protected void Init(BaseBuilder builder, SendInfo send, in UiDebugOptions? options)
    {
        base.Init(send);
        Builder = builder;
        Options = options;
    }
    
    public virtual void SendRequest()
    {
        Builder.SendUi(Send, Options);
        Builder.SendAnimations(Send);
    }
    
    protected override void EnterPool()
    {
        base.EnterPool();
        Builder.TryDispose();
        Builder = null;
    }
}