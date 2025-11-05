using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Animation;

public class InfiniteAnimationDelay : BasePoolable, IAnimationDelay
{
    public bool IsDelayed => true;
    
    public static InfiniteAnimationDelay Create(IUiFrameworkPlugin plugin) => plugin.PluginPool.Get<InfiniteAnimationDelay>(); 
    
    public void OnStarted() {}
    public void OnTick() { }
}