using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Network;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Json;

public class JsonUtf8Writer : BasePoolable
{
    private const int SegmentSize = 4096;

    private readonly List<SizedArray<byte>> _segments = new(100);
    
    private int _byteIndex;
    private byte[] _buffer;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(byte character)
    {
        if (_byteIndex >= SegmentSize)
        {
            Flush();
        }

        _buffer[_byteIndex++] = character;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(char character)
    {
        if (_byteIndex >= SegmentSize)
        {
            Flush();
        }

        if (character < 127)
        {
            _buffer[_byteIndex++] = (byte)character;
            return;
        }

        byte[] bytes = Utf8CharCache.ToUtf8String(character);
        for (int i = 0; i < bytes.Length; i++)
        {
            _buffer[_byteIndex++] = bytes[i];
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(byte[] text)
    {
        int length = text.Length;
        if (_byteIndex + length >= SegmentSize)
        {
            Flush();
        }
        
        byte[] buffer = _buffer;
        int byteIndex = _byteIndex;
        for (int i = 0; i < length; i++)
        {
            buffer[byteIndex++] = text[i];
        }
        
        _byteIndex = byteIndex;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(ReadOnlySpan<char> text)
    {
        int length = text.Length;
        if (_byteIndex + length * 2 >= SegmentSize)
        {
            Flush();
        }
        
        byte[] buffer = _buffer;
        int byteIndex = _byteIndex;
        for (int i = 0; i < length; i++)
        {
            char character = text[i];
            if (character < 127)
            {
                buffer[byteIndex++] = (byte)character;
                continue;
            }

            byte[] bytes = Utf8CharCache.ToUtf8String(character);
            for (int j = 0; j < bytes.Length; j++)
            {
                buffer[byteIndex++] = bytes[i];
            }
        }
        
        _byteIndex = byteIndex;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void Flush()
    {
        if (_byteIndex == 0)
        {
            return;
        }
        
        _segments.Add(new SizedArray<byte>(_buffer, _byteIndex));
        _byteIndex = 0;
        _buffer = ArrayPool<byte>.Shared.Rent(SegmentSize);
    }

    public int WriteToArray(byte[] bytes)
    {
        Flush();
        int writeIndex = 0;
        for (int i = 0; i < _segments.Count; i++)
        {
            SizedArray<byte> segment = _segments[i];
            Buffer.BlockCopy(segment.Array, 0, bytes, writeIndex, segment.Size);
            writeIndex += segment.Size;
        }

        return writeIndex;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public uint GetSize()
    {
        uint size = 0;
        int count = _segments.Count;
        for (int i = 0; i < count; i++)
        {
            size += (uint)_segments[i].Size;
        }

        return size;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteToNetwork(NetWrite write)
    {
        Flush();
        write.UInt32(GetSize());
        WriteToNetwork((Stream)write);
    }

#if BENCHMARKS || DEBUG
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
        byte[] bytes = new byte[GetSize()];
        WriteToArray(bytes);
        return bytes;
    }

    public override string ToString()
    {
        return Encoding.UTF8.GetString(ToArray());
    }

    protected override void LeavePool()
    {
        _buffer = ArrayPool<byte>.Shared.Rent(SegmentSize);
    }

    protected override void EnterPool()
    {
        for (int index = 0; index < _segments.Count; index++)
        {
            byte[] bytes = _segments[index].Array;
            ArrayPool<byte>.Shared.Return(bytes);
        }
        _segments.Clear();
        _byteIndex = 0;
        ArrayPool<byte>.Shared.Return(_buffer);
        _buffer = null;
        //_charIndex = 0;
    }
}