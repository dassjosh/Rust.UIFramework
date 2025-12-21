using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Animation;

public class FieldAnimation<T> : BaseAnimation, IFieldAnimation<T>
{
    public Tracked<T> Field { get; private set; }
    IAnimationInterpolator<T> IFieldAnimation<T>.Interpolator { get => (IAnimationInterpolator<T>)base.Interpolator; set => base.Interpolator = value; }

    public override bool HasChanged => base.HasChanged || Field.HasChanged;
    
    public static FieldAnimation<T> Create(IUiFrameworkPlugin plugin, Tracked<T> field) => plugin.PluginPool.Get<FieldAnimation<T>>().Init(plugin, field);
    
    protected FieldAnimation<T> Init(IUiFrameworkPlugin plugin, Tracked<T> field)
    {
        base.Init(plugin);
        Field = field;
        Interpolator = AnimationInterpolator<T>.Create(this, field);
        return this;
    }
    
    protected override void EnterPool()
    {
        base.EnterPool();
        Field = default;
    }
}