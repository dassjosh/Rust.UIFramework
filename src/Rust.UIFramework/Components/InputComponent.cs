using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components
{
    public class InputComponent : BaseTextComponent
    {
        private const string Type = "UnityEngine.UI.InputField";

        public int CharsLimit;
        public string Command;
        public InputMode Mode;
        public InputField.LineType LineType;

        public override void WriteComponent(JsonFrameworkWriter writer)
        {
            writer.WriteStartObject();
            writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
            writer.AddField(JsonDefaults.Input.CharacterLimitName, CharsLimit, JsonDefaults.Input.CharacterLimitValue);
            writer.AddField(JsonDefaults.Input.LineTypeName, LineType);
            
            if (HasMode(InputMode.ReadOnly))
            {
                writer.AddField(JsonDefaults.Input.ReadOnlyName, true, false);
            }
            else
            {
                writer.AddField(JsonDefaults.Common.CommandName, Command, JsonDefaults.Common.NullValue);
            }

            if (HasMode(InputMode.Password))
            {
                writer.AddFieldRaw(JsonDefaults.Input.PasswordName, true);
            }

            if (HasMode(InputMode.NeedsKeyboard))
            {
                writer.AddFieldRaw(JsonDefaults.Input.NeedsKeyboardName, true);
            }
            
            if (HasMode(InputMode.HudNeedsKeyboard))
            {
                writer.AddFieldRaw(JsonDefaults.Input.NeedsHudKeyboardName, true);
            }

            if (HasMode(InputMode.AutoFocus))
            {
                writer.AddKeyField(JsonDefaults.Input.AutoFocusName);
            }
            
            base.WriteComponent(writer);
            writer.WriteEndObject();
        }

        public bool HasMode(InputMode mode)
        {
            return (Mode & mode) == mode;
        }

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
}