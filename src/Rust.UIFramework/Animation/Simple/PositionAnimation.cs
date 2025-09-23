using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Animation;

public class PositionAnimation : SimpleAnimation<UiPosition>
{
    public static PositionAnimation Create(IAnimationBuilder builder, in UiReference reference, ISimpleAnimator<UiPosition> animator, IAnimationDuration duration)
    {
        PositionAnimation animation = builder.PluginPool.Get<PositionAnimation>();
        animation.Init(builder.Plugin, reference, animator, duration);
        return animation;
    }
    
    protected override void WriteAnimation(JsonFrameworkWriter writer, UiPosition value, float progress)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, JsonDefaults.Common.RectTransformName);
        writer.AddFieldRaw(JsonDefaults.RectTransform.AnchorMinName, value.Min);
        writer.AddFieldRaw(JsonDefaults.RectTransform.AnchorMaxName, value.Max);
        writer.WriteEndObject();
    }
}