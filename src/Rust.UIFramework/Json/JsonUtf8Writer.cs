using System;
using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Network;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Json;

public sealed class JsonUtf8Writer
{
    /// <summary>
    /// Size of the segments used to store the UTF8 JSON string
    /// </summary>
    private const int SegmentSize = 4096;

    private readonly List<SizedArray<byte>> _segments = [];
    
    private int _byteIndex;
    private uint _size;
    private byte[] _buffer;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(byte value)
    {
        CheckForFlush(Utf8Sizes.Sbyte);
        Utf8Formatter.TryFormat(value, _buffer.AsSpan(_byteIndex), out int written);
        _byteIndex += written;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(sbyte value)
    {
        CheckForFlush(Utf8Sizes.Sbyte);
        Utf8Formatter.TryFormat(value, _buffer.AsSpan(_byteIndex), out int written);
        _byteIndex += written;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(short value)
    {
        CheckForFlush(Utf8Sizes.Short);
        Utf8Formatter.TryFormat(value, _buffer.AsSpan(_byteIndex), out int written);
        _byteIndex += written;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(int value)
    {
        CheckForFlush(Utf8Sizes.Int);
        Utf8Formatter.TryFormat(value, _buffer.AsSpan(_byteIndex), out int written);
        _byteIndex += written;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(long value)
    {
        CheckForFlush(Utf8Sizes.Long);
        Utf8Formatter.TryFormat(value, _buffer.AsSpan(_byteIndex), out int written);
        _byteIndex += written;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(ushort value)
    {
        CheckForFlush(Utf8Sizes.UShort);
        Utf8Formatter.TryFormat(value, _buffer.AsSpan(_byteIndex), out int written);
        _byteIndex += written;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(uint value)
    {
        CheckForFlush(Utf8Sizes.UInt);
        Utf8Formatter.TryFormat(value, _buffer.AsSpan(_byteIndex), out int written);
        _byteIndex += written;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(ulong value)
    {
        CheckForFlush(Utf8Sizes.ULong);
        Utf8Formatter.TryFormat(value, _buffer.AsSpan(_byteIndex), out int written);
        _byteIndex += written;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(float value)
    {
        CheckForFlush(Utf8Sizes.Float);
        int written = Utf8Formatter.Format(value, _buffer.AsSpan(_byteIndex));
        _byteIndex += written;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(float value, int maxFractionDigits)
    {
        CheckForFlush(Utf8Sizes.Float);
        int written = Utf8Formatter.Format(value, maxFractionDigits, _buffer.AsSpan(_byteIndex));
        _byteIndex += written;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(byte[] text)
    {
        int length = text.Length;
        CheckForFlush(length);
        
        byte[] buffer = _buffer;
        int byteIndex = _byteIndex;
        for (int i = 0; i < length; i++)
        {
            buffer[byteIndex++] = text[i];
        }
        
        _byteIndex = byteIndex;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteChar(byte character)
    {
        CheckForFlush(1);
        _buffer[_byteIndex++] = character;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteChar(char character)
    {
        CheckForFlush(Utf8Sizes.Char);
        _byteIndex += Utf8Formatter.FormatChar(character, _buffer.AsSpan(_byteIndex));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteChar(char highSurrogate, char lowSurrogate)
    {
        CheckForFlush(Utf8Sizes.SurrogateChar);
        _byteIndex += Utf8Formatter.FormatChar(highSurrogate, lowSurrogate, _buffer.AsSpan(_byteIndex));
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(Utf8String text) => Write(text.String);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(string text) => Write(text.AsSpan());
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(ReadOnlySpan<char> text)
    {
        CheckForFlush(text.Length * Utf8Sizes.Char);
        int written = Encoding.UTF8.GetBytes(text, _buffer.AsSpan(_byteIndex));
        _byteIndex += written;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void CheckForFlush(int addedBytes)
    {
        if (_byteIndex + addedBytes >= SegmentSize)
        {
            Flush();
        }
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void Flush(int rentSize = SegmentSize)
    {
        if (_byteIndex == 0)
        {
            return;
        }
        
        _size += (uint)_byteIndex;
        _segments.Add(new SizedArray<byte>(_buffer, _byteIndex));
        _byteIndex = 0;
        _buffer = ArrayPool<byte>.Shared.Rent(rentSize);
    }

    public int WriteToArray(byte[] bytes)
    {
        Flush();
        if (bytes.Length < _size) throw new ArgumentOutOfRangeException(nameof(bytes), $"bytes.length is less than the size, {bytes.Length} < {_size}");
        int writeIndex = 0;
        for (int i = 0; i < _segments.Count; i++)
        {
            SizedArray<byte> segment = _segments[i];
            Buffer.BlockCopy(segment.Array, 0, bytes, writeIndex, segment.Size);
            writeIndex += segment.Size;
        }

        return writeIndex;
    }

    public void WriteToStream(Stream stream)
    {
        Flush();
        for (int i = 0; i < _segments.Count; i++)
        {
            SizedArray<byte> segment = _segments[i];
            stream.Write(segment.Array, 0, segment.Size);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteToNetwork(NetWrite write)
    {
        Flush();
        write.UInt32(_size);
        WriteToNetwork((Stream)write);
    }

#if BENCHMARKS
    internal void WriteToNetwork(Benchmarks.BenchmarkNetWrite write)
    {
        Flush();
        WriteToNetwork((Stream)write);
    }
#endif

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void WriteToNetwork(Stream write)
    {
        int count = _segments.Count;
        for (int i = 0; i < count; i++)
        {
            SizedArray<byte> segment = _segments[i];
            write.Write(segment.Array, 0, segment.Size);
        }
    }

    public byte[] ToArray()
    {
        Flush();
        byte[] bytes = new byte[_size];
        WriteToArray(bytes);
        return bytes;
    }

    public override string ToString()
    {
        return Encoding.UTF8.GetString(ToArray());
    }

    internal void Init()
    {
        _buffer = ArrayPool<byte>.Shared.Rent(SegmentSize);
    }

    internal void Reset()
    {
        for (int index = 0; index < _segments.Count; index++)
        {
            byte[] bytes = _segments[index].Array;
            ArrayPool<byte>.Shared.Return(bytes);
        }
        _segments.Clear();
        _byteIndex = 0;
        _size = 0;
        ArrayPool<byte>.Shared.Return(_buffer);
        _buffer = null;
    }

    public void WriteRaw(ReadOnlySpan<byte> span)
    {
        Flush(span.Length);
        if (span.TryCopyTo(_buffer))
        {
            _byteIndex += span.Length;
        }
        Flush();
    }
}