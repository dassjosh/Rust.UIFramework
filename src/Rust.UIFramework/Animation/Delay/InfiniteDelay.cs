using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Animation;

public class InfiniteDelay : BasePoolable, IAnimationDelay
{
    public bool IsDelayed => true;
    
    public static InfiniteDelay Create(IUiFrameworkPlugin plugin) => plugin.PluginPool.Get<InfiniteDelay>(); 
    
    public void OnStarted() {}
    public void OnTick() { }
}