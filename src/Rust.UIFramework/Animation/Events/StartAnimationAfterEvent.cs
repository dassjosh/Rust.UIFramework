using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Animation;

public class StartAnimationAfterEvent : BasePoolable, IAnimationCompleted
{
    private BaseAnimation _animation;

    public static StartAnimationAfterEvent Create(BaseAnimation animation, TimeoutAnimationAction action) => animation.PluginPool.Get<StartAnimationAfterEvent>().Init(animation, action);

    public StartAnimationAfterEvent Init(BaseAnimation animation, TimeoutAnimationAction action)
    {
        _animation = animation;
        return this;
    }
    
    public void OnAnimationCompleted(BaseAnimation animation)
    {
        _animation.TriggerDelayComplete();
    }

    protected override void EnterPool()
    {
        _animation = null;
    }
}