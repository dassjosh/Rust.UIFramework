using Newtonsoft.Json;
using UI.Framework.Rust.Colors;
using UI.Framework.Rust.Components;
using UI.Framework.Rust.Json;
using UI.Framework.Rust.Positions;
using Pool = Facepunch.Pool;

namespace UI.Framework.Rust.UiElements
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

        public override void WriteComponents(JsonTextWriter writer)
        {
            JsonCreator.Add(writer, RawImage);
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