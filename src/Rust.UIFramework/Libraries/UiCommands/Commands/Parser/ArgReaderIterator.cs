using System;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal ref struct ArgReaderIterator(IArgReader[] readers)
{
    private int _index;
    public T ParseNext<T>(ReadOnlySpan<char> arg) => ((IArgReader<T>)readers[_index++]).Read(arg);
}