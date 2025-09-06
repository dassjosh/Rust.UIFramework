using System;
using System.Runtime.CompilerServices;
using System.Text;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Extensions;

namespace Oxide.Ext.UiFramework.Libraries;

public readonly ref struct UiArgWriter(StringBuilder sb)
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(bool value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(bool? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(byte value) => sb.AppendSpan(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(byte? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(sbyte value) => sb.AppendSpan(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(sbyte? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(short value) => sb.AppendSpan(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(short? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(ushort value) => sb.AppendSpan(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(ushort? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(int value) => sb.AppendSpan(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(int? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(uint value) => sb.AppendSpan(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(uint? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(long value) => sb.AppendSpan(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(in long? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(ulong value) => sb.AppendSpan(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(in ulong? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(float value) => sb.AppendSpan(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(in float? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(double value) => sb.AppendSpan(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(in double? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(decimal value) => sb.AppendSpan(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(in decimal? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(DateTime value) => sb.AppendSpan(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(in DateTime? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(DateTimeOffset value) => sb.AppendSpan(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(in DateTimeOffset? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(TimeSpan value) => sb.AppendSpan(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(in TimeSpan? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(NetworkableId value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(in NetworkableId? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(char value) => sb.AppendSpan(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(in char? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(string value) => sb.Append(value);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(ReadOnlySpan<char> value) => sb.Append(value);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(UiColor color) => sb.AppendSpan(color);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(UiColor? color) => sb.AppendArg(color);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendStartQuote() => sb.Append(UiCommands.StartQuote);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendEndQuote() => sb.Append(UiCommands.EndQuote);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendNull() => sb.Append(UiCommands.NullArg);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal void AppendSpace()
    {
        if (sb.Length != 0)
        {
            sb.Append(' ');
        }
    }

    internal void Insert(string value) => Insert(value.AsSpan());
    internal void Insert(ReadOnlySpan<char> value)
    {
        sb.Insert(0, ' ');
        sb.Insert(0, value);
    }
    
    public override string ToString()
    {
        return UiPool.Internal.ToStringAndFree(sb);
    }
}