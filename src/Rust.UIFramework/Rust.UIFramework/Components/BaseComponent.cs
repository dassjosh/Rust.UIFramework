using Facepunch;
using Newtonsoft.Json;

namespace Oxide.Ext.UiFramework.Components
{
    public abstract class BaseComponent : Pool.IPooled
    {
        public virtual void EnterPool() { }

        public virtual void LeavePool() { }

        public abstract void WriteComponent(JsonTextWriter writer);
    }
}