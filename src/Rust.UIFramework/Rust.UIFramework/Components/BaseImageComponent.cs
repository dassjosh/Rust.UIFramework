namespace Oxide.Ext.UiFramework.Components
{
    public class BaseImageComponent : FadeInComponent
    {
        public const string Type = "UnityEngine.UI.Image";

        public string Sprite;
        public string Material;

        public override void EnterPool()
        {
            base.EnterPool();
            Sprite = null;
            Material = null;
        }
    }
}