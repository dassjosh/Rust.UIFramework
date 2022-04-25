using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Positions;
using Pool = Facepunch.Pool;

namespace Oxide.Ext.UiFramework.UiElements
{
    public class UiButton : BaseUiComponent
    {
        public ButtonComponent Button;

        public static UiButton CreateCommand(UiPosition pos, UiColor color, string command)
        {
            UiButton button = CreateBase<UiButton>(pos);
            button.Button.Color = color;
            button.Button.Command = command;
            return button;
        }

        public static UiButton CreateClose(UiPosition pos, UiColor color, string close)
        {
            UiButton button = CreateBase<UiButton>(pos);
            button.Button.Color = color;
            button.Button.Close = close;
            return button;
        }

        protected override void WriteComponents(JsonTextWriter writer)
        {
            Button.WriteComponent(writer);
            base.WriteComponents(writer);
        }

        public override void EnterPool()
        {
            base.EnterPool();
            Pool.Free(ref Button);
        }

        public override void LeavePool()
        {
            base.LeavePool();
            Button = Pool.Get<ButtonComponent>();
        }

        public override void SetFadeIn(float duration)
        {
            Button.FadeIn = duration;
        }
    }
}