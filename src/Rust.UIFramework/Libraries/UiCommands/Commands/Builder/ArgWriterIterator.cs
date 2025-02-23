using System;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal ref struct ArgWriterIterator(UiArgWriter writer, IArgWriter[] writers)
{
    private readonly UiArgWriter _writer = writer;
    private readonly IArgWriter[] _writers = writers;
    
    internal long ProtectionKey;
    private int _index;

    public ArgWriterIterator(ArgWriterIterator iterator, long protectionKey) : this(new UiArgWriter(StringBuilderPool.Instance.Get()), iterator._writers)
    {
        ProtectionKey = protectionKey;
    }
    
    public void WriteNext<T>(T arg)
    {
        _writer.AppendSpace();
        ((IArgWriter<T>)_writers[_index++]).Write(_writer, arg);
    }

    internal void Write(string arg)
    {
        _writer.Append(arg);
    }
    
    internal void Write(ReadOnlySpan<char> arg)
    {
        _writer.Append(arg);
    }

    public override string ToString() => _writer.ToString();
}