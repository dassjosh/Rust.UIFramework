using UnityEngine;

namespace Oxide.Ext.UiFramework.Components
{
    public class BaseTextComponent : FadeInComponent
    {
        public int FontSize = 14;
        public string Font;
        public TextAnchor Align;
        public string Text;

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