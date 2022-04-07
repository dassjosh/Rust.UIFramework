using Oxide.Ext.UiFramework.Colors;
using Pool = Facepunch.Pool;

namespace Oxide.Ext.UiFramework.Components
{
    public class BaseComponent : Pool.IPooled
    {
        public UiColor Color;

        public virtual void EnterPool()
        {
            Color = null;
        }

        public virtual void LeavePool()
        {
        }
    }
}