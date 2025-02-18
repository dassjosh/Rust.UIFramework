using System;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

internal class CommandParser(PluginId pluginId, Action<BasePlayer> method, ICommandProtection protection, ICooldownHandler cooldown, IPermissionHandler permission) 
    : BaseCommandParser(protection, cooldown, permission, [])
{
    protected override void RunCommandInternal(BasePlayer player, UiCommandTokenizer args)
    {
        method(player);
    }
}

internal class CommandParser<T0>(PluginId pluginId, Action<BasePlayer, T0> method, ICommandProtection protection, ICooldownHandler cooldown, IPermissionHandler permission)
    : BaseCommandParser(protection, cooldown, permission, ArgCreator.CreateArgHandler<T0>(pluginId))
{
    protected override void RunCommandInternal(BasePlayer player, UiCommandTokenizer args)
    {
        ArgReaderIterator iterator = GetReader();
        T0 arg0 = iterator.ParseNext<T0>(args.GetNext());
        method(player, arg0);
    }
}

internal class CommandParser<T0, T1>(PluginId pluginId, Action<BasePlayer, T0, T1> method, ICommandProtection protection, ICooldownHandler cooldown, IPermissionHandler permission)
    : BaseCommandParser(protection, cooldown, permission, ArgCreator.CreateArgHandler<T0, T1>(pluginId))
{
    protected override void RunCommandInternal(BasePlayer player, UiCommandTokenizer args)
    {
        ArgReaderIterator iterator = GetReader();
        T0 arg0 = iterator.ParseNext<T0>(args.GetNext());
        T1 arg1 = iterator.ParseNext<T1>(args.GetNext());
        method(player, arg0, arg1);
    }
}

internal class CommandParser<T0, T1, T2>(PluginId pluginId, Action<BasePlayer, T0, T1, T2> method, ICommandProtection protection, ICooldownHandler cooldown, IPermissionHandler permission)
    : BaseCommandParser(protection, cooldown, permission, ArgCreator.CreateArgHandler<T0, T1, T2>(pluginId))
{
    protected override void RunCommandInternal(BasePlayer player, UiCommandTokenizer args)
    {
        ArgReaderIterator iterator = GetReader();
        T0 arg0 = iterator.ParseNext<T0>(args.GetNext());
        T1 arg1 = iterator.ParseNext<T1>(args.GetNext());
        T2 arg2 = iterator.ParseNext<T2>(args.GetNext());
        method(player, arg0, arg1, arg2);
    }
}

internal class CommandParser<T0, T1, T2, T3>(PluginId pluginId, Action<BasePlayer, T0, T1, T2, T3> method, ICommandProtection protection, ICooldownHandler cooldown, IPermissionHandler permission)
    : BaseCommandParser(protection, cooldown, permission, ArgCreator.CreateArgHandler<T0, T1, T2, T3>(pluginId))
{
    protected override void RunCommandInternal(BasePlayer player, UiCommandTokenizer args)
    {
        ArgReaderIterator iterator = GetReader();
        T0 arg0 = iterator.ParseNext<T0>(args.GetNext());
        T1 arg1 = iterator.ParseNext<T1>(args.GetNext());
        T2 arg2 = iterator.ParseNext<T2>(args.GetNext());
        T3 arg3 = iterator.ParseNext<T3>(args.GetNext());
        method(player, arg0, arg1, arg2, arg3);
    }
}

internal class CommandParser<T0, T1, T2, T3, T4>(PluginId pluginId, Action<BasePlayer, T0, T1, T2, T3, T4> method, ICommandProtection protection, ICooldownHandler cooldown, IPermissionHandler permission)
    : BaseCommandParser(protection, cooldown, permission, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4>(pluginId))
{
    protected override void RunCommandInternal(BasePlayer player, UiCommandTokenizer args)
    {
        ArgReaderIterator iterator = GetReader();
        T0 arg0 = iterator.ParseNext<T0>(args.GetNext());
        T1 arg1 = iterator.ParseNext<T1>(args.GetNext());
        T2 arg2 = iterator.ParseNext<T2>(args.GetNext());
        T3 arg3 = iterator.ParseNext<T3>(args.GetNext());
        T4 arg4 = iterator.ParseNext<T4>(args.GetNext());
        method(player, arg0, arg1, arg2, arg3, arg4);
    }
}

internal class CommandParser<T0, T1, T2, T3, T4, T5>(PluginId pluginId, Action<BasePlayer, T0, T1, T2, T3, T4, T5> method, ICommandProtection protection, ICooldownHandler cooldown, IPermissionHandler permission)
    : BaseCommandParser(protection, cooldown, permission, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4, T5>(pluginId))
{
    protected override void RunCommandInternal(BasePlayer player, UiCommandTokenizer args)
    {
        ArgReaderIterator iterator = GetReader();
        T0 arg0 = iterator.ParseNext<T0>(args.GetNext());
        T1 arg1 = iterator.ParseNext<T1>(args.GetNext());
        T2 arg2 = iterator.ParseNext<T2>(args.GetNext());
        T3 arg3 = iterator.ParseNext<T3>(args.GetNext());
        T4 arg4 = iterator.ParseNext<T4>(args.GetNext());
        T5 arg5 = iterator.ParseNext<T5>(args.GetNext());
        method(player, arg0, arg1, arg2, arg3, arg4, arg5);
    }
}

internal class CommandParser<T0, T1, T2, T3, T4, T5, T6>(PluginId pluginId, Action<BasePlayer, T0, T1, T2, T3, T4, T5, T6> method, ICommandProtection protection, ICooldownHandler cooldown, IPermissionHandler permission)
    : BaseCommandParser(protection, cooldown, permission, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4, T5, T6>(pluginId))
{
    protected override void RunCommandInternal(BasePlayer player, UiCommandTokenizer args)
    {
        ArgReaderIterator iterator = GetReader();
        T0 arg0 = iterator.ParseNext<T0>(args.GetNext());
        T1 arg1 = iterator.ParseNext<T1>(args.GetNext());
        T2 arg2 = iterator.ParseNext<T2>(args.GetNext());
        T3 arg3 = iterator.ParseNext<T3>(args.GetNext());
        T4 arg4 = iterator.ParseNext<T4>(args.GetNext());
        T5 arg5 = iterator.ParseNext<T5>(args.GetNext());
        T6 arg6 = iterator.ParseNext<T6>(args.GetNext());
        method(player, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
    }
}

internal class CommandParser<T0, T1, T2, T3, T4, T5, T6, T7>(PluginId pluginId, Action<BasePlayer, T0, T1, T2, T3, T4, T5, T6, T7> method, ICommandProtection protection, ICooldownHandler cooldown, IPermissionHandler permission)
    : BaseCommandParser(protection, cooldown, permission, ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4, T5, T6, T7>(pluginId))
{
    protected override void RunCommandInternal(BasePlayer player, UiCommandTokenizer args)
    {
        ArgReaderIterator iterator = GetReader();
        T0 arg0 = iterator.ParseNext<T0>(args.GetNext());
        T1 arg1 = iterator.ParseNext<T1>(args.GetNext());
        T2 arg2 = iterator.ParseNext<T2>(args.GetNext());
        T3 arg3 = iterator.ParseNext<T3>(args.GetNext());
        T4 arg4 = iterator.ParseNext<T4>(args.GetNext());
        T5 arg5 = iterator.ParseNext<T5>(args.GetNext());
        T6 arg6 = iterator.ParseNext<T6>(args.GetNext());
        T7 arg7 = iterator.ParseNext<T7>(args.GetNext());
        method(player, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
    }
}