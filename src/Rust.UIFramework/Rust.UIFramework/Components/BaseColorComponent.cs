using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components
{
    public class BaseColorComponent : BaseComponent
    {
        public UiColor Color;

        public override void WriteComponent(JsonTextWriter writer)
        {
            JsonCreator.AddField(writer, JsonDefaults.ColorName, Color, JsonDefaults.ColorValue);
        }
        
        public override void EnterPool()
        {
            Color = null;
        }
    }
}