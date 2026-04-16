using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Oxide.Ext.UiFramework.Exceptions;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Logging;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.Libraries;

public delegate void OnPluginNoPermission(BasePlayer player, MethodInfo method, string errorMessage);
public delegate void OnPluginCooldown(BasePlayer player, MethodInfo method, float cooldown, float remaining, string errorMessage);
public delegate void OnPluginProtectionFailed(BasePlayer player, MethodInfo method);

public delegate void OnPlayerNoPermission(BasePlayer player, string errorMessage);
public delegate void OnPlayerCooldown(BasePlayer player, float cooldown, float remaining, string errorMessage);
public delegate void OnPlayerProtectionFailed(BasePlayer player);

[GenerateRegisterCommands]
public partial class UiCommands : BaseUiFrameworkLibrary, ISingleton
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

    private RegisteredCommand ParseCommand(IUiFrameworkPlugin plugin, Delegate @delegate, IArgHandler[] handlers)
    {
        if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        if (@delegate == null) throw new ArgumentNullException(nameof(@delegate));
        PluginId pluginId = plugin.Id();
        MethodInfo method = @delegate.Method;
        UiCommandAttribute attribute = method.GetCustomAttribute<UiCommandAttribute>();
        if (attribute == null)
        {
            throw new MissingUiCommandAttributeException(pluginId, method);
        }
        
        CommandId commandId = new(_idHandler.GetId(pluginId, method));
        if (_commands.ContainsKey(commandId))
        {
            throw new DuplicateUiCommandRegistrationException(pluginId, method);
        }
        
        RegisteredCommand command = new(plugin, commandId, @delegate, attribute, handlers);
        CreateCooldown(command);
        CreatePermission(command);
        CreateProtection(command);
        _logger.Debug("Registered UiCommand. Plugin: {0}, Method: {1}({2})", plugin.FullName(), method.Name, string.Join(", ", method.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}")));
        return command;
    }

    public void RegisterPluginNoPermissionCallback(IUiFrameworkPlugin plugin, OnPluginNoPermission callback) => GetCallbacks(plugin).PlayerNoPermission = callback;
    public void RegisterPluginPlayerCooldownCallback(IUiFrameworkPlugin plugin, OnPluginCooldown callback) => GetCallbacks(plugin).PlayerOnCooldown = callback;
    public void RegisterPluginValidationFailedCallback(IUiFrameworkPlugin plugin, OnPluginProtectionFailed callback) => GetCallbacks(plugin).ProtectionValidationFailed = callback;

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
        ReadOnlySpan<char> span = tokenizer.GetNext();
        if (!uint.TryParse(span, out uint id))
        {
            _logger.Debug($"{nameof(OnCommandReceived)} Invalid command id: {{0}}", span.ToString());
            return;
        }
        
        CommandId commandId = new(id);
        _commands[commandId]?.RunCommand(player, tokenizer);
    }
    
    internal void OnPlayerNoPermission(PluginId pluginId, BasePlayer player, MethodInfo method, string errorMessage)
    {
        if (_callbacks.TryGetValue(pluginId, out PluginCallbacks callback) && callback.PlayerNoPermission != null)
        {
            callback.PlayerNoPermission(player, method, errorMessage);
        }
    }
    
    internal void OnPlayerCooldown(PluginId pluginId, BasePlayer player, MethodInfo method, float cooldown, float remaining, string errorMessage)
    {
        if (_callbacks.TryGetValue(pluginId, out PluginCallbacks callback) && callback.PlayerOnCooldown != null)
        {
            callback.PlayerOnCooldown(player, method, cooldown, remaining, errorMessage);
        }
    }
    
    internal void OnProtectionValidationFailed(PluginId pluginId, BasePlayer player, MethodInfo method)
    {
        if (_callbacks.TryGetValue(pluginId, out PluginCallbacks callback) && callback.ProtectionValidationFailed != null)
        {
            callback.ProtectionValidationFailed(player, method);
        }
    }

    private static void CreateCooldown(RegisteredCommand command)
    {
        UiCooldownAttribute cooldown = command.Delegate.Method.GetCustomAttribute<UiCooldownAttribute>();
        if (cooldown != null)
        {
            command.SetCooldown(new CooldownHandler(command, cooldown.Cooldown, cooldown.ErrorMessage));
        }
    }
    
    private static void CreatePermission(RegisteredCommand command)
    {
        UiPermissionAttribute permission = command.Delegate.Method.GetCustomAttribute<UiPermissionAttribute>();
        if (permission != null)
        {
            command.SetPermission(new PermissionHandler(command, permission.Permissions, permission.Mode, permission.ErrorMessage));
        }
    }

    private static void CreateProtection(RegisteredCommand command)
    {
        UiProtectionAttribute protection = command.Delegate.Method.GetCustomAttribute<UiProtectionAttribute>();
        ProtectionType type = protection?.Protection ?? ProtectionType.Simple;
        command.SetProtection(type switch
        {
            ProtectionType.Simple => new SimpleProtection(command),
            ProtectionType.Advanced => new AdvancedProtection(command, protection!.ProtectionKeyLifetime, protection.MultiUse),
            ProtectionType.Extreme => new ExtremeProtection(command, protection!.ProtectionKeyLifetime, protection.MultiUse),
            _ => null
        });
    }

    private sealed class PluginCallbacks
    {
        public OnPluginNoPermission PlayerNoPermission;
        public OnPluginCooldown PlayerOnCooldown;
        public OnPluginProtectionFailed ProtectionValidationFailed;
    }
}