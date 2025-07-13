using Oxide.Ext.UiFramework.Positions;
using Xunit.Sdk;

namespace Rust.UiFramework.UnitTests.Positions;

public class GridPositionBuilderTests
{
    [Theory]
    [MemberData(nameof(GridPositionBuilderData))]
    public async Task GridPositionBuilder_SetsCorrectValues(GridPositionBuilderTheoryData data)
    {
        // Arrange
        GridPosition grid = new GridPositionBuilder(data.Rows, data.Cols)
            .SetHorizontalPadding(data.HorizontalPadding)
            .SetVerticalPadding(data.VerticalPadding)
            .SetColWidth(data.ColWidth)
            .SetRowHeight(data.RowHeight)
            .SetColOffset(data.ColOffset)
            .SetRowOffset(data.RowOffset).Build();
        
        // Act
        
        // Assert
        await Verify(grid.ToPosition());
    }

    public static TheoryData<GridPositionBuilderTheoryData> GridPositionBuilderData =>
    [
        new GridPositionBuilderTheoryData(1, 1, 0, 0, 1, 1, 0, 0),
        
        new GridPositionBuilderTheoryData(2, 2, 0, 0, 1, 1, 0, 0),
        new GridPositionBuilderTheoryData(2, 2, 0, 0, 1, 1, 1, 0),
        new GridPositionBuilderTheoryData(2, 2, 0, 0, 1, 1, 0, 1),
        new GridPositionBuilderTheoryData(2, 2, 0, 0, 1, 1, 1, 1),
        
        
        new GridPositionBuilderTheoryData(4, 4, 0, 0, 1, 1, 0, 0),
        new GridPositionBuilderTheoryData(4, 4, 0.01f, 0.02f, 1, 1, 0, 0),
        new GridPositionBuilderTheoryData(4, 4, 0.03f, 0.04f, 1, 1, 0, 0),
        
        
        new GridPositionBuilderTheoryData(8, 8, 0, 0, 2, 2, 2, 2),
        new GridPositionBuilderTheoryData(8, 8, 0, 0, 2, 2, 4, 6),
        
        new GridPositionBuilderTheoryData(16, 8, 0.001f, 0.002f, 2, 3, 4, 6),
        new GridPositionBuilderTheoryData(16, 8, 0.001f, 0.002f, 3, 2, 4, 6),
    ];

    public record GridPositionBuilderTheoryData(int Rows, int Cols, float HorizontalPadding, float VerticalPadding, int ColWidth, int RowHeight, int ColOffset, int RowOffset);
}