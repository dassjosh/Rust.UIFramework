using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Animation;

public class ColorAnimation : BaseAnimation
{
    public UiColor StartColor;
    public UiColor EndColor;
    private Utf8String _elementType;

    public static ColorAnimation Create(UiColor startColor, UiColor endColor, BaseUiComponent component, float updateRate, float delay, float duration, int repeats, float repeatDelay)
    {
        ColorAnimation animation = UiFrameworkPool.Get<ColorAnimation>();
        animation.Init(startColor, endColor, component, updateRate, delay, duration, repeats, repeatDelay);
        return animation;
    }

    private void Init(UiColor startColor, UiColor endColor, BaseUiComponent component, float updateRate, float delay, float duration, int repeats, float repeatDelay)
    {
        base.Init(component, updateRate, delay, duration, repeats, repeatDelay);
        StartColor = startColor;
        EndColor = endColor;
        _elementType = component.Component.Type;
    }
    
    protected override void WriteAnimation(JsonFrameworkWriter writer, float value)
    {
        UiColor color = UiColor.Lerp(StartColor, EndColor, value);
        UiFrameworkExtension.GlobalLogger.Debug("{0} -> {1} * {2} = {3}", StartColor, EndColor, value, color);
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