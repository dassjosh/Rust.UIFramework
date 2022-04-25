using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Json;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components
{
    public class InputComponent : BaseTextComponent
    {
        private const string Type = "UnityEngine.UI.InputField";

        public int CharsLimit;
        public string Command;
        public bool IsPassword;
        public bool IsReadyOnly;
        public bool NeedsKeyboard = true;
        public InputField.LineType LineType;

        public override void WriteComponent(JsonTextWriter writer)
        {
            writer.WriteStartObject();
            JsonCreator.AddFieldRaw(writer, JsonDefaults.ComponentTypeName, InputComponent.Type);
            JsonCreator.AddField(writer, JsonDefaults.CharacterLimitName, CharsLimit, JsonDefaults.CharacterLimitValue);
            JsonCreator.AddField(writer, JsonDefaults.CommandName, Command, JsonDefaults.NullValue);
            JsonCreator.AddField(writer, JsonDefaults.LineTypeName, LineType);

            if (IsPassword)
            {
                JsonCreator.AddFieldRaw(writer, JsonDefaults.PasswordName, JsonDefaults.PasswordValue);
            }

            if (IsReadyOnly)
            {
                JsonCreator.AddFieldRaw(writer, JsonDefaults.ReadOnlyName, JsonDefaults.ReadOnlyValue);
            }

            if (NeedsKeyboard)
            {
                JsonCreator.AddFieldRaw(writer, JsonDefaults.InputNeedsKeyboardName, JsonDefaults.InputNeedsKeyboardValue);
            }
            
            base.WriteComponent(writer);
            writer.WriteEndObject();
        }

        public override void EnterPool()
        {
            base.EnterPool();
            CharsLimit = 0;
            Command = null;
            NeedsKeyboard = true;
            IsPassword = false;
            LineType = default(InputField.LineType);
        }
    }
}