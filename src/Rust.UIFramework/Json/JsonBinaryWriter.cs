using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;
using Network;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Json
{
    public class JsonBinaryWriter : BasePoolable
    {
        private const int SegmentSize = 2048;

        private List<SizedArray<byte>> _segments;
        private int _charIndex;
        private int _size;
        private char[] _charBuffer;

        public void Write(char character)
        {
            _charBuffer[_charIndex] = character;
            _charIndex++;
            if (_charIndex >= SegmentSize)
            {
                Flush();
            }
        }

        public void Write(string text)
        {
            int length = text.Length;
            char[] buffer = _charBuffer;
            int charIndex = _charIndex;
            for (int i = 0; i < length; i++)
            {
                buffer[charIndex + i] = text[i];
            }
            _charIndex += length;
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
            _size += size;
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

            return _size;
        }

        public void WriteToNetwork(NetWrite write)
        {
            Flush();
            write.UInt32((uint)_size);
            for (int i = 0; i < _segments.Count; i++)
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
        
        protected override void LeavePool()
        {
            _segments = UiFrameworkPool.GetList<SizedArray<byte>>();
            if (_segments.Capacity < 100)
            {
                _segments.Capacity = 100;
            }
            _charBuffer = ArrayPool<char>.Shared.Rent(SegmentSize * 2);
        }

        protected override void EnterPool()
        {
            for (int index = 0; index < _segments.Count; index++)
            {
                byte[] bytes = _segments[index].Array;
                ArrayPool<byte>.Shared.Return(bytes);
            }
            
            ArrayPool<char>.Shared.Return(_charBuffer);
            UiFrameworkPool.FreeList(_segments);
            _charBuffer = null;
            _size = 0;
            _charIndex = 0;
        }
    }
}