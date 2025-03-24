using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Animation;

public class ColorAnimation : BaseAnimation
{
    public UiColor StartColor;
    public UiColor EndColor;
    private Utf8String _elementType;

    public static ColorAnimation Create(UiColor startColor, UiColor endColor, BaseUiComponent component, float delay, float duration)
    {
        ColorAnimation animation = UiFrameworkPool.Get<ColorAnimation>();
        animation.Init(startColor, endColor, component, delay, duration);
        return animation;
    }

    private void Init(UiColor startColor, UiColor endColor, BaseUiComponent component, float delay, float duration)
    {
        base.Init(component, delay, duration);
        StartColor = startColor;
        EndColor = endColor;
        _elementType = component.Component.Type;
    }
    
    protected override void WriteAnimation(JsonFrameworkWriter writer, float value)
    {
        UiColor color = Points?.GetColor(StartColor, EndColor, value) ?? UiColor.Lerp(StartColor, EndColor, value);
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