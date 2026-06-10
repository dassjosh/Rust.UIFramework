using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Libraries;

internal class DownloadImageRequest(PluginId pluginId, string name, IRegisterImageOptions options, DownloadImageRequestHandler handler) : RegisterImageRequest(pluginId, name, options, handler), IDownloadImageRequest
{
    public string Url => handler.Url;
    public IDownloadImageState State => handler.State;
}