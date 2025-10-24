using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Animation;

public class FieldAnimation<TField> : BaseAnimation, IFieldAnimation<TField>
{
    public Tracked<TField> Field { get; private set; }
    IAnimationInterpolator<TField> IFieldAnimation<TField>.Interpolator { get => (IAnimationInterpolator<TField>)base.Interpolator; set => base.Interpolator = value; }

    public override bool HasChanged => base.HasChanged || Field.HasChanged;
    
    public static FieldAnimation<TField> Create(IUiFrameworkPlugin plugin, Tracked<TField> field) => plugin.PluginPool.Get<FieldAnimation<TField>>().Init(plugin, field);
    
    protected FieldAnimation<TField> Init(IUiFrameworkPlugin plugin, Tracked<TField> field)
    {
        base.Init(plugin);
        Field = field;
        Interpolator = Interpolator<TField>.Create(plugin, field);
        return this;
    }
    
    protected override void EnterPool()
    {
        base.EnterPool();
        Field = default;
    }
}