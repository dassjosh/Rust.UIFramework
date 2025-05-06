using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Network;
using Oxide.Ext.UiFramework.Benchmarks;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Json;

public class JsonBinaryWriter : BasePoolable
{
    private const int SegmentSize = 4096;

    private readonly List<SizedArray<byte>> _segments = new(100);
    private short _charIndex;
    private readonly char[] _charBuffer = new char[SegmentSize * 2];

    public void Write(char character)
    {
        _charBuffer[_charIndex] = character;
        _charIndex++;
        if (_charIndex >= SegmentSize)
        {
            Flush();
        }
    }

    public void Write(ReadOnlySpan<char> text)
    {
        int length = text.Length;
        Span<char> buffer = _charBuffer.AsSpan();
        int charIndex = _charIndex;
        for (int i = 0; i < length; i++)
        {
            buffer[charIndex + i] = text[i];
        }
        _charIndex += (short)length;
        if (_charIndex >= SegmentSize)
        {
            Flush();
        }
    }

    private void Flush()
    {
        if (_charIndex == 0)
        {
            return;
        }

        byte[] segment = ArrayPool<byte>.Shared.Rent(SegmentSize * 2);
            
        int size = Encoding.UTF8.GetBytes(_charBuffer, 0, _charIndex, segment, 0);
        _segments.Add(new SizedArray<byte>(segment, size));
        _charIndex = 0;
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

    public void WriteToNetwork(NetWrite write)
    {
        Flush();
        write.UInt32(GetSize());
        WriteToNetwork((Stream)write);
    }

#if BENCHMARKS
    internal void WriteToNetwork(BenchmarkNetWrite write)
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
        
    protected override void LeavePool()
    {

    }

    protected override void EnterPool()
    {
        for (int index = 0; index < _segments.Count; index++)
        {
            byte[] bytes = _segments[index].Array;
            ArrayPool<byte>.Shared.Return(bytes);
        }
        _segments.Clear();
        _charIndex = 0;
    }
}