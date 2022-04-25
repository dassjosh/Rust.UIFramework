using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Positions;
using Pool = Facepunch.Pool;

namespace Oxide.Ext.UiFramework.UiElements
{
    public class UiWebImage : BaseUiComponent
    {
        public RawImageComponent RawImage;

        public static UiWebImage Create(string png, UiPosition pos, UiColor color)
        {
            UiWebImage image = CreateBase<UiWebImage>(pos);
            image.RawImage.Color = color;
            image.RawImage.Url = png;

            return image;
        }

        protected override void WriteComponents(JsonTextWriter writer)
        {
            RawImage.WriteComponent(writer);
            base.WriteComponents(writer);
        }

        public override void EnterPool()
        {
            base.EnterPool();
            Pool.Free(ref RawImage);
        }

        public override void LeavePool()
        {
            base.LeavePool();
            RawImage = Pool.Get<RawImageComponent>();
        }

        public override void SetFadeIn(float duration)
        {
            RawImage.FadeIn = duration;
        }
    }
}