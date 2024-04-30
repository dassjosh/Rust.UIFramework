using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Components
{
    public abstract class BasePoolableComponent : BasePoolable, IComponent
    {
        public bool Enabled = true;
        
        public virtual void WriteComponent(JsonFrameworkWriter writer)
        {
            writer.AddField(JsonDefaults.Common.EnabledName, Enabled, true);
        }

        protected override void EnterPool()
        {
            Reset();
        }

        public virtual void Reset()
        {
            Enabled = true;
        }
    }
}