using Newtonsoft.Json;
using UI.Framework.Rust.Colors;
using UI.Framework.Rust.Components;
using UI.Framework.Rust.Json;
using UI.Framework.Rust.Positions;
using UnityEngine;
using Pool = Facepunch.Pool;

namespace UI.Framework.Rust.UiElements
{
    public class UiInput : BaseUiComponent
    {
        public InputComponent Input;

        public static UiInput Create(int size, UiColor textColor, UiPosition pos, string cmd, string font, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, bool isPassword = false)
        {
            UiInput input = CreateBase<UiInput>(pos);
            InputComponent inputComp = input.Input;
            inputComp.FontSize = size;
            inputComp.Color = textColor;
            inputComp.Align = align;
            inputComp.Font = font;
            inputComp.Command = cmd;
            inputComp.CharsLimit = charsLimit;
            inputComp.IsPassword = isPassword;
            return input;
        }

        public override void WriteComponents(JsonTextWriter writer)
        {
            JsonCreator.Add(writer, Input);
            base.WriteComponents(writer);
        }
            
        public override void EnterPool()
        {
            base.EnterPool();
            Input.Align = TextAnchor.UpperLeft;
            Pool.Free(ref Input);
        }
            
        public override void LeavePool()
        {
            base.LeavePool();
            Input = Pool.Get<InputComponent>();
        }

        public override void SetFadeIn(float duration)
        {
            Input.FadeIn = duration;
        }
    }
}