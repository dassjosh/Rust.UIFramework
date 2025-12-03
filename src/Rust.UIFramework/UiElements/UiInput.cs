using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Exceptions.UiElements;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Libraries;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.UiElements;

[GenerateUiElement]
[GenerateBuilderMethods]
public partial class UiInput : BaseUiComponent, IFadeIn<UiInput>, IUiColor<UiInput>
{
    public partial int FontSize { get; set; }
    public partial string Font { get; set; }
    public partial TextAnchor Align { get; set; }
    
    [PropertyName(nameof(TextComponent.Text))]
    public partial string TextValue { get; set; }
    
    public partial VerticalWrapMode VerticalOverflow { get; set; }
    public partial int CharsLimit { get; set; }
    public partial string Command { get; set; }
    public partial InputMode Mode { get; set; }
    public partial InputField.LineType LineType { get; set; }
    public partial UiReference Placeholder { get; set; }
    public partial float FadeIn { get; set; }
    public partial UiColor Color { get; set; }
    
    public readonly InputComponent Input;
    
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
    
    public UiInput SetCommand(ICommandBuilder<InputArg> command) => SetCommand(command.Build(InputArg.Empty));
    
    public UiInput SetInputMode(InputMode mode)
    {
        Mode = mode;
        return this;
    }
    
    public UiInput SetPlaceholder<T>(T placeholder) where T : BaseUiComponent 
    {
        NonGraphicalElementException.ThrowIfNonGraphicalElement(placeholder);
        Placeholder = placeholder;
        return this;
    }
}