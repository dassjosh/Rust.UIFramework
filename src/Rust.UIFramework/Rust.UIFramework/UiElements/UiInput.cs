using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.UiElements
{
    public class UiInput : BaseUiTextOutline
    {
        public InputComponent Input;

        public static UiInput Create(UiPosition pos, UiOffset? offset, UiColor textColor, string text, int size, string cmd, string font, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, bool isPassword = false, bool readOnly = false, InputField.LineType lineType = InputField.LineType.SingleLine)
        {
            UiInput input = CreateBase<UiInput>(pos, offset);
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

        protected override void WriteComponents(JsonFrameworkWriter writer)
        {
            Input.WriteComponent(writer);
            base.WriteComponents(writer);
        }
            
        protected override void EnterPool()
        {
            base.EnterPool();
            UiFrameworkPool.Free(ref Input);
            if (Outline != null)
            {
                UiFrameworkPool.Free(ref Outline);
            }
        }
            
        protected override void LeavePool()
        {
            base.LeavePool();
            Input = UiFrameworkPool.Get<InputComponent>();
        }
        
        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
        }
    }
}