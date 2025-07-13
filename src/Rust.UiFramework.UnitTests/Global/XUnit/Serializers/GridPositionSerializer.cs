using Oxide.Ext.UiFramework.Positions;
using Rust.UiFramework.UnitTests.Global.XUnit.Serializers;
using Xunit.Sdk;

[assembly: RegisterXunitSerializer(typeof(GridPositionSerializer), typeof(GridPosition))]

namespace Rust.UiFramework.UnitTests.Global.XUnit.Serializers;

public class GridPositionSerializer : IXunitSerializer
{
    public static readonly GridPositionSerializer Instance = new(); 
    
    public object Deserialize(Type type, string serializedValue)
    {
        string[] parts = serializedValue.Replace("(", "").Replace(")", "").Split(",");
        UiPosition initial = new(float.Parse(parts[0]), float.Parse(parts[1]), float.Parse(parts[2]), float.Parse(parts[3]));
        int numCols = int.Parse(parts[4]);
        int numRows = int.Parse(parts[5]);
        float xPadding = float.Parse(parts[6]);
        float yPadding = float.Parse(parts[7]);
        return new GridPosition(initial.XMin, initial.YMin, initial.XMax, initial.YMax, numCols, numRows, xPadding, yPadding);
    }

    public bool IsSerializable(Type type, object value, out string failureReason)
    {
        if (type == typeof(GridPosition))
        {
            failureReason = null;
            return true;
        }

        failureReason = "Not a GridPosition";
        return false;
    }

    public string Serialize(object value)
    {
        GridPosition grid = (GridPosition)value;
        UiPosition initial = grid.InitialState;
        return $"({initial.Min.x},{initial.Min.y},{initial.Max.x},{initial.Max.y}),({grid.NumCols},{grid.NumRows}),({grid.Padding})";
    }
}