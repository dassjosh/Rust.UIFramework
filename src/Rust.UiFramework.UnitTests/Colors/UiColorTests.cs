using Oxide.Ext.UiFramework.Colors;
using UnityEngine;

namespace Rust.UiFramework.UnitTests.Colors;

public class UiColorTests
{
    [Fact]
    public void Constructor_Byte_SetsCorrectValues()
    {
        //Arrange
        UiColor color = new((byte)50, (byte)100, (byte)150, (byte)200);

        //Act
        
        //Assert
        color.Red.Should().Be(50);
        color.Green.Should().Be(100);
        color.Blue.Should().Be(150);
        color.Alpha.Should().Be(200);

        color.RedFloat.Should().BeApproximately(0.19607843f, 0.001f);
        color.GreenFloat.Should().BeApproximately(0.39215687f, 0.001f);
        color.BlueFloat.Should().BeApproximately(0.5882353f, 0.001f);
        color.AlphaFloat.Should().BeApproximately(0.78431374f, 0.001f);
    }
    
    [Fact]
    public void Constructor_Int_SetsCorrectValues()
    {
        //Arrange
        UiColor color = new(50, 100, 150, 200);

        //Act
        
        //Assert
        color.Red.Should().Be(50);
        color.Green.Should().Be(100);
        color.Blue.Should().Be(150);
        color.Alpha.Should().Be(200);

        color.RedFloat.Should().BeApproximately(0.19607843f, 0.001f);
        color.GreenFloat.Should().BeApproximately(0.39215687f, 0.001f);
        color.BlueFloat.Should().BeApproximately(0.5882353f, 0.001f);
        color.AlphaFloat.Should().BeApproximately(0.78431374f, 0.001f);
    }

    [Fact]
    public void Constructor_Float_SetsCorrectValues()
    {
        //Arrange
        UiColor color = new(0.19607843f, 0.39215687f, 0.5882353f, 0.78431374f);

        //Act
        
        //Assert
        color.Red.Should().Be(50);
        color.Green.Should().Be(100);
        color.Blue.Should().Be(150);
        color.Alpha.Should().Be(200);

        color.RedFloat.Should().BeApproximately(0.19607843f, 0.001f);
        color.GreenFloat.Should().BeApproximately(0.39215687f, 0.001f);
        color.BlueFloat.Should().BeApproximately(0.5882353f, 0.001f);
        color.AlphaFloat.Should().BeApproximately(0.78431374f, 0.001f);
    }
    
    [Fact]
    public void Constructor_Color_SetsCorrectValues()
    {
        //Arrange
        UiColor color = new(new Color(0.19607843f, 0.39215687f, 0.5882353f, 0.78431374f));

        //Act
        
        //Assert
        color.Red.Should().Be(50);
        color.Green.Should().Be(100);
        color.Blue.Should().Be(150);
        color.Alpha.Should().Be(200);

        color.RedFloat.Should().BeApproximately(0.19607843f, 0.001f);
        color.GreenFloat.Should().BeApproximately(0.39215687f, 0.001f);
        color.BlueFloat.Should().BeApproximately(0.5882353f, 0.001f);
        color.AlphaFloat.Should().BeApproximately(0.78431374f, 0.001f);
    }
    
    [Theory]
    [InlineData("#FF0000", 0xFF, 0, 0, 0xFF)]
    [InlineData("#00FF00", 0, 0xFF, 0, 0xFF)]
    [InlineData("#0000FF", 0, 0, 0xFF, 0xFF)]
    [InlineData("#000000FF", 0, 0, 0, 0xFF)]
    [InlineData("#FFFFFF", 0xFF, 0xFF, 0xFF, 0xFF)]
    public void ParseHexColor_SetsCorrectValues(string hex, byte r, byte g, byte b, byte a)
    {
        //Arrange
        UiColor color = UiColor.ParseHexColor(hex);

        //Act
        
        //Assert
        color.Red.Should().Be(r);
        color.Green.Should().Be(g);
        color.Blue.Should().Be(b);
        color.Alpha.Should().Be(a);

        color.ToHexRGB().Should().Be($"{r:X2}{g:X2}{b:X2}");
        color.ToHexRGBA().Should().Be($"{r:X2}{g:X2}{b:X2}{a:X2}");
        color.ToHtmlColor().Should().Be($"#{r:X2}{g:X2}{b:X2}{a:X2}");
    }
    
    [Theory]
    [InlineData("1.0 0 0", 0xFF, 0, 0, 0xFF)]
    [InlineData("0 1.0 0", 0, 0xFF, 0, 0xFF)]
    [InlineData("0 0 1.0", 0, 0, 0xFF, 0xFF)]
    [InlineData("0 0 0 1", 0, 0, 0, 0xFF)]
    [InlineData("1.0 1.0 1.0 1.0", 0xFF, 0xFF, 0xFF, 0xFF)]
    public void ParseRustColor_SetsCorrectValues(string hex, byte r, byte g, byte b, byte a)
    {
        //Arrange
        UiColor color = UiColor.ParseRustColor(hex);

        //Act
        
        //Assert
        color.Red.Should().Be(r);
        color.Green.Should().Be(g);
        color.Blue.Should().Be(b);
        color.Alpha.Should().Be(a);
    }
    
    [Fact]
    public void WithAlpha_SetsCorrectValues()
    {
        //Arrange
        UiColor color = UiColor.ParseHexColor("#FFFFFFFF");
        
        //Act
        UiColor byteAlpha = color.WithAlpha(0x7F);
        UiColor stringHexAlpha = color.WithAlpha("7F");
        UiColor intAlpha = color.WithAlpha(127);
        UiColor floatAlpha = color.WithAlpha(0.5f);
        
        //Assert
        byteAlpha.Red.Should().Be(0xFF);
        byteAlpha.Green.Should().Be(0xFF);
        byteAlpha.Blue.Should().Be(0xFF);
        byteAlpha.Alpha.Should().Be(0x7F);
        
        stringHexAlpha.Red.Should().Be(0xFF);
        stringHexAlpha.Green.Should().Be(0xFF);
        stringHexAlpha.Blue.Should().Be(0xFF);
        stringHexAlpha.Alpha.Should().Be(0x7F);
        
        intAlpha.Red.Should().Be(0xFF);
        intAlpha.Green.Should().Be(0xFF);
        intAlpha.Blue.Should().Be(0xFF);
        intAlpha.Alpha.Should().Be(0x7F);
        
        floatAlpha.Red.Should().Be(0xFF);
        floatAlpha.Green.Should().Be(0xFF);
        floatAlpha.Blue.Should().Be(0xFF);
        floatAlpha.Alpha.Should().Be(0x7F);
    }
    
    [Fact]
    public void MultiplyAlpha_SetsCorrectValues()
    {
        //Arrange
        UiColor color = UiColor.ParseHexColor("#FFFFFFFF");
        UiColor half = UiColor.ParseHexColor("#7F7F7F7F");
        
        //Act
        UiColor fullToHalf = color.MultiplyAlpha(0.5f);
        UiColor halfDoubled = half.MultiplyAlpha(2f);
        
        //Assert
        fullToHalf.Red.Should().Be(0xFF);
        fullToHalf.Green.Should().Be(0xFF);
        fullToHalf.Blue.Should().Be(0xFF);
        fullToHalf.Alpha.Should().Be(0x7F);
        
        halfDoubled.Red.Should().Be(0x7F);
        halfDoubled.Green.Should().Be(0x7F);
        halfDoubled.Blue.Should().Be(0x7F);
        halfDoubled.Alpha.Should().Be(0xFE);
    }
    
    [Theory]
    [MemberData(nameof(LightenData))]
    public void Lighten_SetsCorrectValues(float lighten, string expected)
    {
        //Arrange
        UiColor color = UiColor.ParseHexColor("#7F7F7F");
        
        //Act
        UiColor lightenedColor = color.Lighten(lighten);
        
        //Assert
        lightenedColor.Should().BeEquivalentTo(UiColor.ParseHexColor(expected));
    }
    
    public static TheoryData<float, string> LightenData() =>
    [
        (-1f, "#7F7F7F"),
        (0f, "#7F7F7F"),
        (0.25f, "#9F9F9F"),
        (0.5f, "#BFBFBF"),
        (0.75f, "#DFDFDF"),
        (1f, "#FFFFFF"),
        (2f, "#FFFFFF")
    ];
    
    [Theory]
    [MemberData(nameof(DarkenData))]
    public void Darken_SetsCorrectValues(float darken, string expected)
    {
        //Arrange
        UiColor color = UiColor.ParseHexColor("#7F7F7F");
        
        //Act
        UiColor darkenedColor = color.Darken(darken);
        
        //Assert
        darkenedColor.Should().BeEquivalentTo(UiColor.ParseHexColor(expected));
    }
    
    public static TheoryData<float, string> DarkenData() =>
    [
        (-1f, "#7F7F7F"),
        (0f, "#7F7F7F"),
        (0.25f, "#5F5F5F"),
        (0.5f, "3F3F3F"),
        (0.75f, "#1F1F1F"),
        (1f, "#000000"),
        (2f, "#000000")
    ];
    
    [Theory]
    [MemberData(nameof(LerpData))]
    public void Lerp_SetsCorrectValues(float lerp, string expected)
    {
        //Arrange
        UiColor startColor = UiColor.ParseHexColor("#3F3F3F");
        UiColor endColor = UiColor.ParseHexColor("#BFBFBF");
        
        //Act
        UiColor lerpedColor = UiColor.Lerp(startColor, endColor, lerp);
        
        //Assert
        lerpedColor.Should().BeEquivalentTo(UiColor.ParseHexColor(expected));
    }
    
    public static TheoryData<float, string> LerpData() =>
    [
        (-1f, "#3F3F3F"),
        (0f, "#3F3F3F"),
        (0.25f, "#5F5F5F"),
        (0.5f, "7F7F7F"),
        (0.75f, "9F9F9F"),
        (1f, "#BFBFBF"),
        (2f, "#BFBFBF")
    ];
    
    [Theory]
    [MemberData(nameof(LerpUnclampedData))]
    public void LerpUnClamped_SetsCorrectValues(float lerp, string expected)
    {
        //Arrange
        UiColor startColor = UiColor.ParseHexColor("#3F3F3F");
        UiColor endColor = UiColor.ParseHexColor("#BFBFBF");
        
        //Act
        UiColor lerpedColor = UiColor.LerpUnclamped(startColor, endColor, lerp);
        
        //Assert
        lerpedColor.Should().BeEquivalentTo(UiColor.ParseHexColor(expected));
    }
    
    public static TheoryData<float, string> LerpUnclampedData() =>
    [
        (-1f, "#000000"),
        (-0.25f, "#1F1F1F"),
        (0f, "#3F3F3F"),
        (0.25f, "#5F5F5F"),
        (0.5f, "7F7F7F"),
        (0.75f, "9F9F9F"),
        (1f, "#BFBFBF"),
        (1.25f, "#DFDFDF"),
        (2f, "#FFFFFF")
    ];
}