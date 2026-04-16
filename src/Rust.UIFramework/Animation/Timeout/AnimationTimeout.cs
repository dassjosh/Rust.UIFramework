using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Animation;

public class AnimationTimeout : BasePoolable, IAnimationTimeout, IAnimationComponent
{
    public float Timeout { get; set; }
    public AnimationTimeoutAction Action { get; set; } = AnimationTimeoutAction.CancelAnimation;
    public bool HasTimedOut => Owner.Time.CurrentTime - _startTime >= Timeout;
    
    public IAnimation Owner { get; private set; }
    
    private float _startTime;
    
    public static AnimationTimeout Create(IAnimation owner, float timeout, AnimationTimeoutAction action) => owner.Plugin.PluginPool.Get<AnimationTimeout>().Init(owner, timeout, action);
    
    protected AnimationTimeout Init(IAnimation owner, float timeout, AnimationTimeoutAction action)
    {
        Owner = owner;
        Timeout = timeout;
        Action = action;
        return this;
    }
    
    public void OnTick() { }

    public void OnStarted()
    {
        _startTime = Owner.Time.CurrentTime;
    }
    
    protected override void EnterPool()
    {
        base.EnterPool();
        Timeout = 0;
        _startTime = 0;
        Action = AnimationTimeoutAction.CancelAnimation;
        Owner = null;
    }
}