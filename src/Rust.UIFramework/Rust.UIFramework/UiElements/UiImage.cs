using Newtonsoft.Json;
using UI.Framework.Rust.Colors;
using UI.Framework.Rust.Components;
using UI.Framework.Rust.Json;
using UI.Framework.Rust.Positions;
using Pool = Facepunch.Pool;

namespace UI.Framework.Rust.UiElements
{
    public class UiImage : BaseUiComponent
    {
        public RawImageComponent RawImage;

        public static UiImage Create(string png, UiPosition pos, UiColor color)
        {
            UiImage image = CreateBase<UiImage>(pos);
            image.RawImage.Color = color;
            if (png.StartsWith("http"))
            {
                image.RawImage.Url = png;
            }
            else
            {
                image.RawImage.Png = png;
            }

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