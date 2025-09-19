using Oxide.Ext.UiFramework.Types;

namespace Rust.UiFramework.UnitTests.Extensions;

public class StringExtTests
{
    [Theory]
    [MemberData(nameof(LerpData))]
    public void String_Lerp_ProducesExpectedResult(StringLerpTheoryData data)
    {
        //Arrange
        
        //Act
        string result = LevenshteinDistanceExt.Lerp(data.Start, data.End, data.Lerp);
        
        //Assert
        result.Should().Be(data.Expected);
    }
    
    public static TheoryData<StringLerpTheoryData> LerpData =>
    [
        new StringLerpTheoryData("", "", 0f, ""),
        new StringLerpTheoryData("", "", 0.5f, ""),
        new StringLerpTheoryData("", "", 1f, ""),
        new StringLerpTheoryData("", "", -1f, ""),
        new StringLerpTheoryData("", "", 2f, ""),
         
        new StringLerpTheoryData("", "1234567890", 0f, ""),
        new StringLerpTheoryData("", "1234567890", 0.1f, "1"),
        new StringLerpTheoryData("", "1234567890", 0.2f, "12"),
        new StringLerpTheoryData("", "1234567890", 0.3f, "123"),
        new StringLerpTheoryData("", "1234567890", 0.4f, "1234"),
        new StringLerpTheoryData("", "1234567890", 0.5f, "12345"),
        new StringLerpTheoryData("", "1234567890", 0.6f, "123456"),
        new StringLerpTheoryData("", "1234567890", 0.7f, "1234567"),
        new StringLerpTheoryData("", "1234567890", 0.8f, "12345678"),
        new StringLerpTheoryData("", "1234567890", 0.9f, "123456789"),
        new StringLerpTheoryData("", "1234567890", 1f, "1234567890"),
         
        new StringLerpTheoryData("", "適当に選んだ日本語文字", 0f, ""),
        new StringLerpTheoryData("", "適当に選んだ日本語文字", 0.1f, "適"),
        new StringLerpTheoryData("", "適当に選んだ日本語文字", 0.2f, "適当"),
        new StringLerpTheoryData("", "適当に選んだ日本語文字", 0.3f, "適当に"),
        new StringLerpTheoryData("", "適当に選んだ日本語文字", 0.4f, "適当に選"),
        new StringLerpTheoryData("", "適当に選んだ日本語文字", 0.5f, "適当に選んだ"),
        new StringLerpTheoryData("", "適当に選んだ日本語文字", 0.6f, "適当に選んだ日"),
        new StringLerpTheoryData("", "適当に選んだ日本語文字", 0.7f, "適当に選んだ日本"),
        new StringLerpTheoryData("", "適当に選んだ日本語文字", 0.8f, "適当に選んだ日本語"),
        new StringLerpTheoryData("", "適当に選んだ日本語文字", 0.9f, "適当に選んだ日本語文"),
        new StringLerpTheoryData("", "適当に選んだ日本語文字", 1f, "適当に選んだ日本語文字"),
         
        new StringLerpTheoryData("", SurrogateString, 0f, ""),
        new StringLerpTheoryData("", SurrogateString, 0.1f, "👽"),
        new StringLerpTheoryData("", SurrogateString, 0.2f, "👽👾"),
        new StringLerpTheoryData("", SurrogateString, 0.3f, "👽👾👿💀"),
        new StringLerpTheoryData("", SurrogateString, 0.4f, "👽👾👿💀💁"),
        new StringLerpTheoryData("", SurrogateString, 0.5f, "👽👾👿💀💁💂"),
        new StringLerpTheoryData("", SurrogateString, 0.6f, "👽👾👿💀💁💂💃"),
        new StringLerpTheoryData("", SurrogateString, 0.7f, "👽👾👿💀💁💂💃💄"),
        new StringLerpTheoryData("", SurrogateString, 0.8f, "👽👾👿💀💁💂💃💄💅💆"),
        new StringLerpTheoryData("", SurrogateString, 0.9f, "👽👾👿💀💁💂💃💄💅💆💇"),
        new StringLerpTheoryData("", SurrogateString, 1f, "👽👾👿💀💁💂💃💄💅💆💇💈"),
         
        new StringLerpTheoryData("", MixedSurrogateString, 0f, ""),
        new StringLerpTheoryData("", MixedSurrogateString, 0.1f, "1👽22👾333👿"),
        new StringLerpTheoryData("", MixedSurrogateString, 0.2f, "1👽22👾333👿4444💀5555"),
        new StringLerpTheoryData("", MixedSurrogateString, 0.3f, "1👽22👾333👿4444💀55555💁666666💂"),
        new StringLerpTheoryData("", MixedSurrogateString, 0.4f, "1👽22👾333👿4444💀55555💁666666💂7777777💃8"),
        new StringLerpTheoryData("", MixedSurrogateString, 0.5f, "1👽22👾333👿4444💀55555💁666666💂7777777💃88888888💄9"),
        new StringLerpTheoryData("", MixedSurrogateString, 0.6f, "1👽22👾333👿4444💀55555💁666666💂7777777💃88888888💄999999999💅"),
        new StringLerpTheoryData("", MixedSurrogateString, 0.7f, "1👽22👾333👿4444💀55555💁666666💂7777777💃88888888💄999999999💅000000000"),
        new StringLerpTheoryData("", MixedSurrogateString, 0.8f, "1👽22👾333👿4444💀55555💁666666💂7777777💃88888888💄999999999💅0000000000💆1111111"),
        new StringLerpTheoryData("", MixedSurrogateString, 0.9f, "1👽22👾333👿4444💀55555💁666666💂7777777💃88888888💄999999999💅0000000000💆11111111111💇1212"),
        new StringLerpTheoryData("", MixedSurrogateString, 1f, MixedSurrogateString),
         
        new StringLerpTheoryData("Welcome to the Jungle", "Welcome to our server!", 0f, "Welcome to the Jungle"),
        new StringLerpTheoryData("Welcome to the Jungle", "Welcome to our server!", 0.5f, "Welcome to our seungle"),
        new StringLerpTheoryData("Welcome to the Jungle", "Welcome to our server!", 1f, "Welcome to our server!"),
    ];
    
    public record StringLerpTheoryData(string Start, string End, float Lerp, string Expected);

    private const string SurrogateString = "👽👾👿💀💁💂💃💄💅💆💇💈";
    private const string MixedSurrogateString = "1👽22👾333👿4444💀55555💁666666💂7777777💃88888888💄999999999💅0000000000💆11111111111💇121212121212💈";
}