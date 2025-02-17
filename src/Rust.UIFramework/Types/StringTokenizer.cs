using System;

namespace Oxide.Ext.UiFramework.Types
{
    public ref struct StringTokenizer
    {
        private ReadOnlySpan<char> _remaining;
        private readonly ReadOnlySpan<char> _token;
        private readonly int _maxLength;
        public int Index { get; private set; }

        public ReadOnlySpan<char> Current { get; private set; }

        public StringTokenizer(string str, string token) : this(str, token, str.Length) { }

        public StringTokenizer(string str, string token, int maxLength)
        {
            _remaining = str;
            _token = token;
            _maxLength = maxLength;
            Index = -1;
            Current = default;
        }

        public bool MoveNext()
        {
            if (_remaining.Length == 0)
            {
                return false;
            }
            
            int index = _remaining.IndexOf(_token);
            if (index == -1 || index > _maxLength)
            {
                index = _remaining.Length;
            }
            
            if (index == 0)
            {
                _remaining = _remaining[1..];
                return MoveNext();
            }

            Current = _remaining[..index];
            _remaining = _remaining[Math.Min(_remaining.Length, index + 1)..];
            Index++;
            return true;
        }
    }
}