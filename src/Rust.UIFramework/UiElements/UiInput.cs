using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Exceptions.UiElements;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Libraries;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiInput : BaseUiText<UiInput>
{
    public readonly InputComponent Input;

    public int CharsLimit { get => Input.CharsLimit; set => Input.CharsLimit = value; }
    public string Command { get => Input.Command; set => Input.Command = value; }
    public InputMode Mode { get => Input.Mode; set => Input.Mode = value; }
    public InputField.LineType LineType { get => Input.LineType; set => Input.LineType = value; }
    public UiReference Placeholder { get => Input.Placeholder; set => Input.Placeholder = value; }
    
    public UiInput() : this(new InputComponent()) { }

    private UiInput(InputComponent component) : base(component)
    {
        Input = component;
    }
    
    public UiInput Init(string text, int size, UiColor textColor, string cmd, string font, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
    {
        TextValue = text;
        FontSize = size;
        Color = textColor;
        Align = align;
        Font = font;
        Command = cmd;
        CharsLimit = charsLimit;
        Mode = mode;
        LineType = lineType;
        return this;
    }
    
    public UiInput SetCharsLimit(int limit)
    {
        CharsLimit = limit;
        return this;
    }

    public UiInput SetIsPassword(bool isPassword) => SetMode(InputMode.Password, isPassword);

    public UiInput SetIsReadonly(bool isReadonly) => SetMode(InputMode.ReadOnly, isReadonly);

    public UiInput SetAutoFocus(bool autoFocus) => SetMode(InputMode.AutoFocus, autoFocus);

    /// <summary>
    /// Sets if the input should block keyboard input when focused.
    /// This should not be used when the loot panel / crafting UI is open. Use SetNeedsHudKeyboard instead
    /// </summary>
    /// <param name="needsKeyboard"></param>
    public UiInput SetNeedsKeyboard(bool needsKeyboard) => SetMode(InputMode.NeedsKeyboard, needsKeyboard);

    /// <summary>
    /// Sets if the input should block keyboard input when focused on a loot panel / crafting ui.
    /// This should only be used if a loot panel / crafting ui is open when displaying the UI.
    /// </summary>
    /// <param name="needsKeyboard"></param>
    public UiInput SetHudNeedsKeyboard(bool needsKeyboard) => SetMode(InputMode.HudNeedsKeyboard, needsKeyboard);

    public bool HasMode(InputMode mode) => Input.HasMode(mode);
    
    public UiInput SetMode(InputMode mode, bool enabled)
    {
        if (enabled)
        {
            Mode |= mode;
        }
        else
        {
            Mode &= ~mode;
        }

        return this;
    }

    public UiInput SetLineType(InputField.LineType lineType)
    {
        LineType = lineType;
        return this;
    }
    
    public UiInput SetCommand(string command)
    {
        Command = command;
        return this;
    }
    
    public UiInput SetCommand(ICommandBuilder<InputArg> command) => SetCommand(command.Build(InputArg.Empty));
    
    public UiInput SetInputMode(InputMode mode)
    {
        Mode = mode;
        return this;
    }

    public UiInput SetPlaceholder(in UiReference placeholder)
    {
        Placeholder = placeholder;
        return this;
    }
    
    public UiInput SetPlaceholder<T>(T placeholder) where T : BaseUiComponent 
    {
        NonGraphicalElementException.ThrowIfNonGraphicalElement(placeholder);
        Placeholder = placeholder;
        return this;
    }
}