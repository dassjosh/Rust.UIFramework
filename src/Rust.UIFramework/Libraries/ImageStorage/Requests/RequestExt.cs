using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

public static class RequestExt
{
    extension(IDownloadImageRequest request)
    {
        public IDownloadImageRequest WithBorderRadius(in UiBorderRadius radius, UiColor? transparentColor = null,
            bool antiAlias = true, float edgeWidth = 1f,
            bool enableBorder = false, float borderWidth = 1f, UiColor? borderColor = null,
            bool enableDashedBorder = false, float dashLength = 1f, float gapLength = 1f)
        {
            BorderRadiusData data = new(radius, transparentColor ?? UiColors.Clear, antiAlias, edgeWidth, enableBorder, borderWidth, borderColor ?? UiColors.Clear, enableDashedBorder, dashLength, gapLength);
            return request.WithBorderRadius(data);
        }

        internal IDownloadImageRequest WithBorderRadius(BorderRadiusData data)
        {
            DownloadImageRequest download = (DownloadImageRequest)request;
            download.Handler.AddModifier(new BorderRadiusImageModifier(download.Handler, data));
            return request;
        }
    }
}