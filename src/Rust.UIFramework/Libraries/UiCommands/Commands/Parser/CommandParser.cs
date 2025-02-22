using System;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Extensions;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal class CommandParser(Plugin plugin, Action<BasePlayer> method, ICommandProtection protection, ICooldownHandler cooldown, IPermissionHandler permission) 
    : BaseCommandParser(plugin, method.Method, protection, cooldown, permission, [])
{
    protected override void RunCommandInternal(BasePlayer player, UiCommandTokenizer args)
    {
        try
        {
            method(player);
        }
        catch (Exception ex)
        {
            LogException(ex);
        }
    }
}

internal class CommandParser<T0>(Plugin plugin, Action<BasePlayer, T0> method, ICommandProtection protection, ICooldownHandler cooldown, IPermissionHandler permission)
    : BaseCommandParser(plugin, method.Method, protection, cooldown, permission, ArgCreator.CreateArgHandler<T0>(plugin.Id()))
{
    protected override void RunCommandInternal(BasePlayer player, UiCommandTokenizer args)
    {
        ArgReaderIterator iterator = GetReader();
        T0 arg0 = iterator.ParseNext<T0>(ref args);
        try
        {
            method(player, arg0);
        }
        catch (Exception ex)
        {
            LogException(ex);
        }
    }
}

internal class CommandParser<T0, T1>(Plugin plugin, Action<BasePlayer, T0, T1> method, ICommandProtection protection, ICooldownHandler cooldown, IPermissionHandler permission)
    : BaseCommandParser(plugin, method.Method, protection, cooldown, permission, ArgCreator.CreateArgHandler<T0, T1>(plugin.Id()))
{
    protected override void RunCommandInternal(BasePlayer player, UiCommandTokenizer args)
    {
        ArgReaderIterator iterator = GetReader();
        T0 arg0 = iterator.ParseNext<T0>(ref args);
        T1 arg1 = iterator.ParseNext<T1>(ref args);
        try
        {
            method(player, arg0, arg1);
        }
        catch (Exception ex)
        {
            LogException(ex);
        }
    }
}

internal class CommandParser<T0, T1, T2>(Plugin plugin, Action<BasePlayer, T0, T1, T2> method, ICommandProtection protection, ICooldownHandler cooldown, IPermissionHandler permission)
    : BaseCommandParser(plugin, method.Method, protection, cooldown, permission, ArgCreator.CreateArgHandler<T0, T1, T2>(plugin.Id()))
{
    protected override void RunCommandInternal(BasePlayer player, UiCommandTokenizer args)
    {
        ArgReaderIterator iterator = GetReader();
        T0 arg0 = iterator.ParseNext<T0>(ref args);
        T1 arg1 = iterator.ParseNext<T1>(ref args);
        T2 arg2 = iterator.ParseNext<T2>(ref args);
        try
        {
            method(player, arg0, arg1, arg2);
        }
        catch (Exception ex)
        {
            LogException(ex);
        }
    }
}

internal class CommandParser<T0, T1, T2, T3>(Plugin plugin, Action<BasePlayer, T0, T1, T2, T3> method, ICommandProtection protection, ICooldownHandler cooldown, IPermissionHandler permission)
    : BaseCommandParser(plugin, method.Method, protection, cooldown, permission, ArgCreator.CreateArgHandler<T0, T1, T2, T3>(plugin.Id()))
{
    protected override void RunCommandInternal(BasePlayer player, UiCommandTokenizer args)
    {
        ArgReaderIterator iterator = GetReader();
        T0 arg0 = iterator.ParseNext<T0>(ref args);
        T1 arg1 = iterator.ParseNext<T1>(ref args);
        T2 arg2 = iterator.ParseNext<T2>(ref args);
        T3 arg3 = iterator.ParseNext<T3>(ref args);
        try
        {
            method(player, arg0, arg1, arg2, arg3);
        }
        catch (Exception ex)
        {
            LogException(ex);
        }
    }
}

internal class CommandParser<T0, T1, T2, T3, T4>(Plugin plugin, Action<BasePlayer, T0, T1, T2, T3, T4> method, ICommandProtection protection, ICooldownHandler cooldown, IPermissionHandler permission)
    : BaseCommandParser(plugin, method.Method, protection, cooldown, permission, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4>(plugin.Id()))
{
    protected override void RunCommandInternal(BasePlayer player, UiCommandTokenizer args)
    {
        ArgReaderIterator iterator = GetReader();
        T0 arg0 = iterator.ParseNext<T0>(ref args);
        T1 arg1 = iterator.ParseNext<T1>(ref args);
        T2 arg2 = iterator.ParseNext<T2>(ref args);
        T3 arg3 = iterator.ParseNext<T3>(ref args);
        T4 arg4 = iterator.ParseNext<T4>(ref args);
        try
        {
            method(player, arg0, arg1, arg2, arg3, arg4);
        }
        catch (Exception ex)
        {
            LogException(ex);
        }
    }
}

internal class CommandParser<T0, T1, T2, T3, T4, T5>(Plugin plugin, Action<BasePlayer, T0, T1, T2, T3, T4, T5> method, ICommandProtection protection, ICooldownHandler cooldown, IPermissionHandler permission)
    : BaseCommandParser(plugin, method.Method, protection, cooldown, permission, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4, T5>(plugin.Id()))
{
    protected override void RunCommandInternal(BasePlayer player, UiCommandTokenizer args)
    {
        ArgReaderIterator iterator = GetReader();
        T0 arg0 = iterator.ParseNext<T0>(ref args);
        T1 arg1 = iterator.ParseNext<T1>(ref args);
        T2 arg2 = iterator.ParseNext<T2>(ref args);
        T3 arg3 = iterator.ParseNext<T3>(ref args);
        T4 arg4 = iterator.ParseNext<T4>(ref args);
        T5 arg5 = iterator.ParseNext<T5>(ref args);
        try
        {
            method(player, arg0, arg1, arg2, arg3, arg4, arg5);
        }
        catch (Exception ex)
        {
            LogException(ex);
        }
    }
}

internal class CommandParser<T0, T1, T2, T3, T4, T5, T6>(Plugin plugin, Action<BasePlayer, T0, T1, T2, T3, T4, T5, T6> method, ICommandProtection protection, ICooldownHandler cooldown, IPermissionHandler permission)
    : BaseCommandParser(plugin, method.Method, protection, cooldown, permission, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4, T5, T6>(plugin.Id()))
{
    protected override void RunCommandInternal(BasePlayer player, UiCommandTokenizer args)
    {
        ArgReaderIterator iterator = GetReader();
        T0 arg0 = iterator.ParseNext<T0>(ref args);
        T1 arg1 = iterator.ParseNext<T1>(ref args);
        T2 arg2 = iterator.ParseNext<T2>(ref args);
        T3 arg3 = iterator.ParseNext<T3>(ref args);
        T4 arg4 = iterator.ParseNext<T4>(ref args);
        T5 arg5 = iterator.ParseNext<T5>(ref args);
        T6 arg6 = iterator.ParseNext<T6>(ref args);
        try
        {
            method(player, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
        }
        catch (Exception ex)
        {
            LogException(ex);
        }
    }
}

internal class CommandParser<T0, T1, T2, T3, T4, T5, T6, T7>(Plugin plugin, Action<BasePlayer, T0, T1, T2, T3, T4, T5, T6, T7> method, ICommandProtection protection, ICooldownHandler cooldown, IPermissionHandler permission)
    : BaseCommandParser(plugin, method.Method, protection, cooldown, permission, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4, T5, T6, T7>(plugin.Id()))
{
    protected override void RunCommandInternal(BasePlayer player, UiCommandTokenizer args)
    {
        ArgReaderIterator iterator = GetReader();
        T0 arg0 = iterator.ParseNext<T0>(ref args);
        T1 arg1 = iterator.ParseNext<T1>(ref args);
        T2 arg2 = iterator.ParseNext<T2>(ref args);
        T3 arg3 = iterator.ParseNext<T3>(ref args);
        T4 arg4 = iterator.ParseNext<T4>(ref args);
        T5 arg5 = iterator.ParseNext<T5>(ref args);
        T6 arg6 = iterator.ParseNext<T6>(ref args);
        T7 arg7 = iterator.ParseNext<T7>(ref args);
        try
        {
            method(player, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }
        catch (Exception ex)
        {
            LogException(ex);
        }
    }
}