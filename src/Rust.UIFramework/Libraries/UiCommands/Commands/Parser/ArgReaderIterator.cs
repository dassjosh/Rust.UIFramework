using System;
using Oxide.Core;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal ref struct ArgReaderIterator(IArgReader[] readers)
{
    private int _index;
    public T ParseNext<T>(ref UiCommandTokenizer args)
    {
        ReadOnlySpan<char> arg = args.GetNext();
        T value = ((IArgReader<T>)readers[_index++]).Read(arg);
        return value;
    }
}