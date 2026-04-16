using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Animation;

public class TriggerDelayAnimation : BasePoolable, ITriggerDelayAnimation, IAnimationComponent
{
    public bool IsDelayed => !_triggered;
    private bool _triggered;
    public IAnimation Owner { get; private set; }

    public static TriggerDelayAnimation Create(IAnimation owner) => owner.Plugin.PluginPool.Get<TriggerDelayAnimation>().Init(owner);
    
    protected TriggerDelayAnimation Init(IAnimation owner)
    {
        Owner = owner;
        return this;
    }
    
    public void Trigger() => _triggered = true;
    
    public void OnStarted() { }
    public void OnTick() { }
    
    protected override void EnterPool()
    {       
        base.EnterPool();
        _triggered = false;
        Owner = null;
    }
}