using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Animation;

public interface IAnimationInterpolator<T> : IAnimationInterpolator
{
    Tracked<T> Field { get; }
    IAnimator<T> Animator { get; set; }
    T Value => Field.Value;
}