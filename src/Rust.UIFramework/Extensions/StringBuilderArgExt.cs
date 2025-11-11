using System;
using System.Runtime.CompilerServices;
using System.Text;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Extensions;

internal static class StringBuilderArgExt
{
    extension(StringBuilder sb)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void AppendArg(in byte? value)
        {
            if (!value.HasValue)
            {
                sb.Append(UiCommands.NullArg);
                return;
            }
        
            sb.AppendSpan(value.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void AppendArg(in sbyte? value)
        {
            if (!value.HasValue)
            {
                sb.Append(UiCommands.NullArg);
                return;
            }
        
            sb.AppendSpan(value.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void AppendArg(in short? value)
        {
            if (!value.HasValue)
            {
                sb.Append(UiCommands.NullArg);
                return;
            }
        
            sb.AppendSpan(value.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void AppendArg(in ushort? value)
        {
            if (!value.HasValue)
            {
                sb.Append(UiCommands.NullArg);
                return;
            }
        
            sb.AppendSpan(value.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void AppendArg(in int? value)
        {
            if (!value.HasValue)
            {
                sb.Append(UiCommands.NullArg);
                return;
            }
        
            sb.AppendSpan(value.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void AppendArg(in uint? value)
        {
            if (!value.HasValue)
            {
                sb.Append(UiCommands.NullArg);
                return;
            }
        
            sb.AppendSpan(value.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void AppendArg(in long? value)
        {
            if (!value.HasValue)
            {
                sb.Append(UiCommands.NullArg);
                return;
            }
        
            sb.AppendSpan(value.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void AppendArg(in ulong? value)
        {
            if (!value.HasValue)
            {
                sb.Append(UiCommands.NullArg);
                return;
            }
        
            sb.AppendSpan(value.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void AppendArg(in float? value)
        {
            if (!value.HasValue)
            {
                sb.Append(UiCommands.NullArg);
                return;
            }
        
            sb.AppendSpan(value.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void AppendArg(in double? value)
        {
            if (!value.HasValue)
            {
                sb.Append(UiCommands.NullArg);
                return;
            }
        
            sb.AppendSpan(value.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void AppendArg(in decimal? value)
        {
            if (!value.HasValue)
            {
                sb.Append(UiCommands.NullArg);
                return;
            }
        
            sb.AppendSpan(value.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendArg(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                sb.Append(UiCommands.NullArg);
                return;
            }
        
            sb.Append(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void AppendArg(bool value)
        {
            sb.Append(value ? '1' : '0');
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void AppendArg(in bool? value)
        {
            if (!value.HasValue)
            {
                sb.Append(UiCommands.NullArg);
                return;
            }
        
            sb.AppendArg(value.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void AppendArg(in DateTime? value)
        {
            if (!value.HasValue)
            {
                sb.Append(UiCommands.NullArg);
                return;
            }
        
            sb.AppendSpan(value.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void AppendArg(in DateTimeOffset? value)
        {
            if (!value.HasValue)
            {
                sb.Append(UiCommands.NullArg);
                return;
            }
        
            sb.AppendSpan(value.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void AppendArg(in TimeSpan? value)
        {
            if (!value.HasValue)
            {
                sb.Append(UiCommands.NullArg);
                return;
            }
        
            sb.AppendSpan(value.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void AppendArg(in NetworkableId? value)
        {
            if (!value.HasValue)
            {
                sb.Append(UiCommands.NullArg);
                return;
            }
        
            sb.AppendArg(value.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void AppendArg(in char? value)
        {
            if (!value.HasValue)
            {
                sb.Append(UiCommands.NullArg);
                return;
            }
        
            sb.AppendSpan(value.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void AppendArg(in UiColor? value)
        {
            if (!value.HasValue)
            {
                sb.Append(UiCommands.NullArg);
                return;
            }
        
            sb.AppendSpan(value.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void AppendArg(in UiRotation? value)
        {
            if (!value.HasValue)
            {
                sb.Append(UiCommands.NullArg);
                return;
            }
        
            sb.AppendSpan(value.Value.Rotation);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void AppendArg(in UiPadding? value)
        {
            if (!value.HasValue)
            {
                sb.Append(UiCommands.NullArg);
                return;
            }
        
            sb.AppendSpan(value.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void AppendArg(in UiScale? value)
        {
            if (!value.HasValue)
            {
                sb.Append(UiCommands.NullArg);
                return;
            }
        
            sb.AppendSpan(value.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void AppendArg(in UiBorderWidth? value)
        {
            if (!value.HasValue)
            {
                sb.Append(UiCommands.NullArg);
                return;
            }
        
            sb.AppendSpan(value.Value);
        }
    }
}