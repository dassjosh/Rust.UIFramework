using UnityEngine;

namespace UI.Framework.Rust.Components
{
    public class BaseTextComponent : FadeInComponent
    {
        public int FontSize = 14;
        public string Font;
        public TextAnchor Align;

        public override void EnterPool()
        {
            base.EnterPool();
            FontSize = 14;
            Font = null;
            Align = TextAnchor.UpperLeft;
        }
    }
}