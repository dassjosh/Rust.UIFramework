using Network;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Threading;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Animation;

public class UiTrackerRequest : BasePoolable, IUiChannelObject<UiTrackerRequest>
{
    public SendInfo Send;
    public BaseBuilder Builder;
    
    public static UiTrackerRequest Create(IUiFrameworkPlugin plugin, BaseBuilder builder, SendInfo send)
    {
        UiTrackerRequest request = plugin.PluginPool.Get<UiTrackerRequest>();
        request.Init(builder, send);
        return request;
    }

    public void Init(BaseBuilder builder, SendInfo send)
    {
        Builder = builder;
        Send = send;
    }
    
    public void Enqueue()
    {
        Singleton<AnimationTrackerChannel>.Instance.Enqueue(this);
    }

    public void OnCompleted()
    {
        Dispose();
    }
    
    protected override void EnterPool()
    {
        base.EnterPool();
        Builder.TryDispose();
        Builder = null;
    }
}