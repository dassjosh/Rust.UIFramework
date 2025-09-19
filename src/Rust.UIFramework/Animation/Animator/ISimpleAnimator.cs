namespace Oxide.Ext.UiFramework.Animation;

public interface IAnimator;

public interface ISimpleAnimator<out T> : IAnimator
{
    T Get(float progress);
}