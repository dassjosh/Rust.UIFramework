using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components
{
    public abstract class BaseColorComponent : BaseComponent
    {
        public UiColor Color;

        public override void WriteComponent(JsonFrameworkWriter writer)
        {
            writer.AddField(JsonDefaults.Color.ColorName, Color);
        }
    }
}