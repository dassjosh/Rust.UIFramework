namespace Oxide.Ext.UiFramework.Animation;

public interface ICustomAnimator;

public interface ICustomAnimator<out T> : ICustomAnimator
{
    T Get(float progress);
}