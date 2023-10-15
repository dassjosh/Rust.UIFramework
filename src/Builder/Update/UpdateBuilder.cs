using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Builder.Update
{
    public class UpdateBuilder : BaseUiBuilder
    {
        public static UpdateBuilder Create() => UiFrameworkPool.Get<UpdateBuilder>();
        
        protected override void WriteComponentsInternal(JsonFrameworkWriter writer)
        {
            int count = Components.Count;
            for (int index = 0; index < count; index++)
            {
                Components[index].WriteUpdateComponent(writer);
            }

            count = Anchors.Count;
            if (count != 0)
            {
                for (int index = 0; index < count; index++)
                {
                    Anchors[index].WriteUpdateComponent(writer);
                }
            }
        }
        
        #region Add Components
        public override void AddComponent(BaseUiComponent component, UiReference parent)
        {
            UiReferenceException.ThrowIfInvalidReference(parent);
            component.Reference = parent;
            Components.Add(component);
        }
        
        protected override void AddAnchor(BaseUiComponent component, UiReference parent)
        {
            UiReferenceException.ThrowIfInvalidReference(parent);
            component.Reference = parent;
            Anchors.Add(component);
        }
        #endregion
    }
}