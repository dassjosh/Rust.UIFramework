using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Interfaces;

public interface IAnimationBuilder : IPoolable
{
    IUiFrameworkPlugin Plugin { get; }
    void AddAnimation(ISendableAnimation animation);
}