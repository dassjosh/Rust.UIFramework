using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal class CommandBuilder : BaseCommandBuilder, ICommandBuilder
{
    private readonly string _staticCommand;
    
    public CommandBuilder(PluginId pluginId, CommandId command, ICommandProtection protection) : base(command, protection, [])
    {
        _staticCommand = protection switch
        {
            null => $"{UiCommands.UiCommandName} {command.Id}",
            SimpleProtection simple => $"{UiCommands.UiCommandName} {command.Id} {simple.GetProtectionKey()}",
            _ => null
        };
    }

    public string Build()
    {
        return _staticCommand ?? FinishBuilding(StartBuilding());
    }
}

internal class CommandBuilder<T0>(PluginId pluginId, CommandId command, ICommandProtection protection) : BaseCommandBuilder(command, protection, ArgCreator.CreateArgHandler<T0>(pluginId)), ICommandBuilder<T0>
{
    public string Build(T0 arg0)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteNext(arg0);
        return FinishBuilding(writer);
    }
}

internal class CommandBuilder<T0, T1>(PluginId pluginId, CommandId command, ICommandProtection protection) : BaseCommandBuilder(command, protection, ArgCreator.CreateArgHandler<T0, T1>(pluginId)), ICommandBuilder<T0, T1>
{
    public string Build(T0 arg0, T1 arg1)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteNext(arg0);
        writer.WriteNext(arg1);
        return FinishBuilding(writer);
    }
}

internal class CommandBuilder<T0, T1, T2>(PluginId pluginId, CommandId command, ICommandProtection protection) : BaseCommandBuilder(command, protection, ArgCreator.CreateArgHandler<T0, T1, T2>(pluginId)), ICommandBuilder<T0, T1, T2>
{
    public string Build(T0 arg0, T1 arg1, T2 arg2)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteNext(arg0);
        writer.WriteNext(arg1);
        writer.WriteNext(arg2);
        return FinishBuilding(writer);
    }
}

internal class CommandBuilder<T0, T1, T2, T3>(PluginId pluginId, CommandId command, ICommandProtection protection) : BaseCommandBuilder(command, protection, ArgCreator.CreateArgHandler<T0, T1, T2, T3>(pluginId)), ICommandBuilder<T0, T1, T2, T3>
{
    public string Build(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteNext(arg0);
        writer.WriteNext(arg1);
        writer.WriteNext(arg2);
        writer.WriteNext(arg3);
        return FinishBuilding(writer);
    }
}

internal class CommandBuilder<T0, T1, T2, T3, T4>(PluginId pluginId, CommandId command, ICommandProtection protection) : BaseCommandBuilder(command, protection, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4>(pluginId)), ICommandBuilder<T0, T1, T2, T3, T4>
{
    public string Build(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteNext(arg0);
        writer.WriteNext(arg1);
        writer.WriteNext(arg2);
        writer.WriteNext(arg3);
        writer.WriteNext(arg4);
        return FinishBuilding(writer);
    }
}

internal class CommandBuilder<T0, T1, T2, T3, T4, T5>(PluginId pluginId, CommandId command, ICommandProtection protection) : BaseCommandBuilder(command, protection, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4, T5>(pluginId)), ICommandBuilder<T0, T1, T2, T3, T4, T5>
{
    public string Build(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteNext(arg0);
        writer.WriteNext(arg1);
        writer.WriteNext(arg2);
        writer.WriteNext(arg3);
        writer.WriteNext(arg4);
        writer.WriteNext(arg5);
        return FinishBuilding(writer);
    }
}

internal class CommandBuilder<T0, T1, T2, T3, T4, T5, T6>(PluginId pluginId, CommandId command, ICommandProtection protection) : BaseCommandBuilder(command, protection, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4, T5, T6>(pluginId)), ICommandBuilder<T0, T1, T2, T3, T4, T5, T6>
{
    public string Build(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteNext(arg0);
        writer.WriteNext(arg1);
        writer.WriteNext(arg2);
        writer.WriteNext(arg3);
        writer.WriteNext(arg4);
        writer.WriteNext(arg5);
        writer.WriteNext(arg6);
        return FinishBuilding(writer);
    }
}

internal class CommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7>(PluginId pluginId, CommandId command, ICommandProtection protection) : BaseCommandBuilder(command, protection, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4, T5, T6, T7>(pluginId)), ICommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7>
{
    public string Build(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteNext(arg0);
        writer.WriteNext(arg1);
        writer.WriteNext(arg2);
        writer.WriteNext(arg3);
        writer.WriteNext(arg4);
        writer.WriteNext(arg5);
        writer.WriteNext(arg6);
        writer.WriteNext(arg7);
        return FinishBuilding(writer);
    }
}