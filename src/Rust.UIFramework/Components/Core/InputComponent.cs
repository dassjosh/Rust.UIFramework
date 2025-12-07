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
    
    public override Utf8String Type => JsonDefaults.Input.Type;
    public override ComponentType ComponentType => ComponentType.Input;

    protected override void WriteComponentFields(JsonFrameworkWriter writer, SerializeMode mode)
    {
        base.WriteComponentFields(writer, mode);
        writer.AddField(JsonDefaults.Input.CharacterLimitName, _charsLimit, mode);
        writer.AddField(JsonDefaults.Input.LineTypeName, _lineType, mode);
        if (mode == SerializeMode.Create)
        {
            writer.AddField(JsonDefaults.Input.PasswordName, HasMode(InputMode.Password), false);
            writer.AddField(JsonDefaults.Input.NeedsKeyboardName, HasMode(InputMode.NeedsKeyboard), false);
            writer.AddField(JsonDefaults.Input.NeedsHudKeyboardName, HasMode(InputMode.HudNeedsKeyboard), false);
            writer.AddField(JsonDefaults.Input.AutoFocusName, HasMode(InputMode.AutoFocus), false);
            writer.AddField(JsonDefaults.Input.ReadOnlyName, HasMode(InputMode.ReadOnly), false);
        }
        else if(_mode.ShouldSerialize(mode))
        {
            writer.AddField(JsonDefaults.Input.PasswordName, HasMode(InputMode.Password), _mode.ShouldSerialize(mode));
            writer.AddField(JsonDefaults.Input.NeedsKeyboardName, HasMode(InputMode.NeedsKeyboard), _mode.ShouldSerialize(mode));
            writer.AddField(JsonDefaults.Input.NeedsHudKeyboardName, HasMode(InputMode.HudNeedsKeyboard), _mode.ShouldSerialize(mode));
            writer.AddField(JsonDefaults.Input.AutoFocusName, HasMode(InputMode.AutoFocus), _mode.ShouldSerialize(mode));
            writer.AddField(JsonDefaults.Input.ReadOnlyName, HasMode(InputMode.ReadOnly), _mode.ShouldSerialize(mode));
        }

        writer.AddCommandField(JsonDefaults.Common.CommandName, _command, mode);
        if (_placeholder.ShouldSerialize(mode) && Placeholder.IsValidName())
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