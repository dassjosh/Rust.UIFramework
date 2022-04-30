using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components
{
    public abstract class FadeInComponent : BaseColorComponent
    {
        public float FadeIn;

        public override void WriteComponent(JsonTextWriter writer)
        {
            JsonCreator.AddField(writer, JsonDefaults.Common.FadeInName, FadeIn, JsonDefaults.Common.FadeIn);
            base.WriteComponent(writer);
        }

        protected override void EnterPool()
        {
            base.EnterPool();
            FadeIn = 0;
        }
    }
}