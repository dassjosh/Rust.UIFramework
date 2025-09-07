using System;
using System.Runtime.CompilerServices;
using System.Text;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Libraries;

namespace Oxide.Ext.UiFramework.Extensions;

internal static class StringBuilderArgExt
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, in byte? value)
    {
        if (!value.HasValue)
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.AppendSpan(value.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, in sbyte? value)
    {
        if (!value.HasValue)
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.AppendSpan(value.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, in short? value)
    {
        if (!value.HasValue)
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.AppendSpan(value.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, in ushort? value)
    {
        if (!value.HasValue)
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.AppendSpan(value.Value);
    }
    
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, in int? value)
    {
        if (!value.HasValue)
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.AppendSpan(value.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, in uint? value)
    {
        if (!value.HasValue)
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.AppendSpan(value.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, in long? value)
    {
        if (!value.HasValue)
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.AppendSpan(value.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, in ulong? value)
    {
        if (!value.HasValue)
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.AppendSpan(value.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, in float? value)
    {
        if (!value.HasValue)
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.AppendSpan(value.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, in double? value)
    {
        if (!value.HasValue)
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.AppendSpan(value.Value);
    }
    
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, in decimal? value)
    {
        if (!value.HasValue)
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.AppendSpan(value.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AppendArg(this StringBuilder sb, string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.Append(value);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, bool value)
    {
        sb.Append(value ? '1' : '0');
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
    internal static void AppendArg(this StringBuilder sb, in DateTime? value)
    {
        if (!value.HasValue)
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.AppendSpan(value.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, in DateTimeOffset? value)
    {
        if (!value.HasValue)
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.AppendSpan(value.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, in TimeSpan? value)
    {
        if (!value.HasValue)
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.AppendSpan(value.Value);
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
        
        sb.AppendSpan(value.Value);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static void AppendArg(this StringBuilder sb, in UiColor? value)
    {
        if (!value.HasValue)
        {
            sb.Append(UiCommands.NullArg);
            return;
        }
        
        sb.AppendSpan(value.Value);
    }
}