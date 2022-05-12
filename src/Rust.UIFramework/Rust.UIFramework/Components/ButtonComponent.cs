using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Components
{
    public class ButtonComponent : BaseImageComponent
    {
        private const string Type = "UnityEngine.UI.Button";

        public string Command;
        public string Close;

        public override void WriteComponent(JsonFrameworkWriter writer)
        {
            writer.WriteStartObject();
            writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
            writer.AddField(JsonDefaults.Common.CommandName, Command, JsonDefaults.Common.NullValue);
            writer.AddField(JsonDefaults.Button.CloseName, Close, JsonDefaults.Common.NullValue);
            base.WriteComponent(writer);
            writer.WriteEndObject();
        }

        protected override void EnterPool()
        {
            base.EnterPool();
            Command = null;
            Close = null;
        }
        
        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
        }
    }
}