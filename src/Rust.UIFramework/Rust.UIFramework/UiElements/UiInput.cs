using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.UiElements
{
    public class UiInput : BaseUiOutline
    {
        public InputComponent Input;

        public static UiInput Create(UiPosition pos, UiOffset offset, UiColor textColor, string text, int size, string cmd, string font, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
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
            comp.Mode = mode;
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
            Input.SetMode(InputMode.Password, isPassword);
        }

        public void SetIsReadonly(bool isReadonly)
        {
            Input.SetMode(InputMode.ReadOnly, isReadonly);
        }
        
        public void SetAutoFocus(bool autoFocus)
        {
            Input.SetMode(InputMode.AutoFocus, autoFocus);
        }
        
        /// <summary>
        /// Sets if the input should block keyboard input when focused.
        /// This should not be used when the loot panel / crafting UI is open. Use SetNeedsHudKeyboard instead
        /// </summary>
        /// <param name="needsKeyboard"></param>
        public void SetNeedsKeyboard(bool needsKeyboard)
        {
            Input.SetMode(InputMode.NeedsKeyboard, needsKeyboard);
        }
        
        /// <summary>
        /// Sets if the input should block keyboard input when focused a loot panel / crafting ui is open.
        /// This should not if a loot panel / crafting ui won't be open when displaying the UI.
        /// </summary>
        /// <param name="needsKeyboard"></param>
        public void SetNeedsHudKeyboard(bool needsKeyboard)
        {
            Input.SetMode(InputMode.HudNeedsKeyboard, needsKeyboard);
        }

        public void SetLineType(InputField.LineType lineType)
        {
            Input.LineType = lineType;
        }

        protected override void WriteComponents(JsonFrameworkWriter writer)
        {
            Input.WriteComponent(writer);
            base.WriteComponents(writer);
        }
            
        protected override void EnterPool()
        {
            base.EnterPool();
            Input.Dispose();
            Outline?.Dispose();
        }
            
        protected override void LeavePool()
        {
            base.LeavePool();
            Input = UiFrameworkPool.Get<InputComponent>();
        }
    }
}