using Oxide.Ext.UiFramework.Colors;

namespace Rust.UiFramework.UnitTests.Global.Json;

public class UiColorVerifyConverter : WriteOnlyJsonConverter<UiColor>
{
    public override void Write(VerifyJsonWriter writer, UiColor value)
    {
       writer.WriteValue(value.ToHtmlColor());
    }
}