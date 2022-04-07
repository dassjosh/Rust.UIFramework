namespace Oxide.Ext.UiFramework.Components
{
    public class FadeInComponent : BaseComponent
    {
        public float FadeIn;

        public override void EnterPool()
        {
            base.EnterPool();
            FadeIn = 0;
        }
    }
}