using System;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

public class InputComponent : TextComponent
{
    public int CharsLimit;
    public string Command;
    public InputMode Mode;
    public InputField.LineType LineType;
    public UiReference Placeholder;

    public override Utf8String Type => JsonDefaults.Input.Type;

    protected override void WriteComponentFields(JsonFrameworkWriter writer)
    {
        writer.AddField(JsonDefaults.Input.CharacterLimitName, CharsLimit, JsonDefaults.Input.CharacterLimit);
        writer.AddField(JsonDefaults.Input.LineTypeName, LineType, JsonDefaults.Input.LineType);
        writer.AddField(JsonDefaults.Input.PasswordName, HasMode(InputMode.Password), false);
        writer.AddField(JsonDefaults.Input.NeedsKeyboardName, HasMode(InputMode.NeedsKeyboard), false);
        writer.AddField(JsonDefaults.Input.NeedsHudKeyboardName, HasMode(InputMode.HudNeedsKeyboard), false);
        writer.AddField(JsonDefaults.Input.AutoFocusName, HasMode(InputMode.AutoFocus), false);
        writer.AddField(JsonDefaults.Input.ReadOnlyName, HasMode(InputMode.ReadOnly), false);
        if (Placeholder.IsValidName())
        {
            writer.AddFieldRaw(JsonDefaults.Input.PlaceholderName, Placeholder.Name);
        }
        writer.AddCommand(JsonDefaults.Common.CommandName, Command);
        base.WriteComponentFields(writer);
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
        CharsLimit = JsonDefaults.Input.CharacterLimit;
        Command = null;
        Mode = JsonDefaults.Input.Mode;
        LineType = JsonDefaults.Input.LineType;
        Placeholder = default;
    }
}