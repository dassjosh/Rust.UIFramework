using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Animation;

public abstract class BaseAnimationInterpolator : BasePoolable, IAnimationInterpolator, IAnimationComponent
{
    public abstract bool HasChanged { get; }
    public virtual void OnStarted() { }
    public abstract void OnTick(float progress);
    public IAnimation Owner { get; protected set; }
    
    protected void Init(IAnimation owner) => Owner = owner;
    
    protected override void EnterPool()
    {
        base.EnterPool();
        Owner = null;
    }
}