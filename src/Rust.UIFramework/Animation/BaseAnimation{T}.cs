using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Animation;

public abstract class BaseAnimation<T> : BaseAnimation
{
    public IAnimator<T> Animator { get; private set; }
    
    protected void Init(in UiReference reference, IAnimator<T> animator, IAnimationDuration duration)
    {
        base.Init(reference, duration);
        Animator = animator;
    }

    public BaseAnimation<T> WithAnimator(IAnimator<T> animator)
    {
        if (Animator is BasePoolable poolable)
        {
            poolable.Dispose();
        }

        Animator = animator;
        return this;
    }
    
    protected override void WriteAnimation(JsonFrameworkWriter writer, float value) => WriteAnimation(writer, Animator.Get(value));

    protected abstract void WriteAnimation(JsonFrameworkWriter writer, T value);

    protected override void EnterPool()
    {
        base.EnterPool();
        if (Animator is BasePoolable poolable)
        {
            poolable.Dispose();
        }
    }
}