using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

public static class RequestExt
{
    extension(IDownloadImageRequest request)
    {
        public IDownloadImageRequest WithBorderRadius(in UiBorderRadius radius, bool antiAlias, float edgeWidth, UiColor? replacementColor)
        {
            DownloadImageRequest download = (DownloadImageRequest)request;
            download.Handler.AddModifier(new BorderRadiusImageModifier(download.Handler, new BorderRadiusImageData(radius, antiAlias, edgeWidth, replacementColor ?? UiColors.Clear)));
            return request;
        }
    }
}