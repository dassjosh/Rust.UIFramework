using System;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Builder.Update;

public class UpdateBuilder : BaseUiBuilder
{
    public UpdateMode UpdateMode = UpdateMode.AutoDestroy;
    
    [Obsolete]
    public static UpdateBuilder Create() => UiFrameworkPool.Get<UpdateBuilder>();
    public static UpdateBuilder Create(IUiFrameworkPlugin plugin) => plugin.Pool.Get<UpdateBuilder>();

    public UpdateBuilder SetUpdateMode(UpdateMode mode)
    {
        UpdateMode = mode;
        return this;
    }
    
    #region Add Components
    public override void AddComponent(BaseUiComponent component, in UiReference parent)
    {
        UiReferenceException.ThrowIfInvalidReference(parent);
        component.Reference = parent;
        component.Update = UpdateMode;
        Components.Add(component);
    }
        
    protected override void AddAnchor(BaseUiComponent component, in UiReference parent)
    {
        UiReferenceException.ThrowIfInvalidReference(parent);
        component.Reference = parent;
        component.Update = UpdateMode;
        Anchors.Add(component);
    }
    #endregion

    protected override void EnterPool()
    {
        base.EnterPool();
        UpdateMode = UpdateMode.AutoDestroy;
    }
}