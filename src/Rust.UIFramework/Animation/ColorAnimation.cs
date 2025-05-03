using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Animation;

public class ColorAnimation : BaseAnimation<UiColor>
{
    private Utf8String _elementType;
    
    public static ColorAnimation Create(in AnimationReference reference, IAnimator<UiColor> animator, IAnimationDuration duration)
    {
        ColorAnimation animation = UiFrameworkPool.Get<ColorAnimation>();
        animation.Init(reference, animator, duration);
        return animation;
    }

    private void Init(in AnimationReference reference, IAnimator<UiColor> animator, IAnimationDuration duration)
    {
        base.Init(reference.Reference, animator, duration);
        _elementType = reference.Type;
    }
    
    protected override void WriteAnimation(JsonFrameworkWriter writer, UiColor value)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, _elementType);
        writer.AddFieldRaw(JsonDefaults.Color.ColorName, value);
        writer.WriteEndObject();
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        _elementType = default;
    }
}