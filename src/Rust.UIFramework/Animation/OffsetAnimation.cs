using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Animation;

public class OffsetAnimation : BaseAnimation
{
    public UiOffset Start;
    public UiOffset End;
    
    public static OffsetAnimation Create(in AnimationReference reference,in UiOffset start, in UiOffset end, float delay, float duration)
    {
        OffsetAnimation animation = UiFrameworkPool.Get<OffsetAnimation>();
        animation.Init(reference, start, end, delay, duration);
        return animation;
    }

    private void Init(in AnimationReference reference, in UiOffset start, in UiOffset end, float delay, float duration)
    {
        base.Init(reference, delay, duration);
        Start = start;
        End = end;
    }
    
    protected override void WriteAnimation(JsonFrameworkWriter writer, float value)
    {
        UiOffset animated = CustomAnimator is ICustomAnimator<UiOffset> animator ? animator.Get(value) : UiOffset.LerpUnclamped(Start, End, value);
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, JsonDefaults.Common.RectTransformName);
        writer.AddField(JsonDefaults.Offset.OffsetMinName, animated.Min, JsonDefaults.Common.Min);
        writer.AddField(JsonDefaults.Offset.OffsetMaxName, animated.Max, JsonDefaults.Common.Max);
        writer.WriteEndObject();
    }
    
    protected override void EnterPool()
    {
        base.EnterPool();
        Start = default;
        End = default;
    }
}