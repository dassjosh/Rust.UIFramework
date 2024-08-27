using System;
using System.IO;
using Facepunch;
using Network;
using UnityEngine;

#if BENCHMARKS
namespace Oxide.Ext.UiFramework.Benchmarks;

internal class BenchmarkNetWrite : Stream, Pool.IPooled
{
    public byte[] Data;
    private int _position;
    private int _length;
    
    public override void Flush()
    {
        
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        int num;
        switch (origin)
        {
            case SeekOrigin.Current:
                num = _position + (int) offset;
                break;
            case SeekOrigin.End:
                num = _length + (int) offset;
                break;
            default:
                num = (int) offset;
                break;
        }
        _position = num >= 0 && num <= _length ? num : throw new ArgumentOutOfRangeException(nameof (offset));
        return _position;
    }

    public override void SetLength(long value)
    {
        throw new NotImplementedException();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        int max = _length - _position;
        int num = Mathf.Clamp(count, 0, max);
        Buffer.BlockCopy(Data, _position, buffer, offset, count);
        _position += num;
        return num;
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        EnsureCapacity(count);
        Buffer.BlockCopy(buffer, offset, Data, _position, count);
        _position += count;
        if (_position <= _length)
            return;
        _length = _position;
    }

    public override bool CanRead => true;
    public override bool CanSeek => true;
    public override bool CanWrite  => true;
    public override long Length => _length;
    public override long Position
    {
        get => _position;
        set => Seek(value, SeekOrigin.Begin);
    }
    
    public void EnterPool()
    {
        if (Data == null)
            return;
        BaseNetwork.ArrayPool.Return(Data);
        Data = null;
    }

    public void LeavePool()
    {
        _position = 0;
        _length = 0;
    }
    
    private void EnsureCapacity(int spaceRequired)
    {
        if (Data == null)
        {
            int num = spaceRequired <= 2048 ? 2048 : spaceRequired;
            int minSize = NextPowerOfTwo(num);
            Data = minSize <= 4194304 ? BaseNetwork.ArrayPool.Rent(minSize) : throw new Exception(string.Format("Preventing NetWrite buffer from growing too large (requiredLength={0})", num));
        }
        else
        {
            if (Data.Length - _position >= spaceRequired)
                return;
            int val1 = _position + spaceRequired;
            int minSize = NextPowerOfTwo(Math.Max(val1, Data.Length));
            byte[] dst = minSize <= 4194304 ? BaseNetwork.ArrayPool.Rent(minSize) : throw new Exception(string.Format("Preventing NetWrite buffer from growing too large (requiredLength={0})", val1));
            Buffer.BlockCopy(Data, 0, dst, 0, _length);
            BaseNetwork.ArrayPool.Return(Data);
            Data = dst;
        }
    }
    
    private int NextPowerOfTwo(int value)
    {
        value -= 1;
        value |= value >> 16;
        value |= value >> 16;
        value |= value >> 8;
        value |= value >> 4;
        value |= value >> 2;
        value |= value >> 1;
        return value + 1;
    }
}
#endif