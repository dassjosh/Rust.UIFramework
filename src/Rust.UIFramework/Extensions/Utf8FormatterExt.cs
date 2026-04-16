using System;
using System.Buffers.Text;
using System.Runtime.CompilerServices;

namespace Oxide.Ext.UiFramework.Extensions;

public static class Utf8FormatterExt
{
    // Default max precision for a single-precision float in general format
    private const int DefaultMaxFractionalDigits = 6; 
    
    extension(Utf8Formatter)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetCharSize(char c)
        {
            if (c <= 0x7F)
            {
                return 1;
            }

            if (c <= 0x7FF)
            {
                return 2;
            }

            return 3;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FormatChar(char c, Span<byte> buffer)
        {
            if (c <= 0x7F)
            {
                buffer[0] = (byte)c;
                return 1;
            }

            if (c <= 0x7FF)
            {
                buffer[0] = (byte)(0xC0 | (c >> 6));
                buffer[1] = (byte)(0x80 | (c & 0x3F));
                return 2;
            }

            buffer[0] = (byte)(0xE0 | (c >> 12));
            buffer[1] = (byte)(0x80 | ((c >> 6) & 0x3F));
            buffer[2] = (byte)(0x80 | (c & 0x3F));
            return 3;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int FormatChar(char highSurrogate, char lowSurrogate, Span<byte> buffer)
        {
            int codePoint = 0x10000 + (((highSurrogate - 0xD800) << 10) | (lowSurrogate - 0xDC00));
            
            buffer[0] = (byte)(0xF0 | (codePoint >> 18));
            buffer[1] = (byte)(0x80 | ((codePoint >> 12) & 0x3F));
            buffer[2] = (byte)(0x80 | ((codePoint >> 6) & 0x3F));
            buffer[3] = (byte)(0x80 | (codePoint & 0x3F));

            return 4;
        }
        
        public static int Format(float value, Span<byte> buffer)
        {
            return FormatInternal(value, DefaultMaxFractionalDigits, buffer);
        }
        
        public static int Format(float value, int maxFractionalDigits, Span<byte> buffer)
        {
            return FormatInternal(value, maxFractionalDigits, buffer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int FormatInternal(float value, int maxFractionalDigits, Span<byte> buffer)
        {
            int written = 0;

            // Handle zero - check bit pattern to catch both +0 and -0
            if (value == 0f)
            {
                buffer[0] = (byte)'0';
                return 1;
            }
    
            // Handle negative
            if (value < 0)
            {
                buffer[written++] = (byte)'-';
                value = -value; 
            }
    
            // Fast path for 1.0
            if (value == 1f)
            {
                buffer[written++] = (byte)'1';
                return written;
            }

            // Write integer part
            long integerPart = (long)value;
            float fractionalPart = value - integerPart;

            if (integerPart == 0)
            {
                buffer[written++] = (byte)'0';
            }
            else
            {
                written += WriteLongUtf8(integerPart, buffer[written..]);
            }

            // Early exit if no fractional part
            if (fractionalPart == 0f || maxFractionalDigits <= 0)
            {
                return written;
            }
    
            buffer[written++] = (byte)'.';
            int fractionalStart = written;
            
            const float epsilon = 1e-7f; // Float precision threshold

            int i;
            for (i = 0; i < maxFractionalDigits; i++)
            {
                fractionalPart *= 10.0f;
                int digit = (int)fractionalPart; 
        
                buffer[written++] = (byte)('0' + digit);
                fractionalPart -= digit;
        
                // Early exit if remainder is negligible
                if (fractionalPart < epsilon)
                {
                    break;
                }
            }

            if (i == maxFractionalDigits)
            {
                int currentIndex = written - 1;
                fractionalPart *= 10.0f;
                int digit = (int)fractionalPart;
                if (digit >= 5)
                {
                    while (true)
                    {
                        byte c = buffer[currentIndex];
                        if (c != (byte)'9')
                        {
                            buffer[currentIndex] = (byte)(c + 1);
                            break;
                        }
                        
                        buffer[currentIndex--] = (byte)'0';
                    }

                    written = currentIndex + 1;
                }
            }

            // Truncate trailing zeros - work backwards
            while (written > fractionalStart && buffer[written - 1] == (byte)'0')
            {
                written--;
            }

            // If all fractional digits were zero, remove the decimal point
            if (written == fractionalStart)
            {
                written--;
            }

            return written;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int WriteLongUtf8(long value, Span<byte> buffer)
        {
            // Fast paths for common cases
            if (value < 10)
            {
                buffer[0] = (byte)('0' + value);
                return 1;
            }
            
            if (value < 1000)
            {
                if (value < 100)
                {
                    buffer[1] = (byte)('0' + value % 10);
                    buffer[0] = (byte)('0' + value / 10);
                    return 2;
                }

                buffer[2] = (byte)('0' + value % 10);
                value /= 10;
                buffer[1] = (byte)('0' + value % 10);
                buffer[0] = (byte)('0' + value / 10);
                return 3;
            }

            // For larger numbers, calculate length first
            int length = 0;
            long temp = value;
            do
            {
                temp /= 10;
                length++;
            } while (temp > 0);

            // Write digits backwards
            int pos = length - 1;
            do
            {
                long quotient = value / 10;
                buffer[pos--] = (byte)('0' + (value - quotient * 10));
                value = quotient;
            } while (value > 0);

            return length;
        }
    }
}