using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal class BorderRadiusRequest(PluginId pluginId, string name, BorderRadiusRequestHandler handler) : RegisterImageRequest(pluginId, name, RegisterImageOptions.Default, handler), IBorderRadiusRequest
{
    public UiDimensions2D Size => handler.Data.Size;
    public UiBorderRadius Radius => handler.Data.Radius;
    public bool AntiAlias => handler.Data.AntiAlias;
    public float EdgeWidth => handler.Data.EdgeWidth;
}