using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Animation;

public class PositionAnimation : BaseAnimation
{
    public UiPosition Start;
    public UiPosition End;
    
    public static PositionAnimation Create(in UiPosition start, in UiPosition end, BaseUiComponent component, float updateRate, float delay, float duration, int repeats, float repeatDelay)
    {
        PositionAnimation animation = UiFrameworkPool.Get<PositionAnimation>();
        animation.Init(start, end, component, updateRate, delay, duration, repeats, repeatDelay);
        return animation;
    }

    private void Init(in UiPosition start, in UiPosition end, BaseUiComponent component, float updateRate, float delay, float duration, int repeats, float repeatDelay)
    {
        base.Init(component, updateRate, delay, duration, repeats, repeatDelay);
        Start = start;
        End = end;
    }
    
    protected override void WriteAnimation(JsonFrameworkWriter writer, float value)
    {
        UiPosition animated = UiPosition.Lerp(Start, End, value);
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, JsonDefaults.Common.RectTransformName);
        writer.AddField(JsonDefaults.Position.AnchorMinName, animated.Min, JsonDefaults.Common.Min);
        writer.AddField(JsonDefaults.Position.AnchorMaxName, animated.Max, JsonDefaults.Common.Max);
        writer.WriteEndObject();
    }
    
    protected override void EnterPool()
    {
        base.EnterPool();
        Start = default;
        End = default;
    }
}