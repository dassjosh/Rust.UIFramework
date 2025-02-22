using System.Text;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal abstract class BaseCommandBuilder(CommandId command, ICommandProtection protection, IArgWriter[] writers)
{
    private readonly string _commandPrefix = $"{UiCommands.UiCommandName} {command.Id}";

    protected ArgWriterIterator StartBuilding()
    {
        StringBuilder sb = StringBuilderPool.Instance.Get();
        sb.Append(_commandPrefix);
        UiArgWriter argWriter = new(sb);
        ArgWriterIterator iterator = new(argWriter, writers);
        if (protection is not null)
        {
            iterator = protection.StartWriteProtection(iterator);
        }
        return iterator;
    }
    
    protected string FinishBuilding(ArgWriterIterator writer)
    {
        return protection is not null ? protection.FinishWriteProtection(writer) : writer.ToString();
    }
}