namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal abstract class BaseCoreCommandBuilder(CommandId command, ICommandProtection protection, IArgWriter[] writers) : BaseCommandBuilder($"{UiCommands.UiCommandName} {command.Id}", writers, 0)
{
    protected override ArgWriterIterator StartBuilding()
    {
        ArgWriterIterator iterator = base.StartBuilding();
        if (protection is not null)
        {
            iterator = protection.StartWriteProtection(iterator);
        }
        return iterator;
    }
        
    protected string FinishBuilding(ArgWriterIterator writer) => protection is not null ? protection.FinishWriteProtection(writer) : writer.ToString();
}