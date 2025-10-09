using Network;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Builder.Cached;
using Oxide.Ext.UiFramework.Builder.Combined;
using Oxide.Ext.UiFramework.Builder.UI;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using Array = JSON.Array;

namespace Rust.UiFramework.UnitTests.Builder;

[CollectionDefinition(nameof(UiCombinedBuilderTests), DisableParallelization = true)]
public class UiCombinedBuilderTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public async Task UiCombinedBuilder_GeneratesCorrectJson(int offset)
    {
        // Arrange
        BaseBuilder[] builders = [CreateBuilder1(), CreateBuilder2(), CreateBuilder3()];
        
        // Act
        using UiCombinedBuilder combined = UiCombinedBuilder.Create(UnitTestHelpers.Plugin);
        for(int i = 0; i < builders.Length; i++)
        {
            combined.Add(builders[(offset + i) % builders.Length]);
        }
        string json = combined.GetJsonString();
        
        // Assert
        await VerifyJson(json);
        Array jsonObject = Array.Parse(json);
        jsonObject.Should().NotBeNull();
    }

    private BaseBuilder CreateBuilder1()
    {
        UiBuilder builder = UiBuilder.Create(UnitTestHelpers.Plugin, new UiReference(UiLayer.Overlay, "UI"), UiPosition.Full, default, UiColors.Red);
        builder.TextButton(builder.Root, new UiPosition(0.25f, 0.25f, 0.75f, 0.75f), default, "Text", 14, UiColors.White, UiColors.Red, "command");
        return builder;
    }

    private BaseBuilder CreateBuilder2()
    {
        UiBuilder builder = UiBuilder.Create(UnitTestHelpers.Plugin, new UiReference(UiLayer.Overlay, "UI"), UiPosition.Full, default, UiColors.Green);
        builder.SpriteButton(builder.Root, new UiPosition(0.25f, 0.25f, 0.75f, 0.75f), default, UiColors.Blue, "sprite", "command");
        return builder.ToCachedBuilder();
    }
    
    private BaseBuilder CreateBuilder3()
    {
        UiBuilder builder = UiBuilder.CreateUpdate(UnitTestHelpers.Plugin);
        builder.Update<UiPanel>("panel").SetColor(UiColors.Orange);
        return builder;
    }
}