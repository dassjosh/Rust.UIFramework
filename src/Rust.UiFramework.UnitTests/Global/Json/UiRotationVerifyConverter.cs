using Oxide.Ext.UiFramework.Types;

namespace Rust.UiFramework.UnitTests.Global.Json;

public class UiRotationVerifyConverter : WriteOnlyJsonConverter<UiRotation>
{
    public override void Write(VerifyJsonWriter writer, UiRotation value)
    {
        writer.WriteValue($"{value.Rotation}");
    }
}