using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Json;

public class InputComponentSerializer : TextComponentSerializer<InputComponent>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void SerializeComponent(JsonFrameworkWriter writer, InputComponent component, InputComponent defaults, SerializeMode mode)
    {
        base.SerializeComponent(writer, component, defaults, mode);
        writer.AddField(JsonDefaults.Input.CharacterLimitName, component.CharsLimit, defaults.CharsLimit);
        writer.AddField(JsonDefaults.Input.LineTypeName, component.LineType, defaults.LineType);
        writer.AddField(JsonDefaults.Input.PasswordName, component.HasMode(InputMode.Password), defaults.HasMode(InputMode.Password));
        writer.AddField(JsonDefaults.Input.NeedsKeyboardName, component.HasMode(InputMode.NeedsKeyboard), defaults.HasMode(InputMode.NeedsKeyboard));
        writer.AddField(JsonDefaults.Input.NeedsHudKeyboardName, component.HasMode(InputMode.HudNeedsKeyboard), defaults.HasMode(InputMode.HudNeedsKeyboard));
        writer.AddField(JsonDefaults.Input.AutoFocusName, component.HasMode(InputMode.AutoFocus), defaults.HasMode(InputMode.AutoFocus));
        writer.AddField(JsonDefaults.Input.ReadOnlyName, component.HasMode(InputMode.ReadOnly), defaults.HasMode(InputMode.ReadOnly));
        writer.AddCommand(JsonDefaults.Common.CommandName, component.Command, defaults.Command);
        if (component.Placeholder.IsValidName())
        {
            writer.AddFieldRaw(JsonDefaults.Input.PlaceholderName, component.Placeholder.Name);
        }
    }
}