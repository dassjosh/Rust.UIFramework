using System.Text;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Json
{
    public class JsonFrameworkWriter : BasePoolable
    {
        private const char QuoteChar = '\"';
        private const char ArrayStartChar = '[';
        private const char ArrayEndChar = ']';
        private const char ObjectStartChar = '{';
        private const char ObjectEndChar = '}';
        private const char CommaChar = ',';
        private const string Separator = "\":";
        private const string PropertyComma = ",\"";

        private bool _propertyComma;
        private bool _objectComma;

        private StringBuilder _writer;

        private void Init()
        {
            _writer = UiFrameworkPool.GetStringBuilder();
        }

        public static JsonFrameworkWriter Create()
        {
            JsonFrameworkWriter writer = UiFrameworkPool.Get<JsonFrameworkWriter>();
            writer.Init();
            return writer;
        }

        private void OnDepthIncrease()
        {
            if (_objectComma)
            {
                _writer.Append(CommaChar);
            }
            
            _propertyComma = false;
            _objectComma = false;
        }
        
        private void OnDepthDecrease()
        {
            _objectComma = true;
        }

        #region Field Handling
        public void AddFieldRaw(string name, string value)
        {
            WritePropertyName(name);
            WriteValue(value);
        }

        public void AddFieldRaw(string name, int value)
        {
            WritePropertyName(name);
            WriteValue(value);
        }

        public void AddFieldRaw(string name, bool value)
        {
            WritePropertyName(name);
            WriteValue(value);
        }

        public void AddField(string name, string value, string defaultValue)
        {
            if (value != null && value != defaultValue)
            {
                WritePropertyName(name);
                WriteValue(value);
            }
        }
        
        public void AddField(string name, Vector2 value, Vector2 defaultValue)
        {
            if (value != defaultValue)
            {
                WritePropertyName(name);
                WriteValue(value);
            }
        }

        public void AddPosition(string name, Vector2 value, Vector2 defaultValue)
        {
            if (value != defaultValue)
            {
                WritePropertyName(name);
                WritePosition(value);
            }
        }

        public void AddOffset(string name, Vector2Short value, Vector2Short defaultValue)
        {
            if (value != defaultValue)
            {
                WritePropertyName(name);
                WriteOffset(value);
            }
        }

        public void AddField(string name, TextAnchor value)
        {
            if (value != TextAnchor.UpperLeft)
            {
                WritePropertyName(name);
                WriteValue(EnumExt<TextAnchor>.ToString(value));
            }
        }

        public void AddField(string name, InputField.LineType value)
        {
            if (value != InputField.LineType.SingleLine)
            {
                WritePropertyName(name);
                WriteValue(EnumExt<InputField.LineType>.ToString(value));
            }
        }
        
        public void AddField(string name, Image.Type value)
        {
            if (value != Image.Type.Simple)
            {
                WritePropertyName(name);
                WriteValue(EnumExt<Image.Type>.ToString(value));
            }
        }

        public void AddField(string name, int value, int defaultValue)
        {
            if (value != defaultValue)
            {
                WritePropertyName(name);
                WriteValue(value);
            }
        }

        public void AddField(string name, float value, float defaultValue)
        {
            if (Math.Abs(value - defaultValue) >= 0.0001)
            {
                WritePropertyName(name);
                WriteValue(value);
            }
        }
        
        public void AddField(string name, ulong value, ulong defaultValue)
        {
            if (value != defaultValue)
            {
                WritePropertyName(name);
                WriteValue(value);
            }
        }

        public void AddField(string name, UiColor color)
        {
            if (color.Value != JsonDefaults.Color.ColorValue)
            {
                WritePropertyName(name);
                WriteValue(color);
            }
        }
        
        public void AddKeyField(string name)
        {
            WritePropertyName(name);
            WriteValue(string.Empty);
        }
        
        public void AddTextField(string name, string value)
        {
            WritePropertyName(name);
            WriteTextValue(value);
        }

        public void AddMouse()
        {
            WriteStartObject();
            AddFieldRaw(JsonDefaults.Common.ComponentTypeName, JsonDefaults.Common.NeedsCursorValue);
            WriteEndObject();
        }

        public void AddKeyboard()
        {
            WriteStartObject();
            AddFieldRaw(JsonDefaults.Common.ComponentTypeName, JsonDefaults.Common.NeedsKeyboardValue);
            WriteEndObject();
        }
        #endregion
        
        #region Writing
        
        #endregion
        public void WriteStartArray()
        {
            OnDepthIncrease();
            _writer.Append(ArrayStartChar);
        }
        
        public void WriteEndArray()
        {
            _writer.Append(ArrayEndChar);
            OnDepthDecrease();
        }

        public void WriteStartObject()
        {
            OnDepthIncrease();
            _writer.Append(ObjectStartChar);
        }
        
        public void WriteEndObject()
        {
            _writer.Append(ObjectEndChar);
            OnDepthDecrease();
        }

        public void WritePropertyName(string name)
        {
            if (_propertyComma)
            {
                _writer.Append(PropertyComma);
            }
            else
            {
                _propertyComma = true;
                _writer.Append(QuoteChar);
            }
            
            _writer.Append(name);
            _writer.Append(Separator);
        }

        public void WriteValue(bool value)
        {
            _writer.Append(value ? "true" : "false");
        }
        
        public void WriteValue(int value)
        {
            _writer.Append(value.ToString());
        }
        
        public void WriteValue(float value)
        {
            _writer.Append(value.ToString());
        }
        
        public void WriteValue(ulong value)
        {
            _writer.Append(value.ToString());
        }

        public void WriteValue(string value)
        {
            _writer.Append(QuoteChar);
            _writer.Append(value);
            _writer.Append(QuoteChar);
        }

        public void WriteTextValue(string value)
        {
            _writer.Append(QuoteChar);
            for (int i = 0; i < value.Length; i++)
            {
                char character = value[i];
                if (character == '\"')
                {
                    _writer.Append("\\\"");
                }
                else
                {
                    _writer.Append(character);
                }
            }
            _writer.Append(QuoteChar);
        }

        public void WriteValue(Vector2 pos)
        {
            _writer.Append(QuoteChar);
            VectorExt.WriteVector2(_writer, pos);
            _writer.Append(QuoteChar);
        }
        
        public void WritePosition(Vector2 pos)
        {
            _writer.Append(QuoteChar);
            VectorExt.WritePos(_writer, pos);
            _writer.Append(QuoteChar);
        }
        
        public void WriteOffset(Vector2Short offset)
        {
            _writer.Append(QuoteChar);
            VectorExt.WritePos(_writer, offset);
            _writer.Append(QuoteChar);
        }
        
        public void WriteValue(UiColor color)
        {
            _writer.Append(QuoteChar);
            UiColorExt.WriteColor(_writer, color);
            _writer.Append(QuoteChar);
        }

        protected override void EnterPool()
        {
            UiFrameworkPool.FreeStringBuilder(ref _writer);
        }

        public override string ToString()
        {
            return _writer.ToString();
        }

        public string ToJson()
        {
            string json = _writer.ToString();
            Dispose();
            return json;
        }
    }
}