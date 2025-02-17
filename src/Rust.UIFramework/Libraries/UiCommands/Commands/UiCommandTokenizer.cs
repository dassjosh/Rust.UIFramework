using System;
using Oxide.Ext.UiFramework.Exceptions.UiCommands;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal ref struct UiCommandTokenizer(string str)
{
    private ReadOnlySpan<char> _remaining = str;

    public ReadOnlySpan<char> GetNext()
    {
        ReadOnlySpan<char> remaining = _remaining;
        if (remaining.Length == 0)
        {
            throw new FailedToParseArgumentException();
        }

        int index;
        if (remaining[0] == '"')
        {
            remaining = remaining[1..];
            index = remaining.IndexOf('"');
        }
        else
        {
            index = remaining.IndexOf(' ');
        }

        if (index == -1)
        {
            index = remaining.Length;
        }
            
        if (index == 0)
        {
            _remaining = remaining[1..];
            return GetNext();
        }
        
        _remaining = remaining[Math.Min(remaining.Length, index + 1)..];
        return remaining[..index];
    }
}