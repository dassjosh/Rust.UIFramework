using Oxide.Ext.UiFramework.Positions;

namespace Rust.UiFramework.UnitTests.Global.Json;

public class UiPositionVerifyConverter : WriteOnlyJsonConverter<UiPosition>
{
    public override void Write(VerifyJsonWriter writer, UiPosition value)
    {
        writer.WriteValue(value.ToString());
    }
}