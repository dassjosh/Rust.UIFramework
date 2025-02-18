using System;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

public interface IArgReader;

public interface IArgReader<out T> : IArgReader
{
    T Read(ReadOnlySpan<char> arg);
}