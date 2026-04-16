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
    
    public partial bool IsPassword { get; set; }
    [PropertyName(nameof(InputComponent.NeedsKeyboard))]
    public partial bool InputNeedsKeyboard { get; set; }
    public partial bool HudNeedsKeyboard { get; set; }
    public partial bool AutoFocus { get; set; }
    public partial bool ReadOnly { get; set; }
    
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

    public bool HasMode(InputMode mode) => Input.HasMode(mode);
    
    public UiInput SetMode(InputMode mode, bool enabled)
    {
        Input.SetMode(mode, enabled);
        return this;
    }
    
    public UiInput SetCommand(ICommandBuilder<InputArg> command) => SetCommand(command.Build(InputArg.Empty));
    
    public UiInput SetPlaceholder<T>(T placeholder) where T : BaseUiComponent 
    {
        NonGraphicalElementException.ThrowIfNonGraphicalElement(placeholder);
        Placeholder = placeholder;
        return this;
    }
}