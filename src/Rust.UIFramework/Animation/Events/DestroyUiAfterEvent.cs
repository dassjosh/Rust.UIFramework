using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Animation;

public sealed class DestroyUiAfterEvent : BasePoolable, IAnimationEvent
{
    public string Name;
    
    public static DestroyUiAfterEvent Create(IUiFrameworkPlugin plugin, string name) => plugin.PluginPool.Get<DestroyUiAfterEvent>().Init(name);

    private DestroyUiAfterEvent Init(string name)
    {
        Name = name;
        return this;
    }
    
    public bool IsForEvent(AnimationEventType type) => type == AnimationEventType.Finalized;
    public void OnAnimationEvent(in AnimationRef<IAnimation> animation, AnimationEventType type)
    {
        if (!animation.IsValid)
        {
            return;
        }
        
        ISendableAnimation sendable = animation.Animation.GetSendable();
        if (sendable is not null)
        {
            BaseBuilder.DestroyUi(sendable.Send, Name);
        }
    }
    
    protected override void EnterPool()
    {
        Name = default;
    }
}