namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

public interface IArgWriter;

public interface IArgWriter<in T> : IArgWriter
{
    void Write(UiArgWriter writer, T arg);
}