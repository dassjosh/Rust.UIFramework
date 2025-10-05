using System;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

public class InputComponent : TextComponent
{
    private readonly TrackedValue<int> _charsLimit = new(JsonDefaults.Input.CharacterLimit);
    private readonly TrackedValue<string> _command = new();
    private readonly TrackedValue<InputMode> _mode = new(JsonDefaults.Input.Mode);
    private readonly TrackedValue<InputField.LineType> _lineType = new(JsonDefaults.Input.LineType);
    private readonly TrackedValue<UiReference> _placeholder = new();
    
    public int CharsLimit { get => _charsLimit.Value; set => _charsLimit.Value = value; } 
    public string Command { get => _command.Value; set => _command.Value = value; }
    public InputMode Mode { get => _mode.Value; set => _mode.Value = value; }
    public InputField.LineType LineType { get => _lineType.Value; set => _lineType.Value = value; }
    public UiReference Placeholder { get => _placeholder.Value; set => _placeholder.Value = value; }

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
        else
        {
            writer.AddField(JsonDefaults.Input.PasswordName, HasMode(InputMode.Password), _mode.ShouldSerialize(mode));
            writer.AddField(JsonDefaults.Input.NeedsKeyboardName, HasMode(InputMode.NeedsKeyboard), _mode.ShouldSerialize(mode));
            writer.AddField(JsonDefaults.Input.NeedsHudKeyboardName, HasMode(InputMode.HudNeedsKeyboard), _mode.ShouldSerialize(mode));
            writer.AddField(JsonDefaults.Input.AutoFocusName, HasMode(InputMode.AutoFocus), _mode.ShouldSerialize(mode));
            writer.AddField(JsonDefaults.Input.ReadOnlyName, HasMode(InputMode.ReadOnly), _mode.ShouldSerialize(mode));
        }

        writer.AddCommand(JsonDefaults.Common.CommandName, _command, mode);
        if (_placeholder.ShouldSerialize(mode) && Placeholder.IsValidName())
        {
            writer.AddFieldRaw(JsonDefaults.Input.PlaceholderName, Placeholder.Name);
        }
    }

    public bool HasMode(InputMode mode) => (Mode & mode) == mode;

    [Obsolete("Use SetMode on UiInput instead")]
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

    public override void Reset()
    {
        base.Reset();
        _charsLimit.Reset();
        _command.Reset();
        _mode.Reset();
        _lineType.Reset();
        _placeholder.Reset();
    }
}