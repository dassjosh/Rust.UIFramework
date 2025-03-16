using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Builder.Update;

public class UpdateBuilder : BaseUiBuilder
{
    public static UpdateBuilder Create() => UiFrameworkPool.Get<UpdateBuilder>();
        
    protected override void WriteComponentsInternal(JsonFrameworkWriter writer)
    {
        WriteComponents(writer, Components, 0);
        WriteComponents(writer, Anchors, 0);
    }
        
    #region Add Components
    public override void AddComponent(BaseUiComponent component, in UiReference parent)
    {
        UiReferenceException.ThrowIfInvalidReference(parent);
        component.Reference = parent;
        Components.Add(component);
    }
        
    protected override void AddAnchor(BaseUiComponent component, in UiReference parent)
    {
        UiReferenceException.ThrowIfInvalidReference(parent);
        component.Reference = parent;
        Anchors.Add(component);
    }
    #endregion
}