using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;

namespace Rust.UiFramework.UnitTests.Json;

public class JsonFrameworkWriterTests
{
    [Fact]
    public void JsonFrameworkWriter_Create_IsNotNull()
    {
        // Arrange
        using JsonFrameworkWriter writer = JsonFrameworkWriter.Create(UnitTestHelpers.UnitTestPool);
        
        // Act
        
        // Assert
        writer.Should().NotBeNull();
    }

    [Fact]
    public async Task CommonFields_Serialize_ToCorrectJson()
    {
        // Arrange
        using JsonFrameworkWriter writer = JsonFrameworkWriter.Create(UnitTestHelpers.UnitTestPool);
        
        // Act
        writer.WriteStartObject();
        writer.AddFieldRaw((Utf8String)"Utf8StringRaw", (Utf8String)"Value");
        writer.AddFieldRaw((Utf8String)"StringRaw", (Utf8String)"string");
        writer.AddFieldRaw((Utf8String)"IntRaw", 123);
        writer.AddFieldRaw((Utf8String)"UlongRaw", ulong.MaxValue);
        writer.AddFieldRaw((Utf8String)"BoolRaw", true);
        writer.AddFieldRaw((Utf8String)"UiColorRaw", UiColor.ParseHexColor("#7F7F7F7F"));
        writer.AddFieldRaw((Utf8String)"Vector2Raw", new Vector2(100, 200));
        writer.AddField<TextAnchor>((Utf8String)"TextAnchorEnumRaw", TextAnchor.MiddleLeft);
        
        writer.AddField((Utf8String)"Utf8String", "Value", "NotValue");
        writer.AddField((Utf8String)"Int", 123, 456);
        writer.AddField((Utf8String)"Float", 12.3f, 45.6f);
        writer.AddField((Utf8String)"Ulong", ulong.MaxValue, 999ul);
        writer.AddField((Utf8String)"Bool", true, false);
        writer.AddField((Utf8String)"UiColor", UiColor.ParseHexColor("#7F7F7F7F"), UiColors.Blue);
        writer.AddField((Utf8String)"Vector2", new Vector2(100, 200), new Vector2(1, 1));
        writer.AddField((Utf8String)"TextAnchorEnum", TextAnchor.MiddleLeft, TextAnchor.MiddleRight);
        
        writer.AddField((Utf8String)"Utf8StringHidden", "Value", "Value");
        writer.AddField((Utf8String)"IntHidden", 123, 123);
        writer.AddField((Utf8String)"FloatHidden", 12.3f, 12.3f);
        writer.AddField((Utf8String)"UlongHidden", ulong.MaxValue, ulong.MaxValue);
        writer.AddField((Utf8String)"BoolHidden", true, true);
        writer.AddField((Utf8String)"UiColorHidden", UiColor.ParseHexColor("#7F7F7F7F"), UiColor.ParseHexColor("#7F7F7F7F"));
        writer.AddField((Utf8String)"Vector2Hidden", new Vector2(100, 200), new Vector2(100, 200));
        writer.AddField((Utf8String)"TextAnchorEnumHidden", TextAnchor.MiddleLeft, TextAnchor.MiddleLeft);
        
        writer.AddKeyField((Utf8String)"KeyField");
        
        writer.WriteEndObject();
        
        // Assert
        string json = writer.ToString();
        await VerifyJson(json);
        JSON.Object jsonObject = JSON.Object.Parse(json);
        jsonObject.Should().NotBeNull();
    }
    
    [Theory]
    [MemberData(nameof(UserInputString_TheoryData))]
    public async Task UserInputString_Serializes_ToCorrectJson(string value)
    {
        // Arrange
        using JsonFrameworkWriter writer = JsonFrameworkWriter.Create(UnitTestHelpers.UnitTestPool);
        
        // Act
        writer.WriteStartObject();
        writer.AddTextField((Utf8String)"TextField", value);
        writer.WriteEndObject();
        
        // Assert
        string json = writer.ToString();
        await VerifyJson(json);
        JSON.Object jsonObject = JSON.Object.Parse(json);
        jsonObject.Should().NotBeNull();
        string parsedValue = jsonObject["TextField"].Str;
        parsedValue.Should().NotBeNull();
        parsedValue.Replace(JsonFrameworkWriter.StartQuoteString, '"').Replace(JsonFrameworkWriter.EndQuoteString, '"').Replace(JsonFrameworkWriter.BackslashString, "\\").Should().Be(value);
    }
    
    [Theory]
    [MemberData(nameof(UserInputString_TheoryData))]
    public async Task CommandInputString_Serializes_ToCorrectJson(string value)
    {
        // Arrange
        using JsonFrameworkWriter writer = JsonFrameworkWriter.Create(UnitTestHelpers.UnitTestPool);
        
        // Act
        writer.WriteStartObject();
        writer.AddCommand((Utf8String)"TextField", value, null);
        writer.WriteEndObject();
        
        // Assert
        string json = writer.ToString();
        await VerifyJson(json);
        JSON.Object jsonObject = JSON.Object.Parse(json);
        jsonObject.Should().NotBeNull();
        string parsedValue = jsonObject["TextField"].Str;
        parsedValue.Should().NotBeNull();
        parsedValue.Replace(JsonFrameworkWriter.BackslashString, "\\").Replace("\\\"", "\"").Should().Be(value);
    }
    
    public static TheoryData<string> UserInputString_TheoryData() 
        => [string.Empty, "123", "a\"b\"c", "\"d\"", "\\e\\", "f\ng\nh", "適当に選んだ日本語文字", "\'i\'", "\tj\t", "\\\\", "1", "\u00E7", "1\U0001F47D", "2\uD83D\uDC7D", "3\"\"", "\"\"4"];
}