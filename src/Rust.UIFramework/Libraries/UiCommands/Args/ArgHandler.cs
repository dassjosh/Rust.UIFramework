using System;
using System.Text;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal delegate T ReadFunc<out T>(ReadOnlySpan<char> arg);
internal delegate void WriteFunc<in T>(StringBuilder sb, T arg);

internal class ArgHandler<T>(ReadFunc<T> reader, WriteFunc<T> writer) : IArgHandler<T>
{
    public T Read(ReadOnlySpan<char> arg) => reader(arg);

    public void Write(StringBuilder sb, T arg) => writer(sb, arg);
}