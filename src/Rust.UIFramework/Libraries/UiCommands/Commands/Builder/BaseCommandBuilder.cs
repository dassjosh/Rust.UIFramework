using System.Text;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Libraries;

internal class BaseCommandBuilder(string command, ICommandProtection protection, IArgWriter[] writers, int argIndex = 0)
{
    protected readonly IArgWriter[] Writers = writers;
    protected readonly ICommandProtection Protection = protection;

    protected ArgWriterIterator StartBuilding()
    {
        StringBuilder sb = StringBuilderPool.Instance.Get();
        sb.Append(command);
        UiArgWriter argWriter = new(sb);
        ArgWriterIterator iterator = new(argWriter, Writers, argIndex);
        return iterator;
    }
    
    protected string ProtectCommand(ArgWriterIterator writer) => Protection?.ProtectCommand(writer) ?? writer.ToString();
}