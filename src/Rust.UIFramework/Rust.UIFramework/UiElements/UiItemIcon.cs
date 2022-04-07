using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Positions;
using Pool = Facepunch.Pool;

namespace Oxide.Ext.UiFramework.UiElements
{
    public class UiItemIcon : BaseUiComponent
    {
        public ItemIconComponent Icon;
        
        public static UiItemIcon Create(int itemId, UiPosition pos, UiColor color)
        {
            return Create(itemId, 0, pos, color);
        }
        
        public static UiItemIcon Create(int itemId, ulong skinId, UiPosition pos, UiColor color)
        {
            UiItemIcon icon = CreateBase<UiItemIcon>(pos);
            icon.Icon.Color = color;
            icon.Icon.ItemId = itemId;
            icon.Icon.SkinId = skinId;
            return icon;
        }

        public override void WriteComponents(JsonTextWriter writer)
        {
            JsonCreator.Add(writer, Icon);
            base.WriteComponents(writer);
        }

        public override void EnterPool()
        {
            base.EnterPool();
            Pool.Free(ref Icon);
        }

        public override void LeavePool()
        {
            base.LeavePool();
            Icon = Pool.Get<ItemIconComponent>();
        }

        public override void SetFadeIn(float duration)
        {
            Icon.FadeIn = duration;
        }
    }
}