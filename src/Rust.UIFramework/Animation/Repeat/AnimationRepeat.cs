using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Animation;

public class AnimationRepeat : BasePoolable, IAnimationRepeat
{
    public int Repeats { get; set; }
    public float RepeatDelay { get; set; }
    
    public static AnimationRepeat Create(IUiFrameworkPlugin plugin) => plugin.PluginPool.Get<AnimationRepeat>();
    
    public bool OnRepeat()
    {
        if (Repeats > 0)
        {
            Repeats--;
            return true;
        }

        return false;
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        Repeats = default;
        RepeatDelay = default;
    }
}