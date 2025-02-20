using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Components;

public abstract class SubComponent : BasePoolable, ISubComponent
{
    public bool Enabled = true;
    
    public abstract bool AllowMultiple { get; }
    
    public virtual void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.AddField(JsonDefaults.Common.EnabledName, Enabled, true);
    }
    
    public virtual void Reset()
    {
        Enabled = true;
    }

    protected override void EnterPool()
    {
        Reset();
    }
}