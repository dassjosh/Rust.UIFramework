using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiInput : BaseUiText<UiInput>
{
    public readonly InputComponent Input = new();
    internal override CoreComponent Component => Input;

    public static UiInput Create(string text, int size, UiColor textColor, string cmd, string font, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
    {
        UiInput input = CreateBase<UiInput>();
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
    
    public UiInput SetCharsLimit(int limit)
    {
        Input.CharsLimit = limit;
        return this;
    }

    public UiInput SetIsPassword(bool isPassword)
    {
        Input.SetMode(InputMode.Password, isPassword);
        return this;
    }

    public UiInput SetIsReadonly(bool isReadonly)
    {
        Input.SetMode(InputMode.ReadOnly, isReadonly);
        return this;
    }
        
    public UiInput SetAutoFocus(bool autoFocus)
    {
        Input.SetMode(InputMode.AutoFocus, autoFocus);
        return this;
    }
        
    /// <summary>
    /// Sets if the input should block keyboard input when focused.
    /// This should not be used when the loot panel / crafting UI is open. Use SetNeedsHudKeyboard instead
    /// </summary>
    /// <param name="needsKeyboard"></param>
    public UiInput SetNeedsKeyboard(bool needsKeyboard)
    {
        Input.SetMode(InputMode.NeedsKeyboard, needsKeyboard);
        return this;
    }
        
    /// <summary>
    /// Sets if the input should block keyboard input when focused a loot panel / crafting ui is open.
    /// This should not be used if a loot panel / crafting ui won't be open when displaying the UI.
    /// </summary>
    /// <param name="needsKeyboard"></param>
    public UiInput SetNeedsHudKeyboard(bool needsKeyboard)
    {
        Input.SetMode(InputMode.HudNeedsKeyboard, needsKeyboard);
        return this;
    }

    public UiInput SetLineType(InputField.LineType lineType)
    {
        Input.LineType = lineType;
        return this;
    }
    
    public UiInput SetCommand(string command)
    {
        Input.Command = command;
        return this;
    }
    
    public UiInput SetInputMode(InputMode mode)
    {
        Input.Mode = mode;
        return this;
    }
}