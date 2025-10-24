using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Animation;

public class Interpolator<T> : BaseInterpolator, IAnimationInterpolator<T>
{
    public override bool HasChanged => Field.HasChanged;
    public Tracked<T> Field { get; private set; }
    public IAnimator<T> Animator { get; set; }
    
    public static Interpolator<T> Create(IUiFrameworkPlugin plugin, Tracked<T> field) => plugin.PluginPool.Get<Interpolator<T>>().Init(field);
    
    protected Interpolator<T> Init(Tracked<T> field)
    {
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