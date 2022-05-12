using Oxide.Ext.UiFramework.Json;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components
{
    public abstract class BaseTextComponent : BaseFadeInComponent
    {
        public int FontSize = JsonDefaults.BaseText.FontSize;
        public string Font;
        public TextAnchor Align;
        public string Text;

        public override void WriteComponent(JsonFrameworkWriter writer)
        {
            writer.AddTextField(JsonDefaults.BaseText.TextName, Text);
            writer.AddField(JsonDefaults.BaseText.FontSizeName, FontSize, JsonDefaults.BaseText.FontSize);
            writer.AddField(JsonDefaults.BaseText.FontName, Font, JsonDefaults.BaseText.FontValue);
            writer.AddField(JsonDefaults.BaseText.AlignName, Align);
            base.WriteComponent(writer);
        }

        protected override void EnterPool()
        {
            base.EnterPool();
            FontSize = JsonDefaults.BaseText.FontSize;
            Font = null;
            Align = TextAnchor.UpperLeft;
            Text = null;
        }
    }
}