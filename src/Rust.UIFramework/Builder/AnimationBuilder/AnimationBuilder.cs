using System;
using System.Collections.Generic;
using Network;
using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Builder;

public class AnimationBuilder : BaseBuilder, IAnimationBuilder
{
    private readonly List<ISendableAnimation> _animations = [];
    private readonly List<ICancelAnimationRequest> _cancelAnimations = [];

    public static AnimationBuilder Create(IUiFrameworkPlugin plugin)
    {
        AnimationBuilder builder = plugin.PluginPool.Get<AnimationBuilder>();
        builder.Init(plugin);
        return builder;
    }

    void IAnimationBuilder.AddAnimation(ISendableAnimation animation) => _animations.Add(animation);
    void IAnimationBuilder.CancelAnimation(ICancelAnimationRequest cancel) => _cancelAnimations.Add(cancel);

    internal override void SendUi(SendInfo send, in UiDebugOptions? options) { }

    internal override void SendAnimations(SendInfo send)
    {
        for (int index = 0; index < _cancelAnimations.Count; index++)
        {
            ICancelAnimationRequest cancel = _cancelAnimations[index];
            Animations.CancelAnimations(send, cancel);
        }
        
        for (int index = 0; index < _animations.Count; index++)
        {
            ISendableAnimation animation = _animations[index];
            Singleton<AnimationHandler>.Instance.EnqueueAnimation(animation, SendInfoBuilder.GetForAnimations(send));
            Singleton<AnimationTracker>.Instance.OnAnimationQueued(animation, send, string.Empty);
        }
    }

    public override byte[] GetBytes() => throw new NotSupportedException($"Cannot get bytes for an {nameof(AnimationBuilder)}");
    public override void Combine(SendInfo send, JsonFrameworkWriter writer) { }

    protected override void EnterPool()
    {
        ClearAnimationList(_animations);
        _cancelAnimations.TryFreeValues();
        Plugin = null;
    }
}