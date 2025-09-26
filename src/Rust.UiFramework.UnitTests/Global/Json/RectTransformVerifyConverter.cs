using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Positions;

namespace Rust.UiFramework.UnitTests.Global.Json;

public class RectTransformVerifyConverter : WriteOnlyJsonConverter<RectTransformComponent>
{
    public override void Write(VerifyJsonWriter writer, RectTransformComponent value)
    {
       writer.WriteStartObject();
       writer.WriteMember(value, value.Position, nameof(RectTransformComponent.Position), UiPosition.Full);
       writer.WriteMember(value, value.Offset, nameof(RectTransformComponent.Offset), default);
       writer.WriteMember(value, value.Rotation, nameof(RectTransformComponent.Rotation), default);
       writer.WriteMember(value, value.Padding, nameof(RectTransformComponent.Padding), default);
       writer.WriteEndObject();
    }
}