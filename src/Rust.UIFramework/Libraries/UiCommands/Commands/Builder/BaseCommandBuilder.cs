using System.Text;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal abstract class BaseCommandBuilder(PluginCommand command, ICommandProtection protection, IArgWriter[] writers)
{
    private readonly string _commandPrefix = $"{UiCommands.UiCommandName}{command.Plugin} {command.CommandId}";

    protected ArgWriterIterator StartBuilding()
    {
        StringBuilder sb = StringBuilderPool.Instance.Get();
        sb.Append(_commandPrefix);
        ArgWriterIterator writer = new(sb, writers);
        if (protection is not null)
        {
            writer = protection.StartWriteProtection(writer);
        }
        return writer;
    }
    
    protected string FinishBuilding(ArgWriterIterator writer)
    {
        return protection is not null ? protection.FinishWriteProtection(writer) : writer.ToString();
    }
}