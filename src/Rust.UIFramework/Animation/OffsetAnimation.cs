using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Animation;

public class OffsetAnimation : BaseAnimation
{
    public UiOffset Start;
    public UiOffset End;
    
    public static OffsetAnimation Create(in UiOffset start, in UiOffset end, BaseUiComponent component, float delay, float duration)
    {
        OffsetAnimation animation = UiFrameworkPool.Get<OffsetAnimation>();
        animation.Init(start, end, component, delay, duration);
        return animation;
    }

    private void Init(in UiOffset start, in UiOffset end, BaseUiComponent component, float delay, float duration)
    {
        base.Init(component, delay, duration);
        Start = start;
        End = end;
    }
    
    protected override void WriteAnimation(JsonFrameworkWriter writer, float value)
    {
        UiOffset animated = Points?.GetOffset(Start, End, value) ?? UiOffset.Lerp(Start, End, value);
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