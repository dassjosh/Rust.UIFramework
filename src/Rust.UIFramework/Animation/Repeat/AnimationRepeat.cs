using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Animation;

public class AnimationRepeat : BasePoolable, IAnimationRepeat, IAnimationComponent
{
    public int Repeats { get; set; }
    public float RepeatDelay { get; set; }
    public IAnimation Owner { get; private set; }
    
    public static AnimationRepeat Create(IAnimation owner, int repeats, float repeatDelay) => owner.Plugin.PluginPool.Get<AnimationRepeat>().Init(owner, repeats, repeatDelay);
    
    protected AnimationRepeat Init(IAnimation owner, int repeats, float repeatDelay)
    {
        Owner = owner;
        Repeats = repeats;
        RepeatDelay = repeatDelay;
        return this;
    }
    
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
        Owner = null;
    }
}