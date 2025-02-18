using System;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal ref struct ArgReaderIterator(IArgReader[] readers)
{
    private int _index;
    public T ParseNext<T>(UiCommandTokenizer args) => ((IArgReader<T>)readers[_index++]).Read(args.GetNext());
}