
namespace Oxide.Ext.UiFramework.Benchmarks;
#if BENCHMARKS

using System;
using System.IO;
using Facepunch;
using Network;
using UnityEngine;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Helpers;
using SilentOrbit.ProtocolBuffers;
using UnityEngine.Assertions;

internal class BenchmarkNetWrite : Stream, Pool.IPooled, IStreamWriter
{
    private static readonly MemoryStream StringBuffer = new();
    private BufferStream _stream;
    public int RefCount = 1;

    public void AddReference() => Interlocked.Increment(ref RefCount);

    public void RemoveReference()
    {
        if (Interlocked.Decrement(ref RefCount) == 0)
        {
            BenchmarkNetWrite netWrite = this;
            Pool.Free(ref netWrite);
        }
    }

    public void EnterPool()
    {
        _stream?.Dispose();
        _stream = null;
    }

    public void LeavePool() => RefCount = 1;
    
    public bool Start(uint size)
    {
        byte[] buffer = RentBuffer((int)BitOperations.RoundUpToPowerOf2(size));
        _stream = Pool.Get<BufferStream>().Initialize(buffer, -1);
        return true;
    }
    
    private static byte[] RentBuffer(int minSize)
    {
        return minSize > BufferStream.Shared.MaximumPooledSize ? new byte[minSize] : BufferStream.Shared.ArrayPool.Rent(minSize);
    }

    private static void ReturnBuffer(byte[] buffer)
    {
        if (buffer == null || buffer.Length > BufferStream.Shared.MaximumPooledSize)
            return;
        BufferStream.Shared.ArrayPool.Return(buffer);
    }

    public void Send(SendInfo info) { }

    public void SendImmediate(SendInfo info) { }

    public (byte[] Buffer, int Length) GetBuffer()
    {
        ArraySegment<byte> buffer = _stream.GetBuffer();
        Assert.IsNotNull(buffer.Array, "buffer.Array != null");
        Assert.IsTrue(buffer.Offset == 0, "buffer.Offset == 0");
        return (buffer.Array, buffer.Count);
    }

    public Span<byte> GetBufferSpan()
    {
        (byte[] Buffer, int Length) buffer = GetBuffer();
        return new Span<byte>(buffer.Buffer, 0, buffer.Length);
    }

    public byte PeekPacketID()
    {
        ArraySegment<byte> buffer = _stream.GetBuffer();
        return buffer.Array == null || buffer.Count <= 0 ? (byte) 0 : buffer.Array[buffer.Offset];
    }

    public void UInt8(byte val) => Write(in val);
    public void UInt16(ushort val) => Write(in val);
    public void UInt32(uint val) => Write(in val);
    public void UInt64(ulong val) => Write(in val);
    public void Int8(sbyte val) => Write(in val);
    public void Int16(short val) => Write(in val);
    public void Int32(int val) => Write(in val);
    public void Int64(long val) => Write(in val);
    public void Bool(bool val) => Write(val ? (byte) 1 : (byte) 0);
    public void Float(float val) => Write(in val);
    public void Double(double val) => Write(in val);
    public void Bytes(byte[] val) => Write(val, 0, val.Length);
    public void VarUInt32(uint val) => ProtocolParser.WriteUInt32(_stream, val);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteUInt32(uint value, bool variableLength)
    {
        if (variableLength)
            VarUInt32(value);
        else
            UInt32(value);
    }

    public void String(string val, bool variableLength = false)
    {
        if (string.IsNullOrEmpty(val))
        {
            BytesWithSize((MemoryStream) null, variableLength);
        }
        else
        {
            if (StringBuffer.Capacity < val.Length * 8)
                StringBuffer.Capacity = val.Length * 8;
            StringBuffer.Position = 0L;
            StringBuffer.SetLength(StringBuffer.Capacity);
            int bytes = Encoding.UTF8.GetBytes(val, 0, val.Length, StringBuffer.GetBuffer(), 0);
            StringBuffer.SetLength(bytes);
            BytesWithSize(StringBuffer, variableLength);
        }
    }

    public void BytesWithSize(MemoryStream val, bool variableLength = false)
    {
        if (val == null || val.Length == 0L)
            WriteUInt32(0U, variableLength);
        else
            BytesWithSize(val.GetBuffer(), (int) val.Length, variableLength);
    }

    public void BytesWithSize(byte[] b, bool variableLength = false)
    {
        BytesWithSize(b, b.Length, variableLength);
    }

    public void BytesWithSize(byte[] b, int length, bool variableLength = false)
    {
        if (b == null || b.Length == 0 || length == 0)
            WriteUInt32(0U, variableLength);
        else if ((uint) length > 10485760U /*0xA00000*/)
        {
            WriteUInt32(0U, variableLength);
            Debug.LogError("BytesWithSize: Too big " + length);
        }
        else
        {
            WriteUInt32((uint) length, variableLength);
            Write(b, 0, length);
        }
    }

    private void Write<T>(in T val) where T : unmanaged => _stream.Write(val);

    public override bool CanSeek => false;
    public override bool CanRead => false;
    public override bool CanWrite => true;
    public override long Length => _stream.Position;

    public override long Position
    {
        get => _stream.Position;
        set => _stream.Position = (int) value;
    }

    public override void Flush() { }
    public override int Read(byte[] buffer, int offset, int count) => throw new NotSupportedException();
    public override int ReadByte() => throw new NotSupportedException();

    public override void Write(byte[] buffer, int offset, int count)
    {
        Span<byte> segment = _stream.GetBuffer().AsSpan(_stream.Position, count);
        new Span<byte>(buffer, offset, count).CopyTo(segment);
    }

    public override void WriteByte(byte value) => UInt8(value);
    public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
    public override void SetLength(long value) => throw new NotSupportedException();
}
#endif