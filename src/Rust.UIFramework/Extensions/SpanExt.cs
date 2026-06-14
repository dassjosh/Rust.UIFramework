using System;
using System.Threading;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Extensions;

/// <summary>
/// <see cref="Span{T}"/> Extension Methods
/// </summary>
public static class SpanExt
{
    private static readonly ThreadLocal<char[]> Buffer = new(() => new char[128]);

    public delegate bool TryParseDelegate<T>(ReadOnlySpan<char> input, ReadOnlySpan<char> token, out ReadOnlySpan<char> remaining, out T parsed);
    public delegate T ParseDelegate<out T>(ReadOnlySpan<char> input, ReadOnlySpan<char> token, out ReadOnlySpan<char> remaining);

    /// <param name="input">Input string</param>
    extension(ReadOnlySpan<char> input)
    {
        public bool IsEmptyOrWhitespace => input.IsEmpty || input.IsWhiteSpace();
        
        /// <summary>
        /// Parses the next string from the input splitting on the token
        /// </summary>
        /// <param name="token">Token to split on</param>
        /// <param name="remaining">Remaining text of the span</param>
        /// <param name="parsed">The parsed string</param>
        /// <returns>True if successfully parsed; false otherwise</returns>
        public bool TryParseNextString(ReadOnlySpan<char> token, out ReadOnlySpan<char> remaining, out ReadOnlySpan<char> parsed)
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

        public bool TryParseNextFloat(ReadOnlySpan<char> token, out ReadOnlySpan<char> remaining, out float parsed)
        {
            parsed = default;
            return input.TryParseNextString(token, out remaining, out ReadOnlySpan<char> parsedStr) && float.TryParse(parsedStr, out parsed);
        }

        public bool TryParseNextInt(ReadOnlySpan<char> token, out ReadOnlySpan<char> remaining, out int parsed)
        {
            parsed = default;
            return input.TryParseNextString(token, out remaining, out ReadOnlySpan<char> parsedStr) && int.TryParse(parsedStr, out parsed);
        }

        public bool TryParseNextLong(ReadOnlySpan<char> token, out ReadOnlySpan<char> remaining, out long parsed)
        {
            parsed = default;
            return input.TryParseNextString(token, out remaining, out ReadOnlySpan<char> parsedStr) && long.TryParse(parsedStr, out parsed);
        }

        public bool TryParseNextUlong(ReadOnlySpan<char> token, out ReadOnlySpan<char> remaining, out ulong parsed)
        {
            parsed = default;
            return input.TryParseNextString(token, out remaining, out ReadOnlySpan<char> parsedStr) && ulong.TryParse(parsedStr, out parsed);
        }

        public void ParseNextString(ReadOnlySpan<char> token, out ReadOnlySpan<char> result, out ReadOnlySpan<char> remaining)
        {
            if (input.Length == 0)
            {
                throw new IndexOutOfRangeException();
            }

            int end = input.IndexOf(token);
            if (end == -1)
            {
                remaining = ReadOnlySpan<char>.Empty;
                result = input;
                return;
            }

            remaining = input[(end + token.Length)..];
            result = input[..end];
        }

        public float ParseNextFloat(ReadOnlySpan<char> token, out ReadOnlySpan<char> remaining)
        {
            input.ParseNextString(token, out remaining, out ReadOnlySpan<char> parsed);
            return float.Parse(parsed);
        }

        public int ParseNextInt(ReadOnlySpan<char> token, out ReadOnlySpan<char> remaining)
        {
            input.ParseNextString(token, out remaining, out ReadOnlySpan<char> parsed);
            return int.Parse(parsed);
        }

        public long ParseNextLong(ReadOnlySpan<char> token, out ReadOnlySpan<char> remaining)
        {
            input.ParseNextString(token, out remaining, out ReadOnlySpan<char> parsed);
            return long.Parse(parsed);
        }

        public ulong ParseNextUlong(ReadOnlySpan<char> token, out ReadOnlySpan<char> remaining)
        {
            input.ParseNextString(token, out remaining, out ReadOnlySpan<char> parsed);
            return ulong.Parse(parsed);
        }
        
        public (float, float) ParseTwoFloats(ReadOnlySpan<char> token) => input.ParseTwo(token, ParseNextFloat);
        public bool TryParseTwoFloats(ReadOnlySpan<char> token, out (float, float) parsed)=> input.TryParse(token, TryParseNextFloat, out parsed);
        public bool TryParseTwoInts(ReadOnlySpan<char> token, out (int, int) parsed) => input.TryParse(token, TryParseNextInt, out parsed);
        public (float, float, float, float) ParseFourFloats(ReadOnlySpan<char> token) => input.ParseFour(token, ParseNextFloat);
        public bool TryParseFourFloats(ReadOnlySpan<char> token, out (float, float, float, float) parsed) => input.TryParse(token, TryParseNextFloat, out parsed);

        public (T, T) ParseTwo<T>(ReadOnlySpan<char> token, ParseDelegate<T> parser)
        {
            T first = parser(input, token, out input);
            T second = parser(input, token, out input);
            return (first, second);
        }

        public (T, T, T, T) ParseFour<T>(ReadOnlySpan<char> token, ParseDelegate<T> parser)
        {
            T first = parser(input, token, out input);
            T second = parser(input, token, out input);
            T third = parser(input, token, out input);
            T forth = parser(input, token, out input);
            return (first, second, third, forth);
        }

        public bool TryParse<T>(ReadOnlySpan<char> token, TryParseDelegate<T> tryParser, out (T, T) parsed)
        {
            bool firstParsed = tryParser(input, token, out input, out T first);
            bool secondParsed = tryParser(input, token, out input, out T second);
            bool success = firstParsed && secondParsed;
            parsed = success ? (first, second) : default;
            return success;
        }

        public bool TryParse<T>(ReadOnlySpan<char> token, TryParseDelegate<T> tryParser, out (T, T, T, T) parsed)
        {
            bool firstParsed = tryParser(input, token, out input, out T first);
            bool secondParsed = tryParser(input, token, out input, out T second);
            bool thirdParsed = tryParser(input, token, out input, out T third);
            bool fourthParsed = tryParser(input, token, out input, out T fourth);
            bool success = firstParsed && secondParsed && thirdParsed && fourthParsed;
            parsed = success ? (first, second, third, fourth) : default;
            return success;
        }
    }

    extension(Span<char> span)
    {
        public void Write(float value, out Span<char> remaining)
        {
            value.TryFormat(span, out int charsWritten);
            remaining = span[charsWritten..];
        }

        public void Write(int value, out Span<char> remaining)
        {
            value.TryFormat(span, out int charsWritten);
            remaining = span[charsWritten..];
        }

        public void Write(long value, out Span<char> remaining)
        {
            value.TryFormat(span, out int charsWritten);
            remaining = span[charsWritten..];
        }

        public void Write(ulong value, out Span<char> remaining)
        {
            value.TryFormat(span, out int charsWritten);
            remaining = span[charsWritten..];
        }

        public void Write(char value, out Span<char> remaining)
        {
            span[0] = value;
            remaining = span[1..];
        }
    }

    public static void WriteSpace(Span<char> span, out Span<char> remaining) => span.Write(' ', out remaining);

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
    
    /// <summary>
    /// Tries to write the formatted values to out span
    /// </summary>
    /// <param name="value">Value to be formatted</param>
    /// <param name="written">Span the format is written to</param>
    /// <returns>true if the format was successful; false otherwise</returns>
    public static bool TryFormat(this UiColor value, out ReadOnlySpan<char> written)
    {
        Span<char> span = Buffer.Value.AsSpan();
        ReadOnlySpan<char> format = "X2".AsSpan();
        
        value.Red.TryFormat(span, out int _, format);
        value.Green.TryFormat(span[2..], out int _, format);
        value.Blue.TryFormat(span[4..], out int _, format);
        if (value.Alpha == byte.MaxValue)
        {
            written = span[..6];
            return true;
        }

        value.Alpha.TryFormat(span[6..], out int _, format);
        written = span[..8];
        return true;
    }
    
    /// <summary>
    /// Tries to write the formatted values to out span
    /// </summary>
    /// <param name="value">Value to be formatted</param>
    /// <param name="written">Span the format is written to</param>
    /// <returns>true if the format was successful; false otherwise</returns>
    public static bool TryFormat(this in UiPadding value, out ReadOnlySpan<char> written)
    {
        Span<char> span = Buffer.Value.AsSpan();
        Span<char> original = span;

        span.Write(value.Left, out span);
        span.Write(' ', out span);
        span.Write(value.Top, out span);
        span.Write(' ', out span);
        span.Write(value.Right, out span);
        span.Write(' ', out span);
        span.Write(value.Bottom, out span);
        
        written = original[..^span.Length];
        return true;
    }
    
    /// <summary>
    /// Tries to write the formatted values to out span
    /// </summary>
    /// <param name="value">Value to be formatted</param>
    /// <param name="written">Span the format is written to</param>
    /// <returns>true if the format was successful; false otherwise</returns>
    public static bool TryFormat(this UiScale value, out ReadOnlySpan<char> written)
    {
        Span<char> span = Buffer.Value.AsSpan();
        Span<char> original = span;

        span.Write(value.Horizontal, out span);
        span.Write(' ', out span);
        span.Write(value.Vertical, out span);
        written = original[..^span.Length];
        return true;
    }
    
    /// <summary>
    /// Tries to write the formatted values to out span
    /// </summary>
    /// <param name="value">Value to be formatted</param>
    /// <param name="written">Span the format is written to</param>
    /// <returns>true if the format was successful; false otherwise</returns>
    public static bool TryFormat(this in UiBorderWidth value, out ReadOnlySpan<char> written)
    {
        Span<char> span = Buffer.Value.AsSpan();
        Span<char> original = span;

        span.Write(value.Left, out span);
        span.Write(' ', out span);
        span.Write(value.Top, out span);
        span.Write(' ', out span);
        span.Write(value.Right, out span);
        span.Write(' ', out span);
        span.Write(value.Bottom, out span);
        
        written = original[..^span.Length];
        return true;
    }
}