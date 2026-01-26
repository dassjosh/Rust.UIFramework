using System.Threading.Tasks;
using Oxide.Ext.UiFramework.Threading;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.Extensions;

#if SERVER
using System;
using Network;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.UiElements;
#endif

namespace Oxide.Ext.UiFramework.Animation;

internal class AnimationTrackerChannel : BaseThreadedUiChannel<UiSendRequest>, ISingleton
{
    private AnimationTrackerChannel() : base(1) { }
    
    protected override ValueTask ProcessItem(UiSendRequest item)
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
        return ValueTask.Completed;
    }
}