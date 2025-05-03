using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Animation;

public class OffsetAnimation : BaseAnimation<UiOffset>
{
    public static OffsetAnimation Create(in UiReference reference, IAnimator<UiOffset> animator, IAnimationDuration duration)
    {
        OffsetAnimation animation = UiFrameworkPool.Get<OffsetAnimation>();
        animation.Init(reference, animator, duration);
        return animation;
    }
    
    protected override void WriteAnimation(JsonFrameworkWriter writer, UiOffset value)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, JsonDefaults.Common.RectTransformName);
        writer.AddField(JsonDefaults.Offset.OffsetMinName, value.Min, JsonDefaults.Common.Min);
        writer.AddField(JsonDefaults.Offset.OffsetMaxName, value.Max, JsonDefaults.Common.Max);
        writer.WriteEndObject();
    }
}