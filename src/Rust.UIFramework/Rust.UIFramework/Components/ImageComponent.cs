namespace UI.Framework.Rust.Components
{
    public class ImageComponent : FadeInComponent
    {
        public static string Type = "UnityEngine.UI.Image";

        public string Sprite;
        public string Material;
        public string Png;

        public override void EnterPool()
        {
            base.EnterPool();
            Sprite = null;
            Material = null;
            Png = null;
        }
    }
}