using System;
using System.Threading;

namespace Oxide.Ext.UiFramework.Extensions
{
    /// <summary>
    /// <see cref="Span{T}"/> Extension Methods
    /// </summary>
    public static class SpanExt
    {
        private static readonly ThreadLocal<char[]> Buffer = new(() => new char[128]);
        
        /// <summary>
        /// Parses the next string from the input splitting on the token
        /// </summary>
        /// <param name="input">Input string</param>
        /// <param name="token">Token to split on</param>
        /// <param name="remaining">Remaining text of the span</param>
        /// <param name="parsed">The parsed string</param>
        /// <returns>True if successfully parsed; false otherwise</returns>
        public static bool TryParseNextString(this ReadOnlySpan<char> input, ReadOnlySpan<char> token, out ReadOnlySpan<char> remaining, out ReadOnlySpan<char> parsed)
        {
            if (input.Length == 0)
            {
                remaining = ReadOnlySpan<char>.Empty;
                parsed = ReadOnlySpan<char>.Empty;
                return false;
            }

            int end = input.IndexOf(token);
            if (end == -1)
            {
                remaining = ReadOnlySpan<char>.Empty;
                parsed = input;
                return true;
            }

            remaining = input[(end + token.Length)..];
            parsed = input[..end];
            return true;
        }
        
        public static ReadOnlySpan<char> ParseNextString(this ReadOnlySpan<char> input, ReadOnlySpan<char> token, out ReadOnlySpan<char> remaining)
        {
            if (input.Length == 0)
            {
                throw new IndexOutOfRangeException();
            }

            int end = input.IndexOf(token);
            if (end == -1)
            {
                remaining = ReadOnlySpan<char>.Empty;
                return input;
            }

            remaining = input[(end + token.Length)..];
            return input[..end];
        }

        /// <summary>
        /// Tries to write the formatted values to out span
        /// </summary>
        /// <param name="value">Value to be formatted</param>
        /// <param name="written">Span the format is written to</param>
        /// <param name="format">The format to apply to the span</param>
        /// <param name="provider">Formatting provider</param>
        /// <returns>true if the format was successful; false otherwise</returns>
        public static bool TryFormat(this byte value, out ReadOnlySpan<char> written, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
        {
            Span<char> span = Buffer.Value.AsSpan();
            if (value.TryFormat(span, out int charsWritten, format, provider))
            {
                written = span[..charsWritten];
                return true;
            }

            written = default;
            return false;
        }
        
        /// <summary>
        /// Tries to write the formatted values to out span
        /// </summary>
        /// <param name="value">Value to be formatted</param>
        /// <param name="written">Span the format is written to</param>
        /// <param name="format">The format to apply to the span</param>
        /// <param name="provider">Formatting provider</param>
        /// <returns>true if the format was successful; false otherwise</returns>
        public static bool TryFormat(this sbyte value, out ReadOnlySpan<char> written, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
        {
            Span<char> span = Buffer.Value.AsSpan();
            if (value.TryFormat(span, out int charsWritten, format, provider))
            {
                written = span[..charsWritten];
                return true;
            }

            written = default;
            return false;
        }
        
        /// <summary>
        /// Tries to write the formatted values to out span
        /// </summary>
        /// <param name="value">Value to be formatted</param>
        /// <param name="written">Span the format is written to</param>
        /// <param name="format">The format to apply to the span</param>
        /// <param name="provider">Formatting provider</param>
        /// <returns>true if the format was successful; false otherwise</returns>
        public static bool TryFormat(this short value, out ReadOnlySpan<char> written, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
        {
            Span<char> span = Buffer.Value.AsSpan();
            if (value.TryFormat(span, out int charsWritten, format, provider))
            {
                written = span[..charsWritten];
                return true;
            }

            written = default;
            return false;
        }
        
        /// <summary>
        /// Tries to write the formatted values to out span
        /// </summary>
        /// <param name="value">Value to be formatted</param>
        /// <param name="written">Span the format is written to</param>
        /// <param name="format">The format to apply to the span</param>
        /// <param name="provider">Formatting provider</param>
        /// <returns>true if the format was successful; false otherwise</returns>
        public static bool TryFormat(this ushort value, out ReadOnlySpan<char> written, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
        {
            Span<char> span = Buffer.Value.AsSpan();
            if (value.TryFormat(span, out int charsWritten, format, provider))
            {
                written = span[..charsWritten];
                return true;
            }

            written = default;
            return false;
        }
        
        /// <summary>
        /// Tries to write the formatted values to out span
        /// </summary>
        /// <param name="value">Value to be formatted</param>
        /// <param name="written">Span the format is written to</param>
        /// <param name="format">The format to apply to the span</param>
        /// <param name="provider">Formatting provider</param>
        /// <returns>true if the format was successful; false otherwise</returns>
        public static bool TryFormat(this int value, out ReadOnlySpan<char> written, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
        {
            Span<char> span = Buffer.Value.AsSpan();
            if (value.TryFormat(span, out int charsWritten, format, provider))
            {
                written = span[..charsWritten];
                return true;
            }

            written = default;
            return false;
        }
        
        /// <summary>
        /// Tries to write the formatted values to out span
        /// </summary>
        /// <param name="value">Value to be formatted</param>
        /// <param name="written">Span the format is written to</param>
        /// <param name="format">The format to apply to the span</param>
        /// <param name="provider">Formatting provider</param>
        /// <returns>true if the format was successful; false otherwise</returns>
        public static bool TryFormat(this uint value, out ReadOnlySpan<char> written, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
        {
            Span<char> span = Buffer.Value.AsSpan();
            if (value.TryFormat(span, out int charsWritten, format, provider))
            {
                written = span[..charsWritten];
                return true;
            }

            written = default;
            return false;
        }
        
        /// <summary>
        /// Tries to write the formatted values to out span
        /// </summary>
        /// <param name="value">Value to be formatted</param>
        /// <param name="written">Span the format is written to</param>
        /// <param name="format">The format to apply to the span</param>
        /// <param name="provider">Formatting provider</param>
        /// <returns>true if the format was successful; false otherwise</returns>
        public static bool TryFormat(this long value, out ReadOnlySpan<char> written, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
        {
            Span<char> span = Buffer.Value.AsSpan();
            if (value.TryFormat(span, out int charsWritten, format, provider))
            {
                written = span[..charsWritten];
                return true;
            }

            written = default;
            return false;
        }
        
        /// <summary>
        /// Tries to write the formatted values to out span
        /// </summary>
        /// <param name="value">Value to be formatted</param>
        /// <param name="written">Span the format is written to</param>
        /// <param name="format">The format to apply to the span</param>
        /// <param name="provider">Formatting provider</param>
        /// <returns>true if the format was successful; false otherwise</returns>
        public static bool TryFormat(this ulong value, out ReadOnlySpan<char> written, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
        {
            Span<char> span = Buffer.Value.AsSpan();
            if (value.TryFormat(span, out int charsWritten, format, provider))
            {
                written = span[..charsWritten];
                return true;
            }

            written = default;
            return false;
        }
        
        /// <summary>
        /// Tries to write the formatted values to out span
        /// </summary>
        /// <param name="value">Value to be formatted</param>
        /// <param name="written">Span the format is written to</param>
        /// <param name="format">The format to apply to the span</param>
        /// <param name="provider">Formatting provider</param>
        /// <returns>true if the format was successful; false otherwise</returns>
        public static bool TryFormat(this decimal value, out ReadOnlySpan<char> written, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
        {
            Span<char> span = Buffer.Value.AsSpan();
            if (value.TryFormat(span, out int charsWritten, format, provider))
            {
                written = span[..charsWritten];
                return true;
            }

            written = default;
            return false;
        }
        
        /// <summary>
        /// Tries to write the formatted values to out span
        /// </summary>
        /// <param name="value">Value to be formatted</param>
        /// <param name="written">Span the format is written to</param>
        /// <param name="format">The format to apply to the span</param>
        /// <param name="provider">Formatting provider</param>
        /// <returns>true if the format was successful; false otherwise</returns>
        public static bool TryFormat(this float value, out ReadOnlySpan<char> written, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
        {
            Span<char> span = Buffer.Value.AsSpan();
            if (value.TryFormat(span, out int charsWritten, format, provider))
            {
                written = span[..charsWritten];
                return true;
            }

            written = default;
            return false;
        }
        
        /// <summary>
        /// Tries to write the formatted values to out span
        /// </summary>
        /// <param name="value">Value to be formatted</param>
        /// <param name="written">Span the format is written to</param>
        /// <param name="format">The format to apply to the span</param>
        /// <param name="provider">Formatting provider</param>
        /// <returns>true if the format was successful; false otherwise</returns>
        public static bool TryFormat(this double value, out ReadOnlySpan<char> written, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
        {
            Span<char> span = Buffer.Value.AsSpan();
            if (value.TryFormat(span, out int charsWritten, format, provider))
            {
                written = span[..charsWritten];
                return true;
            }

            written = default;
            return false;
        }
        
        /// <summary>
        /// Tries to write the formatted values to out span
        /// </summary>
        /// <param name="value">Value to be formatted</param>
        /// <param name="written">Span the format is written to</param>
        /// <param name="format">The format to apply to the span</param>
        /// <param name="provider">Formatting provider</param>
        /// <returns>true if the format was successful; false otherwise</returns>
        public static bool TryFormat(this DateTime value, out ReadOnlySpan<char> written, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
        {
            Span<char> span = Buffer.Value.AsSpan();
            if (value.TryFormat(span, out int charsWritten, format, provider))
            {
                written = span[..charsWritten];
                return true;
            }

            written = default;
            return false;
        }
        
        /// <summary>
        /// Tries to write the formatted values to out span
        /// </summary>
        /// <param name="value">Value to be formatted</param>
        /// <param name="written">Span the format is written to</param>
        /// <param name="format">The format to apply to the span</param>
        /// <param name="provider">Formatting provider</param>
        /// <returns>true if the format was successful; false otherwise</returns>
        public static bool TryFormat(this DateTimeOffset value, out ReadOnlySpan<char> written, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
        {
            Span<char> span = Buffer.Value.AsSpan();
            if (value.TryFormat(span, out int charsWritten, format, provider))
            {
                written = span[..charsWritten];
                return true;
            }

            written = default;
            return false;
        }
        
        /// <summary>
        /// Tries to write the formatted values to out span
        /// </summary>
        /// <param name="value">Value to be formatted</param>
        /// <param name="written">Span the format is written to</param>
        /// <param name="format">The format to apply to the span</param>
        /// <param name="provider">Formatting provider</param>
        /// <returns>true if the format was successful; false otherwise</returns>
        public static bool TryFormat(this TimeSpan value, out ReadOnlySpan<char> written, ReadOnlySpan<char> format = default, IFormatProvider provider = null)
        {
            Span<char> span = Buffer.Value.AsSpan();
            if (value.TryFormat(span, out int charsWritten, format, provider))
            {
                written = span[..charsWritten];
                return true;
            }

            written = default;
            return false;
        }
    }
}