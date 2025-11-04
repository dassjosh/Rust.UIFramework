using Oxide.Ext.UiFramework.Builder.UI;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using Oxide.Ext.UiFramework.Animation;
using UnityEngine;

namespace Rust.UiFramework.UnitTests.Animations;

public class ImageDownloadAnimationTests
{
    // [Fact(Skip = "Skipping for now")]
    // public void DownloadImageAnimation_DoesNotError()
    // {
    //     // Arrange
    //     UiBuilder builder = UiBuilder.Create(UnitTestHelpers.Plugin, new UiReference("parent", "name"), UiPosition.Full, default, UiColors.Red);
    //     builder.AnimateImageDownload(builder.Root, "www.example.com", new ImageDownloadOptions
    //     {
    //         FallbackImageNameOrUrl = "www.failed.com",
    //         AutomaticUpdate = new ImageAnimationOptions
    //         {
    //             TimeoutImageNameOrUrl = "www.timeout.com",
    //             EnableAutoImageUpdate = true,
    //             DownloadingImageNameOrUrl = "www.download.com"
    //         }
    //     });
    // }

    [Fact]
    public void AnimationTest()
    {
        // UiBuilder builder = UiBuilder.Create(UnitTestHelpers.Plugin, new UiReference(UiLayer.Overlay, "UiName"), UiPosition.MiddleMiddle, new UiOffset(600, 500), UiColors.White);
        //
        // UiPanel panel = builder.Root as UiPanel;
        //     
        // builder.Animate(panel).Duration(5f).AnimateField(p => p.RectTransform.AsTrackable().Position).Lerp(new UiPosition(0.5f, -0.5f, 0.5f, -0.5f), UiPosition.MiddleMiddle).Ease();

        Easing a = EasingFunctions.Ease;
        Easing b = new ConfigurableBezier(new Vector2(0, 0), new Vector2(0.25f, 0.1f), new Vector2(0.25f, 1f), new Vector2(1f, 1f)).ToEasing();
        Easing c = new ConfigurableBezier(new Vector2(0,0), new Vector2(0.05f, 0), new Vector2(0.133333f, 0.06f), new Vector2(0.166666f, 0.4f), new Vector2(0.208333f, 0.82f), new Vector2(0.25f, 1), new Vector2(1, 1)).ToEasing();

        var sampleA = a.Sample();
        var sampleB = b.Sample();
        var sampleC = c.Sample();
        
        for (int i = 0; i < 100; i++)
        {
            //Console.WriteLine($"{i/100f:0.00}: {sampleA[i]}, {sampleB[i]} = {Mathf.Abs(sampleA[i] - sampleB[i])}");
            Console.WriteLine($"{i/100f:0.00}: {sampleC[i]}");
        }

    }
}