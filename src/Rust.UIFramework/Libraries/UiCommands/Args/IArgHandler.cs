using System;

namespace Oxide.Ext.UiFramework.Libraries;

public interface IArgHandler : IArgReader, IArgWriter;
public interface IArgHandler<T> : IArgReader<T>, IArgWriter<T>, IArgHandler;

public interface IArgReader
{
    bool IsInputArg() => false;
}

public interface IArgReader<out T> : IArgReader
{
    T Read(ReadOnlySpan<char> arg);
}

public interface IArgWriter;

public interface IArgWriter<in T> : IArgWriter
{
    void Write(UiArgWriter writer, T arg);
}