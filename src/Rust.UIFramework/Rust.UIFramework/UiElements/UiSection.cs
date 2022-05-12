using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.UiElements
{
    public class UiSection : BaseUiComponent
    {
        public static UiSection Create(UiPosition pos, UiOffset? offset)
        {
            UiSection panel = CreateBase<UiSection>(pos, offset);
            return panel;
        }
        
        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
        }
    }
}