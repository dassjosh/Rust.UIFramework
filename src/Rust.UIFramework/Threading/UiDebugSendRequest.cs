using Network;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Libraries;

namespace Oxide.Ext.UiFramework.Threading;

internal class UiDebugSendRequest : UiSendRequest
{
    public UiDebugOptions Options;

    public static UiDebugSendRequest Create(BaseBuilder builder, SendInfo send, in UiDebugOptions options)
    {
        UiDebugSendRequest request = builder.PluginPool?.Get<UiDebugSendRequest>() ?? UiPool.Internal.Get<UiDebugSendRequest>();
        request.Init(builder, send);
        request.Options = options;
        return request;
    }

    private void Init(BaseBuilder builder, SendInfo send, in UiDebugOptions options)
    {
        base.Init(builder, send);
        Options = options;
    }
    
    public override void SendRequest()
    {
        Builder.SendUi(Send, Options);
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        Options = default;
    }
}