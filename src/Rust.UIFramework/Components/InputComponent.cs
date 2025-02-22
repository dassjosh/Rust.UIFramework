using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components;

public class InputComponent : BaseTextComponent
{
    public int CharsLimit;
    public string Command;
    public InputMode Mode;
    public InputField.LineType LineType;

    public override void WriteComponent(JsonFrameworkWriter writer)
    {
        writer.WriteStartObject();
        writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, JsonDefaults.Input.Type);
        writer.AddField(JsonDefaults.Input.CharacterLimitName, CharsLimit, JsonDefaults.Input.CharacterLimitValue);
        writer.AddField(JsonDefaults.Input.LineTypeName, LineType);
            
        if (HasMode(InputMode.ReadOnly))
        {
            writer.AddFieldRaw(JsonDefaults.Input.ReadOnlyName, true);
        }
        else
        {
            writer.AddField(JsonDefaults.Common.CommandName, Command, JsonDefaults.Common.NullValue);
        }
        
        writer.AddField(JsonDefaults.Input.PasswordName, HasMode(InputMode.Password), false);
        writer.AddField(JsonDefaults.Input.NeedsKeyboardName, HasMode(InputMode.NeedsKeyboard), false);
        writer.AddField(JsonDefaults.Input.NeedsHudKeyboardName, HasMode(InputMode.HudNeedsKeyboard), false);
        writer.AddField(JsonDefaults.Input.AutoFocusName, HasMode(InputMode.AutoFocus), false);
            
        base.WriteComponent(writer);
        writer.WriteEndObject();
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

    public override void Reset()
    {
        base.Reset();
        CharsLimit = JsonDefaults.Input.CharacterLimitValue;
        Command = null;
        Mode = default;
        LineType = default;
    }
}