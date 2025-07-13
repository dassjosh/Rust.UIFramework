using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Network;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;
using UnityEngine;

#if BENCHMARKS
using Oxide.Ext.UiFramework.Benchmarks;
#endif

namespace Oxide.Ext.UiFramework.Json;

public sealed class JsonFrameworkWriter : BasePoolable
{
    internal const char StartQuoteString = '“';
    internal const char EndQuoteString = '“';
    internal const string BackslashString = "⧹";
    
    private const byte QuoteChar = (byte)'\"';
    private const byte ArrayStartChar = (byte)'[';
    private const byte ArrayEndChar = (byte)']';
    private const byte ObjectStartChar = (byte)'{';
    private const byte ObjectEndChar = (byte)'}';
    private const byte CommaChar = (byte)',';
    private const byte True = (byte)'1';
    private const byte False = (byte)'0';
    private static readonly Utf8String StartQuote = StartQuoteString;
    private static readonly Utf8String EndQuote = EndQuoteString;
    private static readonly Utf8String Separator = "\":";
    private static readonly Utf8String PropertyComma = ",\"";
    
    private static readonly Utf8String EscapeQuote = "\\\"";
    private static readonly Utf8String Backslash = BackslashString;

    private bool _propertyComma;
    private bool _objectComma;
        
    private JsonUtf8Writer _writer;

    private void Init()
    {
        _writer = PluginPool.Get<JsonUtf8Writer>();
    }

    public static JsonFrameworkWriter Create(UiPluginPool pool)
    {
        JsonFrameworkWriter writer = pool.Get<JsonFrameworkWriter>();
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
    
    public void AddFieldRaw(in Utf8String name, ulong value)
    {
        WritePropertyName(name);
        WriteValue(value);
    }

    public void AddFieldRaw(in Utf8String name, bool value)
    {
        WritePropertyName(name);
        WriteValue(value);
    }
    
    public void AddFieldRaw(in Utf8String name, UiColor color)
    {
        WritePropertyName(name);
        WriteValue(color);
    }
    
    public void AddFieldRaw(in Utf8String name, Vector2 value)
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
    
    public void AddField<T>(in Utf8String name, T value, T defaultValue) where T : struct, Enum
    {
        if (!EqualityComparer<T>.Default.Equals(value, defaultValue))
        {
            WritePropertyName(name);
            WriteValue(Utf8EnumCache<T>.ToUtf8Number(value));
        }
    }
    
    public void AddField<T>(in Utf8String name, T? value) where T : struct, Enum
    {
        if (value.HasValue)
        {
            WritePropertyName(name);
            WriteValue(Utf8EnumCache<T>.ToUtf8Number(value.Value));
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

    public void AddComponent(in Utf8String name, IComponent component, bool add)
    {
        if (add)
        {
            AddComponent(name, component);
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
    
    public void AddKeyField(in Utf8String name, bool add)
    {
        if (add)
        {
            AddKeyField(name);
        }
    }
    
    public void AddTextField(in Utf8String name, string value)
    {
        WritePropertyName(name);
        WriteTextValue(value);
    }
    
    public void AddCommand(in Utf8String name, string value)
    {
        WritePropertyName(name);
        WriteCommandValue(value);
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
            WriteQuote();
        }
            
        _writer.Write(name);
        _writer.Write(Separator);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteQuote()
    {
        _writer.Write(QuoteChar);
    }
    
    public void WriteValue(in Utf8String value)
    {
        WriteQuote();
        _writer.Write(value);
        WriteQuote();
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
        if (!string.IsNullOrEmpty(value))
        {
            bool isInQuote = false;
            for (int i = 0; i < value.Length;)
            {
                char character = value[i];
                if (i + 1 < value.Length && char.IsHighSurrogate(character) && char.IsLowSurrogate(value[i + 1]))
                {
                    _writer.Write(value.AsSpan().Slice(i, 2));
                    i += 2;
                    continue;
                }

                switch (character)
                {
                    case '\"':
                        isInQuote = !isInQuote;
                        _writer.Write(isInQuote ? EndQuote : StartQuote);
                        break;
                    case '\\':
                        _writer.Write(Backslash);
                        break;
                    default:
                        _writer.Write(character);
                        break;
                }

                i += 1;
            }
        }
        _writer.Write(QuoteChar);
    }
    
    public void WriteCommandValue(string value)
    {
        _writer.Write(QuoteChar);
        if (!string.IsNullOrEmpty(value))
        {
            for (int i = 0; i < value.Length;)
            {
                char character = value[i];
                if (i + 1 < value.Length && char.IsHighSurrogate(character) && char.IsLowSurrogate(value[i + 1]))
                {
                    _writer.Write(value.AsSpan().Slice(i, 2));
                    i += 2;
                    continue;
                }
                switch (character)
                {
                    case '\"':
                        _writer.Write(EscapeQuote);
                        break;
                    case '\\':
                        _writer.Write(Backslash);
                        break;
                    default:
                        _writer.Write(character);
                        break;
                }
                
                i += 1;
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
        _writer = null;
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