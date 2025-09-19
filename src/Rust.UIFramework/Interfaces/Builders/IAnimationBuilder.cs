using Oxide.Ext.UiFramework.Animation;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Interfaces.Builders;

public interface IAnimationBuilder : IPoolable
{
    IUiFrameworkPlugin Plugin { get; }
    internal void AddAnimation(BaseAnimation animation);
}