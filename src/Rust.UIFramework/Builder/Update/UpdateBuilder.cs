using System;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;

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

    protected override void LeavePool()
    {
        base.LeavePool();
        UpdateMode = UpdateMode.AutoDestroy;
        NamingMode = NamingMode.Reference;
    }
}