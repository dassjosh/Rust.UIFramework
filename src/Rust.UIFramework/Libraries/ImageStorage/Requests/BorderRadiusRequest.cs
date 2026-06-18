using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class BorderRadiusRequest(PluginId pluginId, string name, BorderRadiusRequestHandler handler, RegisterImageOptions options) : RegisterImageRequest(pluginId, name, options, handler), IBorderRadiusRequest
{
    public UiSize2D Size => handler.Data.Size;
    public UiBorderRadius Radius => handler.Data.Radius;
    public bool AntiAlias => handler.Data.AntiAlias;
    public float EdgeWidth => handler.Data.EdgeWidth;
}