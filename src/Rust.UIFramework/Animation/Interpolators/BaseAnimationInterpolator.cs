using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Animation;

public abstract class BaseAnimationInterpolator : BasePoolable, IAnimationInterpolator
{
    public abstract bool HasChanged { get; }
    public virtual void OnStarted() { }
    public abstract void OnTick(float progress);
}