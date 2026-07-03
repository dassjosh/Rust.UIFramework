using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Helpers;

namespace Rust.UiFramework.UnitTests.Helpers;

public sealed class GenericMathTests
{
    [Fact]
    public void Add_Int_ReturnsSum()
    {
        Assert.Equal(7, GenericMath.Add(3, 4));
    }

    [Fact]
    public void Subtract_Int_ReturnsDifference()
    {
        Assert.Equal(6, GenericMath.Subtract(10, 4));
    }

    [Fact]
    public void Multiply_Int_ReturnsProduct()
    {
        Assert.Equal(42, GenericMath.Multiply(6, 7));
    }

    [Fact]
    public void Divide_Int_ReturnsQuotient()
    {
        Assert.Equal(5, GenericMath.Divide(10, 2));
    }

    [Fact]
    public void Divide_Int_ByZero_ThrowsDivideByZeroException()
    {
        DivideByZeroException ex = Assert.Throws<DivideByZeroException>(() => GenericMath.Divide(10, 0));
        Assert.Equal("Cannot divide by zero.", ex.Message);
    }

    [Fact]
    public void IsZero_Int_WhenZero_ReturnsTrue()
    {
        Assert.True(GenericMath.IsZero(0));
    }

    [Fact]
    public void IsZero_Int_WhenNonZero_ReturnsFalse()
    {
        Assert.False(GenericMath.IsZero(1));
    }

    [Fact]
    public void And_Int_ReturnsBitwiseAnd()
    {
        Assert.Equal(0b0010, GenericMath.And(0b0110, 0b1010));
    }

    [Fact]
    public void Or_Int_ReturnsBitwiseOr()
    {
        Assert.Equal(0b1110, GenericMath.Or(0b0110, 0b1010));
    }

    [Fact]
    public void HasMask_Int_WhenMaskExists_ReturnsTrue()
    {
        Assert.True(GenericMath.HasMask(0b0111, 0b0011));
    }

    [Fact]
    public void HasMask_Int_WhenMaskDoesNotExist_ReturnsFalse()
    {
        Assert.False(GenericMath.HasMask(0b0101, 0b0011));
    }

    [Fact]
    public void Add_Double_ReturnsSum()
    {
        Assert.Equal(7.5, GenericMath.Add(3.25, 4.25));
    }

    [Fact]
    public void Divide_Double_ReturnsQuotient()
    {
        Assert.Equal(2.5, GenericMath.Divide(5.0, 2.0));
    }

    [Fact]
    public void IsZero_Double_WhenZero_ReturnsTrue()
    {
        Assert.True(GenericMath.IsZero(0.0));
    }

    [Fact]
    public void Lerp_Float_ReturnsInterpolatedValue()
    {
        float result = GenericMath.Lerp(10f, 20f, 0.25f);

        Assert.True(Math.Abs(result - 12.5f) < 0.0001f);
    }

    [Fact]
    public void Lerp_Float_WhenTIsZero_ReturnsStart()
    {
        Assert.Equal(10f, GenericMath.Lerp(10f, 20f, 0f));
    }

    [Fact]
    public void Lerp_Float_WhenTIsOne_ReturnsEnd()
    {
        Assert.Equal(20f, GenericMath.Lerp(10f, 20f, 1f));
    }

    [Fact]
    public void UnsupportedType_ThrowsNotSupportedException()
    {
        TypeInitializationException ex = Assert.Throws<TypeInitializationException>(() => GenericMath.Add(UiColors.Red, UiColors.Green));
        Assert.IsType<NotSupportedException>(ex.InnerException);
    }
}
