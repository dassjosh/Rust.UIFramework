using System;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Animation;

public class AnimationGroup : SendableAnimation, IAnimationGroup
{
    public void AddAnimation(ISendableAnimation animation) => AddChildAnimation((BaseAnimation)animation);
    public void CancelAnimation(ICancelAnimationRequest cancel) => throw new NotSupportedException($"Cannot Cancel animations on a {nameof(AnimationGroup)}");

    public void RemoveAnimation(ISendableAnimation animation) => RemoveChildAnimation((BaseAnimation)animation);

    public static AnimationGroup Create(IUiFrameworkPlugin plugin) => plugin.PluginPool.Get<AnimationGroup>();
}