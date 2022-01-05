namespace UI.Framework.Rust.Components
{
    public class RawImageComponent : BaseComponent
    {
        public static string Type = "UnityEngine.UI.RawImage";

        public string Sprite;
        public string Url;
        public string Png;

        public override void EnterPool()
        {
            base.EnterPool();
            Sprite = null;
            Url = null;
            Png = null;
        }
    }
}