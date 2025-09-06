using System;
using System.Runtime.CompilerServices;
using System.Text;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Libraries;

namespace Oxide.Ext.UiFramework.Extensions;

public static class StringBuilderExt
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AppendSpan(this StringBuilder sb, byte value)
    {
        if (value.TryFormat(out ReadOnlySpan<char> written))
        {
            sb.Append(written);
        }
        else
        {
            sb.Append(value);
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AppendSpan(this StringBuilder sb, sbyte value)
    {
        if (value.TryFormat(out ReadOnlySpan<char> written))
        {
            sb.Append(written);
        }
        else
        {
            sb.Append(value);
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AppendSpan(this StringBuilder sb, short value)
    {
        if (value.TryFormat(out ReadOnlySpan<char> written))
        {
            sb.Append(written);
        }
        else
        {
            sb.Append(value);
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AppendSpan(this StringBuilder sb, ushort value)
    {
        if (value.TryFormat(out ReadOnlySpan<char> written))
        {
            sb.Append(written);
        }
        else
        {
            sb.Append(value);
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AppendSpan(this StringBuilder sb, int value)
    {
        if (value.TryFormat(out ReadOnlySpan<char> written))
        {
            sb.Append(written);
        }
        else
        {
            sb.Append(value);
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AppendSpan(this StringBuilder sb, uint value)
    {
        if (value.TryFormat(out ReadOnlySpan<char> written))
        {
            sb.Append(written);
        }
        else
        {
            sb.Append(value);
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AppendSpan(this StringBuilder sb, long value)
    {
        if (value.TryFormat(out ReadOnlySpan<char> written))
        {
            sb.Append(written);
        }
        else
        {
            sb.Append(value);
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AppendSpan(this StringBuilder sb, ulong value)
    {
        if (value.TryFormat(out ReadOnlySpan<char> written))
        {
            sb.Append(written);
        }
        else
        {
            sb.Append(value);
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AppendSpan(this StringBuilder sb, float value)
    {
        if (value.TryFormat(out ReadOnlySpan<char> written))
        {
            sb.Append(written);
        }
        else
        {
            sb.Append(value);
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AppendSpan(this StringBuilder sb, double value)
    {
        if (value.TryFormat(out ReadOnlySpan<char> written))
        {
            sb.Append(written);
        }
        else
        {
            sb.Append(value);
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AppendSpan(this StringBuilder sb, decimal value)
    {
        if (value.TryFormat(out ReadOnlySpan<char> written))
        {
            sb.Append(written);
        }
        else
        {
            sb.Append(value);
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AppendSpan(this StringBuilder sb, DateTime value)
    {
        if (value.TryFormat(out ReadOnlySpan<char> written))
        {
            sb.Append(written);
        }
        else
        {
            sb.Append(value);
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AppendSpan(this StringBuilder sb, DateTimeOffset value)
    {
        if (value.TryFormat(out ReadOnlySpan<char> written))
        {
            sb.Append(written);
        }
        else
        {
            sb.Append(value);
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AppendSpan(this StringBuilder sb, TimeSpan value)
    {
        if (value.TryFormat(out ReadOnlySpan<char> written))
        {
            sb.Append(written);
        }
        else
        {
            sb.Append(value);
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AppendSpan(this StringBuilder sb, char value)
    {
        sb.Append(value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AppendSpan(this StringBuilder sb, UiColor value)
    {
        if (value.TryFormat(out ReadOnlySpan<char> written))
        {
            sb.Append(written);
        }
        else
        {
            sb.Append(value.ToHexRGBA());
        }
    }
}