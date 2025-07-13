namespace Oxide.Ext.UiFramework.Libraries;

internal class BaseCommandBuilder(string command, ICommandProtection protection, IArgWriter[] writers, int argIndex = 0)
{
    protected readonly IArgWriter[] Writers = writers;
    protected readonly ICommandProtection Protection = protection;

    protected ArgWriterIterator StartBuilding()
    {
        UiArgWriter argWriter = new(UiPool.Internal.GetStringBuilder());
        ArgWriterIterator iterator = new(argWriter, Writers, argIndex);
        return iterator;
    }
    
    protected string FinishBuilding(ArgWriterIterator writerIterator)
    {
        UiArgWriter writer = writerIterator.Writer;
        Protection?.ProtectCommand(command, ref writer);
        writer.Insert(command);
        return writer.ToString();
    }
}