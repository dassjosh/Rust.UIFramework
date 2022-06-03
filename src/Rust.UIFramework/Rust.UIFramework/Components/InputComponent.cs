using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;
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
        public bool AutoFocus;
        public InputField.LineType LineType;

        public override void WriteComponent(JsonFrameworkWriter writer)
        {
            writer.WriteStartObject();
            writer.AddFieldRaw(JsonDefaults.Common.ComponentTypeName, Type);
            writer.AddField(JsonDefaults.Input.CharacterLimitName, CharsLimit, JsonDefaults.Input.CharacterLimitValue);
            writer.AddField(JsonDefaults.Common.CommandName, Command, JsonDefaults.Common.NullValue);
            writer.AddField(JsonDefaults.Input.LineTypeName, LineType);

            if (IsPassword)
            {
                writer.AddKeyField(JsonDefaults.Input.PasswordName);
            }

            if (IsReadyOnly)
            {
                writer.AddFieldRaw(JsonDefaults.Input.ReadOnlyName, true);
            }

            if (NeedsKeyboard)
            {
                writer.AddKeyField(JsonDefaults.Input.InputNeedsKeyboardName);
            }

            if (AutoFocus)
            {
                writer.AddKeyField(JsonDefaults.Input.AutoFocusName);
            }
            
            base.WriteComponent(writer);
            writer.WriteEndObject();
        }

        protected override void EnterPool()
        {
            base.EnterPool();
            CharsLimit = JsonDefaults.Input.CharacterLimitValue;
            Command = null;
            IsPassword = false;
            IsReadyOnly = false;
            NeedsKeyboard = true;
            AutoFocus = false;
            LineType = default(InputField.LineType);
        }
        
        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
        }
    }
}