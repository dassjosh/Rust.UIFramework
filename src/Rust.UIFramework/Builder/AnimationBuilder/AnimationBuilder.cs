using System;
using System.Collections.Generic;
using Network;
using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Builder;

public class AnimationBuilder : BaseBuilder, IAnimationBuilder
{
    private readonly List<ISendableAnimation> _animations = [];

    public static AnimationBuilder Create(IUiFrameworkPlugin plugin)
    {
        AnimationBuilder builder = plugin.PluginPool.Get<AnimationBuilder>();
        builder.Init(plugin);
        return builder;
    }

    void IAnimationBuilder.AddAnimation(ISendableAnimation animation) => _animations.Add(animation);

    internal override void SendUi(SendInfo send, in UiDebugOptions? options)
    {
        for (int index = 0; index < _animations.Count; index++)
        {
            ISendableAnimation animation = _animations[index];
            Singleton<AnimationHandler>.Instance.EnqueueAnimation(animation, SendInfoBuilder.GetForAnimations(send));
            Singleton<AnimationTracker>.Instance.OnAnimationQueued(animation, send, string.Empty);
        }
    }

    public override byte[] GetBytes() => throw new NotSupportedException($"Cannot get bytes for an {nameof(AnimationBuilder)}");
    public override void Combine(SendInfo send, JsonFrameworkWriter writer)
    {
        SendUi(send, null);
    }

    protected override void EnterPool()
    {
        ClearAnimationList(_animations);
        Plugin = null;
    }
}