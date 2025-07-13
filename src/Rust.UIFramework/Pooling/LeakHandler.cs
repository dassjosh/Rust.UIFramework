using System;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Pooling
{
    internal class LeakHandler(PluginId pluginId, string poolType)
    {
        private bool _isFirstLeakError = true;
        private DateTime _nextLeakError;

        internal void OnLeak(int index, int poolSize)
        {
            if (ShouldLogLeak())
            {
                UiFrameworkExtension.GlobalLogger.Warning("Plugin: {0} Pool: {1} is leaking entities!!! {2}/{3}", pluginId.PluginName(), poolType, index, poolSize);
            }
        }

        private bool ShouldLogLeak()
        {
            if (!pluginId.IsExtensionPlugin)
            {
                return true;
            }

            if (_isFirstLeakError)
            {
                _isFirstLeakError = false;
                return false;
            }

            if (_nextLeakError >= DateTime.UtcNow)
            {
                return false;
            }
            
            _nextLeakError = DateTime.UtcNow + TimeSpan.FromSeconds(30);
            return true;
        }
    }
}