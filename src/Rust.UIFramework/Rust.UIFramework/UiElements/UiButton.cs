using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.UiElements
{
    public class UiButton : BaseUiComponent
    {
        public ButtonComponent Button;

        public static UiButton CreateCommand(UiPosition pos, UiOffset offset, UiColor color, string command)
        {
            UiButton button = CreateBase<UiButton>(pos, offset);
            button.Button.Color = color;
            button.Button.Command = command;
            return button;
        }

        public static UiButton CreateClose(UiPosition pos, UiOffset offset, UiColor color, string close)
        {
            UiButton button = CreateBase<UiButton>(pos, offset);
            button.Button.Color = color;
            button.Button.Close = close;
            return button;
        }
        
        public void SetFadeIn(float duration)
        {
            Button.FadeIn = duration;
        }

        public void SetImageType(Image.Type type)
        {
            Button.ImageType = type;
        }

        public void SetSprite(string sprite)
        {
            Button.Sprite = sprite;
        }
        
        public void SetMaterial(string material)
        {
            Button.Material = material;
        }
        
        public void SetSpriteMaterialImage(string sprite = null, string material = null, Image.Type type = Image.Type.Simple)
        {
            Button.Sprite = sprite;
            Button.Material = material;
            Button.ImageType = type;
        }

        protected override void WriteComponents(JsonFrameworkWriter writer)
        {
            Button.WriteComponent(writer);
            base.WriteComponents(writer);
        }

        protected override void EnterPool()
        {
            base.EnterPool();
            UiFrameworkPool.Free(ref Button);
        }

        protected override void LeavePool()
        {
            base.LeavePool();
            Button = UiFrameworkPool.Get<ButtonComponent>();
        }
        
        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
        }
    }
}