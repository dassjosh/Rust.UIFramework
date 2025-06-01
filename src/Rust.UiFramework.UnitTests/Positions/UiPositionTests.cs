using Oxide.Ext.UiFramework.Positions;
using UnityEngine;

namespace Rust.UiFramework.UnitTests.Positions;

public class UiPositionTests
{
    [Fact]
    public void Constructor_SetsCorrectValues()
    {
        // Arrange
        UiPosition position = new(1f, 2f, 3f, 4f);
        
        // Act
        
        // Assert
        position.Min.Should().Be(new Vector2(1f, 2f));
        position.Max.Should().Be(new Vector2(3f, 4f));
    }
    
    [Fact]
    public void Constructor_Vector2_SetsCorrectValues()
    {
        // Arrange
        UiPosition position = new(new Vector2(1f, 2f), new Vector2(3f, 4f));
        
        // Act
        
        // Assert
        position.Min.Should().Be(new Vector2(1f, 2f));
        position.Max.Should().Be(new Vector2(3f, 4f));
    }
    
    [Fact]
    public void WithXMin_SetsCorrectValues()
    {
        // Arrange
        UiPosition position = new(1f, 2f, 3f, 4f);
        
        // Act
        UiPosition newPosition = position.WithXMin(5f);
        
        // Assert
        newPosition.Min.Should().Be(new Vector2(5f, 2f));
        newPosition.Max.Should().Be(new Vector2(3f, 4f));
    }
    
    [Fact]
    public void WithXMax_SetsCorrectValues()
    {
        // Arrange
        UiPosition position = new(1f, 2f, 3f, 4f);
        
        // Act
        UiPosition newPosition = position.WithXMax(5f);
        
        // Assert
        newPosition.Min.Should().Be(new Vector2(1f, 2f));
        newPosition.Max.Should().Be(new Vector2(5f, 4f));
    }
    
    [Fact]
    public void WithYMin_SetsCorrectValues()
    {
        // Arrange
        UiPosition position = new(1f, 2f, 3f, 4f);
        
        // Act
        UiPosition newPosition = position.WithYMin(5f);
        
        // Assert
        newPosition.Min.Should().Be(new Vector2(1f, 5f));
        newPosition.Max.Should().Be(new Vector2(3f, 4f));
    }
    
    [Fact]
    public void WithYMax_SetsCorrectValues()
    {
        // Arrange
        UiPosition position = new(1f, 2f, 3f, 4f);
        
        // Act
        UiPosition newPosition = position.WithYMax(5f);
        
        // Assert
        newPosition.Min.Should().Be(new Vector2(1f, 2f));
        newPosition.Max.Should().Be(new Vector2(3f, 5f));
    }
    
    [Fact]
    public void SetX_SetsCorrectValues()
    {
        // Arrange
        UiPosition position = new(1f, 2f, 3f, 4f);
        
        // Act
        UiPosition newPosition = position.SetX(5f, 6f);
        
        // Assert
        newPosition.Min.Should().Be(new Vector2(5f, 2f));
        newPosition.Max.Should().Be(new Vector2(6f, 4f));
    }
    
    [Fact]
    public void SetY_SetsCorrectValues()
    {
        // Arrange
        UiPosition position = new(1f, 2f, 3f, 4f);
        
        // Act
        UiPosition newPosition = position.SetY(5f, 6f);
        
        // Assert
        newPosition.Min.Should().Be(new Vector2(1f, 5f));
        newPosition.Max.Should().Be(new Vector2(3f, 6f));
    }
    
    [Fact]
    public void MoveX_SetsCorrectValues()
    {
        // Arrange
        UiPosition position = new(1f, 2f, 3f, 4f);
        
        // Act
        UiPosition newPosition = position.MoveX(5f);
        
        // Assert
        newPosition.Min.Should().Be(new Vector2(6f, 2f));
        newPosition.Max.Should().Be(new Vector2(8f, 4f));
    }
    
    [Fact]
    public void MoveY_SetsCorrectValues()
    {
        // Arrange
        UiPosition position = new(1f, 2f, 3f, 4f);
        
        // Act
        UiPosition newPosition = position.MoveY(5f);
        
        // Assert
        newPosition.Min.Should().Be(new Vector2(1f, 7f));
        newPosition.Max.Should().Be(new Vector2(3f, 9f));
    }
    
    [Fact]
    public void MoveXPadded_SetsCorrectValues()
    {
        // Arrange
        UiPosition position = new(1f, 2f, 3f, 4f);
        
        // Act
        UiPosition newPosition = position.MoveXPadded(0.1f);
        
        // Assert
        newPosition.Min.Should().Be(new Vector2(3.1f, 2f));
        newPosition.Max.Should().Be(new Vector2(5.1f, 4f));
    }
    
    [Fact]
    public void MoveYPadded_SetsCorrectValues()
    {
        // Arrange
        UiPosition position = new(1f, 2f, 3f, 4f);
        
        // Act
        UiPosition newPosition = position.MoveYPadded(0.1f);
        
        // Assert
        newPosition.Min.Should().Be(new Vector2(1f, 4.1f));
        newPosition.Max.Should().Be(new Vector2(3f, 6.1f));
    }
    
    [Fact]
    public void Expand_SetsCorrectValues()
    {
        // Arrange
        UiPosition position = new(1f, 2f, 3f, 4f);
        
        // Act
        UiPosition newPosition = position.Expand(0.1f);
        
        // Assert
        newPosition.Min.Should().Be(new Vector2(0.9f, 1.9f));
        newPosition.Max.Should().Be(new Vector2(3.1f, 4.1f));
    }
    
    [Fact]
    public void Expand_XY_SetsCorrectValues()
    {
        // Arrange
        UiPosition position = new(1f, 2f, 3f, 4f);
        
        // Act
        UiPosition newPosition = position.Expand(0.1f, 0.2f);
        
        // Assert
        newPosition.Min.Should().Be(new Vector2(0.9f, 1.8f));
        newPosition.Max.Should().Be(new Vector2(3.1f, 4.2f));
    }
    
    [Fact]
    public void ExpandHorizontal_SetsCorrectValues()
    {
        // Arrange
        UiPosition position = new(1f, 2f, 3f, 4f);
        
        // Act
        UiPosition newPosition = position.ExpandHorizontal(0.1f);
        
        // Assert
        newPosition.Min.Should().Be(new Vector2(0.9f, 2f));
        newPosition.Max.Should().Be(new Vector2(3.1f, 4f));
    }
    
    [Fact]
    public void ExpandVertical_SetsCorrectValues()
    {
        // Arrange
        UiPosition position = new(1f, 2f, 3f, 4f);
        
        // Act
        UiPosition newPosition = position.ExpandVertical(0.1f);
        
        // Assert
        newPosition.Min.Should().Be(new Vector2(1f, 1.9f));
        newPosition.Max.Should().Be(new Vector2(3f, 4.1f));
    }
    
    [Fact]
    public void Shrink_SetsCorrectValues()
    {
        // Arrange
        UiPosition position = new(1f, 2f, 3f, 4f);
        
        // Act
        UiPosition newPosition = position.Shrink(0.1f);
        
        // Assert
        newPosition.Min.Should().Be(new Vector2(1.1f, 2.1f));
        newPosition.Max.Should().Be(new Vector2(2.9f, 3.9f));
    }
    
    [Fact]
    public void Shrink_XY_SetsCorrectValues()
    {
        // Arrange
        UiPosition position = new(1f, 2f, 3f, 4f);
        
        // Act
        UiPosition newPosition = position.Shrink(0.1f, 0.2f);
        
        // Assert
        newPosition.Min.Should().Be(new Vector2(1.1f, 2.2f));
        newPosition.Max.Should().Be(new Vector2(2.9f, 3.8f));
    }
    
    [Fact]
    public void ShrinkHorizontal_SetsCorrectValues()
    {
        // Arrange
        UiPosition position = new(1f, 2f, 3f, 4f);
        
        // Act
        UiPosition newPosition = position.ShrinkHorizontal(0.1f);
        
        // Assert
        newPosition.Min.Should().Be(new Vector2(1.1f, 2f));
        newPosition.Max.Should().Be(new Vector2(2.9f, 4f));
    }
    
    [Fact]
    public void ShrinkVertical_SetsCorrectValues()
    {
        // Arrange
        UiPosition position = new(1f, 2f, 3f, 4f);
        
        // Act
        UiPosition newPosition = position.ShrinkVertical(0.1f);
        
        // Assert
        newPosition.Min.Should().Be(new Vector2(1f, 2.1f));
        newPosition.Max.Should().Be(new Vector2(3f, 3.9f));
    }
    
    [Fact]
    public void Slice_SetsCorrectValues()
    {
        // Arrange
        UiPosition position = new(1f, 2f, 3f, 4f);
        
        // Act
        UiPosition newPosition = position.Slice(0.1f, 0.2f, 0.3f, 0.4f);
        
        // Assert
        newPosition.Min.Should().Be(new Vector2(1.2f, 2.4f));
        newPosition.Max.Should().Be(new Vector2(1.6f, 2.8f));
    }
    
    [Fact]
    public void SliceHorizontal_SetsCorrectValues()
    {
        // Arrange
        UiPosition position = new(1f, 2f, 3f, 4f);
        
        // Act
        UiPosition newPosition = position.SliceHorizontal(0.1f, 0.2f);
        
        // Assert
        newPosition.Min.Should().Be(new Vector2(1.2f, 2f));
        newPosition.Max.Should().Be(new Vector2(1.4f, 4f));
    }
    
    [Fact]
    public void SliceVertical_SetsCorrectValues()
    {
        // Arrange
        UiPosition position = new(1f, 2f, 3f, 4f);
        
        // Act
        UiPosition newPosition = position.SliceVertical(0.1f, 0.2f);
        
        // Assert
        newPosition.Min.Should().Be(new Vector2(1f, 2.2f));
        newPosition.Max.Should().Be(new Vector2(3f, 2.4f));
    }
    
    [Fact]
    public void Lerp_SetsCorrectValues()
    {
        // Arrange
        UiPosition start = new(1f, 2f, 3f, 4f);
        UiPosition end = new(5f, 6f, 7f, 8f);
        
        // Act
        UiPosition lerp = UiPosition.Lerp(start, end, 0.5f);
        
        // Assert
        lerp.Min.Should().Be(new Vector2(3f, 4f));
        lerp.Max.Should().Be(new Vector2(5f, 6f));
    }
    
    [Fact]
    public void Lerp_LessThan0_SetsCorrectValues()
    {
        // Arrange
        UiPosition start = new(1f, 2f, 3f, 4f);
        UiPosition end = new(5f, 6f, 7f, 8f);
        
        // Act
        UiPosition lerp = UiPosition.Lerp(start, end, -1);
        
        // Assert
        lerp.Min.Should().Be(start.Min);
        lerp.Max.Should().Be(start.Max);
    }
    
    [Fact]
    public void Lerp_GreaterThan1_SetsCorrectValues()
    {
        // Arrange
        UiPosition start = new(1f, 2f, 3f, 4f);
        UiPosition end = new(5f, 6f, 7f, 8f);
        
        // Act
        UiPosition lerp = UiPosition.Lerp(start, end, 2);
        
        // Assert
        lerp.Min.Should().Be(end.Min);
        lerp.Max.Should().Be(end.Max);
    }
}