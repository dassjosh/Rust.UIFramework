using System;
using System.Collections.Generic;
using System.Reflection;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

public delegate void OnPlayerNoPermission(BasePlayer player, string method, string errorMessage);
public delegate void OnPlayerCooldown(BasePlayer player, string method, float cooldown, float remaining, string errorMessage);
public delegate void OnProtectionValidationFailed(BasePlayer player, string method);

public class UiCommands : BaseUiFrameworkLibrary, ISingleton
{
    public const string NullArg = "null";
    internal const string UiCommandName = "UIF_EXT_C";
    private readonly Dictionary<CommandId, ICommandParser> _commands = new();
    private readonly Dictionary<PluginId, PluginCallbacks> _callbacks = new();
    private readonly CommandIdHandler _idHandler = new();

    private UiCommands() { }
    
    public ICommandBuilder RegisterCommand(Plugin plugin, Action<BasePlayer> method)
    {
        ParseCommand(plugin, method.Method, out PluginId pluginId, out CommandId command, out ICommandProtection protection, out ICooldownHandler cooldown, out IPermissionHandler permission);
        _commands[command] = new CommandParser(plugin, method, protection, cooldown, permission);
        return new CommandBuilder(pluginId, command, protection);
    }

    public ICommandBuilder<T0> RegisterCommand<T0>(Plugin plugin, Action<BasePlayer, T0> method)
    {
        ParseCommand(plugin, method.Method, out PluginId pluginId, out CommandId command, out ICommandProtection protection, out ICooldownHandler cooldown, out IPermissionHandler permission);
        _commands[command] = new CommandParser<T0>(plugin, method, protection, cooldown, permission);
        return new CommandBuilder<T0>(pluginId, command, protection);
    }

    public ICommandBuilder<T0, T1> RegisterCommand<T0, T1>(Plugin plugin, Action<BasePlayer, T0, T1> method)
    {
        ParseCommand(plugin, method.Method, out PluginId pluginId, out CommandId command, out ICommandProtection protection, out ICooldownHandler cooldown, out IPermissionHandler permission);
        _commands[command] = new CommandParser<T0, T1>(plugin, method, protection, cooldown, permission);
        return new CommandBuilder<T0, T1>(pluginId, command, protection);
    }

    public ICommandBuilder<T0, T1, T2> RegisterCommand<T0, T1, T2>(Plugin plugin, Action<BasePlayer, T0, T1, T2> method)
    {
        ParseCommand(plugin, method.Method, out PluginId pluginId, out CommandId command, out ICommandProtection protection, out ICooldownHandler cooldown, out IPermissionHandler permission);
        _commands[command] = new CommandParser<T0, T1, T2>(plugin, method, protection, cooldown, permission);
        return new CommandBuilder<T0, T1, T2>(pluginId, command, protection);
    }

    public ICommandBuilder<T0, T1, T2, T3> RegisterCommand<T0, T1, T2, T3>(Plugin plugin, Action<BasePlayer, T0, T1, T2, T3> method)
    {
        ParseCommand(plugin, method.Method, out PluginId pluginId, out CommandId command, out ICommandProtection protection, out ICooldownHandler cooldown, out IPermissionHandler permission);
        _commands[command] = new CommandParser<T0, T1, T2, T3>(plugin, method, protection, cooldown, permission);
        return new CommandBuilder<T0, T1, T2, T3>(pluginId, command, protection);
    }

    public ICommandBuilder<T0, T1, T2, T3, T4> RegisterCommand<T0, T1, T2, T3, T4>(Plugin plugin, Action<BasePlayer, T0, T1, T2, T3, T4> method)
    {
        ParseCommand(plugin, method.Method, out PluginId pluginId, out CommandId command, out ICommandProtection protection, out ICooldownHandler cooldown, out IPermissionHandler permission);
        _commands[command] = new CommandParser<T0, T1, T2, T3, T4>(plugin, method, protection, cooldown, permission);
        return new CommandBuilder<T0, T1, T2, T3, T4>(pluginId, command, protection);
    }

    public ICommandBuilder<T0, T1, T2, T3, T4, T5> RegisterCommand<T0, T1, T2, T3, T4, T5>(Plugin plugin, Action<BasePlayer, T0, T1, T2, T3, T4, T5> method)
    {
        ParseCommand(plugin, method.Method, out PluginId pluginId, out CommandId command, out ICommandProtection protection, out ICooldownHandler cooldown, out IPermissionHandler permission);
        _commands[command] = new CommandParser<T0, T1, T2, T3, T4, T5>(plugin, method, protection, cooldown, permission);
        return new CommandBuilder<T0, T1, T2, T3, T4, T5>(pluginId, command, protection);
    }

    public ICommandBuilder<T0, T1, T2, T3, T4, T5, T6> RegisterCommand<T0, T1, T2, T3, T4, T5, T6>(Plugin plugin, Action<BasePlayer, T0, T1, T2, T3, T4, T5, T6> method)
    {
        ParseCommand(plugin, method.Method, out PluginId pluginId, out CommandId command, out ICommandProtection protection, out ICooldownHandler cooldown, out IPermissionHandler permission);
        _commands[command] = new CommandParser<T0, T1, T2, T3, T4, T5, T6>(plugin, method, protection, cooldown, permission);
        return new CommandBuilder<T0, T1, T2, T3, T4, T5, T6>(pluginId, command, protection);
    }

    public ICommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7> RegisterCommand<T0, T1, T2, T3, T4, T5, T6, T7>(Plugin plugin, Action<BasePlayer, T0, T1, T2, T3, T4, T5, T6, T7> method)
    {
        ParseCommand(plugin, method.Method, out PluginId pluginId, out CommandId command, out ICommandProtection protection, out ICooldownHandler cooldown, out IPermissionHandler permission);
        _commands[command] = new CommandParser<T0, T1, T2, T3, T4, T5, T6, T7>(plugin, method, protection, cooldown, permission);
        return new CommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7>(pluginId, command, protection);
    }

    private void ParseCommand(Plugin plugin, MethodInfo method, out PluginId pluginId, out CommandId command, out ICommandProtection protection, out ICooldownHandler cooldown, out IPermissionHandler permission)
    {
        if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        if (method == null) throw new ArgumentNullException(nameof(method));
        pluginId = plugin.Id();
        UiCommandAttribute attribute = method.GetCustomAttribute<UiCommandAttribute>();
        if (attribute == null)
        {
            throw new MissingUiCommandAttributeException(pluginId, method);
        }
        
        command = new CommandId(_idHandler.GetId(pluginId, method));
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
        GetCallbacks(plugin).PlayerNoPermission = callback;
    }
    
    public void RegisterPlayerCooldownCallback(Plugin plugin, OnPlayerCooldown callback)
    {
        GetCallbacks(plugin).PlayerOnCooldown = callback;
    }
    
    public void RegisterValidationFailedCallback(Plugin plugin, OnProtectionValidationFailed callback)
    {
        GetCallbacks(plugin).ProtectionValidationFailed = callback;
    }

    private PluginCallbacks GetCallbacks(Plugin plugin)
    {
        PluginId pluginId = plugin.Id();
        if (!_callbacks.TryGetValue(pluginId, out PluginCallbacks callbacks))
        {
            _callbacks[pluginId] = callbacks = new PluginCallbacks();
        }

        return callbacks;
    }
    
    public void RegisterCustomParser<T>(Plugin plugin, IArgHandler<T> handler)
    {
        PluginId pluginId = plugin.Id();
        ArgCreator.RegisterPluginHandler(pluginId, handler);
    }

    protected override void OnPluginUnloaded(Plugin plugin)
    {
        PluginId pluginId = plugin.Id();
        foreach (CommandId id in _idHandler.GetPluginCommands(pluginId))
        {
            _commands.Remove(id);
        }
        _callbacks.Remove(pluginId);
        ArgCreator.RemovePluginHandler(pluginId);
        _idHandler.OnPluginUnloaded(pluginId);
    }

    internal void OnCommandReceived(BasePlayer player, UiCommandTokenizer tokenizer)
    {
        uint commandId = uint.Parse(tokenizer.GetNext());
        CommandId pluginCommand = new(commandId);
        _commands[pluginCommand]?.RunCommand(player, tokenizer);
    }
    
    internal void OnPlayerNoPermission(PluginId pluginId, BasePlayer player, string method, string errorMessage)
    {
        if (_callbacks.TryGetValue(pluginId, out PluginCallbacks callback) && callback.PlayerNoPermission != null)
        {
            callback.PlayerNoPermission(player, method, errorMessage);
        }
    }
    
    internal void OnPlayerCooldown(PluginId pluginId, BasePlayer player, string method, float cooldown, float remaining, string errorMessage)
    {
        if (_callbacks.TryGetValue(pluginId, out PluginCallbacks callback) && callback.PlayerOnCooldown != null)
        {
            callback.PlayerOnCooldown(player, method, cooldown, remaining, errorMessage);
        }
    }
    
    internal void OnProtectionValidationFailed(PluginId pluginId, BasePlayer player, string method)
    {
        if (_callbacks.TryGetValue(pluginId, out PluginCallbacks callback) && callback.ProtectionValidationFailed != null)
        {
            callback.ProtectionValidationFailed(player, method);
        }
    }

    private static ICooldownHandler CreateCooldown(PluginId pluginId, MethodInfo method)
    {
        UiCooldownAttribute cooldown = method.GetCustomAttribute<UiCooldownAttribute>();
        return cooldown != null ? new CooldownHandler(pluginId, method.Name, cooldown.Cooldown, cooldown.ErrorMessage) : null;
    }
    
    private static IPermissionHandler CreatePermission(PluginId pluginId, MethodInfo method)
    {
        UiPermissionAttribute permission = method.GetCustomAttribute<UiPermissionAttribute>();
        return permission != null ? new PermissionHandler(pluginId, method.Name, permission.Permissions, permission.Mode, permission.ErrorMessage) : null;
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

    private sealed class PluginCallbacks
    {
        public OnPlayerNoPermission PlayerNoPermission;
        public OnPlayerCooldown PlayerOnCooldown;
        public OnProtectionValidationFailed ProtectionValidationFailed;
    }
}