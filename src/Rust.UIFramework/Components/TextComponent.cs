using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components;

public class TextComponent : BaseTextComponent
{
    public override void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, JsonDefaults.BaseText.Type);
        base.WriteComponent(writer);
        writer.WriteEndObject();
    }
}