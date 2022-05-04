using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.UiElements
{
    public class UiWebImage : BaseUiComponent
    {
        public RawImageComponent RawImage;

        public static UiWebImage Create(UiPosition pos, UiOffset? offset, UiColor color, string png)
        {
            UiWebImage image = CreateBase<UiWebImage>(pos, offset);
            image.RawImage.Color = color;
            image.RawImage.Url = png;

            return image;
        }
        
        public void SetFadeIn(float duration)
        {
            RawImage.FadeIn = duration;
        }

        protected override void WriteComponents(JsonFrameworkWriter writer)
        {
            RawImage.WriteComponent(writer);
            base.WriteComponents(writer);
        }

        protected override void EnterPool()
        {
            base.EnterPool();
            UiFrameworkPool.Free(ref RawImage);
        }

        protected override void LeavePool()
        {
            base.LeavePool();
            RawImage = UiFrameworkPool.Get<RawImageComponent>();
        }
    }
}