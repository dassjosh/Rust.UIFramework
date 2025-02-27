using System;

namespace Oxide.Ext.UiFramework.Types;

public ref struct StringTokenizer(string str, string token, int maxLength)
{
    private ReadOnlySpan<char> _remaining = str;
    private readonly ReadOnlySpan<char> _token = token;
    public int Index { get; private set; } = -1;

    public ReadOnlySpan<char> Current { get; private set; } = default;

    public StringTokenizer(string str, string token) : this(str, token, str.Length) { }

    public bool MoveNext()
    {
        if (_remaining.Length == 0)
        {
            return false;
        }
            
        int index = _remaining.IndexOf(_token);
        if (index == -1 || index > maxLength)
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