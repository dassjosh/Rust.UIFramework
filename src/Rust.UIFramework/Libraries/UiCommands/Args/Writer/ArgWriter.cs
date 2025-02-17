using System;
using System.Text;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal class ArgWriter<T>(Action<StringBuilder, T> writeAction) : IArgWriter<T>
{
    public void Write(StringBuilder sb, T arg) => writeAction.Invoke(sb, arg);
}