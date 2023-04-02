using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Json;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.UiElements
{
    public abstract class BaseUiImage : BaseUiOutline
    {
        public readonly ImageComponent Image = new ImageComponent();
        
        public void SetImageType(Image.Type type)
        {
            Image.ImageType = type;
        }
        
        public void SetSprite(string sprite)
        {
            Image.Sprite = sprite;
        }
        
        public void SetMaterial(string material)
        {
            Image.Material = material;
        }
        
        public void SetSpriteMaterialImage(string sprite = null, string material = null, Image.Type type = UnityEngine.UI.Image.Type.Simple)
        {
            Image.Sprite = sprite;
            Image.Material = material;
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
            Image.Reset();
        }
    }
}