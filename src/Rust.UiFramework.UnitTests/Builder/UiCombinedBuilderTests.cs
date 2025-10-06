using Network;
using Oxide.Ext.UiFramework.Builder.Cached;
using Oxide.Ext.UiFramework.Builder.Combined;
using Oxide.Ext.UiFramework.Builder.UI;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Rust.UiFramework.UnitTests.Builder;

[CollectionDefinition(nameof(UiCombinedBuilderTests), DisableParallelization = true)]
public class UiCombinedBuilderTests
{
    [Fact]
    public async Task UiCombinedBuilder_GeneratesCorrectJson()
    {
        // Arrange
        UiBuilder normalBuilder = UiBuilder.Create(UnitTestHelpers.Plugin, new UiReference(UiLayer.Overlay, "UI"), UiPosition.Full, default, UiColors.Red);
        normalBuilder.TextButton(normalBuilder.Root, new UiPosition(0.25f, 0.25f, 0.75f, 0.75f), default, "Text", 14, UiColors.White, UiColors.Red, "command");
        
        UiBuilder cacheBuilder = UiBuilder.Create(UnitTestHelpers.Plugin, new UiReference(UiLayer.Overlay, "UI"), UiPosition.Full, default, UiColors.Green);
        normalBuilder.SpriteButton(normalBuilder.Root, new UiPosition(0.25f, 0.25f, 0.75f, 0.75f), default, UiColors.Blue, "sprite", "command");
        
        CachedUiBuilder cachedUiBuilder = cacheBuilder.ToCachedBuilder();
        
        UiBuilder update = UiBuilder.CreateUpdate(UnitTestHelpers.Plugin);
        update.Update<UiPanel>("panel").SetColor(UiColors.Orange);
        
        // Act
        UiCombinedBuilder combined = UiCombinedBuilder.Create(UnitTestHelpers.Plugin, normalBuilder, cachedUiBuilder, update);
        string json = combined.GetJsonString();
        
        // Assert
        await VerifyJson(json);
    }
}