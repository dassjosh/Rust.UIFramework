using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.UiElements
{
    public class UiPanel : BaseUiComponent
    {
        public ImageComponent Image;

        public void AddSprite(string sprite)
        {
            Image.Sprite = sprite;
        }

        public void AddMaterial(string material)
        {
            Image.Material = material;
        }

        public static UiPanel Create(UiPosition pos, UiOffset offset, UiColor color)
        {
            UiPanel panel = CreateBase<UiPanel>(pos, offset);
            panel.Image.Color = color;
            return panel;
        }

        public static UiPanel Create(Position pos, Offset offset, UiColor color)
        {
            UiPanel panel = CreateBase<UiPanel>(pos, offset);
            panel.Image.Color = color;
            return panel;
        }

        protected override void WriteComponents(JsonTextWriter writer)
        {
            Image.WriteComponent(writer);
            base.WriteComponents(writer);
        }

        public override void EnterPool()
        {
            base.EnterPool();
            UiFrameworkPool.Free(ref Image);
        }

        public override void LeavePool()
        {
            base.LeavePool();
            Image = UiFrameworkPool.Get<ImageComponent>();
        }

        public override void SetFadeIn(float duration)
        {
            Image.FadeIn = duration;
        }
    }
}