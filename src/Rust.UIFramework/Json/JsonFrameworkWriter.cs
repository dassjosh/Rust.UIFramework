using System;
using System.IO;
using System.Runtime.CompilerServices;
using Network;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine;

#if BENCHMARKS
using Oxide.Ext.UiFramework.Benchmarks;
#endif

namespace Oxide.Ext.UiFramework.Json;

[GenerateJsonWriter]
public sealed partial class JsonFrameworkWriter : BasePoolable
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
    private const byte Space = (byte)' ';
    private static readonly Utf8String StartQuote = Utf8String.FromChar(StartQuoteString);
    private static readonly Utf8String EndQuote = Utf8String.FromChar(EndQuoteString);
    private static readonly Utf8String Separator = "\":";
    private static readonly Utf8String PropertyComma = ",\"";
    
    private static readonly Utf8String EscapeQuote = "\\\"";
    private static readonly Utf8String Backslash = BackslashString;

    private bool _propertyComma;
    private bool _objectComma;
    
    private enum TextMode : byte {Text, Command}

    private readonly JsonUtf8Writer _writer = new();

    public static JsonFrameworkWriter Create(IUiFrameworkPlugin plugin) => plugin.PluginPool.Get<JsonFrameworkWriter>().Init();

    private JsonFrameworkWriter Init()
    {
        _writer.Init();
        return this;
    }

    #region Comma Handling
    private void OnDepthIncrease()
    {
        if (_objectComma)
        {
            _writer.WriteChar(CommaChar);
            _objectComma = false;
        }
            
        _propertyComma = false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void OnDepthDecrease()
    {
        _objectComma = true;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ResetCommaState()
    {
        _objectComma = false;
        _propertyComma = false;
    }
    #endregion

    #region Raw
    public void WriteRaw(ReadOnlySpan<byte> span) => _writer.WriteRaw(span);
    #endregion
    
    #region Specialised Field Handling
    public void AddField<T>(Utf8String name, T value) where T : unmanaged, Enum
    {
        WritePropertyName(name);
        WriteEnumInternal(value);
    }
    
    public void AddField(in UiPosition value, SerializeMode mode)
    {
        if (mode == SerializeMode.Create)
        {
            AddField(JsonDefaults.RectTransform.AnchorMinName, value.Min, JsonDefaults.RectTransform.AnchorMin);
            AddField(JsonDefaults.RectTransform.AnchorMaxName, value.Max, JsonDefaults.RectTransform.AnchorMax);
        }
        else
        {
            WritePropertyName(JsonDefaults.RectTransform.AnchorMinName);
            WriteValue(value.Min);
            WritePropertyName(JsonDefaults.RectTransform.AnchorMaxName);
            WriteValue(value.Max);
        }
    }
    
    public void AddField(in UiOffset value, SerializeMode mode)
    { 
        if (mode == SerializeMode.Create)
        {
            AddField(JsonDefaults.RectTransform.OffsetMinName, value.Min, JsonDefaults.RectTransform.OffsetMin);
            AddField(JsonDefaults.RectTransform.OffsetMaxName, value.Max, JsonDefaults.RectTransform.OffsetMax);
        }
        else
        {
            WritePropertyName(JsonDefaults.RectTransform.OffsetMinName);
            WriteValue(value.Min);
            WritePropertyName(JsonDefaults.RectTransform.OffsetMaxName);
            WriteValue(value.Max);
        }
    }
    
    public void AddField(Utf8String name, Tracked<string> value, SerializeMode mode)
    {
        if (value.ShouldSerialize(mode) && value.Value != null)
        {
            WritePropertyName(name);
            WriteValue(value.Value);
        }
    }
    
    public void AddField<T>(Utf8String name, Tracked<T> value, SerializeMode mode) where T : unmanaged, Enum
    {
        if (value.ShouldSerialize(mode))
        {
            WritePropertyName(name);
            WriteEnumInternal(value.Value);
        }
    }
    
    public void AddField<T>(Utf8String name, Tracked<T?> value, SerializeMode mode) where T : unmanaged, Enum
    {
        if (value.ShouldSerialize(mode))
        {
            T? enumVal = value.Value;
            if (enumVal.HasValue)
            {
                WritePropertyName(name);
                WriteEnumInternal(enumVal.Value);
            }
        }
    }
    
    public void AddComponent(Utf8String name, IComponent component, SerializeMode mode, bool add)
    {
        if (add)
        {
            AddComponent(name, component, mode);
        }
    }
    
    public void AddComponent(Utf8String name, IComponent component, SerializeMode mode)
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
    
    public void AddKeyField(Utf8String name)
    {
        WritePropertyName(name);
        WriteEmptyString();
    }
    
    public void AddKeyField(Utf8String name, bool add)
    {
        if (add)
        {
            AddKeyField(name);
        }
    }
    
    public void AddTextField(Utf8String name, string value)
    {
        WritePropertyName(name);
        WriteTextValue(value);
    }
    
    public void AddTextField(Utf8String name, string value, string defaultValue)
    {
        if (value != defaultValue)
        {
            WritePropertyName(name);
            WriteTextValue(value);
        }
    }
    
    public void AddTextField(Utf8String name, Tracked<string> value, SerializeMode mode)
    {
        if (value.ShouldSerialize(mode))
        {
            WritePropertyName(name);
            WriteTextValue(value.Value);
        }
    }
    
    public void AddCommandField(Utf8String name, string value, string defaultValue)
    {
        if (value != null && value != defaultValue)
        {
            WritePropertyName(name);
            WriteCommandValue(value);
        }
    }
    
    public void AddCommandField(Utf8String name, Tracked<string> value, SerializeMode mode)
    {
        if (value.ShouldSerialize(mode))
        {
            WritePropertyName(name);
            WriteCommandValue(value.Value);
        }
    }
    #endregion
        
    #region Writing
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteStartArray()
    {
        OnDepthIncrease();
        _writer.WriteChar(ArrayStartChar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteEndArray()
    {
        _writer.WriteChar(ArrayEndChar);
        OnDepthDecrease();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteStartObject()
    {
        OnDepthIncrease();
        _writer.WriteChar(ObjectStartChar);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteEndObject()
    {
        _writer.WriteChar(ObjectEndChar);
        OnDepthDecrease();
    }

    public void WritePropertyName(Utf8String name)
    {
        if (_propertyComma)
        {
            _writer.WriteString(PropertyComma);
        }
        else
        {
            _propertyComma = true;
            WriteQuote();
        }
            
        _writer.WriteString(name);
        _writer.WriteString(Separator);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteQuote() => _writer.WriteChar(QuoteChar);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteComma() => _writer.WriteChar(CommaChar);

    public void WriteValue(Utf8String value)
    {
        WriteQuote();
        _writer.WriteString(value);
        WriteQuote();
    }
    
    public void WriteValue(bool value) => _writer.WriteChar(value ? True : False);
    public void WriteValue(byte value) => _writer.Write(value);
    public void WriteValue(sbyte value) => _writer.Write(value);
    public void WriteValue(short value) => _writer.Write(value);
    public void WriteValue(ushort value) => _writer.Write(value);
    public void WriteValue(int value) => _writer.Write(value);
    public void WriteValue(uint value) => _writer.Write(value);
    public void WriteValue(long value) => _writer.Write(value);
    public void WriteValue(ulong value) => _writer.Write(value);
    public void WriteValue(float value) => _writer.Write(value);
    
    public void WriteValue(string value)
    {
        _writer.WriteChar(QuoteChar);
        _writer.WriteString(value);
        _writer.WriteChar(QuoteChar);
    }
    
    public void WriteValue(ReadOnlySpan<char> value)
    {
        _writer.WriteChar(QuoteChar);
        _writer.WriteString(value);
        _writer.WriteChar(QuoteChar);
    }
    
    public void WriteValue(UiRotation rotation)
    {
        _writer.Write(rotation.Rotation);
    }
    
    public void WriteValue(Vector2 pos)
    {
        _writer.WriteChar(QuoteChar);
        _writer.Write(pos.x);
        _writer.WriteChar(Space);
        _writer.Write(pos.y);
        _writer.WriteChar(QuoteChar);
    }
    
    public void WriteValue(Vector3 pos)
    {
        _writer.WriteChar(QuoteChar);
        _writer.Write(pos.x);
        _writer.WriteChar(Space);
        _writer.Write(pos.y);
        _writer.WriteChar(QuoteChar);
        _writer.Write(pos.z);
        _writer.WriteChar(QuoteChar);
    }
    
    public void WriteValue(Vector4 pos)
    {
        _writer.WriteChar(QuoteChar);
        _writer.Write(pos.x);
        _writer.WriteChar(Space);
        _writer.Write(pos.y);
        _writer.WriteChar(QuoteChar);
        _writer.Write(pos.z);
        _writer.WriteChar(QuoteChar);
        _writer.Write(pos.w);
        _writer.WriteChar(QuoteChar);
    }
    
    public void WriteValue(UiColor color)
    {
        const int maxFractionDigits = 4;
        _writer.WriteChar(QuoteChar);
        _writer.Write(color.RedFloat, maxFractionDigits);
        _writer.WriteChar(Space);
        _writer.Write(color.GreenFloat, maxFractionDigits);
        _writer.WriteChar(Space);
        _writer.Write(color.BlueFloat, maxFractionDigits);
        if (color.AlphaB != 255)
        {
            _writer.WriteChar(Space);
            _writer.Write(color.AlphaFloat, maxFractionDigits);
        }
        _writer.WriteChar(QuoteChar);
    }
    
    public void WriteValue(in UiPadding padding)
    {
        _writer.WriteChar(QuoteChar);
        _writer.Write(padding.Left);
        _writer.WriteChar(Space);
        _writer.Write(padding.Top);
        _writer.WriteChar(Space);
        _writer.Write(padding.Right);
        _writer.WriteChar(Space);
        _writer.Write(padding.Bottom);
        _writer.WriteChar(QuoteChar);
    }
    
    public void WriteValue(in UiBorderWidth border)
    {
        _writer.WriteChar(QuoteChar);
        _writer.Write(border.Left);
        _writer.WriteChar(Space);
        _writer.Write(border.Bottom);
        _writer.WriteChar(Space);
        _writer.Write(border.Right);
        _writer.WriteChar(Space);
        _writer.Write(border.Top);
        _writer.WriteChar(QuoteChar);
    }
    
    public void WriteEmptyString()
    {
        _writer.WriteChar(QuoteChar);
        _writer.WriteChar(QuoteChar);
    }
    
    public void WriteTextValue(string value) => WriteTextInternal(value, TextMode.Text);

    public void WriteCommandValue(string value) => WriteTextInternal(value, TextMode.Command);
    #endregion
    
    #region Internal
    private void WriteTextInternal(string value, TextMode mode)
    {
        if (string.IsNullOrEmpty(value))
        {
            WriteEmptyString();
            return;
        }
        
        _writer.WriteChar(QuoteChar);
        int i = 0;
        int length = value.Length;
        bool isInQuote = false;
        while (i < length)
        {
            char character = value[i];
            if (char.IsHighSurrogate(character) && i + 1 < length)
            {
                char lowSurrogate = value[i + 1];
                if (char.IsLowSurrogate(lowSurrogate))
                {
                    _writer.WriteChar(character, lowSurrogate);
                    i += 2;
                    continue;
                }
            }
            switch (character)
            {
                case '\"':
                    if (mode == TextMode.Text)
                    {
                        isInQuote = !isInQuote;
                        _writer.WriteString(isInQuote ? EndQuote : StartQuote);
                    }
                    else
                    {
                        _writer.WriteString(EscapeQuote);
                    }
                    break;
                case '\\':
                    _writer.WriteString(Backslash);
                    break;
                default:
                    _writer.WriteChar(character);
                    break;
            }
                
            i += 1;
        }
        _writer.WriteChar(QuoteChar);
    }

    private void WriteEnumInternal<T>(T value) where T : unmanaged, Enum
    {
        _writer.WriteChar(QuoteChar);
        switch (TypeCache<T>.TypeCode)
        {
            case TypeCode.SByte:
                _writer.Write(Unsafe.As<T, sbyte>(ref value));
                break;
            case TypeCode.Byte:
                _writer.Write(Unsafe.As<T, byte>(ref value));
                break;
            case TypeCode.Int16:
                _writer.Write(Unsafe.As<T, short>(ref value));
                break;
            case TypeCode.UInt16:
                _writer.Write(Unsafe.As<T, ushort>(ref value));
                break;
            case TypeCode.Int32:
                _writer.Write(Unsafe.As<T, int>(ref value));
                break;
            case TypeCode.UInt32:
                _writer.Write(Unsafe.As<T, uint>(ref value));
                break;
            case TypeCode.Int64:
                _writer.Write(Unsafe.As<T, long>(ref value));
                break;
            case TypeCode.UInt64:
                _writer.Write(Unsafe.As<T, ulong>(ref value));
                break;
            default:
                _writer.Write((byte)0);
                break;
        }
        
        _writer.WriteChar(QuoteChar);
    } 
    #endregion

    public void WriteToStream(Stream stream) => _writer.WriteToStream(stream);
    public override string ToString() => _writer.ToString();
    public int WriteTo(byte[] buffer) => _writer.WriteToArray(buffer);

    public void WriteToNetwork(NetWrite write) => _writer.WriteToNetwork(write);

#if BENCHMARKS
    internal void WriteToNetwork(BenchmarkNetWrite write) => _writer.WriteToNetwork(write);
#endif

    public byte[] ToArray() => _writer.ToArray();
    
    protected override void EnterPool()
    {
        ResetCommaState();
        _writer.Reset();
        _propertyComma = false;
        _objectComma = false;
    }
}