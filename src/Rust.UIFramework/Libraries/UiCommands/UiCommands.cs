using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

// ReSharper disable CoVariantArrayConversion

namespace Oxide.Ext.UiFramework.Libraries;

public delegate void OnPluginNoPermission(BasePlayer player, string method, string errorMessage);
public delegate void OnPluginCooldown(BasePlayer player, string method, float cooldown, float remaining, string errorMessage);
public delegate void OnPluginProtectionFailed(BasePlayer player, string method);

public delegate void OnPlayerNoPermission(BasePlayer player, string errorMessage);
public delegate void OnPlayerCooldown(BasePlayer player, float cooldown, float remaining, string errorMessage);
public delegate void OnPlayerProtectionFailed(BasePlayer player);

public class UiCommands : BaseUiFrameworkLibrary, ISingleton
{
    public const string NullArg = "null";
    public const char StartQuote = '“';
    public const char EndQuote = '”';
    internal const string UiCommandName = "UIF.C";
    private readonly Dictionary<CommandId, ICommandParser> _commands = new();
    private readonly Dictionary<PluginId, PluginCallbacks> _callbacks = new();
    private readonly CommandIdHandler _idHandler = new();
    
    private readonly IUiLogger<UiCommands> _logger = Singleton<UiLoggerFactory>.Instance.CreateExtensionLogger<UiCommands>();

    private UiCommands() { }
    
    public ICommandBuilder RegisterCommand(IUiFrameworkPlugin plugin, Action<BasePlayer> method, CommandOptions options = null)
    {
        ParseCommand(plugin, method.Method, options, out PluginId _, out CommandId command, out ICommandProtection protection, out ICooldownHandler cooldown, out IPermissionHandler permission);
        _commands[command] = new CommandParser(plugin, method, protection, cooldown, permission);
        return new CommandBuilder(command.GetCommand(), protection);
    }

    public ICommandBuilder<T0> RegisterCommand<T0>(IUiFrameworkPlugin plugin, Action<BasePlayer, T0> method, CommandOptions options = null)
    {
        ParseCommand(plugin, method.Method, options, out PluginId pluginId, out CommandId command, out ICommandProtection protection, out ICooldownHandler cooldown, out IPermissionHandler permission);
        IArgHandler[] argHandler = ArgCreator.CreateArgHandler<T0>(pluginId);
        _commands[command] = new CommandParser<T0>(plugin, method, protection, cooldown, permission, argHandler);
        return new CommandBuilder<T0>(command.GetCommand(), protection, argHandler);
    }

    public ICommandBuilder<T0, T1> RegisterCommand<T0, T1>(IUiFrameworkPlugin plugin, Action<BasePlayer, T0, T1> method, CommandOptions options = null)
    {
        ParseCommand(plugin, method.Method, options, out PluginId pluginId, out CommandId command, out ICommandProtection protection, out ICooldownHandler cooldown, out IPermissionHandler permission);
        IArgHandler[] argHandler = ArgCreator.CreateArgHandler<T0, T1>(pluginId);
        _commands[command] = new CommandParser<T0, T1>(plugin, method, protection, cooldown, permission, argHandler);
        return new CommandBuilder<T0, T1>(command.GetCommand(), protection, argHandler);
    }

    public ICommandBuilder<T0, T1, T2> RegisterCommand<T0, T1, T2>(IUiFrameworkPlugin plugin, Action<BasePlayer, T0, T1, T2> method, CommandOptions options = null)
    {
        ParseCommand(plugin, method.Method, options, out PluginId pluginId, out CommandId command, out ICommandProtection protection, out ICooldownHandler cooldown, out IPermissionHandler permission);
        IArgHandler[] argHandler = ArgCreator.CreateArgHandler<T0, T1, T2>(pluginId);
        _commands[command] = new CommandParser<T0, T1, T2>(plugin, method, protection, cooldown, permission, argHandler);
        return new CommandBuilder<T0, T1, T2>(command.GetCommand(), protection, argHandler);
    }

    public ICommandBuilder<T0, T1, T2, T3> RegisterCommand<T0, T1, T2, T3>(IUiFrameworkPlugin plugin, Action<BasePlayer, T0, T1, T2, T3> method, CommandOptions options = null)
    {
        ParseCommand(plugin, method.Method, options, out PluginId pluginId, out CommandId command, out ICommandProtection protection, out ICooldownHandler cooldown, out IPermissionHandler permission);
        IArgHandler[] argHandler = ArgCreator.CreateArgHandler<T0, T1, T2, T3>(pluginId);
        _commands[command] = new CommandParser<T0, T1, T2, T3>(plugin, method, protection, cooldown, permission, argHandler);
        return new CommandBuilder<T0, T1, T2, T3>(command.GetCommand(), protection, argHandler);
    }

    public ICommandBuilder<T0, T1, T2, T3, T4> RegisterCommand<T0, T1, T2, T3, T4>(IUiFrameworkPlugin plugin, Action<BasePlayer, T0, T1, T2, T3, T4> method, CommandOptions options = null)
    {
        ParseCommand(plugin, method.Method, options, out PluginId pluginId, out CommandId command, out ICommandProtection protection, out ICooldownHandler cooldown, out IPermissionHandler permission);
        IArgHandler[] argHandler = ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4>(pluginId);
        _commands[command] = new CommandParser<T0, T1, T2, T3, T4>(plugin, method, protection, cooldown, permission, argHandler);
        return new CommandBuilder<T0, T1, T2, T3, T4>(command.GetCommand(), protection, argHandler);
    }

    public ICommandBuilder<T0, T1, T2, T3, T4, T5> RegisterCommand<T0, T1, T2, T3, T4, T5>(IUiFrameworkPlugin plugin, Action<BasePlayer, T0, T1, T2, T3, T4, T5> method, CommandOptions options = null)
    {
        ParseCommand(plugin, method.Method, options, out PluginId pluginId, out CommandId command, out ICommandProtection protection, out ICooldownHandler cooldown, out IPermissionHandler permission);
        IArgHandler[] argHandler = ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4, T5>(pluginId);
        _commands[command] = new CommandParser<T0, T1, T2, T3, T4, T5>(plugin, method, protection, cooldown, permission, argHandler);
        return new CommandBuilder<T0, T1, T2, T3, T4, T5>(command.GetCommand(), protection, argHandler);
    }

    public ICommandBuilder<T0, T1, T2, T3, T4, T5, T6> RegisterCommand<T0, T1, T2, T3, T4, T5, T6>(IUiFrameworkPlugin plugin, Action<BasePlayer, T0, T1, T2, T3, T4, T5, T6> method, CommandOptions options = null)
    {
        ParseCommand(plugin, method.Method, options, out PluginId pluginId, out CommandId command, out ICommandProtection protection, out ICooldownHandler cooldown, out IPermissionHandler permission);
        IArgHandler[] argHandler = ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4, T5, T6>(pluginId);
        _commands[command] = new CommandParser<T0, T1, T2, T3, T4, T5, T6>(plugin, method, protection, cooldown, permission, argHandler);
        return new CommandBuilder<T0, T1, T2, T3, T4, T5, T6>(command.GetCommand(), protection, argHandler);
    }

    public ICommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7> RegisterCommand<T0, T1, T2, T3, T4, T5, T6, T7>(IUiFrameworkPlugin plugin, Action<BasePlayer, T0, T1, T2, T3, T4, T5, T6, T7> method, CommandOptions options = null)
    {
        ParseCommand(plugin, method.Method, options, out PluginId pluginId, out CommandId command, out ICommandProtection protection, out ICooldownHandler cooldown, out IPermissionHandler permission);
        IArgHandler[] argHandler = ArgCreator.CreateArgHandler<T0, T1, T2, T3, T4, T5, T6, T7>(pluginId);
        _commands[command] = new CommandParser<T0, T1, T2, T3, T4, T5, T6, T7>(plugin, method, protection, cooldown, permission, argHandler);
        return new CommandBuilder<T0, T1, T2, T3, T4, T5, T6, T7>(command.GetCommand(), protection, argHandler);
    }

    private void ParseCommand(IUiFrameworkPlugin plugin, MethodInfo method, CommandOptions options, out PluginId pluginId, out CommandId command, out ICommandProtection protection, out ICooldownHandler cooldown, out IPermissionHandler permission)
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

        options ??= CommandOptions.Default;
        cooldown = CreateCooldown(pluginId, method, options);
        permission = CreatePermission(pluginId, method, options);
        protection = CreateProtection(pluginId, method, options);
        _logger.Debug("Registered UiCommand. Plugin: {0}, Method: {1}({2})", plugin.FullName(), method.Name, string.Join(", ", method.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}")));
    }

    public void RegisterPluginNoPermissionCallback(IUiFrameworkPlugin plugin, OnPluginNoPermission callback)
    {
        GetCallbacks(plugin).PlayerNoPermission = callback;
    }
    
    public void RegisterPluginPlayerCooldownCallback(IUiFrameworkPlugin plugin, OnPluginCooldown callback)
    {
        GetCallbacks(plugin).PlayerOnCooldown = callback;
    }
    
    public void RegisterPluginValidationFailedCallback(IUiFrameworkPlugin plugin, OnPluginProtectionFailed callback)
    {
        GetCallbacks(plugin).ProtectionValidationFailed = callback;
    }

    private PluginCallbacks GetCallbacks(IUiFrameworkPlugin plugin)
    {
        PluginId pluginId = plugin.Id();
        if (!_callbacks.TryGetValue(pluginId, out PluginCallbacks callbacks))
        {
            _callbacks[pluginId] = callbacks = new PluginCallbacks();
        }

        return callbacks;
    }
    
    public void RegisterCustomParser<T>(IUiFrameworkPlugin plugin, IArgHandler<T> handler)
    {
        PluginId pluginId = plugin.Id();
        ArgCreator.RegisterPluginHandler(pluginId, handler);
    }

    protected override void OnPluginUnloaded(IUiFrameworkPlugin plugin)
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
        tokenizer.GetNext(); // Skip UiCommandName
        CommandId commandId = new(uint.Parse(tokenizer.GetNext()));
        _commands[commandId]?.RunCommand(player, tokenizer);
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

    private static ICooldownHandler CreateCooldown(PluginId pluginId, MethodInfo method, CommandOptions options)
    {
        UiCooldownAttribute cooldown = method.GetCustomAttribute<UiCooldownAttribute>();
        return cooldown != null ? new CooldownHandler(pluginId, method.Name, cooldown.Cooldown, cooldown.ErrorMessage, options.OnPlayerCooldown) : null;
    }
    
    private static IPermissionHandler CreatePermission(PluginId pluginId, MethodInfo method, CommandOptions options)
    {
        UiPermissionAttribute permission = method.GetCustomAttribute<UiPermissionAttribute>();
        return permission != null ? new PermissionHandler(pluginId, method.Name, permission.Permissions, permission.Mode, permission.ErrorMessage, options.OnPlayerNoPermission) : null;
    }

    private static ICommandProtection CreateProtection(PluginId pluginId, MethodInfo method, CommandOptions options)
    {
        UiProtectionAttribute protection = method.GetCustomAttribute<UiProtectionAttribute>();
        ProtectionType type = protection?.Protection ?? ProtectionType.Simple;
        return type switch
        {
            ProtectionType.Simple => new SimpleProtection(pluginId, method.Name, options.OnPlayerProtectionFailed),
            ProtectionType.Advanced => new AdvancedProtection(pluginId, method.Name, protection!.ProtectionKeyLifetime, protection.MultiUse, options.OnPlayerProtectionFailed),
            ProtectionType.Extreme => new ExtremeProtection(pluginId, method.Name, protection!.ProtectionKeyLifetime, protection.MultiUse, options.OnPlayerProtectionFailed),
            _ => null
        };
    }

    private sealed class PluginCallbacks
    {
        public OnPluginNoPermission PlayerNoPermission;
        public OnPluginCooldown PlayerOnCooldown;
        public OnPluginProtectionFailed ProtectionValidationFailed;
    }
}