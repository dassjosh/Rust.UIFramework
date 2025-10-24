using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.Animation;

public interface IAnimationGroup : ISendableAnimation, IAnimationBuilder
{
    void RemoveAnimation(ISendableAnimation animation);
}