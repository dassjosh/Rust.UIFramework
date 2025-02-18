using System;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal ref struct ArgWriterIterator(UiArgWriter writer, IArgWriter[] writers)
{
    private readonly UiArgWriter _writer = writer;
    internal readonly IArgWriter[] Writers = writers;
    
    internal long ProtectionKey;
    private int _index;

    public ArgWriterIterator(UiArgWriter writer, IArgWriter[] writers, long protectionKey) : this(writer, writers)
    {
        ProtectionKey = protectionKey;
    }
    
    public void WriteNext<T>(T arg)
    {
        ((IArgWriter<T>)Writers[_index++]).Write(_writer, arg);
    }
    
    public void WriteNext(string arg)
    {
        _writer.AppendArg(arg);
    }

    public void WriteNext(ReadOnlySpan<char> span)
    {
        _writer.AppendArg(span);
    }

    public override string ToString() => _writer.ToString();
}