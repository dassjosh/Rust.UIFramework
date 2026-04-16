namespace Oxide.Ext.UiFramework.Libraries;

internal class BaseCommandBuilder(ICommandBuilderData data, PartialArgs? partial)
{
    protected readonly ICommandBuilderData Data = data;

    protected ArgWriterIterator StartBuilding()
    {
        UiArgWriter argWriter = new();
        int index = 0;
        if (partial.HasValue)
        {
            argWriter.Append(partial.Value.Args);
            index = partial.Value.Index;
        }
        
        ArgWriterIterator iterator = new(argWriter, Data.Writers, index);
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