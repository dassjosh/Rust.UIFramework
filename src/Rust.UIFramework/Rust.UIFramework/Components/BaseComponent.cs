using UI.Framework.Rust.Colors;
using Pool = Facepunch.Pool;

namespace UI.Framework.Rust.Components
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