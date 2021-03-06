using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.UiElements
{
    public class UiItemIcon : BaseUiComponent
    {
        public ItemIconComponent Icon;

        public static UiItemIcon Create(UiPosition pos, UiOffset offset, UiColor color, int itemId, ulong skinId = 0)
        {
            UiItemIcon icon = CreateBase<UiItemIcon>(pos, offset);
            icon.Icon.Color = color;
            icon.Icon.ItemId = itemId;
            icon.Icon.SkinId = skinId;
            return icon;
        }
        
        public void SetFadeIn(float duration)
        {
            Icon.FadeIn = duration;
        }

        protected override void WriteComponents(JsonFrameworkWriter writer)
        {
            Icon.WriteComponent(writer);
            base.WriteComponents(writer);
        }

        protected override void EnterPool()
        {
            base.EnterPool();
            UiFrameworkPool.Free(ref Icon);
        }

        protected override void LeavePool()
        {
            base.LeavePool();
            Icon = UiFrameworkPool.Get<ItemIconComponent>();
        }
        
        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
        }
    }
}