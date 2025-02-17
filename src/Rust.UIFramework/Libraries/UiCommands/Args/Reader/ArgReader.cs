using System;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal delegate T ReadFunc<out T>(ReadOnlySpan<char> arg);

internal class ArgReader<T>(ReadFunc<T> func) : IArgReader<T>
{
    public T Read(ReadOnlySpan<char> arg) => func(arg);
}

