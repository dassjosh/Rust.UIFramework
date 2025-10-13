using Oxide.Ext.UiFramework.Types;

namespace Rust.UiFramework.UnitTests.Global.Json;

public class UiBorderWidthVerifyConverter : WriteOnlyJsonConverter<UiBorderWidth>
{
    public override void Write(VerifyJsonWriter writer, UiBorderWidth value)
    {
        writer.WriteValue(value.ToString());
    }
}