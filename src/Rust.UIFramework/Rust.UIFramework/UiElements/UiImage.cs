using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.UiElements
{
    public class UiImage : BaseUiComponent
    {
        public ImageComponent Image;
        
        public static UiImage Create(UiPosition pos, UiOffset? offset, UiColor color, string png)
        {
            UiImage image = CreateBase<UiImage>(pos, offset);
            image.Image.Color = color;
            image.Image.Png = png;
            return image;
        }

        public void SetImageType(Image.Type type)
        {
            Image.ImageType = type;
        }
        
        public void SetFadeIn(float duration)
        {
            Image.FadeIn = duration;
        }
        
        protected override void WriteComponents(JsonFrameworkWriter writer)
        {
            Image.WriteComponent(writer);
            base.WriteComponents(writer);
        }

        protected override void EnterPool()
        {
            base.EnterPool();
            UiFrameworkPool.Free(ref Image);
        }

        protected override void LeavePool()
        {
            base.LeavePool();
            Image = UiFrameworkPool.Get<ImageComponent>();
        }
        
        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
        }
    }
}