using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Animation;

public class ColorAnimation : BaseAnimation
{
    public UiColor StartColor;
    public UiColor EndColor;
    private Utf8String _elementType;

    public static ColorAnimation Create(in AnimationReference reference, UiColor startColor, UiColor endColor, float delay, float duration)
    {
        ColorAnimation animation = UiFrameworkPool.Get<ColorAnimation>();
        animation.Init(reference, startColor, endColor, delay, duration);
        return animation;
    }

    private void Init(in AnimationReference reference, UiColor startColor, UiColor endColor, float delay, float duration)
    {
        base.Init(reference, delay, duration);
        StartColor = startColor;
        EndColor = endColor;
        _elementType = reference.Type;
    }
    
    protected override void WriteAnimation(JsonFrameworkWriter writer, float value)
    {
        UiColor color = CustomAnimator is ICustomAnimator<UiColor> animator ? animator.Get(value) : UiColor.Lerp(StartColor, EndColor, value);
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, _elementType);
        writer.AddFieldRaw(JsonDefaults.Color.ColorName, color);
        writer.WriteEndObject();
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        StartColor = default;
        EndColor = default;
        _elementType = default;
    }
}