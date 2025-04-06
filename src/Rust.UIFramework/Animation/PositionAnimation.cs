using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Animation;

public class PositionAnimation : BaseAnimation<UiPosition>
{
    public static PositionAnimation Create(in UiReference reference, IAnimator<UiPosition> animator, float delay, float duration)
    {
        PositionAnimation animation = UiFrameworkPool.Get<PositionAnimation>();
        animation.Init(reference, animator, delay, duration);
        return animation;
    }
    
    protected override void WriteAnimation(JsonFrameworkWriter writer, UiPosition value)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, JsonDefaults.Common.RectTransformName);
        writer.AddField(JsonDefaults.Position.AnchorMinName, value.Min, JsonDefaults.Common.Min);
        writer.AddField(JsonDefaults.Position.AnchorMaxName, value.Max, JsonDefaults.Common.Max);
        writer.WriteEndObject();
    }
}