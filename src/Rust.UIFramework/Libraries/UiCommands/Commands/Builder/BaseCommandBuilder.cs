namespace Oxide.Ext.UiFramework.Libraries;

internal class BaseCommandBuilder(string command, ICommandProtection protection, IArgWriter[] writers, int argIndex = 0, string partialArgs = null)
{
    protected readonly string Command = command;
    protected readonly IArgWriter[] Writers = writers;
    protected readonly ICommandProtection Protection = protection;

    protected ArgWriterIterator StartBuilding()
    {
        UiArgWriter argWriter = new(UiPool.Internal.GetStringBuilder());
        argWriter.Append(partialArgs);
        ArgWriterIterator iterator = new(argWriter, Writers, argIndex);
        return iterator;
    }
    
    protected string FinishBuilding(ArgWriterIterator writerIterator)
    {
        UiArgWriter writer = writerIterator.Writer;
        Protection?.ProtectCommand(ref writer);
        writer.Insert(Command);
        return writer.ToString();
    }
}