using Oxide.Ext.UiFramework.Types;

namespace Rust.UiFramework.UnitTests.Global.Json;

public class UiPivotVerifyConverter : WriteOnlyJsonConverter<UiPivot>
{
    public override void Write(VerifyJsonWriter writer, UiPivot value)
    {
        writer.WriteValue($"{value.Horizontal} {value.Vertical}");
    }
}