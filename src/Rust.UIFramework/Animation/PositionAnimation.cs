using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Animation;

public class PositionAnimation : BaseAnimation
{
    public UiPosition Start;
    public UiPosition End;
    
    public static PositionAnimation Create(in UiReference reference, in UiPosition start, in UiPosition end, float delay, float duration)
    {
        PositionAnimation animation = UiFrameworkPool.Get<PositionAnimation>();
        animation.Init(reference, start, end, delay, duration);
        return animation;
    }

    private void Init(in UiReference reference, in UiPosition start, in UiPosition end, float delay, float duration)
    {
        base.Init(reference, delay, duration);
        Start = start;
        End = end;
    }
    
    protected override void WriteAnimation(JsonFrameworkWriter writer, float value)
    {
        UiPosition animated = CustomAnimator is ICustomAnimator<UiPosition> animator ? animator.Get(value) : UiPosition.LerpUnclamped(Start, End, value);
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