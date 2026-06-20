using System;
using Facepunch;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

internal ref struct ArgReaderIterator(IArgReader[] readers)
{
    private int _index;
    public T ParseNext<T>(ref UiCommandTokenizer args)
    {
        IArgReader<T> reader = (IArgReader<T>)readers[_index++];
        UiStringView arg = reader.IsInputArg() ? args.ReadToEnd() : args.GetNext();
        T value = reader.Read(arg);
        return value;
    }
}