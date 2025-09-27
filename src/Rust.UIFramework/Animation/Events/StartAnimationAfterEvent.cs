using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Animation;

public class StartAnimationAfterEvent : BasePoolable, IAnimationCompleted
{
    private BaseAnimation _animation;

    public static StartAnimationAfterEvent Create(BaseAnimation animation) => animation.PluginPool.Get<StartAnimationAfterEvent>().Init(animation);

    public StartAnimationAfterEvent Init(BaseAnimation animation)
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