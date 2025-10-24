using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Animation;

public class TriggerDelayAnimation : BasePoolable, ITriggerDelayAnimation
{
    public bool IsDelayed => !_triggered;
    private bool _triggered;

    public static TriggerDelayAnimation Create(IUiFrameworkPlugin plugin) => plugin.PluginPool.Get<TriggerDelayAnimation>();
    
    public void Trigger() => _triggered = true;
    
    public void OnStarted() { }
    public void OnTick() { }
    
    protected override void EnterPool()
    {       
        base.EnterPool();
        _triggered = false;
    }
}