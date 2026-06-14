using Oxide.Ext.UiFramework.Types;

namespace Rust.UiFramework.UnitTests.Global.Json;

public class UiPaddingVerifyConverter : WriteOnlyJsonConverter<UiPadding>
{
    public override void Write(VerifyJsonWriter writer, UiPadding value)
    {
        writer.WriteValue($"{value}");
    }
}