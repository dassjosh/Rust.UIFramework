using System;
using Oxide.Ext.UiFramework.Exceptions;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal ref struct UiCommandTokenizer(string str)
{
    private ReadOnlySpan<char> _remaining = str;

    public void SkipNext(int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            GetNext();
        }
    }
    
    public ReadOnlySpan<char> GetNext()
    {
        ReadOnlySpan<char> remaining = _remaining;
        if (remaining.Length == 0) throw new FailedToParseArgumentException();

        int index;
        //Process escaped quotes
        if (remaining[0] == '\\' && remaining.Length >= 4 && remaining[1] == '"')
        {
            remaining = remaining[2..];
            index = remaining.IndexOf('"');
            ReadOnlySpan<char> quoted = remaining[..(index-1)];
            _remaining = remaining[Math.Min(remaining.Length, index + 1)..];
            return quoted;
        }

        index = remaining.IndexOf(' ');
        if (index == -1)
        {
            index = remaining.Length;
        }
        else if (index == 0)
        {
            _remaining = remaining[1..];
            return GetNext();
        }

        _remaining = remaining[Math.Min(remaining.Length, index + 1)..];
        return remaining[..index];
    }
    
    public ReadOnlySpan<char> GetLast()
    {
        ReadOnlySpan<char> remaining = _remaining;
        if (remaining.Length == 0) throw new FailedToParseArgumentException();

        int index = remaining.LastIndexOf(' ');
        if (index != -1)
        {
            _remaining = remaining[..index];
        }
        
        return remaining[(index + 1)..];
    }
}