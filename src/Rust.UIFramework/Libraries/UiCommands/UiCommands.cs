using System;
using System.Collections.Generic;
using System.Reflection;
using Network;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Exceptions.UiCommands;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

public delegate void OnPlayerNoPermission(BasePlayer player, string permission);
public delegate void OnPlayerCooldown(BasePlayer player, float cooldown, float remaining);

public class UiCommands : BaseUiFrameworkLibrary, ISingleton
{
    internal const string UiCommandName = "UIFramework_EXT_";
    private readonly Dictionary<PluginCommand, ICommandParser> _commands = new();
    private readonly Dictionary<PluginId, OnPlayerNoPermission> _playerNoPermission = new();
    private readonly Dictionary<PluginId, OnPlayerCooldown> _playerOnCooldown = new();
    
    public ICommandBuilder RegisterCommand(Plugin plugin, Action<BasePlayer> method)
    {
        ParseCommand(plugin, method.Method, out PluginId pluginId, out PluginCommand command, out ICommandProtection protection, out ICooldownHandler cooldown, out IPermissionHandler permission);
        _commands[command] = new CommandParser(pluginId, method, protection, cooldown, permission);
        return new CommandBuilder(command, protection);
    }

    public ICommandBuilder<T0> RegisterCommand<T0>(Plugin plugin, Action<BasePlayer, T0> method)
    {
        ParseCommand(plugin, method.Method, out PluginId pluginId, out PluginCommand command, out ICommandProtection protection, out ICooldownHandler cooldown, out IPermissionHandler permission);
        _commands[command] = new CommandParser<T0>(pluginId, method, protection, cooldown, permission);
        return new CommandBuilder<T0>(command, protection);
    }

    public ICommandBuilder<T0, T1> RegisterCommand<T0, T1>(Plugin plugin, Action<BasePlayer, T0, T1> method)
    {
        ParseCommand(plugin, method.Method, out PluginId pluginId, out PluginCommand command, out ICommandProtection protection, out ICooldownHandler cooldown, out IPermissionHandler permission);
        _commands[command] = new CommandParser<T0, T1>(pluginId, method, protection, cooldown, permission);
        return new CommandBuilder<T0, T1>(command, protection);
    }

    public ICommandBuilder<T0, T1, T2> RegisterCommand<T0, T1, T2>(Plugin plugin, Action<BasePlayer, T0, T1, T2> method)
    {
        ParseCommand(plugin, method.Method, out PluginId pluginId, out PluginCommand command, out ICommandProtection protection, out ICooldownHandler cooldown, out IPermissionHandler permission);
        _commands[command] = new CommandParser<T0, T1, T2>(pluginId, method, protection, cooldown, permission);
        return new CommandBuilder<T0, T1, T2>(command, protection);
    }

    public ICommandBuilder<T0, T1, T2, T3> RegisterCommand<T0, T1, T2, T3>(Plugin plugin, Action<BasePlayer, T0, T1, T2, T3> method)
    {
        ParseCommand(plugin, method.Method, out PluginId pluginId, out PluginCommand command, out ICommandProtection protection, out ICooldownHandler cooldown, out IPermissionHandler permission);
        _commands[command] = new CommandParser<T0, T1, T2, T3>(pluginId, method, protection, cooldown, permission);
        return new CommandBuilder<T0, T1, T2, T3>(command, protection);
    }

    public ICommandBuilder<T0, T1, T2, T3, T4> RegisterCommand<T0, T1, T2, T3, T4>(Plugin plugin, Action<BasePlayer, T0, T1, T2, T3, T4> method)
    {
        ParseCommand(plugin, method.Method, out PluginId pluginId, out PluginCommand command, out ICommandProtection protection, out ICooldownHandler cooldown, out IPermissionHandler permission);
        _commands[command] = new CommandParser<T0, T1, T2, T3, T4>(pluginId, method, protection, cooldown, permission);
        return new CommandBuilder<T0, T1, T2, T3, T4>(command, protection);
    }

    public ICommandBuilder<T0, T1, T2, T3, T4, T5> RegisterCommand<T0, T1, T2, T3, T4, T5>(Plugin plugin, Action<BasePlayer, T0, T1, T2, T3, T4, T5> method)
    {
        ParseCommand(plugin, method.Method, out PluginId pluginId, out PluginCommand command, out ICommandProtection protection, out ICooldownHandler cooldown, out IPermissionHandler permission);
        _commands[command] = new CommandParser<T0, T1, T2, T3, T4, T5>(pluginId, method, protection, cooldown, permission);
        return new CommandBuilder<T0, T1, T2, T3, T4, T5>(command, protection);
    }

    public ICommandBuilder<T0, T1, T2, T3, T4, T5, T6> RegisterCommand<T0, T1, T2, T3, T4, T5, T6>(Plugin plugin, Action<BasePlayer, T0, T1, T2, T3, T4, T5, T6> method)
    {
        ParseCommand(plugin, method.Method, out PluginId pluginId, out PluginCommand command, out ICommandProtection protection, out ICooldownHandler cooldown, out IPermissionHandler permission);
        _commands[command] = new CommandParser<T0, T1, T2, T3, T4, T5, T6>(pluginId, method, protection, cooldown, permission);
        return new CommandBuilder<T0, T1, T2, T3, T4, T5, T6>(command, protection);
    }

    public ICommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7> RegisterCommand<T0, T1, T2, T3, T4, T5, T6, T7>(Plugin plugin, Action<BasePlayer, T0, T1, T2, T3, T4, T5, T6, T7> method)
    {
        ParseCommand(plugin, method.Method, out PluginId pluginId, out PluginCommand command, out ICommandProtection protection, out ICooldownHandler cooldown, out IPermissionHandler permission);
        _commands[command] = new CommandParser<T0, T1, T2, T3, T4, T5, T6, T7>(pluginId, method, protection, cooldown, permission);
        return new CommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7>(command, protection);
    }

    private void ParseCommand(Plugin plugin, MethodInfo method, out PluginId pluginId, out PluginCommand command, out ICommandProtection protection, out ICooldownHandler cooldown, out IPermissionHandler permission)
    {
        if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        if (method == null) throw new ArgumentNullException(nameof(method));
        pluginId = plugin.Id();
        UiCommandAttribute attribute = method.GetCustomAttribute<UiCommandAttribute>();
        if (attribute == null)
        {
            throw new MissingUiCommandAttributeException(pluginId, method);
        }
        
        command = new PluginCommand(pluginId, Singleton<CommandIdHandler>.Instance.GetId(method));
        if (_commands.ContainsKey(command))
        {
            throw new DuplicateUiCommandRegistration(pluginId, method);
        }
        
        cooldown = attribute.Cooldown > 0 ? new CooldownHandler(pluginId, attribute.Cooldown) : null;
        permission = !string.IsNullOrEmpty(attribute.Permission) ? new PermissionHandler(pluginId, attribute.Permission) : null;
        protection = CreateProtection(attribute.ProtectionType);
    }

    public void RegisterNoPermissionCallback(Plugin plugin, OnPlayerNoPermission callback)
    {
        _playerNoPermission[plugin.Id()] = callback;
    }
    
    public void RegisterPlayerCooldownCallback(Plugin plugin, OnPlayerCooldown callback)
    {
        _playerOnCooldown[plugin.Id()] = callback;
    }
    
    public void RegisterCustomParser<T>(Plugin plugin, IArgHandler<T> handler)
    {
        PluginId pluginId = plugin.Id();
        ArgCreator.RegisterPluginHandler(pluginId, handler);
    }

    protected override void OnPluginUnloaded(Plugin plugin)
    {
        PluginId pluginId = plugin.Id();
        _commands.RemoveAll(c => c.Key.Plugin == pluginId);
        _playerNoPermission.Remove(pluginId);
        _playerOnCooldown.Remove(pluginId);
        ArgCreator.RemovePluginHandler(pluginId);
    }

    internal void OnCommandReceived(Connection connection, UiCommandTokenizer tokenizer)
    {
        string pluginName = tokenizer.GetNext().ToString();
        uint commandId = uint.Parse(tokenizer.GetNext());
        PluginCommand pluginCommand = new(new PluginId(pluginName), commandId);
        _commands[pluginCommand]?.RunCommand(connection, tokenizer);
    }
    
    internal void OnPlayerNoPermission(PluginId pluginId, BasePlayer player, string permission)
    {
        if (_playerNoPermission.TryGetValue(pluginId, out OnPlayerNoPermission callback))
        {
            callback(player, permission);
        }
    }
    
    internal void OnPlayerCooldown(PluginId pluginId, BasePlayer player, float cooldown, float remaining)
    {
        if (_playerOnCooldown.TryGetValue(pluginId, out OnPlayerCooldown callback))
        {
            callback(player, cooldown, remaining);
        }
    }

    private static ICommandProtection CreateProtection(ProtectionType protection)
    {
        return protection switch
        {
            ProtectionType.None => null,
            ProtectionType.Simple => new SimpleProtection(),
            ProtectionType.Advanced => new AdvancedProtection(),
            ProtectionType.Extreme => new ExtremeProtection(),
            _ => throw new ArgumentOutOfRangeException(nameof(protection), protection, null)
        };
    }
}