using System;
using System.Runtime.CompilerServices;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal delegate T ReadFunc<out T>(ReadOnlySpan<char> arg);
internal delegate void WriteFunc<in T>(UiArgWriter writer, T arg);

internal class ArgHandler<T>(ReadFunc<T> readerFunc, WriteFunc<T> writerFunc) : IArgHandler<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Read(ReadOnlySpan<char> arg) => readerFunc(arg);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Write(UiArgWriter writer, T arg) => writerFunc(writer, arg);
}