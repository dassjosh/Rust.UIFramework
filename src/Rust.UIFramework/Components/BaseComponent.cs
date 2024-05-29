using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Components;

public abstract class BaseComponent : IComponent
{
    public bool Enabled = true;
        
    public virtual void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.AddField(JsonDefaults.Common.EnabledName, Enabled, true);
    }

    public virtual void Reset()
    {
        Enabled = true;
    }
}