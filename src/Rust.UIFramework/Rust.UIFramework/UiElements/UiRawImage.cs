using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.UiElements
{
    public class UiRawImage : BaseUiComponent
    {
        public RawImageComponent RawImage;

        public static UiRawImage CreateUrl(UiPosition pos, UiOffset offset, UiColor color, string url)
        {
            UiRawImage image = CreateBase<UiRawImage>(pos, offset);
            image.RawImage.Color = color;
            image.RawImage.Url = url;
            return image;
        }
        
        public static UiRawImage CreateTexture(UiPosition pos, UiOffset offset, UiColor color, string icon)
        {
            UiRawImage image = CreateBase<UiRawImage>(pos, offset);
            image.RawImage.Color = color;
            image.RawImage.Texture = icon;
            return image;
        }

        public void SetMaterial(string material)
        {
            RawImage.Material = material;
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
        
        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
        }
    }
}