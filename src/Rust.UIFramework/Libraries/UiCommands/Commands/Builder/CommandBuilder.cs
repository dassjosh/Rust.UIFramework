using Oxide.Ext.UiFramework.Plugins;
// ReSharper disable CoVariantArrayConversion

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal class CommandBuilder : BaseCoreCommandBuilder, ICommandBuilder
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

internal class CommandBuilder<T0>(PluginId pluginId, CommandId command, ICommandProtection protection) : BaseCoreCommandBuilder(command, protection, ArgCreator.CreateArgHandler<T0>(pluginId)), ICommandBuilder<T0>
{
    public string Build(T0 arg0)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0);
        return FinishBuilding(writer);
    }
}

internal class CommandBuilder<T0, T1>(PluginId pluginId, CommandId command, ICommandProtection protection) : BaseCoreCommandBuilder(command, protection, ArgCreator.CreateArgHandler<T0, T1>(pluginId)), ICommandBuilder<T0, T1>
{
    public string Build(T0 arg0, T1 arg1)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1);
        return FinishBuilding(writer);
    }

    public IPartialCommand<T1> Partial(T0 arg0)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0);
        return new PartialCommand<T1>(writer.ToString(), Writers, writer.Index);
    }
}

internal class CommandBuilder<T0, T1, T2>(PluginId pluginId, CommandId command, ICommandProtection protection) : BaseCoreCommandBuilder(command, protection, ArgCreator.CreateArgHandler<T0, T1, T2>(pluginId)), ICommandBuilder<T0, T1, T2>
{
    public string Build(T0 arg0, T1 arg1, T2 arg2)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1, arg2);
        return FinishBuilding(writer);
    }
    
    public IPartialCommand<T2> Partial(T0 arg0, T1 arg1)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1);
        return new PartialCommand<T2>(writer.ToString(), Writers, writer.Index);
    }
    
    public IPartialCommand<T1, T2> Partial(T0 arg0)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0);
        return new PartialCommand<T1, T2>(writer.ToString(), Writers, writer.Index);
    }
}

internal class CommandBuilder<T0, T1, T2, T3>(PluginId pluginId, CommandId command, ICommandProtection protection) : BaseCoreCommandBuilder(command, protection, ArgCreator.CreateArgHandler<T0, T1, T2, T3>(pluginId)), ICommandBuilder<T0, T1, T2, T3>
{
    public string Build(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1, arg2, arg3);
        return FinishBuilding(writer);
    }
    
    public IPartialCommand<T3> Partial(T0 arg0, T1 arg1, T2 arg2)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1, arg2);
        return new PartialCommand<T3>(writer.ToString(), Writers, writer.Index);
    }

    public IPartialCommand<T2, T3> Partial(T0 arg0, T1 arg1)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1);
        return new PartialCommand<T2, T3>(writer.ToString(), Writers, writer.Index);
    }
}

internal class CommandBuilder<T0, T1, T2, T3, T4>(PluginId pluginId, CommandId command, ICommandProtection protection) : BaseCoreCommandBuilder(command, protection, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4>(pluginId)), ICommandBuilder<T0, T1, T2, T3, T4>
{
    public string Build(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1, arg2, arg3, arg4);
        return FinishBuilding(writer);
    }
    
    public IPartialCommand<T4> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1, arg2, arg3);
        return new PartialCommand<T4>(writer.ToString(), Writers, writer.Index);
    }
    
    public IPartialCommand<T3, T4> Partial(T0 arg0, T1 arg1, T2 arg2)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1, arg2);
        return new PartialCommand<T3, T4>(writer.ToString(), Writers, writer.Index);
    }
}

internal class CommandBuilder<T0, T1, T2, T3, T4, T5>(PluginId pluginId, CommandId command, ICommandProtection protection) : BaseCoreCommandBuilder(command, protection, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4, T5>(pluginId)), ICommandBuilder<T0, T1, T2, T3, T4, T5>
{
    public string Build(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1, arg2, arg3, arg4, arg5);
        return FinishBuilding(writer);
    }
    
    public IPartialCommand<T5> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1, arg2, arg3, arg4);
        return new PartialCommand<T5>(writer.ToString(), Writers, writer.Index);
    }
    
    public IPartialCommand<T4, T5> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1, arg2, arg3);
        return new PartialCommand<T4, T5>(writer.ToString(), Writers, writer.Index);
    }
}

internal class CommandBuilder<T0, T1, T2, T3, T4, T5, T6>(PluginId pluginId, CommandId command, ICommandProtection protection) : BaseCoreCommandBuilder(command, protection, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4, T5, T6>(pluginId)), ICommandBuilder<T0, T1, T2, T3, T4, T5, T6>
{
    public string Build(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        return FinishBuilding(writer);
    }
    
    public IPartialCommand<T6> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1, arg2, arg3, arg4, arg5);
        return new PartialCommand<T6>(writer.ToString(), Writers, writer.Index);
    }
    
    public IPartialCommand<T5, T6> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1, arg2, arg3, arg4);
        return new PartialCommand<T5, T6>(writer.ToString(), Writers, writer.Index);
    }
}

internal class CommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7>(PluginId pluginId, CommandId command, ICommandProtection protection) : BaseCoreCommandBuilder(command, protection, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4, T5, T6, T7>(pluginId)), ICommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7>
{
    public string Build(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        return FinishBuilding(writer);
    }
    
    public IPartialCommand<T7> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        return new PartialCommand<T7>(writer.ToString(), Writers, writer.Index);
    }
    
    public IPartialCommand<T6, T7> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1, arg2, arg3, arg4, arg5);
        return new PartialCommand<T6, T7>(writer.ToString(), Writers, writer.Index);
    }
}