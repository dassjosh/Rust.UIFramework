using Oxide.Ext.UiFramework.Builder.UI;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Rust.UiFramework.UnitTests.Animations;

public class ImageDownloadAnimationTests
{
    [Fact]
    public void DownloadImageAnimation_DoesNotError()
    {
        // Arrange
        UiBuilder builder = UiBuilder.Create(UnitTestHelpers.Plugin, new UiReference("parent", "name"), UiPosition.Full, default, UiColors.Red);
        builder.AnimateImageDownload(builder.Root, "www.example.com", new ImageDownloadOptions
        {
            FailedImageNameOrUrl = "www.failed.com",
            AutomaticUpdate = new ImageAutomaticUpdateOptions
            {
                TimeoutImageNameOrUrl = "www.timeout.com",
                EnableAutoImageUpdate = true,
                DownloadingImageNameOrUrl = "www.download.com"
            }
        });
    }
}