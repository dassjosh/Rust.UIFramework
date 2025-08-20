using Oxide.Ext.UiFramework.Positions;
using Rust.UiFramework.UnitTests.Global.XUnit.Serializers;
using Xunit.Sdk;

[assembly: RegisterXunitSerializer(typeof(UiPositionSerializer), typeof(UiPosition))]

namespace Rust.UiFramework.UnitTests.Global.XUnit.Serializers;

public class UiPositionSerializer : BaseSerializer<UiPosition>
{
    public static readonly UiPositionSerializer Instance = new(); 
    protected override UiPosition Deserialize(string serializedValue)
    {
        string[] parts = serializedValue.Replace(" ", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty).Split(",");
        UiPosition pos = new(float.Parse(parts[0]), float.Parse(parts[1]), float.Parse(parts[2]), float.Parse(parts[3]));
        return pos;
    }

    protected override string Serialize(UiPosition value)
    {
        return value.ToString();
    }
}