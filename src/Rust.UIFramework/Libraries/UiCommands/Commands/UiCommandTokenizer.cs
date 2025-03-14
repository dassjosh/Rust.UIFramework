using System;
using Oxide.Ext.UiFramework.Exceptions;

namespace Oxide.Ext.UiFramework.Libraries;

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
        //Process quoted strings
        if (remaining[0] == UiCommands.StartQuote && remaining.Length >= 2)
        {
            remaining = remaining[1..];
            index = remaining.IndexOf(UiCommands.EndQuote);
            ReadOnlySpan<char> quoted = remaining[..index];
            _remaining = remaining[Math.Min(remaining.Length, index)..];
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

    public ReadOnlySpan<char> ReadToEnd()
    {
        ReadOnlySpan<char> remaining = _remaining;
        _remaining = ReadOnlySpan<char>.Empty;
        return remaining;
    }
}