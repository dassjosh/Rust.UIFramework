using System;
using System.Collections.Generic;
using System.Reflection;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Exceptions.UiCommands;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

public delegate void OnPlayerNoPermission(BasePlayer player, string method);
public delegate void OnPlayerCooldown(BasePlayer player, string method, float cooldown, float remaining);
public delegate void OnProtectionValidationFailed(BasePlayer player, string method);

public class UiCommands : BaseUiFrameworkLibrary, ISingleton
{
    internal const string UiCommandName = "UIFramework_EXT_";
    private readonly Dictionary<PluginCommand, ICommandParser> _commands = new();
    private readonly Dictionary<PluginId, OnPlayerNoPermission> _playerNoPermission = new();
    private readonly Dictionary<PluginId, OnPlayerCooldown> _playerOnCooldown = new();
    private readonly Dictionary<PluginId, OnProtectionValidationFailed> _protectionValidationFailed = new();
    
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
            throw new DuplicateUiCommandRegistrationException(pluginId, method);
        }

        cooldown = CreateCooldown(pluginId, method);
        permission = CreatePermission(pluginId, method);
        protection = CreateProtection(pluginId, method);
    }

    public void RegisterNoPermissionCallback(Plugin plugin, OnPlayerNoPermission callback)
    {
        _playerNoPermission[plugin.Id()] = callback;
    }
    
    public void RegisterPlayerCooldownCallback(Plugin plugin, OnPlayerCooldown callback)
    {
        _playerOnCooldown[plugin.Id()] = callback;
    }
    
    public void RegisterValidationFailedCallback(Plugin plugin, OnProtectionValidationFailed callback)
    {
        _protectionValidationFailed[plugin.Id()] = callback;
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
        _protectionValidationFailed.Remove(pluginId);
        ArgCreator.RemovePluginHandler(pluginId);
    }

    internal void OnCommandReceived(BasePlayer player, UiCommandTokenizer tokenizer)
    {
        string pluginName = tokenizer.GetNext().ToString();
        uint commandId = uint.Parse(tokenizer.GetNext());
        PluginCommand pluginCommand = new(new PluginId(pluginName), commandId);
        _commands[pluginCommand]?.RunCommand(player, tokenizer);
    }
    
    internal void OnPlayerNoPermission(PluginId pluginId, BasePlayer player, string method)
    {
        if (_playerNoPermission.TryGetValue(pluginId, out OnPlayerNoPermission callback))
        {
            callback(player, method);
        }
    }
    
    internal void OnPlayerCooldown(PluginId pluginId, BasePlayer player, string method, float cooldown, float remaining)
    {
        if (_playerOnCooldown.TryGetValue(pluginId, out OnPlayerCooldown callback))
        {
            callback(player, method, cooldown, remaining);
        }
    }
    
    internal void OnProtectionValidationFailed(PluginId pluginId, BasePlayer player, string method)
    {
        if (_protectionValidationFailed.TryGetValue(pluginId, out OnProtectionValidationFailed callback))
        {
            callback(player, method);
        }
    }

    private static ICooldownHandler CreateCooldown(PluginId pluginId, MethodInfo method)
    {
        UiCooldownAttribute cooldown = method.GetCustomAttribute<UiCooldownAttribute>();
        return cooldown != null ? new CooldownHandler(pluginId, method.Name, cooldown.Cooldown) : null;
    }
    
    private static IPermissionHandler CreatePermission(PluginId pluginId, MethodInfo method)
    {
        UiPermissionAttribute permission = method.GetCustomAttribute<UiPermissionAttribute>();
        return permission != null ? new PermissionHandler(pluginId, method.Name, permission.Permissions, permission.Mode) : null;
    }

    private static ICommandProtection CreateProtection(PluginId pluginId, MethodInfo method)
    {
        UiProtectionAttribute protection = method.GetCustomAttribute<UiProtectionAttribute>();
        ProtectionType type = protection?.Protection ?? ProtectionType.Simple;
        return type switch
        {
            ProtectionType.Simple => new SimpleProtection(pluginId, method.Name),
            ProtectionType.Advanced => new AdvancedProtection(pluginId, method.Name, protection!.ProtectionKeyLifetime),
            ProtectionType.Extreme => new ExtremeProtection(pluginId, method.Name, protection!.ProtectionKeyLifetime),
            _ => null
        };
    }
}