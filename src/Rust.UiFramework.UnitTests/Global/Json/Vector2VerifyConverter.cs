using UnityEngine;

namespace Rust.UiFramework.UnitTests.Global.Json;

public class Vector2VerifyConverter : WriteOnlyJsonConverter<Vector2>
{
    public override void Write(VerifyJsonWriter writer, Vector2 value)
    {
        writer.WriteValue($"{value.x} {value.y}");
    }
}