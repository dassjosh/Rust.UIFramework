using System;
using System.Runtime.CompilerServices;
using System.Text;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Libraries;

public readonly ref struct UiArgWriter(StringBuilder sb)
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(bool value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(bool? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(byte value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(byte? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(sbyte value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(sbyte? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(short value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(short? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(ushort value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(ushort? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(int value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(int? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(uint value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(uint? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(long value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(in long? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(ulong value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(in ulong? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(float value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(in float? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(double value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(in double? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(decimal value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(in decimal? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(DateTime value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(in DateTime? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(DateTimeOffset value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(in DateTimeOffset? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(TimeSpan value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(in TimeSpan? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(NetworkableId value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(in NetworkableId? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(char value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(in char? value) => sb.AppendArg(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(string value) => sb.Append(value);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(ReadOnlySpan<char> value) => sb.Append(value);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(UiColor color) => sb.AppendArg(color);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(UiColor? color) => sb.AppendArg(color);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendQuote() => sb.Append('"');

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
    
    public override string ToString()
    {
        string command = sb.ToString();
        StringBuilderPool.Instance.Free(sb);
        return command;
    }
}