using Network;
using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Libraries;

namespace Oxide.Ext.UiFramework.Threading;

internal class UiSendRequest : BaseUiRequest
{
    public BaseBuilder Builder;
    public UiDebugOptions? Options;

    public static UiSendRequest Create(BaseBuilder builder, SendInfo send, in UiDebugOptions? options)
    {
        UiSendRequest request = builder.PluginPool?.Get<UiSendRequest>() ?? UiPool.Internal.Get<UiSendRequest>();
        request.Init(builder, send, options);
        return request;
    }

    private void Init(BaseBuilder builder, SendInfo send, in UiDebugOptions? options)
    {
        base.Init(send);
        Builder = builder;
        Options = options;
    }
    
    public override ProcessResult Process()
    {
        Builder.SendUi(Send, Options);
        Builder.SendAnimations(Send);
        return ProcessResult.Success;
    }
    
    public override void OnCompleted()
    {
        if (Builder.Plugin != null)
        {
            UiTrackerRequest.Create(Builder.Plugin, Builder, Send).Enqueue();
        }
        else
        {
            Builder.Dispose();
        }

        Dispose();
    }
    
    protected override void EnterPool()
    {
        Options = null;
        Builder = null;
        Send = default;
    }
}