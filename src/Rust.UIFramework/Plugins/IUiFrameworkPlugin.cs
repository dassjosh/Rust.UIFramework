using Oxide.Ext.UiFramework.Libraries;

namespace Oxide.Ext.UiFramework.Plugins;

public interface IUiFrameworkPlugin : IPluginBase
{
    public UiPluginPool PluginPool { get; set; }
}