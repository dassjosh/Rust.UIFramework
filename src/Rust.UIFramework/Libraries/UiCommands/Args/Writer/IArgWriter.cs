using System.Text;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

public interface IArgWriter;

public interface IArgWriter<in T> : IArgWriter
{
    void Write(StringBuilder sb, T arg);
}