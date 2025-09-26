using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Animation;

public class RotationAnimation : SimpleAnimation<UiRotation>
{
    public static RotationAnimation Create(IAnimationBuilder builder, in UiReference reference, ISimpleAnimator<UiRotation> animator, IAnimationDuration duration)
    {
        RotationAnimation animation = builder.PluginPool.Get<RotationAnimation>();
        animation.Init(builder.Plugin, reference, animator, duration);
        return animation;
    }
    
    protected override void WriteAnimation(JsonFrameworkWriter writer, UiRotation value, float progress)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, JsonDefaults.Common.RectTransformName);
        writer.AddFieldRaw(JsonDefaults.RectTransform.RotationName, value.Rotation);
        writer.WriteEndObject();
    }
}