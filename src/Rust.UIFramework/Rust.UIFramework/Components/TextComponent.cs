namespace UI.Framework.Rust.Components
{
    public class TextComponent : BaseTextComponent
    {
        public static string Type = "UnityEngine.UI.Text";

        public string Text;

        public override void EnterPool()
        {
            base.EnterPool();
            Text = null;
        }
    }
}