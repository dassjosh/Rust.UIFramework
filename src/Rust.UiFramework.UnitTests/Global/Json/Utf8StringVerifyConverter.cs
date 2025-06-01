using Oxide.Ext.UiFramework.Types;

namespace Rust.UiFramework.UnitTests.Global.Json;

public class Utf8StringVerifyConverter : WriteOnlyJsonConverter<Utf8String>
{
    public override void Write(VerifyJsonWriter writer, Utf8String value)
    {
        writer.WriteValue(value.ToString());
    }
}