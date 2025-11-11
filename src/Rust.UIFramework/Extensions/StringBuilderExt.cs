using System;
using System.Runtime.CompilerServices;
using System.Text;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Extensions;

public static class StringBuilderExt
{
    extension(StringBuilder sb)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendSpan(byte value)
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
        public void AppendSpan(sbyte value)
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
        public void AppendSpan(short value)
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
        public void AppendSpan(ushort value)
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
        public void AppendSpan(int value)
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
        public void AppendSpan(uint value)
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
        public void AppendSpan(long value)
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
        public void AppendSpan(ulong value)
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
        public void AppendSpan(float value)
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
        public void AppendSpan(double value)
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
        public void AppendSpan(decimal value)
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
        public void AppendSpan(DateTime value)
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
        public void AppendSpan(DateTimeOffset value)
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
        public void AppendSpan(TimeSpan value)
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
        public void AppendSpan(char value)
        {
            sb.Append(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendSpan(UiColor value)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendSpan(UiRotation value)
        {
            if (value.Rotation.TryFormat(out ReadOnlySpan<char> written))
            {
                sb.Append(written);
            }
            else
            {
                sb.Append(value.Rotation);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AppendSpan(in UiPadding value)
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
        public void AppendSpan(UiScale value)
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
        public void AppendSpan(in UiBorderWidth value)
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
    }
}