namespace Oxide.Ext.UiFramework.Animation;

public interface IAnimator<out T> : IAnimator
{
    T Get(float progress);
}