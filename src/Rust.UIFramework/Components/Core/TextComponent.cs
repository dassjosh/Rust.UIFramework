using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Components;

public class TextComponent : BaseTextComponent
{
    public override Utf8String Type => JsonDefaults.BaseText.Type;
    
    public override void WriteComponent(JsonFrameworkWriter writer)
    {
        if (!string.IsNullOrEmpty(Text))
        {
            writer.WriteStartObject();
            writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
            base.WriteComponent(writer);
            writer.WriteEndObject();
        }
    }
}