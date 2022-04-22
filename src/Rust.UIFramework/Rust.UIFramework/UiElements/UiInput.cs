using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Positions;
using UnityEngine;
using UnityEngine.UI;
using Pool = Facepunch.Pool;

namespace Oxide.Ext.UiFramework.UiElements
{
    public class UiInput : BaseUiComponent
    {
        public InputComponent Input;
        public OutlineComponent Outline;

        public static UiInput Create(string text, int size, UiColor textColor, UiPosition pos, string cmd, string font, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, bool isPassword = false, bool readOnly = false, InputField.LineType lineType = InputField.LineType.SingleLine)
        {
            UiInput input = CreateBase<UiInput>(pos);
            InputComponent comp = input.Input;
            comp.Text = text;
            comp.FontSize = size;
            comp.Color = textColor;
            comp.Align = align;
            comp.Font = font;
            comp.Command = cmd;
            comp.CharsLimit = charsLimit;
            comp.IsPassword = isPassword;
            comp.IsReadyOnly = readOnly;
            comp.LineType = lineType;
            return input;
        }

        public void SetTextAlign(TextAnchor align)
        {
            Input.Align = align;
        }
        
        public void SetCharsLimit(int limit)
        {
            Input.CharsLimit = limit;
        }

        public void SetIsPassword(bool isPassword)
        {
            Input.IsPassword = isPassword;
        }

        public void SetIsReadonly(bool isReadonly)
        {
            Input.IsReadyOnly = isReadonly;
        }

        public void SetLineType(InputField.LineType lineType)
        {
            Input.LineType = lineType;
        }

        /// <summary>
        /// Sets if the input should block keyboard input when focused.
        /// Default value is true
        /// </summary>
        /// <param name="needsKeyboard"></param>
        public void SetRequiresKeyboard(bool needsKeyboard = true)
        {
            Input.NeedsKeyboard = needsKeyboard;
        }
        
        public void AddTextOutline(UiColor color)
        {
            Outline = Pool.Get<OutlineComponent>();
            Outline.Color = color;
        }

        public void AddTextOutline(UiColor color, string distance)
        {
            AddTextOutline(color);
            Outline.Distance = distance;
        }

        public void AddTextOutline(UiColor color, string distance, bool useGraphicAlpha)
        {
            AddTextOutline(color, distance);
            Outline.UseGraphicAlpha = useGraphicAlpha;
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