using System;
using System.Runtime.CompilerServices;
using System.Text;
using Oxide.Ext.UiFramework.Libraries.UiCommands;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Extensions;

internal static class StringBuilderExt
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, byte value)
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
    internal static void AppendArg(this StringBuilder sb, in byte? value)
    {
        if (!value.HasValue)
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.AppendArg(value.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, sbyte value)
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
    internal static void AppendArg(this StringBuilder sb, in sbyte? value)
    {
        if (!value.HasValue)
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.AppendArg(value.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, short value)
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
    internal static void AppendArg(this StringBuilder sb, in short? value)
    {
        if (!value.HasValue)
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.AppendArg(value.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, ushort value)
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
    internal static void AppendArg(this StringBuilder sb, in ushort? value)
    {
        if (!value.HasValue)
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.AppendArg(value.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, int value)
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
    internal static void AppendArg(this StringBuilder sb, in int? value)
    {
        if (!value.HasValue)
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.AppendArg(value.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, uint value)
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
    internal static void AppendArg(this StringBuilder sb, in uint? value)
    {
        if (!value.HasValue)
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.AppendArg(value.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, long value)
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
    internal static void AppendArg(this StringBuilder sb, in long? value)
    {
        if (!value.HasValue)
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.AppendArg(value.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, ulong value)
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
    internal static void AppendArg(this StringBuilder sb, in ulong? value)
    {
        if (!value.HasValue)
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.AppendArg(value.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, float value)
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
    internal static void AppendArg(this StringBuilder sb, in float? value)
    {
        if (!value.HasValue)
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.AppendArg(value.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, double value)
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
    internal static void AppendArg(this StringBuilder sb, in double? value)
    {
        if (!value.HasValue)
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.AppendArg(value.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, decimal value)
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
    internal static void AppendArg(this StringBuilder sb, in decimal? value)
    {
        if (!value.HasValue)
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.AppendArg(value.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, bool value)
    {
        sb.Append(value ? "True" : "False");
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, in bool? value)
    {
        if (!value.HasValue)
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.AppendArg(value.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.Append(value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, DateTime value)
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
    internal static void AppendArg(this StringBuilder sb, in DateTime? value)
    {
        if (!value.HasValue)
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.AppendArg(value.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, DateTimeOffset value)
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
    internal static void AppendArg(this StringBuilder sb, in DateTimeOffset? value)
    {
        if (!value.HasValue)
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.AppendArg(value.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, TimeSpan value)
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
    internal static void AppendArg(this StringBuilder sb, in TimeSpan? value)
    {
        if (!value.HasValue)
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.AppendArg(value.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, in NetworkableId? value)
    {
        if (!value.HasValue)
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.AppendArg(value.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, in char? value)
    {
        if (!value.HasValue)
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.AppendArg(value.Value);
    }
    
    /// <summary>
    /// Frees a <see cref="StringBuilder"/> back to the pool returning the created <see cref="string"/>
    /// </summary>
    /// <param name="sb"><see cref="StringBuilder"/> with string and being freed</param>
    internal static string ToStringAndFree(this StringBuilder sb)
    {
        string result = sb.ToString();
        UiFrameworkPool.FreeStringBuilder(sb);
        return result;
    }
}