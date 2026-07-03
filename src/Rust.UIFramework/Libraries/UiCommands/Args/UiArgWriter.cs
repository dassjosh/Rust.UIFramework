using System;
using System.Runtime.CompilerServices;
using System.Text;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Extensions;

namespace Oxide.Ext.UiFramework.Libraries;

public readonly ref struct UiArgWriter()
{
    private readonly StringBuilder _sb = UiPool.Internal.GetStringBuilder();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(bool value) => _sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(bool? value) => _sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(byte value) => _sb.AppendSpan(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(byte? value) => _sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(sbyte value) => _sb.AppendSpan(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(sbyte? value) => _sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(short value) => _sb.AppendSpan(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(short? value) => _sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(ushort value) => _sb.AppendSpan(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(ushort? value) => _sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(int value) => _sb.AppendSpan(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(int? value) => _sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(uint value) => _sb.AppendSpan(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(uint? value) => _sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(long value) => _sb.AppendSpan(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(in long? value) => _sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(ulong value) => _sb.AppendSpan(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(in ulong? value) => _sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(float value) => _sb.AppendSpan(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(in float? value) => _sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(double value) => _sb.AppendSpan(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(in double? value) => _sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(decimal value) => _sb.AppendSpan(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(in decimal? value) => _sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(DateTime value) => _sb.AppendSpan(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(in DateTime? value) => _sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(DateTimeOffset value) => _sb.AppendSpan(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(in DateTimeOffset? value) => _sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(TimeSpan value) => _sb.AppendSpan(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(in TimeSpan? value) => _sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(NetworkableId value) => _sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(in NetworkableId? value) => _sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(char value) => _sb.AppendSpan(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(in char? value) => _sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(string value) => _sb.Append(value);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(ReadOnlySpan<char> value) => _sb.Append(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(ReadOnlyMemory<char> value) => _sb.Append(value);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(UiColor color) => _sb.AppendSpan(color);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(UiColor? color) => _sb.AppendArg(color);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendStartQuote() => _sb.Append(UiCommands.StartQuote);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendEndQuote() => _sb.Append(UiCommands.EndQuote);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendNull() => _sb.Append(UiCommands.NullArg);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void AppendSpace()
    {
        if (_sb.Length != 0)
        {
            _sb.Append(' ');
        }
    }

    internal void Insert(string value) => Insert(value.AsSpan());
    internal void Insert(ReadOnlySpan<char> value)
    {
        if (_sb.Length != 0)
        {
            _sb.Insert(0, ' ');
        }

        _sb.Insert(0, value);
    }
    
    public override string ToString() => UiPool.Internal.ToStringAndFree(_sb);
}