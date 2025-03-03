// ReSharper disable CoVariantArrayConversion

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal class CommandBuilder : BaseCommandBuilder, ICommandBuilder
{
    private readonly string _staticCommand;
    
    public CommandBuilder(string command, ICommandProtection protection) : base(command, protection, [])
    {
        _staticCommand = protection switch
        {
            null => command,
            SimpleProtection simple => $"{command} {simple.GetProtectionKey()}",
            _ => null
        };
    }

    public string Build()
    {
        return _staticCommand ?? ProtectCommand(StartBuilding());
    }
}

internal class CommandBuilder<T0>(string command, ICommandProtection protection, IArgWriter[] writers, int argIndex = 0) : BaseCommandBuilder(command, protection, writers, argIndex), ICommandBuilder<T0>
{
    public string Build(T0 arg0)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0);
        return ProtectCommand(writer);
    }
}

internal class CommandBuilder<T0, T1>(string command, ICommandProtection protection, IArgWriter[] writers, int argIndex = 0) : BaseCommandBuilder(command, protection, writers, argIndex), ICommandBuilder<T0, T1>
{
    public string Build(T0 arg0, T1 arg1)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1);
        return ProtectCommand(writer);
    }

    public ICommandBuilder<T1> Partial(T0 arg0)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0);
        return new CommandBuilder<T1>(writer.ToString(), Protection, Writers, writer.Index);
    }
}

internal class CommandBuilder<T0, T1, T2>(string command, ICommandProtection protection, IArgWriter[] writers, int argIndex = 0) : BaseCommandBuilder(command, protection, writers, argIndex), ICommandBuilder<T0, T1, T2>
{
    public string Build(T0 arg0, T1 arg1, T2 arg2)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1, arg2);
        return ProtectCommand(writer);
    }
    
    public ICommandBuilder<T2> Partial(T0 arg0, T1 arg1)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1);
        return new CommandBuilder<T2>(writer.ToString(), Protection, Writers, writer.Index);
    }
    
    public ICommandBuilder<T1, T2> Partial(T0 arg0)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0);
        return new CommandBuilder<T1, T2>(writer.ToString(), Protection, Writers, writer.Index);
    }
}

internal class CommandBuilder<T0, T1, T2, T3>(string command, ICommandProtection protection, IArgWriter[] writers, int argIndex = 0) : BaseCommandBuilder(command, protection, writers, argIndex), ICommandBuilder<T0, T1, T2, T3>
{
    public string Build(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1, arg2, arg3);
        return ProtectCommand(writer);
    }
    
    public ICommandBuilder<T3> Partial(T0 arg0, T1 arg1, T2 arg2)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1, arg2);
        return new CommandBuilder<T3>(writer.ToString(), Protection, Writers, writer.Index);
    }

    public ICommandBuilder<T2, T3> Partial(T0 arg0, T1 arg1)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1);
        return new CommandBuilder<T2, T3>(writer.ToString(), Protection, Writers, writer.Index);
    }
}

internal class CommandBuilder<T0, T1, T2, T3, T4>(string command, ICommandProtection protection, IArgWriter[] writers, int argIndex = 0) : BaseCommandBuilder(command, protection, writers, argIndex), ICommandBuilder<T0, T1, T2, T3, T4>
{
    public string Build(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1, arg2, arg3, arg4);
        return ProtectCommand(writer);
    }
    
    public ICommandBuilder<T4> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1, arg2, arg3);
        return new CommandBuilder<T4>(writer.ToString(), Protection, Writers, writer.Index);
    }
    
    public ICommandBuilder<T3, T4> Partial(T0 arg0, T1 arg1, T2 arg2)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1, arg2);
        return new CommandBuilder<T3, T4>(writer.ToString(), Protection, Writers, writer.Index);
    }
}

internal class CommandBuilder<T0, T1, T2, T3, T4, T5>(string command, ICommandProtection protection, IArgWriter[] writers, int argIndex = 0) : BaseCommandBuilder(command, protection, writers, argIndex), ICommandBuilder<T0, T1, T2, T3, T4, T5>
{
    public string Build(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1, arg2, arg3, arg4, arg5);
        return ProtectCommand(writer);
    }
    
    public ICommandBuilder<T5> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1, arg2, arg3, arg4);
        return new CommandBuilder<T5>(writer.ToString(), Protection, Writers, writer.Index);
    }
    
    public ICommandBuilder<T4, T5> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1, arg2, arg3);
        return new CommandBuilder<T4, T5>(writer.ToString(), Protection, Writers, writer.Index);
    }
}

internal class CommandBuilder<T0, T1, T2, T3, T4, T5, T6>(string command, ICommandProtection protection, IArgWriter[] writers, int argIndex = 0) : BaseCommandBuilder(command, protection, writers, argIndex), ICommandBuilder<T0, T1, T2, T3, T4, T5, T6>
{
    public string Build(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        return ProtectCommand(writer);
    }
    
    public ICommandBuilder<T6> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1, arg2, arg3, arg4, arg5);
        return new CommandBuilder<T6>(writer.ToString(), Protection, Writers, writer.Index);
    }
    
    public ICommandBuilder<T5, T6> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1, arg2, arg3, arg4);
        return new CommandBuilder<T5, T6>(writer.ToString(), Protection, Writers, writer.Index);
    }
}

internal class CommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7>(string command, ICommandProtection protection, IArgWriter[] writers, int argIndex = 0) : BaseCommandBuilder(command, protection, writers, argIndex), ICommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7>
{
    public string Build(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6, T7 arg7)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        return ProtectCommand(writer);
    }
    
    public ICommandBuilder<T7> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        return new CommandBuilder<T7>(writer.ToString(), Protection, Writers, writer.Index);
    }
    
    public ICommandBuilder<T6, T7> Partial(T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
        ArgWriterIterator writer = StartBuilding();
        writer.WriteArgs(arg0, arg1, arg2, arg3, arg4, arg5);
        return new CommandBuilder<T6, T7>(writer.ToString(), Protection, Writers, writer.Index);
    }
}