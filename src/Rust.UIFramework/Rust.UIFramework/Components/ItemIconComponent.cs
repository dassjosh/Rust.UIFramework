namespace Oxide.Ext.UiFramework.Components
{
    public class ItemIconComponent : BaseImageComponent
    {
        public int ItemId;
        public ulong SkinId;

        public override void EnterPool()
        {
            ItemId = 0;
            SkinId = 0;
        }
    }
}