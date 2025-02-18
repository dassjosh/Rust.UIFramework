using Network;
using Oxide.Ext.UiFramework.Benchmarks;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Json;

public class JsonFrameworkWriter : BasePoolable
{
    private const byte QuoteChar = (byte)'\"';
    private const byte ArrayStartChar = (byte)'[';
    private const byte ArrayEndChar = (byte)']';
    private const byte ObjectStartChar = (byte)'{';
    private const byte ObjectEndChar = (byte)'}';
    private const byte CommaChar = (byte)',';
    private const byte True = (byte)'1';
    private const byte False = (byte)'0';
    private static readonly Utf8String Separator = "\":"u8;
    private static readonly Utf8String PropertyComma = ",\""u8;
    
    private static readonly Utf8String EscapeQuote = "\\\""u8;
    private static readonly Utf8String EscapeBackslash = @"\\"u8;

    private bool _propertyComma;
    private bool _objectComma;
        
    private JsonUtf8Writer _writer;

    private void Init()
    {
        _writer = UiFrameworkPool.Get<JsonUtf8Writer>();
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
            _writer.Write(CommaChar);
            _objectComma = false;
        }
            
        _propertyComma = false;
    }
        
    private void OnDepthDecrease()
    {
        _objectComma = true;
    }

    #region Field Handling
    public void AddFieldRaw(in Utf8String name, in Utf8String value)
    {
        WritePropertyName(name);
        WriteValue(value);
    }
    
    public void AddFieldRaw(in Utf8String name, string value)
    {
        WritePropertyName(name);
        WriteValue(value);
    }
    
    public void AddFieldRaw(in Utf8String name, int value)
    {
        WritePropertyName(name);
        WriteValue(value);
    }

    public void AddFieldRaw(in Utf8String name, bool value)
    {
        WritePropertyName(name);
        WriteValue(value);
    }
    
    public void AddField(in Utf8String name, string value, string defaultValue)
    {
        if (value != null && value != defaultValue)
        {
            WritePropertyName(name);
            WriteValue(value);
        }
    }
    
    public void AddField(in Utf8String name, Vector2 value, Vector2 defaultValue)
    {
        if (value != defaultValue)
        {
            WritePropertyName(name);
            WriteValue(value);
        }
    }
    
    public void AddField(in Utf8String name, TextAnchor value)
    {
        if (value != TextAnchor.UpperLeft)
        {
            WritePropertyName(name);
            WriteValue(Utf8EnumCache<TextAnchor>.ToUtf8String(value));
        }
    }
    
    public void AddField(in Utf8String name, InputField.LineType value)
    {
        if (value != InputField.LineType.SingleLine)
        {
            WritePropertyName(name);
            WriteValue(Utf8EnumCache<InputField.LineType>.ToUtf8String(value));
        }
    }
    
    public void AddField(in Utf8String name, Image.Type value)
    {
        if (value != Image.Type.Simple)
        {
            WritePropertyName(name);
            WriteValue(Utf8EnumCache<Image.Type>.ToUtf8String(value));
        }
    }
    
    public void AddField(in Utf8String name, VerticalWrapMode value)
    {
        if (value != VerticalWrapMode.Truncate)
        {
            WritePropertyName(name);
            WriteValue(Utf8EnumCache<VerticalWrapMode>.ToUtf8String(value));
        }
    }
    
    public void AddField(in Utf8String name, ScrollRect.MovementType value)
    {
        if (value != ScrollRect.MovementType.Clamped)
        {
            WritePropertyName(name);
            WriteValue(Utf8EnumCache<ScrollRect.MovementType>.ToUtf8String(value));
        }
    }
    
    public void AddField(in Utf8String name, TimerFormat value)
    {
        if (value != TimerFormat.None)
        {
            WritePropertyName(name);
            WriteValue(Utf8EnumCache<TimerFormat>.ToUtf8String(value));
        }
    }
    
    public void AddField(in Utf8String name, int value, int defaultValue)
    {
        if (value != defaultValue)
        {
            WritePropertyName(name);
            WriteValue(value);
        }
    }
    
    public void AddField(in Utf8String name, float value, float defaultValue)
    {
        if (value != defaultValue)
        {
            WritePropertyName(name);
            WriteValue(value);
        }
    }
    
    public void AddField(in Utf8String name, ulong value, ulong defaultValue)
    {
        if (value != defaultValue)
        {
            WritePropertyName(name);
            WriteValue(value);
        }
    }
    
    public void AddField(in Utf8String name, bool value, bool defaultValue)
    {
        if (value != defaultValue)
        {
            WritePropertyName(name);
            WriteValue(value);
        }
    }
    
    public void AddField(in Utf8String name, UiColor color)
    {
        if (color != JsonDefaults.Color.ColorValue)
        {
            WritePropertyName(name);
            WriteValue(color);
        }
    }
    
    public void AddField(in Utf8String name, UiColor color, UiColor defaultColor)
    {
        if (color != defaultColor)
        {
            WritePropertyName(name);
            WriteValue(color);
        }
    }
    
    public void AddComponent(in Utf8String name, IComponent component)
    {
        WritePropertyName(name);
        bool objectComma = _objectComma;
        bool propertyComma = _propertyComma;
        _objectComma = false;
        _propertyComma = false;
        if (component != null)
        {
            component.WriteComponent(this);
        }
        else
        {
            WriteStartObject();
            WriteEndObject();
        }
        _objectComma = objectComma;
        _propertyComma = propertyComma;
    }
    
    public void AddKeyField(in Utf8String name)
    {
        WritePropertyName(name);
        WriteBlankValue();
    }
    
    public void AddTextField(in Utf8String name, string value)
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
    
    public void WriteStartArray()
    {
        OnDepthIncrease();
        _writer.Write(ArrayStartChar);
    }
        
    public void WriteEndArray()
    {
        _writer.Write(ArrayEndChar);
        OnDepthDecrease();
    }

    public void WriteStartObject()
    {
        OnDepthIncrease();
        _writer.Write(ObjectStartChar);
    }
        
    public void WriteEndObject()
    {
        _writer.Write(ObjectEndChar);
        OnDepthDecrease();
    }

    public void WritePropertyName(in Utf8String name)
    {
        if (_propertyComma)
        {
            _writer.Write(PropertyComma);
        }
        else
        {
            _propertyComma = true;
            _writer.Write(QuoteChar);
        }
            
        _writer.Write(name);
        _writer.Write(Separator);
    }
    
    public void WriteValue(in Utf8String value)
    {
        _writer.Write(QuoteChar);
        _writer.Write(value);
        _writer.Write(QuoteChar);
    }
    
    public void WriteValue(bool value)
    {
        _writer.Write(value ? True : False);
    }
        
    public void WriteValue(int value)
    {
        _writer.Write(Utf8StringCache<int>.ToString(value));
    }
        
    public void WriteValue(float value)
    {
        _writer.Write(Utf8StringCache<float>.ToString(value));
    }
        
    public void WriteValue(ulong value)
    {
        _writer.Write(Utf8StringCache<ulong>.ToString(value));
    }

    public void WriteValue(string value)
    {
        _writer.Write(QuoteChar);
        _writer.Write(value);
        _writer.Write(QuoteChar);
    }
        
    public void WriteBlankValue()
    {
        _writer.Write(QuoteChar);
        _writer.Write(QuoteChar);
    }
    
    public void WriteTextValue(string value)
    {
        _writer.Write(QuoteChar);
        if (value != null)
        {
            for (int i = 0; i < value.Length; i++)
            {
                char character = value[i];
                if (character == '\"')
                {
                    _writer.Write(EscapeQuote);
                }
                else if (character == '\\' && i + 1 == value.Length)
                {
                    _writer.Write(EscapeBackslash);
                }
                else
                {
                    _writer.Write(character);
                }
            }
        }
        _writer.Write(QuoteChar);
    }

    public void WriteValue(Vector2 pos)
    {
        _writer.Write(QuoteChar);
        VectorCache.WriteVector(_writer, pos);
        _writer.Write(QuoteChar);
    }
        
    public void WriteValue(UiColor color)
    {
        _writer.Write(QuoteChar);
        UiColorCache.WriteColor(_writer, color);
        _writer.Write(QuoteChar);
    }
    #endregion

    protected override void EnterPool()
    {
        _objectComma = false;
        _propertyComma = false;
        _writer.Dispose();
    }

    public override string ToString()
    {
        return _writer.ToString();
    }

    public int WriteTo(byte[] buffer)
    {
        return _writer.WriteToArray(buffer);
    }
        
    public void WriteToNetwork(NetWrite write)
    {
        _writer.WriteToNetwork(write);
    }

#if BENCHMARKS || DEBUG
    internal void WriteToNetwork(BenchmarkNetWrite write)
    {
        _writer.WriteToNetwork(write);
    }
#endif

    public byte[] ToArray()
    {
        return _writer.ToArray();
    }
}