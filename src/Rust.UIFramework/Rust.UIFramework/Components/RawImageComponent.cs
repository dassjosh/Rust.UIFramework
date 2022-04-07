namespace UI.Framework.Rust.Components
{
    public class RawImageComponent : FadeInComponent
    {
        public const string Type = "UnityEngine.UI.RawImage";

        public string Sprite;
        public string Url;

        public override void EnterPool()
        {
            base.EnterPool();
            Sprite = null;
            Url = null;
        }
    }
}