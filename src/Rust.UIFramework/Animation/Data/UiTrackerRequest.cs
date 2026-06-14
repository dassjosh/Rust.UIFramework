using System;
using Cysharp.Threading.Tasks;
using Network;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Threading;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Animation;

public class UiTrackerRequest : BasePoolable, IUiChannelObject
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

    public ProcessResult Process()
    {
#if SERVER
        if (Builder is BaseUiBuilder builder)
        {
            SendInfo send = Send;
            ReadOnlySpan<BaseUiComponent> span = builder.ComponentAsReadonly();
            for (int index = 0; index < span.Length; index++)
            {
                BaseUiComponent component = span[index];
                if (component.Update is not UpdateMode.Update || component.ActiveTracked.HasChanged && !component.ActiveTracked.Value)
                {
                    Singleton<AnimationTracker>.Instance.RemoveUiForSend(send, component.Name);
                }
            }
        }
#endif

        return ProcessResult.Success;
    }

    public void OnCompleted()
    {
        Dispose();
    }
    
    protected override void EnterPool()
    {
        base.EnterPool();
        Send = default;
        Builder.TryDispose();
        Builder = null;
    }
}