using System;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Components;

[GenerateComponent(typeof(IInputComponent))]
public partial class InputComponent : TextComponent, IInputComponent
{
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
}