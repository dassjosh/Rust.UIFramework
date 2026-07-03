using System;
using Facepunch;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

public ref struct UiCommandTokenizer(string str)
{
    private readonly string _str = str ?? throw new ArgumentNullException(nameof(str));
    private int _pos = 0;

    public UiStringView GetNext()
    {
        if (_pos >= _str.Length)
            throw new FailedToParseArgumentException();

        // Skip leading spaces
        while (_pos < _str.Length && _str[_pos] == ' ')
            _pos++;

        if (_pos >= _str.Length)
            throw new FailedToParseArgumentException();

        // Quoted string
        if (_str[_pos] == UiCommands.StartQuote && _pos + 1 < _str.Length)
        {
            int start = _pos + 1;
            int end = _str.IndexOf(UiCommands.EndQuote, start);

            if (end == -1)
                throw new FailedToParseArgumentException();

            _pos = end + 1; // Move past closing quote
            return new UiStringView(_str, start, end - start);
        }

        // Non-quoted token
        int tokenStart = _pos;
        int spaceIndex = _str.IndexOf(' ', _pos);

        if (spaceIndex == -1)
        {
            _pos = _str.Length;
            return new UiStringView(_str, tokenStart, _str.Length - tokenStart);
        }

        _pos = spaceIndex + 1;
        return new UiStringView(_str, tokenStart, spaceIndex - tokenStart);
    }

    public UiStringView ReadToEnd()
    {
        if (_pos >= _str.Length)
            return new UiStringView(_str, _str.Length, 0);

        int start = _pos;
        _pos = _str.Length;
        return new UiStringView(_str, start, _str.Length - start);
    }
}