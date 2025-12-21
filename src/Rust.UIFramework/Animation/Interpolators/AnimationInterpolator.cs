using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Animation;

public class AnimationInterpolator<T> : BaseAnimationInterpolator, IAnimationInterpolator<T>
{
    public override bool HasChanged => Field.HasChanged;
    public Tracked<T> Field { get; private set; }
    public IAnimator<T> Animator { get; set; }
    
    public static AnimationInterpolator<T> Create(IAnimation owner, Tracked<T> field) => owner.Plugin.PluginPool.Get<AnimationInterpolator<T>>().Init(owner, field);
    
    protected AnimationInterpolator<T> Init(IAnimation owner, Tracked<T> field)
    {
        Owner = owner;
        Field = field;
        return this;
    }

    public override void OnTick(float progress)
    {
        T value = Animator.Get(progress);
        Field.SetProperty(value);
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        Field = null;
        Animator.TryReturnToPool();
        Animator = null;
    }
}