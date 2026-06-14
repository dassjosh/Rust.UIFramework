using Oxide.Ext.UiFramework.Types;

namespace Rust.UiFramework.UnitTests.Global.Json;

public class UiTranslateVerifyConverter : WriteOnlyJsonConverter<UiTranslate>
{
    public override void Write(VerifyJsonWriter writer, UiTranslate value)
    {
        writer.WriteValue($"{value.X} {value.Y}");
    }
}