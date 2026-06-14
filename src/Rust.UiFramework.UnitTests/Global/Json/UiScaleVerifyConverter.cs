using Oxide.Ext.UiFramework.Types;

namespace Rust.UiFramework.UnitTests.Global.Json;

public class UiScaleVerifyConverter : WriteOnlyJsonConverter<UiScale>
{
    public override void Write(VerifyJsonWriter writer, UiScale value)
    {
        writer.WriteValue($"{value.Horizontal} {value.Vertical}");
    }
}