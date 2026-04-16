using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Animation;

public class TimeDelayAnimation : BasePoolable, ITimedDelayAnimation, IAnimationComponent
{
    public float Delay { get; set; }
    public bool IsDelayed => Owner.Time.CurrentTime - _startTime < Delay;
    public IAnimation Owner { get; private set; }

    private float _startTime;
    
    public static TimeDelayAnimation Create(IUiFrameworkPlugin plugin, IAnimation owner, float delay) => plugin.PluginPool.Get<TimeDelayAnimation>().Init(owner, delay); 
    
    protected TimeDelayAnimation Init(IAnimation owner, float delay)
    {
        Owner = owner;
        Delay = delay;
        return this;
    }
    
    public void OnStarted() => _startTime = Owner.Time.CurrentTime;
    public void OnTick() { }

    protected override void EnterPool()
    {
        base.EnterPool();
        _startTime = 0;
        Delay = 0;
        Owner = null;
    }
}