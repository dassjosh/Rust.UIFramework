using System.Text;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal class BaseCommandBuilder(string commandPrefix, IArgWriter[] writers, int argIndex)
{
    protected readonly IArgWriter[] Writers = writers;

    protected virtual ArgWriterIterator StartBuilding()
    {
        StringBuilder sb = StringBuilderPool.Instance.Get();
        sb.Append(commandPrefix);
        UiArgWriter argWriter = new(sb);
        ArgWriterIterator iterator = new(argWriter, Writers, argIndex);
        return iterator;
    }
}