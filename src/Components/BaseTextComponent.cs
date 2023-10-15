using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components
{
    public abstract class BaseTextComponent : BaseComponent
    {
        public UiColor Color;
        public float FadeIn;
        public int FontSize = JsonDefaults.BaseText.FontSize;
        public string Font;
        public TextAnchor Align;
        public string Text;
        public VerticalWrapMode VerticalOverflow; 

        public override void WriteComponent(JsonFrameworkWriter writer)
        {
            writer.AddTextField(JsonDefaults.BaseText.TextName, Text);
            writer.AddField(JsonDefaults.BaseText.FontSizeName, FontSize, JsonDefaults.BaseText.FontSize);
            writer.AddField(JsonDefaults.BaseText.FontName, Font, JsonDefaults.BaseText.FontValue);
            writer.AddField(JsonDefaults.BaseText.AlignName, Align);
            writer.AddField(JsonDefaults.BaseText.VerticalOverflowName, VerticalOverflow);
            writer.AddField(JsonDefaults.Common.FadeInName, FadeIn, JsonDefaults.Common.FadeIn);
            writer.AddField(JsonDefaults.Color.ColorName, Color);
            base.WriteComponent(writer);
        }

        public override void Reset()
        {
            base.Reset();
            Color = default(UiColor);
            FadeIn = 0;
            FontSize = JsonDefaults.BaseText.FontSize;
            Font = null;
            Align = TextAnchor.UpperLeft;
            Text = null;
            VerticalOverflow = VerticalWrapMode.Truncate;
        }
    }
}