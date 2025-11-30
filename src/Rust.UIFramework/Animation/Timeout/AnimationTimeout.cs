using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Animation;

public class AnimationTimeout : BasePoolable, IAnimationTimeout
{
    public float Timeout { get; set; }
    public AnimationTimeoutAction Action { get; set; } = AnimationTimeoutAction.CancelAnimation;
    public bool HasTimedOut => AnimationTime.CurrentTime - _startTime >= Timeout;
    private float _startTime;
    
    public static AnimationTimeout Create(IUiFrameworkPlugin plugin, float timeout, AnimationTimeoutAction action) => plugin.PluginPool.Get<AnimationTimeout>().Init(timeout, action);
    
    protected AnimationTimeout Init(float timeout, AnimationTimeoutAction action)
    {
        Timeout = timeout;
        Action = action;
        return this;
    }
    
    public void OnTick() { }

    public void OnStarted()
    {
        _startTime = AnimationTime.CurrentTime;
    }
    
    protected override void EnterPool()
    {
        base.EnterPool();
        Timeout = 0;
        _startTime = 0;
        Action = AnimationTimeoutAction.CancelAnimation;
    }
}