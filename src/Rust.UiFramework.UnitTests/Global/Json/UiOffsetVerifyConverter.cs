using Oxide.Ext.UiFramework.Offsets;

namespace Rust.UiFramework.UnitTests.Global.Json;

public class UiOffsetVerifyConverter : WriteOnlyJsonConverter<UiOffset>
{
    public override void Write(VerifyJsonWriter writer, UiOffset value)
    {
        writer.WriteValue(value.ToString());
    }
}