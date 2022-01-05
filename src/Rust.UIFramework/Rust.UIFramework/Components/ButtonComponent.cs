namespace UI.Framework.Rust.Components
{
    public class ButtonComponent : BaseComponent
    {
        public static string Type = "UnityEngine.UI.Button";

        public string Command;
        public string Close;
        public string Sprite;
        public string Material;

        public override void EnterPool()
        {
            base.EnterPool();
            Command = null;
            Close = null;
            Sprite = null;
            Material = null;
        }
    }
}