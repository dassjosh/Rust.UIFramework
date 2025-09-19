using Oxide.Ext.UiFramework.Interfaces.Builders;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Animation;

public class OffsetAnimation : SimpleAnimation<UiOffset>
{
    public static OffsetAnimation Create(IAnimationBuilder builder, in UiReference reference, ISimpleAnimator<UiOffset> animator, IAnimationDuration duration)
    {
        OffsetAnimation animation = builder.PluginPool.Get<OffsetAnimation>();
        animation.Init(builder.Plugin, reference, animator, duration);
        return animation;
    }
    
    protected override void WriteAnimation(JsonFrameworkWriter writer, UiOffset value)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, JsonDefaults.Common.RectTransformName);
        writer.AddFieldRaw(JsonDefaults.Offset.OffsetMinName, value.Min);
        writer.AddFieldRaw(JsonDefaults.Offset.OffsetMaxName, value.Max);
        writer.WriteEndObject();
    }
}