﻿using Network;
using Oxide.Ext.UiFramework.Benchmarks;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Pooling;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Json;

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
        
    private JsonBinaryWriter _writer;

    private void Init()
    {
        _writer = UiFrameworkPool.Get<JsonBinaryWriter>();
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
    public void AddFieldRaw(string name, string value)
    {
        WritePropertyName(name);
        WriteValue(value);
    }
        
    public void AddFieldRaw(string name, char value)
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

    public void AddOffset(string name, Vector2 value, Vector2 defaultValue)
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
            WriteValue(EnumCache<TextAnchor>.ToString(value));
        }
    }

    public void AddField(string name, InputField.LineType value)
    {
        if (value != InputField.LineType.SingleLine)
        {
            WritePropertyName(name);
            WriteValue(EnumCache<InputField.LineType>.ToString(value));
        }
    }
        
    public void AddField(string name, Image.Type value)
    {
        if (value != Image.Type.Simple)
        {
            WritePropertyName(name);
            WriteValue(EnumCache<Image.Type>.ToString(value));
        }
    }
        
    public void AddField(string name, VerticalWrapMode value)
    {
        if (value != VerticalWrapMode.Truncate)
        {
            WritePropertyName(name);
            WriteValue(EnumCache<VerticalWrapMode>.ToString(value));
        }
    }
        
    public void AddField(string name, ScrollRect.MovementType value)
    {
        if (value != ScrollRect.MovementType.Clamped)
        {
            WritePropertyName(name);
            WriteValue(EnumCache<ScrollRect.MovementType>.ToString(value));
        }
    }
    
    public void AddField(string name, TimerFormat value)
    {
        if (value != TimerFormat.None)
        {
            WritePropertyName(name);
            WriteValue(EnumCache<TimerFormat>.ToString(value));
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
        if (value != defaultValue)
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
        
    public void AddField(string name, bool value, bool defaultValue)
    {
        if (value != defaultValue)
        {
            WritePropertyName(name);
            WriteValue(value);
        }
    }

    public void AddField(string name, UiColor color)
    {
        if (color != JsonDefaults.Color.ColorValue)
        {
            WritePropertyName(name);
            WriteValue(color);
        }
    }
        
    public void AddField(string name, UiColor color, UiColor defaultColor)
    {
        if (color != defaultColor)
        {
            WritePropertyName(name);
            WriteValue(color);
        }
    }
        
    public void AddComponent(string name, IComponent component)
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
        
    public void AddKeyField(string name)
    {
        WritePropertyName(name);
        WriteBlankValue();
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

    public void WritePropertyName(string name)
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

    public void WriteValue(bool value)
    {
        _writer.Write(value ? '1' : '0');
    }

    public void WriteValue(char value)
    {
        _writer.Write(value);
    }
        
    public void WriteValue(int value)
    {
        _writer.Write(StringCache<int>.ToString(value));
    }
        
    public void WriteValue(float value)
    {
        _writer.Write(StringCache<float>.ToString(value));
    }
        
    public void WriteValue(ulong value)
    {
        _writer.Write(StringCache<ulong>.ToString(value));
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
                    _writer.Write("\\\"");
                }
                else if (character == '\\' && i + 1 == value.Length)
                {
                    _writer.Write(@"\\");
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
        
    public void WritePosition(Vector2 pos)
    {
        _writer.Write(QuoteChar);
        VectorCache.WritePosition(_writer, pos);
        _writer.Write(QuoteChar);
    }
        
    public void WriteOffset(Vector2 offset)
    {
        _writer.Write(QuoteChar);
        VectorCache.WriteOffset(_writer, offset);
        _writer.Write(QuoteChar);
    }
        
    public void WriteValue(UiColor color)
    {
        _writer.Write(QuoteChar);
        UiColorCache.WriteColor(_writer, color);
        _writer.Write(QuoteChar);
    }

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

#if BENCHMARKS
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