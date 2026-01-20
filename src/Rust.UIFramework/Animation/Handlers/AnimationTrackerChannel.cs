using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Threading;
using Oxide.Ext.UiFramework.Threading.UiChannel;
using Oxide.Ext.UiFramework.Types;

#if SERVER
using System;
using Network;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.UiElements;
#endif

namespace Oxide.Ext.UiFramework.Animation;

internal class AnimationTrackerChannel : BaseUiChannel<UiSendRequest>, ISingleton
{
    private AnimationTrackerChannel() : base(UiFrameworkConfig.Instance.Threading.EnableAnimationThread) { }
    
    protected override void ProcessItem(UiSendRequest item)
    {
#if SERVER
        if (item.Builder is BaseUiBuilder builder)
        {
            SendInfo send = item.Send;
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
    }
}