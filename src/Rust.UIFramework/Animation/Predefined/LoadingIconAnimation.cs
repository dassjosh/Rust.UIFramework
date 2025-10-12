using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Animation;

public class LoadingIconAnimation : RotationAnimation
{
    public static LoadingIconAnimation Create(IAnimationBuilder builder, in UiReference reference, ISimpleAnimator<UiRotation> animator)
    {
        LoadingIconAnimation animation = builder.PluginPool.Get<LoadingIconAnimation>();
        animation.Init(builder.Plugin, reference, animator, SingleTickDuration.Create(builder.Plugin.PluginPool));
        return animation;
    }
}