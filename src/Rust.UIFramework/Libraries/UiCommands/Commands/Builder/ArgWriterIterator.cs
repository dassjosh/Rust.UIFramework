using System;
using System.Runtime.CompilerServices;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal ref struct ArgWriterIterator(UiArgWriter writer, IArgWriter[] writers, int index = 0)
{
    private readonly UiArgWriter _writer = writer;
    internal int Index { get; private set; } = index;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteArgs<T0>(T0 arg)
    {
        WriteNext(arg);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteArgs<T0, T1>(T0 arg0, T1 arg1)
    {
        WriteArgs(arg0);
        WriteNext(arg1);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteArgs<T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2)
    {
        WriteArgs(arg0, arg1);
        WriteNext(arg2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteArgs<T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
    {
        WriteArgs(arg0, arg1, arg2);
        WriteNext(arg3);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteArgs<T0, T1, T2, T3, T4>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        WriteArgs(arg0, arg1, arg2, arg3);
        WriteNext(arg4);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteArgs<T0, T1, T2, T3, T4, T5>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
        WriteArgs(arg0, arg1, arg2, arg3, arg4);
        WriteNext(arg5);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteArgs<T0, T1, T2, T3, T4, T5, T6>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
    {
        WriteArgs(arg0, arg1, arg2, arg3, arg4, arg5);
        WriteNext(arg6);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteArgs<T0, T1, T2, T3, T4, T5, T6, T7>(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
    {
        WriteArgs(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        WriteNext(arg7);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void WriteNext<T>(T arg)
    {
        _writer.AppendSpace();
        ((IArgWriter<T>)writers[Index++]).Write(_writer, arg);
    }

    internal void Write(string arg)
    {
        _writer.AppendSpace();
        _writer.Append(arg);
    }
    
    internal void Write(ReadOnlySpan<char> arg)
    {
        _writer.AppendSpace();
        _writer.Append(arg);
    }

    public override string ToString() => _writer.ToString();
}