using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using Network;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
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
    
    public void AddFieldRaw(in Utf8String name, float value)
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
    
    public void AddFieldRaw<T>(in Utf8String name, T value) where T : struct, Enum
    {
        WritePropertyName(name);
        WriteValue(Utf8EnumCache<T>.ToUtf8Number(value));
    }
    
    public void AddField(in Utf8String name, Vector2 value, Vector2 defaultValue)
    {
        if (value != defaultValue)
        {
            WritePropertyName(name);
            WriteValue(value);
        }
    }

    public void AddField(TrackedValue<UiPosition> value, SerializeMode mode)
    {
        if (mode == SerializeMode.Create)
        {
            AddField(JsonDefaults.RectTransform.AnchorMinName, value.Value.Min, JsonDefaults.RectTransform.AnchorMin);
            AddField(JsonDefaults.RectTransform.AnchorMaxName, value.Value.Max, JsonDefaults.RectTransform.AnchorMax);
        }
        else if(value.HasChanged)
        {
            UiPosition pos = value.Value;
            WritePropertyName(JsonDefaults.RectTransform.AnchorMinName);
            WriteValue(pos.Min);
            WritePropertyName(JsonDefaults.RectTransform.AnchorMaxName);
            WriteValue(pos.Max);
        }
    }
    
    public void AddField(TrackedValue<UiOffset> value, SerializeMode mode)
    {
        if (mode == SerializeMode.Create)
        {
            AddField(JsonDefaults.RectTransform.OffsetMinName, value.Value.Min, JsonDefaults.RectTransform.OffsetMin);
            AddField(JsonDefaults.RectTransform.OffsetMaxName, value.Value.Max, JsonDefaults.RectTransform.OffsetMax);
        }
        else if(value.HasChanged)
        {
            UiOffset pos = value.Value;
            WritePropertyName(JsonDefaults.RectTransform.OffsetMinName);
            WriteValue(pos.Min);
            WritePropertyName(JsonDefaults.RectTransform.OffsetMaxName);
            WriteValue(pos.Max);
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
    
    public void AddField(in Utf8String name, TrackedValue<bool> value, SerializeMode mode)
    {
        if (value.ShouldSerialize(mode))
        {
            WritePropertyName(name);
            WriteValue(value.Value);
        }
    }
    
    public void AddField(in Utf8String name, TrackedValue<int> value, SerializeMode mode)
    {
        if (value.ShouldSerialize(mode))
        {
            WritePropertyName(name);
            WriteValue(value.Value);
        }
    }
    
    public void AddField(in Utf8String name, TrackedValue<ulong> value, SerializeMode mode)
    {
        if (value.ShouldSerialize(mode))
        {
            WritePropertyName(name);
            WriteValue(value.Value);
        }
    }
    
    public void AddField(in Utf8String name, TrackedValue<float> value, SerializeMode mode)
    {
        if (value.ShouldSerialize(mode))
        {
            WritePropertyName(name);
            WriteValue(value.Value);
        }
    }
    
    public void AddField(in Utf8String name, TrackedValue<string> value, SerializeMode mode)
    {
        if (value.ShouldSerialize(mode))
        {
            WritePropertyName(name);
            WriteValue(value.Value);
        }
    }
    
    public void AddField(in Utf8String name, TrackedValue<UiColor> value, SerializeMode mode)
    {
        if (value.ShouldSerialize(mode))
        {
            WritePropertyName(name);
            WriteValue(value.Value);
        }
    }
    
    public void AddField(in Utf8String name, TrackedValue<UiBorderWidth> value, SerializeMode mode)
    {
        if (value.ShouldSerialize(mode))
        {
            WritePropertyName(name);
            WriteValue(value.Value);
        }
    }
    
    public void AddField(in Utf8String name, TrackedValue<UiRotation> value, SerializeMode mode)
    {
        if (value.ShouldSerialize(mode))
        {
            WritePropertyName(name);
            WriteValue(value.Value.Rotation);
        }
    }
    
    public void AddField(in Utf8String name, TrackedValue<Vector2> value, SerializeMode mode)
    {
        if (value.ShouldSerialize(mode))
        {
            WritePropertyName(name);
            WriteValue(value.Value);
        }
    }
    
    public void AddField<T>(in Utf8String name, TrackedValue<T> value, SerializeMode mode) where T : struct, Enum
    {
        if (value.ShouldSerialize(mode))
        {
            WritePropertyName(name);
            WriteValue(Utf8EnumCache<T>.ToUtf8Number(value.Value));
        }
    }
    
    public void AddField<T>(in Utf8String name, TrackedValue<T?> value, SerializeMode mode) where T : struct, Enum
    {
        if (value.ShouldSerialize(mode))
        {
            T? enumVal = value.Value;
            if (enumVal.HasValue)
            {
                AddFieldRaw(name, enumVal.Value);
            }
        }
    }
    
    public void AddComponent(in Utf8String name, IComponent component, SerializeMode mode, bool add)
    {
        if (add)
        {
            AddComponent(name, component, mode);
        }
    }
    
    public void AddComponent(in Utf8String name, IComponent component, SerializeMode mode)
    {
        if (component is null)
        {
            return;
        }
        
        WritePropertyName(name);
        bool objectComma = _objectComma;
        bool propertyComma = _propertyComma;
        _objectComma = false;
        _propertyComma = false;
        component.WriteComponent(this, mode);
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
    
    public void AddTextField(in Utf8String name, string value, string defaultValue)
    {
        if (value != defaultValue)
        {
            WritePropertyName(name);
            WriteTextValue(value);
        }
    }
    
    public void AddTextField(in Utf8String name, TrackedValue<string> value, SerializeMode mode)
    {
        if (value.ShouldSerialize(mode))
        {
            WritePropertyName(name);
            WriteTextValue(value.Value);
        }
    }
    
    public void AddCommand(in Utf8String name, string value, string defaultValue)
    {
        if (value != null && value != defaultValue)
        {
            WritePropertyName(name);
            WriteCommandValue(value);
        }
    }
    
    public void AddCommand(in Utf8String name, TrackedValue<string> value, SerializeMode mode)
    {
        if (value.ShouldSerialize(mode))
        {
            WritePropertyName(name);
            WriteCommandValue(value.Value);
        }
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

    public void WriteComma()
    {
        _writer.Write(CommaChar);
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
    
    public void WriteValue(in UiPadding padding)
    {
        _writer.Write(QuoteChar);
        UiPaddingCache.WritePadding(_writer, padding);
        _writer.Write(QuoteChar);
    }
    
    public void WriteValue(in UiBorderWidth border)
    {
        _writer.Write(QuoteChar);
        UiBorderWidthCache.WriteBorderWidth(_writer, border);
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
            int i = 0;
            while (i < value.Length)
            {
                char character = value[i];
                if (char.IsHighSurrogate(character) && i + 1 < value.Length && char.IsLowSurrogate(value[i + 1]))
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
            int i = 0;
            while (i < value.Length)
            {
                char character = value[i];
                if (char.IsHighSurrogate(character) && i + 1 < value.Length && char.IsLowSurrogate(value[i + 1]))
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
    
    public void WriteNull()
    {
        _writer.Write(JsonDefaults.Common.NullValue);
    }
    #endregion

    public void ResetCommaState()
    {
        _objectComma = false;
        _propertyComma = false;
    }

    protected override void EnterPool()
    {
        _objectComma = false;
        _propertyComma = false;
        _writer.Dispose();
        _writer = null;
    }

    public void WriteToStream(Stream stream)
    {
        _writer.WriteToStream(stream);
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

    public void WriteRaw(ReadOnlySpan<byte> span)
    {
        _writer.WriteRaw(span);
    }
}