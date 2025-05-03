using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Animation;

public class DestroyAfterEvent : BasePoolable, IAnimationCompleted
{
    public UiReference? Target;

    public static DestroyAfterEvent Create(in UiReference? target)
    {
        DestroyAfterEvent @event = UiFrameworkPool.Get<DestroyAfterEvent>();
        @event.Init(target);
        return @event;
    }

    private void Init(in UiReference? target)
    {
        Target = target;
    }
    
    public void OnAnimationCompleted(BaseAnimation animation)
    {
        BaseBuilder.DestroyUi(animation.Send, Target.HasValue ? Target.Value.Name : animation.Reference.Name);
    }

    protected override void EnterPool()
    {
        Target = default;
    }
}