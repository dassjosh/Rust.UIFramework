using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Components
{
    public abstract class BaseComponent : BasePoolable
    {
        public abstract void WriteComponent(JsonTextWriter writer);
    }
}