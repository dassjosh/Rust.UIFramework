namespace UI.Framework.Rust.Components
{
    public class ImageComponent : BaseImageComponent
    {
        public string Png;

        public override void EnterPool()
        {
            base.EnterPool();
            Png = null;
        }
    }
}