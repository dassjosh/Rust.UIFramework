using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Logging;

/// <summary>
/// Debug Logger used for logging debug information
/// </summary>
public class DebugLogger
{
    private readonly StringBuilder _logger = StringBuilderPool.Instance.Get();
    private const char IndentCharacter = '\t';

    private int _indent;

    /// <summary>
    /// Increments the Indent
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void IncrementIndent() => _indent++;
        
    /// <summary>
    /// Decrements the Indent
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DecrementIndent() => _indent = Math.Max(_indent - 1, 0);
        
    /// <summary>
    /// Appends the current indent into the logger
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendIndent() => _logger.Append(IndentCharacter, _indent);

    /// <summary>
    /// Appends the field name into the logger
    /// </summary>
    /// <param name="name">Name of the field</param>
    public void AppendFieldPrefix(string name)
    {
        AppendIndent();
        _logger.Append(name);
        _logger.Append(": ");
    }

    /// <summary>
    /// Appends a field into the logger
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="value">Value of the field</param>
    public void AppendField(string name, string value) => AppendField(name, value.AsSpan());
        
    /// <summary>
    /// Appends a field into the logger
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="value">Value of the field</param>
    public void AppendField(string name, ReadOnlySpan<char> value)
    {
        AppendFieldPrefix(name);
        _logger.Append(value);
        _logger.AppendLine();
    }

    /// <summary>
    /// Appends a field with the given name and int value
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="value">Value of the field</param>
    public void AppendField(string name, int value)
    {
        char[] array = System.Buffers.ArrayPool<char>.Shared.Rent(20);
        Span<char> span = array.AsSpan();
        if (value.TryFormat(span, out int written))
        {
            AppendField(name, span.Slice(0, written));
        }
        else
        {
            AppendField(name, value.ToString());
        }
        System.Buffers.ArrayPool<char>.Shared.Return(array);
    }

    /// <summary>
    /// Appends a field with the given name and double value
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="value">Value of the field</param>
    public void AppendField(string name, double value)
    {
        char[] array = System.Buffers.ArrayPool<char>.Shared.Rent(64);
        Span<char> span = array.AsSpan();
        if (value.TryFormat(span, out int written))
        {
            AppendField(name, span.Slice(0, written));
        }
        else
        {
            AppendField(name, value.ToString(CultureInfo.InvariantCulture));
        }
        System.Buffers.ArrayPool<char>.Shared.Return(array);
    }

    /// <summary>
    /// Appends a field with the given name and float value
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="value">Value of the field</param>
    public void AppendField(string name, float value)
    {
        char[] array = System.Buffers.ArrayPool<char>.Shared.Rent(64);
        Span<char> span = array.AsSpan();
        if (value.TryFormat(span, out int written))
        {
            AppendField(name, span.Slice(0, written));
        }
        else
        {
            AppendField(name, value.ToString(CultureInfo.InvariantCulture));
        }
        System.Buffers.ArrayPool<char>.Shared.Return(array);
    }

    /// <summary>
    /// Appends a field with the given name and ulong value
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="value">Value of the field</param>
    public void AppendField(string name, ulong value)
    {
        char[] array = System.Buffers.ArrayPool<char>.Shared.Rent(32);
        Span<char> span = array.AsSpan();
        if (value.TryFormat(span, out int written))
        {
            AppendField(name, span.Slice(0, written));
        }
        else
        {
            AppendField(name, value.ToString());
        }
        System.Buffers.ArrayPool<char>.Shared.Return(array);
    }

    /// <summary>
    /// Appends a field with the given name and long value
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="value">Value of the field</param>
    public void AppendField(string name, long value)
    {
        char[] array = System.Buffers.ArrayPool<char>.Shared.Rent(32);
        Span<char> span = array.AsSpan();
        if (value.TryFormat(span, out int written))
        {
            AppendField(name, span.Slice(0, written));
        }
        else
        {
            AppendField(name, value.ToString());
        }
        System.Buffers.ArrayPool<char>.Shared.Return(array);
    }

    /// <summary>
    /// Appends a field with the given name and bool value
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="value">Value of the field</param>
    public void AppendField(string name, bool value)
    {
        AppendFieldPrefix(name);
        _logger.AppendLine(value ? "Yes" : "No");
    }

    /// <summary>
    /// Appends a field with the given name and enum value
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="value">Value of the field</param>
    public void AppendFieldEnum<T>(string name, T value)  where T : Enum, IComparable, IFormattable, IConvertible => AppendField(name, EnumCache<T>.ToString(value));

    /// <summary>
    /// Appends a field with the given name and int amount over int total value
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="amount">Amount / Top value</param>
    /// <param name="total">Total / Bottom Value</param>
    public void AppendFieldOutOf(string name, int amount, int total)
    {
        AppendFieldPrefix(name);
        _logger.Append(StringCache<int>.ToString(amount));
        _logger.Append('/');
        _logger.AppendLine(StringCache<int>.ToString(total));
    }

    /// <summary>
    /// Appends a field with the given name and method info
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="info">Method info to append</param>
    public void AppendMethod(string name, MethodInfo info)
    {
        AppendFieldPrefix(name);
        _logger.Append(info.DeclaringType?.Name ?? "Unknown Type");
        _logger.Append('.');
        _logger.AppendLine(info.Name);
    }

    /// <summary>
    /// Appends a field with the given name and values seperated by a space
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="value1">First value</param>
    /// <param name="value2">Second value</param>
    public void AppendField(string name, string value1, string value2)
    {
        AppendFieldPrefix(name);
        _logger.Append(value1);
        _logger.Append(' ');
        _logger.AppendLine(value2);
    }

    /// <summary>
    /// Appends a field with the given name and TimeSpan value
    /// </summary>
    /// <param name="name">Name of the field</param>
    /// <param name="time">TimeSpan value</param>
    public void AppendField(string name, TimeSpan time)
    {
        AppendFieldPrefix(name);
        bool hasTime = AppendTimeSlice(time.TotalDays, time.Hours, " Days", false);
        hasTime = AppendTimeSlice(time.TotalHours, time.Hours, " Hours", hasTime);
        hasTime = AppendTimeSlice(time.TotalMinutes, time.Minutes, " Hours", hasTime);
        hasTime = AppendTimeSlice(time.TotalSeconds, time.Seconds, " Seconds", hasTime);
        if (hasTime)
        {
            return;
        }
            
        hasTime = AppendTimeSlice(time.TotalMilliseconds, time.Milliseconds, " Milliseconds", false);
        if (hasTime)
        {
            return;
        }
            
        _logger.Append("0 Seconds");
    }

    private bool AppendTimeSlice(double total, int time, string suffix, bool hasTime)
    {
        if (total < 1)
        {
            return false;
        }
            
        if (hasTime)
        {
            _logger.Append(' ');
        }

        _logger.Append(StringCache<int>.ToString(time));
        _logger.Append(suffix);
        return true;
    }

    /// <summary>
    /// Appends a field with the given name and Null value
    /// </summary>
    /// <param name="name">Name of the field</param>
    public void AppendNullField(string name)
    {
        AppendFieldPrefix(name);
        _logger.AppendLine("IS NULL");
    }

    /// <summary>
    /// Appends a line to the logger
    /// </summary>
    public void AppendLine()
    {
        _logger.AppendLine();
    }

    /// <summary>
    /// Appends a line to the logger with the given character repeated amount time
    /// </summary>
    /// <param name="character">Character to repeat</param>
    /// <param name="amount">Amount of times to repeat the character</param>
    public void AppendLine(char character, int amount)
    {
        AppendIndent();
        _logger.Append(character, amount);
        _logger.AppendLine();
    }
        
    /// <summary>
    /// Appends a line to the logger with the given string value
    /// </summary>
    /// <param name="value">Value of the line</param>
    public void AppendLine(string value)
    {
        AppendIndent();
        _logger.AppendLine(value);
    }

    /// <summary>
    /// Appends a <see cref="IDebugLoggable"/> object to the logger with the given name
    /// </summary>
    /// <param name="name">Name of the object</param>
    /// <param name="obj">Object to be logged</param>
    public void AppendObject(string name, IDebugLoggable obj)
    {
        if (obj == null)
        {
            AppendNullField(name);
            return;
        }
            
        StartObject(name);
        obj.LogDebug(this);
        EndObject();
    }
        
    /// <summary>
    /// Appends an <see cref="IEnumerable{T}"/> where T is string items to add to the logger
    /// </summary>
    /// <param name="name">Name of the list</param>
    /// <param name="items">String items to add</param>
    public void AppendList(string name, IEnumerable<string> items)
    {
        List<string> list = ListPool<string>.Instance.Get();
        AppendList(name, list);
        ListPool<string>.Instance.Free(list);
    }

    /// <summary>
    /// Appends an <see cref="List{T}"/> where T is string items to add to the logger
    /// </summary>
    /// <param name="name">Name of the list</param>
    /// <param name="items">String items to add</param>
    public void AppendList(string name, List<string> items)
    {
        if (items.Count == 0)
        {
            AppendField(name, "[]");
            return;
        }
            
        StartArray(name);
        for (int index = 0; index < items.Count; index++)
        {
            AppendLine(items[index]);
        }
        EndArray();
    }

    /// <summary>
    /// Appends an <see cref="IEnumerable{T}"/> where T is <see cref="IDebugLoggable"/> items to add to the logger
    /// </summary>
    /// <param name="name">Name of the list</param>
    /// <param name="items">Loggable items to add</param>
    public void AppendList<T>(string name, IEnumerable<T> items) where T : IDebugLoggable
    {
        List<string> list = ListPool<string>.Instance.Get();
        AppendList(name, list);
        ListPool<string>.Instance.Free(list);
    }
        
    /// <summary>
    /// Appends an <see cref="List{T}"/> where T is <see cref="IDebugLoggable"/> items to add to the logger
    /// </summary>
    /// <param name="name">Name of the list</param>
    /// <param name="items">Loggable items to add</param>
    public void AppendList<T>(string name, List<T> items) where T : IDebugLoggable
    {
        if (items.Count == 0)
        {
            AppendField(name, "[]");
            return;
        }
            
        StartArray(name);
        for (int index = 0; index < items.Count; index++)
        {
            AppendObject(string.Empty, items[index]);
        }
        EndArray();
    }

    /// <summary>
    /// Starts an array on the logger with the given name
    /// </summary>
    /// <param name="name">Name of the array</param>
    public void StartArray(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            AppendFieldPrefix(name);
            _logger.AppendLine();
        }
        AppendIndent();
        _logger.AppendLine("[");
        IncrementIndent();
    }

    /// <summary>
    /// Ends an array on the logger
    /// </summary>
    public void EndArray()
    {
        DecrementIndent();
        AppendIndent();
        _logger.Append("]");
        _logger.AppendLine();
    }
        
    /// <summary>
    /// Starts an object on the logger with the given name
    /// </summary>
    /// <param name="name"></param>
    public void StartObject(string name)
    {
        if (!string.IsNullOrEmpty(name))
        {
            AppendFieldPrefix(name);
            _logger.AppendLine();
        }

        AppendIndent();
        _logger.AppendLine("{");
        IncrementIndent();
    }

    /// <summary>
    /// Ends an object on the logger
    /// </summary>
    public void EndObject()
    {
        DecrementIndent();
        AppendIndent();
        _logger.Append("}");
        _logger.AppendLine();
    }

    /// <summary>
    /// Returns the logged data as a string
    /// </summary>
    /// <returns></returns>
    public override string ToString() => _logger.ToStringAndFree();
}