using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Json;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components
{
    public class BaseTextComponent : FadeInComponent
    {
        public int FontSize = 14;
        public string Font;
        public TextAnchor Align;
        public string Text;

        public override void WriteComponent(JsonTextWriter writer)
        {
            JsonCreator.AddTextField(writer, JsonDefaults.TextName, Text);
            JsonCreator.AddField(writer, JsonDefaults.FontSizeName, FontSize, JsonDefaults.FontSizeValue);
            JsonCreator.AddField(writer, JsonDefaults.FontName, Font, JsonDefaults.FontValue);
            JsonCreator.AddField(writer, JsonDefaults.AlignName, Align);
            base.WriteComponent(writer);
        }

        public override void EnterPool()
        {
            base.EnterPool();
            FontSize = 14;
            Font = null;
            Align = TextAnchor.UpperLeft;
            Text = null;
        }
    }
}