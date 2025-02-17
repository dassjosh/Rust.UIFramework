using System;
using System.Text;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal ref struct ArgWriterIterator(StringBuilder sb, IArgWriter[] writers)
{
    internal readonly IArgWriter[] Writers = writers;
    internal long ProtectionKey;
    private int _index;

    public ArgWriterIterator(StringBuilder sb, IArgWriter[] writers, long protectionKey) : this(sb, writers)
    {
        ProtectionKey = protectionKey;
    }
    
    public void WriteNext<T>(T arg)
    {
        WriteSpace();
        ((IArgWriter<T>)Writers[_index++]).Write(sb, arg);
    }
    
    public void WriteNext(string arg)
    {
        WriteSpace();
        sb.Append(arg);
    }

    public void WriteNext(ReadOnlySpan<char> span)
    {
        WriteSpace();
        sb.Append(span);
    }

    private void WriteSpace()
    {
        if (sb.Length != 0)
        {
            sb.Append(' ');
        }
    }

    public override string ToString()
    {
        string command = sb.ToString();
        StringBuilderPool.Instance.Free(sb);
        return command;
    }
}