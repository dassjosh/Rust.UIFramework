namespace Oxide.Ext.UiFramework.Components
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