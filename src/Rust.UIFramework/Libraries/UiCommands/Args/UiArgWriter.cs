using System;
using System.Runtime.CompilerServices;
using System.Text;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

public readonly ref struct UiArgWriter(StringBuilder sb)
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(bool value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(bool? value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(byte value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(byte? value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(sbyte value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(sbyte? value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(short value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(short? value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(ushort value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(ushort? value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(int value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(int? value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(uint value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(uint? value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(long value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(in long? value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(ulong value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(in ulong? value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(float value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(in float? value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(double value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(in double? value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(decimal value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(in decimal? value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(DateTime value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(in DateTime? value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(DateTimeOffset value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(in DateTimeOffset? value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(TimeSpan value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(in TimeSpan? value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(NetworkableId value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(in NetworkableId? value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(char value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }
    
    public void AppendArg(in char? value)
    {
        AppendSpace();
        sb.AppendSpan(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(string value) => AppendArg(value.AsSpan());
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendArg(in ReadOnlySpan<char> span)
    {
        AppendSpace();
        sb.Append('"');
        sb.Append(span);
        sb.Append('"');
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void AppendSpace()
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