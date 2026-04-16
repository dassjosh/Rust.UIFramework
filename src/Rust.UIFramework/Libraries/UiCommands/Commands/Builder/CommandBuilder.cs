namespace Oxide.Ext.UiFramework.Libraries;

internal class CommandBuilder : BaseCommandBuilder, ICommandBuilder
{
    private readonly string _staticCommand;
    
    public CommandBuilder(ICommandBuilderData data) : base(data, default)
    {
        _staticCommand = data.Protection switch
        {
            null => data.Command,
            SimpleProtection simple => $"{data.Command} {simple.GetProtectionKey()}",
            _ => null
        };
    }

    public string Build()
    {
        return _staticCommand ?? FinishBuilding(StartBuilding());
    }
}