using Newtonsoft.Json;
using UI.Framework.Rust.Colors;
using UI.Framework.Rust.Components;
using UI.Framework.Rust.Json;
using UI.Framework.Rust.Positions;
using Pool = Facepunch.Pool;

namespace UI.Framework.Rust.UiElements
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

        public override void WriteComponents(JsonTextWriter writer)
        {
            JsonCreator.Add(writer, Button);
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