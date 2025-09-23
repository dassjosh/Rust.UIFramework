using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Libraries;

internal static class ImageDownloadExt
{
    internal static UiRawImage HandleImageDownloadUpdate(this UiRawImage image, BaseUiBuilder builder, string nameOrUrl, ImageDownloadOptions options)
    {
        if (options.AutomaticUpdate is { EnableAutoImageUpdate: true } && builder is IAnimationBuilder animationBuilder && nameOrUrl.StartsWith("http") && Singleton<UiImageStorage>.Instance.IsDownloading(nameOrUrl))
        {
            animationBuilder.AnimateImageDownload(image, nameOrUrl, options);
        }

        return image;
    }
}