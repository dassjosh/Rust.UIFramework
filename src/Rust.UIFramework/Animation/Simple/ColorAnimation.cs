using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Animation;

public class ColorAnimation : SimpleAnimation<UiColor>
{
    private Utf8String _elementType;
    
    public static ColorAnimation Create(IAnimationBuilder builder, in AnimationReference reference, ISimpleAnimator<UiColor> animator, IAnimationDuration duration) 
        => builder.PluginPool.Get<ColorAnimation>().Init(builder.Plugin, reference, animator, duration);

    private ColorAnimation Init(IUiFrameworkPlugin plugin, in AnimationReference reference, ISimpleAnimator<UiColor> animator, IAnimationDuration duration)
    {
        base.Init(plugin, reference.Reference, animator, duration);
        _elementType = reference.Type;
        return this;
    }
    
    protected override void WriteAnimation(JsonFrameworkWriter writer, UiColor value)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, _elementType);
        writer.AddFieldRaw(JsonDefaults.Color.ColorName, value);
        writer.WriteEndObject();
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        _elementType = default;
    }
}