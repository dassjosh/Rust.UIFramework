using System;
using System.Buffers.Text;
using System.Runtime.CompilerServices;

namespace Oxide.Ext.UiFramework.Extensions;

public static class Utf8FormatterExt
{
    extension(Utf8Formatter)
    {
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
    }
}