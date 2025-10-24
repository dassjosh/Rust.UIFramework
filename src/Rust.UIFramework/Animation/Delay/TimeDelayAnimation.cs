using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Animation;

public class TimeDelayAnimation : BasePoolable, ITimedDelayAnimation
{
    public float Delay { get; set; }
    public bool IsDelayed => AnimationTime.CurrentTime - _startTime < Delay;

    private float _startTime;
    
    public static TimeDelayAnimation Create(IUiFrameworkPlugin plugin) => plugin.PluginPool.Get<TimeDelayAnimation>(); 
    
    public void OnStarted() => _startTime = AnimationTime.CurrentTime;
    public void OnTick() { }

    protected override void EnterPool()
    {
        base.EnterPool();
        _startTime = 0;
        Delay = 0;
    }
}