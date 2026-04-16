using System;

namespace Oxide.Ext.UiFramework.Libraries;

internal ref struct ArgReaderIterator(IArgReader[] readers)
{
    private int _index;
    public T ParseNext<T>(ref UiCommandTokenizer args)
    {
        IArgReader<T> reader = (IArgReader<T>)readers[_index++];
        ReadOnlySpan<char> arg = reader.IsInputArg() ? args.ReadToEnd() : args.GetNext();
        T value = reader.Read(arg);
        return value;
    }
}