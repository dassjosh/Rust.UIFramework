using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.UnitTests.Global.XUnit.Serializers;
using Xunit.Sdk;

[assembly: RegisterXunitSerializer(typeof(GridPositionSerializer), typeof(GridPosition))]

namespace Rust.UiFramework.UnitTests.Global.XUnit.Serializers;

public class GridPositionSerializer : BaseSerializer<GridPosition>
{
    public static readonly GridPositionSerializer Instance = new(); 
    
    protected override GridPosition Deserialize(string serializedValue)
    {
        string[] parts = serializedValue.Replace("(", "").Replace(")", "").Split(",");
        UiPosition initial = new(float.Parse(parts[0]), float.Parse(parts[1]), float.Parse(parts[2]), float.Parse(parts[3]));
        int numCols = int.Parse(parts[4]);
        int numRows = int.Parse(parts[5]);
        float xPadding = float.Parse(parts[6]);
        float yPadding = float.Parse(parts[7]);
        UiPadding padding = new(xPadding, yPadding);
        return new GridPosition(initial, padding, numCols, numRows);
    }

    protected override string Serialize(GridPosition value)
    {
        UiPosition initial = value.InitialState;
        return $"({initial.Min.x},{initial.Min.y},{initial.Max.x},{initial.Max.y}),({value.NumCols},{value.NumRows}),({value.Padding})";
    }
}