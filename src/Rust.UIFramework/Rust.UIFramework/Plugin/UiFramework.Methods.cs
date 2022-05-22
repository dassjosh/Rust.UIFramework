using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Pooling.ArrayPool;

namespace Oxide.Ext.UiFramework.Plugin
{
    public partial class UiFramework
    {
        #region Unloading
        public override void HandleRemovedFromManager(PluginManager manager)
        {
            UiFrameworkPool.OnUnload();
            UiFrameworkArrayPool<byte>.Clear();
            UiFrameworkArrayPool<char>.Clear();
            UiColorCache.OnUnload();
            UiNameCache.OnUnload();
            base.HandleRemovedFromManager(manager);
        }
        #endregion
    }
}