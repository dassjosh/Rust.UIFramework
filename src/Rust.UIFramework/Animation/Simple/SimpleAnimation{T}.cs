using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Animation;

public abstract class SimpleAnimation<T> : BaseAnimation
{
    public ISimpleAnimator<T> Animator { get; private set; }
    
    protected void Init(IUiFrameworkPlugin plugin, in UiReference reference, ISimpleAnimator<T> animator, IAnimationDuration duration)
    {
        base.Init(plugin, reference, duration);
        Animator = animator;
    }

    public SimpleAnimation<T> WithAnimator(ISimpleAnimator<T> animator)
    {
        Animator.TryReturnToPool();
        Animator = animator;
        return this;
    }
    
    public override void WriteAnimation(JsonFrameworkWriter writer, float elapsedPercentage)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentName, Reference.Name);
        writer.AddFieldRaw(JsonDefaults.Common.Update, true);
        writer.WritePropertyName(JsonDefaults.Common.ComponentsName);
        writer.WriteStartArray();
        float progress = Progressor?.GetProgress(Mathf.Clamp01(elapsedPercentage)) ?? elapsedPercentage;
        WriteAnimation(writer, Animator.Get(progress), progress);
        writer.WriteEndArray();
        writer.WriteEndObject();
    }
    
    protected abstract void WriteAnimation(JsonFrameworkWriter writer, T value, float progress);

    protected override void EnterPool()
    {
        base.EnterPool();
        Animator.TryReturnToPool();
    }
}