using System;
using System.Collections.Generic;
using Network;
using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Interfaces.Builders;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Builder;

public class AnimationBuilder : BaseBuilder, IAnimationBuilder
{
    public IUiFrameworkPlugin Plugin { get; private set; }
    
    private readonly List<BaseAnimation> _animations = [];

    public static AnimationBuilder Create(IUiFrameworkPlugin plugin) => plugin.PluginPool.Get<AnimationBuilder>().Init(plugin);

    private AnimationBuilder Init(IUiFrameworkPlugin plugin)
    {
        Plugin = plugin;
        return this;
    }

    void IAnimationBuilder.AddAnimation(BaseAnimation animation) => _animations.Add(animation);

    internal override void SendUi(SendInfo send)
    {
        for (int index = 0; index < _animations.Count; index++)
        {
            BaseAnimation animation = _animations[index];
            Singleton<AnimationHandler>.Instance.EnqueueAnimation(animation, SendInfoBuilder.GetForAnimations(send));
            Singleton<AnimationTracker>.Instance.OnAnimatedPanelCreated(send, string.Empty, animation.Reference.Name, animation.Id);
        }
    }

    public override byte[] GetBytes() => throw new NotSupportedException($"Cannot get bytes for an {nameof(AnimationBuilder)}");

    protected override void EnterPool()
    {
        ClearAnimationList(_animations);
        Plugin = null;
    }
}