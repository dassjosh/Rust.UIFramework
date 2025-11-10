namespace Oxide.Ext.UiFramework.Libraries;

internal class BaseCommandBuilder(ICommandBuilderData data, int argIndex = 0, string partialArgs = null)
{
    protected readonly ICommandBuilderData Data = data;

    protected ArgWriterIterator StartBuilding()
    {
        UiArgWriter argWriter = new();
        argWriter.Append(partialArgs);
        ArgWriterIterator iterator = new(argWriter, Data.Writers, argIndex);
        return iterator;
    }
    
    protected string FinishBuilding(ArgWriterIterator writerIterator)
    {
        UiArgWriter writer = writerIterator.Writer;
        Data.Protection?.ProtectCommand(ref writer);
        writer.Insert(Data.Command);
        return writer.ToString();
    }
}