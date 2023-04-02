using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components
{
    public abstract class BaseColorComponent : IComponent
    {
        public UiColor Color;

        public virtual void WriteComponent(JsonFrameworkWriter writer)
        {
            writer.AddField(JsonDefaults.Color.ColorName, Color);
        }

        public virtual void Reset()
        {
            Color = default(UiColor);
        }
    }
}