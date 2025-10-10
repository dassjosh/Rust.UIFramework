using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
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
        
        TrackedValue<TextAnchor> textAnchor = new();
        TrackedValue<TextAnchor?> nullableTextAnchor = new();
        TrackedValue<string> trackedString = new("123");
        TrackedValue<int> trackedInt = new(234);
        TrackedValue<ulong> trackedUlong = new();
        TrackedValue<float> trackedFloat = new();
        TrackedValue<bool> trackedBool = new(false);
        TrackedValue<UiColor> trackedUiColor = new(UiColors.White);
        TrackedValue<Vector2> trackedVector2 = new(new Vector2(100, 200));
        TrackedValue<UiPosition> trackedUiPosition = new(UiPosition.Full);
        TrackedValue<UiOffset> trackedUiOffset = new(UiOffset.Scaled);
        TrackedValue<UiBorderWidth> trackedBorderWidth = new();
        TrackedValue<UiRotation> trackedRotation = new();
        
        // Act
        writer.WriteStartObject();
        writer.AddFieldRaw((Utf8String)"Utf8StringRaw", (Utf8String)"Value");
        writer.AddFieldRaw((Utf8String)"StringRaw", "string");
        writer.AddFieldRaw((Utf8String)"IntRaw", 123);
        writer.AddFieldRaw((Utf8String)"UlongRaw", ulong.MaxValue);
        writer.AddFieldRaw((Utf8String)"FloatRaw", 30.5f);
        writer.AddFieldRaw((Utf8String)"BoolRaw", true);
        writer.AddFieldRaw((Utf8String)"UiColorRaw", UiColor.ParseHexColor("#7F7F7F7F"));
        writer.AddFieldRaw((Utf8String)"Vector2Raw", new Vector2(100, 200));
        writer.AddFieldRaw((Utf8String)"EnumRaw", TextAnchor.UpperCenter);
        writer.AddFieldRaw((Utf8String)"UiBorderWidth", new UiBorderWidth(1, 2, 4, 8));
        
        writer.AddField((Utf8String)"TextAnchorEnumCreateIsDefault", textAnchor, SerializeMode.Create);
        textAnchor.Value = TextAnchor.MiddleCenter;
        writer.AddField((Utf8String)"TextAnchorEnumCreateIsNotDefault", textAnchor, SerializeMode.Create);
        writer.AddField((Utf8String)"TextAnchorEnumUpdateHasChanged", textAnchor, SerializeMode.Update);
        textAnchor.ResetHasChanged();
        writer.AddField((Utf8String)"TextAnchorEnumUpdateHasNotChanged", textAnchor, SerializeMode.Update);
        
        writer.AddField((Utf8String)"NullableEnumCreateIsDefault", nullableTextAnchor, SerializeMode.Create);
        nullableTextAnchor.Value = TextAnchor.UpperRight;
        writer.AddField((Utf8String)"NullableEnumCreateIsNotDefault", nullableTextAnchor, SerializeMode.Create);
        writer.AddField((Utf8String)"NullableEnumUpdateHasChanged", nullableTextAnchor, SerializeMode.Update);
        nullableTextAnchor.ResetHasChanged();
        writer.AddField((Utf8String)"NullableEnumUpdateHasNotChanged", nullableTextAnchor, SerializeMode.Update);
        
        writer.AddField((Utf8String)"StringCreateIsDefault", trackedString, SerializeMode.Create);
        trackedString.Value = "321";
        writer.AddField((Utf8String)"StringCreateIsNotDefault", trackedString, SerializeMode.Create);
        writer.AddField((Utf8String)"StringUpdateHasChanged", trackedString, SerializeMode.Update);
        trackedString.ResetHasChanged();
        writer.AddField((Utf8String)"StringUpdateHasNotChanged", trackedString, SerializeMode.Update);
        
        writer.AddField((Utf8String)"IntCreateIsDefault", trackedInt, SerializeMode.Create);
        trackedInt.Value = 345;
        writer.AddField((Utf8String)"IntCreateIsNotDefault", trackedInt, SerializeMode.Create);
        writer.AddField((Utf8String)"IntUpdateHasChanged", trackedInt, SerializeMode.Update);
        trackedInt.ResetHasChanged();
        writer.AddField((Utf8String)"IntUpdateHasNotChanged", trackedInt, SerializeMode.Update);
        
        writer.AddField((Utf8String)"UlongCreateIsDefault", trackedUlong, SerializeMode.Create);
        trackedUlong.Value = 76561197960265728UL;
        writer.AddField((Utf8String)"UlongCreateIsNotDefault", trackedUlong, SerializeMode.Create);
        writer.AddField((Utf8String)"UlongUpdateHasChanged", trackedUlong, SerializeMode.Update);
        trackedUlong.ResetHasChanged();
        writer.AddField((Utf8String)"UlongUpdateHasNotChanged", trackedUlong, SerializeMode.Update);
        
        writer.AddField((Utf8String)"FloatCreateIsDefault", trackedFloat, SerializeMode.Create);
        trackedFloat.Value = 20.5f;
        writer.AddField((Utf8String)"FloatCreateIsNotDefault", trackedFloat, SerializeMode.Create);
        writer.AddField((Utf8String)"FloatUpdateHasChanged", trackedFloat, SerializeMode.Update);
        trackedFloat.ResetHasChanged();
        writer.AddField((Utf8String)"FloatUpdateHasNotChanged", trackedFloat, SerializeMode.Update);
        
        writer.AddField((Utf8String)"BoolCreateIsDefault", trackedBool, SerializeMode.Create);
        trackedBool.Value = true;
        writer.AddField((Utf8String)"BoolCreateIsNotDefault", trackedBool, SerializeMode.Create);
        writer.AddField((Utf8String)"BoolUpdateHasChanged", trackedBool, SerializeMode.Update);
        trackedBool.ResetHasChanged();
        writer.AddField((Utf8String)"BoolUpdateHasNotChanged", trackedBool, SerializeMode.Update);
        
        writer.AddField((Utf8String)"UiColorCreateIsDefault", trackedUiColor, SerializeMode.Create);
        trackedUiColor.Value = UiColors.Gray;
        writer.AddField((Utf8String)"UiColorCreateIsNotDefault", trackedUiColor, SerializeMode.Create);
        writer.AddField((Utf8String)"UiColorUpdateHasChanged", trackedUiColor, SerializeMode.Update);
        trackedUiColor.ResetHasChanged();
        writer.AddField((Utf8String)"UiColorUpdateHasNotChanged", trackedUiColor, SerializeMode.Update);
        
        writer.AddField((Utf8String)"Vector2CreateIsDefault", trackedVector2, SerializeMode.Create);
        trackedVector2.Value = new Vector2(300, 400);
        writer.AddField((Utf8String)"Vector2CreateIsNotDefault", trackedVector2, SerializeMode.Create);
        writer.AddField((Utf8String)"Vector2UpdateHasChanged", trackedVector2, SerializeMode.Update);
        trackedVector2.ResetHasChanged();
        writer.AddField((Utf8String)"Vector2UpdateHasNotChanged", trackedVector2, SerializeMode.Update);
        
        // writer.AddField((Utf8String)"UiPositionCreateIsDefault", trackedUiPosition, SerializeMode.Create);
        // trackedUiPosition.Value = new Vector2(300, 400);
        // writer.AddField((Utf8String)"UiPositionCreateIsNotDefault", trackedUiPosition, SerializeMode.Create);
        // writer.AddField((Utf8String)"UiPositionUpdateHasChanged", trackedUiPosition, SerializeMode.Update);
        // trackedUiPosition.ResetHasChanged();
        // writer.AddField((Utf8String)"UiPositionpdateHasNotChanged", trackedUiPosition, SerializeMode.Update);
        
        writer.AddField((Utf8String)"UiRotationCreateIsDefault", trackedRotation, SerializeMode.Create);
        trackedRotation.Value = new UiRotation(30);
        writer.AddField((Utf8String)"UiRotationWidthCreateIsNotDefault", trackedRotation, SerializeMode.Create);
        writer.AddField((Utf8String)"UiRotationWidthUpdateHasChanged", trackedRotation, SerializeMode.Update);
        trackedRotation.ResetHasChanged();
        writer.AddField((Utf8String)"UiRotationWidthUpdateHasNotChanged", trackedRotation, SerializeMode.Update);
        
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