using System;
using Network;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Builder.Update;

[Obsolete("UpdateBuilder is obsolete and will be removed in a future version. Use UiBuilder.CreateUpdate() instead", false)]
public class UpdateBuilder : BaseUiBuilder
{
    [Obsolete("UpdateBuilder is obsolete and will be removed in a future version. Use UiBuilder.CreateUpdate() instead", false)]
    public static UpdateBuilder Create() => UiFrameworkPool.Get<UpdateBuilder>();
    
    [Obsolete("UpdateBuilder is obsolete and will be removed in a future version. Use UiBuilder.CreateUpdate() instead", false)]
    public static UpdateBuilder Create(IUiFrameworkPlugin plugin) => plugin.PluginPool.Get<UpdateBuilder>();

    public UpdateBuilder SetUpdateMode(UpdateMode mode)
    {
        UpdateMode = mode;
        return this;
    }

    [Obsolete("Use Component<T>(in UiReference reference) Instead")]
    public override void AddComponent(BaseUiComponent component, in UiReference parent)
    {
        UiReferenceException.ThrowIfInvalidReference(parent);
        component.Reference = parent;
        Components.Add(component);
    }
    
    internal override void SendAnimations(SendInfo send) { }

    public override void Combine(SendInfo send, JsonFrameworkWriter writer)
    {
        throw new NotSupportedException("Combine is not supported for UpdateBuilder");
    }
    
    protected override void LeavePool()
    {
        base.LeavePool();
        UpdateMode = UpdateMode.Replace;
        NamingMode = NamingMode.Reference;
    }
}