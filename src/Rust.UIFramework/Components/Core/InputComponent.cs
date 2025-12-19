using System;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent]
[GenerateBuilderMethods]
public partial class InputComponent : TextComponent
{
    [TrackedDefaults(typeof(JsonDefaults.Input), nameof(JsonDefaults.Input.CharacterLimit))]
    public partial int CharsLimit { get; set; }
    public partial string Command { get; set; }
    [TrackedDefaults(typeof(JsonDefaults.Input), nameof(JsonDefaults.Input.Mode))]
    public partial InputMode Mode { get; set; }
    [TrackedDefaults(typeof(JsonDefaults.Input), nameof(JsonDefaults.Input.LineType))]
    public partial InputField.LineType LineType { get; set; }
    public partial UiReference Placeholder { get; set; }
    
    [GenerateBuilderMethod]
    public bool IsPassword { get => HasMode(InputMode.Password); set => SetMode(InputMode.Password, value); }
    [GenerateBuilderMethod]
    public bool NeedsKeyboard { get => HasMode(InputMode.NeedsKeyboard); set => SetMode(InputMode.NeedsKeyboard, value); }
    [GenerateBuilderMethod]
    public bool HudNeedsKeyboard { get => HasMode(InputMode.HudNeedsKeyboard); set => SetMode(InputMode.HudNeedsKeyboard, value); }
    [GenerateBuilderMethod]
    public bool AutoFocus { get => HasMode(InputMode.AutoFocus); set => SetMode(InputMode.AutoFocus, value); }
    [GenerateBuilderMethod]
    public bool ReadOnly { get => HasMode(InputMode.ReadOnly); set => SetMode(InputMode.ReadOnly, value); }
    
    public override Utf8String Type => JsonDefaults.Input.Type;
    public override ComponentType ComponentType => ComponentType.Input;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        base.WriteComponentFields(writer, mode);
        writer.AddField(JsonDefaults.Input.CharacterLimitName, CharsLimitTracked, mode);
        writer.AddField(JsonDefaults.Input.LineTypeName, LineTypeTracked, mode);
        if (mode == SerializeMode.Create)
        {
            writer.AddField(JsonDefaults.Input.PasswordName, IsPassword, false);
            writer.AddField(JsonDefaults.Input.NeedsKeyboardName, NeedsKeyboard, false);
            writer.AddField(JsonDefaults.Input.NeedsHudKeyboardName, HudNeedsKeyboard, false);
            writer.AddField(JsonDefaults.Input.AutoFocusName, AutoFocus, false);
            writer.AddField(JsonDefaults.Input.ReadOnlyName, ReadOnly, false);
        }
        else if(ModeTracked.ShouldSerialize(mode))
        {
            writer.AddField(JsonDefaults.Input.PasswordName, IsPassword, ModeTracked.ShouldSerialize(mode));
            writer.AddField(JsonDefaults.Input.NeedsKeyboardName, NeedsKeyboard, ModeTracked.ShouldSerialize(mode));
            writer.AddField(JsonDefaults.Input.NeedsHudKeyboardName, HudNeedsKeyboard, ModeTracked.ShouldSerialize(mode));
            writer.AddField(JsonDefaults.Input.AutoFocusName, AutoFocus, ModeTracked.ShouldSerialize(mode));
            writer.AddField(JsonDefaults.Input.ReadOnlyName, ReadOnly, ModeTracked.ShouldSerialize(mode));
        }

        writer.AddCommandField(JsonDefaults.Common.CommandName, CommandTracked, mode);
        if (PlaceholderTracked.ShouldSerialize(mode) && Placeholder.IsValidName())
        {
            writer.AddField(JsonDefaults.Input.PlaceholderName, Placeholder.Name);
        }
    }

    public bool HasMode(InputMode mode) => (Mode & mode) == mode;
    
    public void SetMode(InputMode mode, bool enabled)
    {
        if (enabled)
        {
            Mode |= mode;
        }
        else
        {
            Mode &= ~mode;
        }
    }
}