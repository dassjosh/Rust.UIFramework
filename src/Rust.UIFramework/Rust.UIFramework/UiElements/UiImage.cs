using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Positions;
using Pool = Facepunch.Pool;

namespace Oxide.Ext.UiFramework.UiElements
{
    public class UiImage : BaseUiComponent
    {
        public ImageComponent Image;
        
        public static UiImage Create(string png, UiPosition pos, UiColor color)
        {
            UiImage image = CreateBase<UiImage>(pos);
            image.Image.Color = color;
            image.Image.Png = png;
            return image;
        }

        protected override void WriteComponents(JsonTextWriter writer)
        {
            Image.WriteComponent(writer);
            base.WriteComponents(writer);
        }

        public override void EnterPool()
        {
            base.EnterPool();
            Pool.Free(ref Image);
        }

        public override void LeavePool()
        {
            base.LeavePool();
            Image = Pool.Get<ImageComponent>();
        }

        public override void SetFadeIn(float duration)
        {
            Image.FadeIn = duration;
        }
    }
}