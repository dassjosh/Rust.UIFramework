using Oxide.Ext.UiFramework.Controls;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;

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
                    Anchors[index].WriteComponent(writer);
                }
            }
        }
    }
}