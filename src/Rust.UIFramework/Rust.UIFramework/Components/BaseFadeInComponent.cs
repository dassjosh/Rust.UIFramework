using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components
{
    public abstract class BaseFadeInComponent : BaseColorComponent
    {
        public float FadeIn;

        public override void WriteComponent(JsonFrameworkWriter writer)
        {
            writer.AddField(JsonDefaults.Common.FadeInName, FadeIn, JsonDefaults.Common.FadeIn);
            base.WriteComponent(writer);
        }

        protected override void EnterPool()
        {
            FadeIn = 0;
        }
    }
}