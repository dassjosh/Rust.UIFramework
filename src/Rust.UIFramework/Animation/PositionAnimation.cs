using Oxide.Ext.UiFramework.Interfaces.Builders;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Animation;

public class PositionAnimation : BaseAnimation<UiPosition>
{
    public static PositionAnimation Create(IAnimationBuilder builder, in UiReference reference, IAnimator<UiPosition> animator, IAnimationDuration duration)
    {
        PositionAnimation animation = builder.PluginPool.Get<PositionAnimation>();
        animation.Init(reference, animator, duration);
        return animation;
    }
    
    protected override void WriteAnimation(JsonFrameworkWriter writer, UiPosition value)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, JsonDefaults.Common.RectTransformName);
        writer.AddFieldRaw(JsonDefaults.Position.AnchorMinName, value.Min);
        writer.AddFieldRaw(JsonDefaults.Position.AnchorMaxName, value.Max);
        writer.WriteEndObject();
    }
}