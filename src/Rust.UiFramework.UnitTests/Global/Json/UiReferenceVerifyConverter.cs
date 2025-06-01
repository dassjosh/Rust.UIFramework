using Oxide.Ext.UiFramework.UiElements;

namespace Rust.UiFramework.UnitTests.Global.Json;

public class UiReferenceVerifyConverter : WriteOnlyJsonConverter<UiReference>
{
    public override void Write(VerifyJsonWriter writer, UiReference value)
    {
       writer.WriteValue($"{value.Parent}:{value.Name}");
    }
}