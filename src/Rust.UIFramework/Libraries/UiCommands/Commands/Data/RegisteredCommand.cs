using System;
using System.Threading.Tasks;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Libraries;

public class RegisteredCommand : ICommandParserData, ICommandBuilderData
{
    internal readonly CommandId Id;
    public string Command { get; }
    internal readonly PluginId PluginId;
    public IUiFrameworkPlugin Plugin { get; }
    public Delegate Delegate { get; }
    public ExecutorMode Mode { get; }
    public ICommandProtection Protection { get; private set; }
    public ICooldownHandler Cooldown { get; private set; }
    public IPermissionHandler Permission { get; private set; }
    public IArgReader[] Readers { get; private set;  }
    public IArgWriter[] Writers { get; private set; }
    
    public OnPlayerNoPermission OnPlayerNoPermission { get; private set; }
    public OnPlayerCooldown OnPlayerCooldown { get; private set; }
    public OnPlayerProtectionFailed OnPlayerProtectionFailed { get; private set; }
    
    public bool RunOnCooldown { get; private set; }
    public bool RunOnNoPermission { get; private set; }

    internal RegisteredCommand(IUiFrameworkPlugin plugin, CommandId id, Delegate @delegate, UiCommandAttribute attribute, IArgHandler[] handlers)
    {
        Plugin = plugin;
        PluginId = plugin.Id();
        Delegate = @delegate;
        Id = id;
        Mode = GetExecutorMode(Delegate);
        Command = id.GetCommand();
        RunOnCooldown = attribute.RunOnCooldown;
        RunOnNoPermission = attribute.RunOnNoPermission;
        // ReSharper disable CoVariantArrayConversion
        Readers = handlers;
        Writers = handlers;
        // ReSharper restore CoVariantArrayConversion
    }
    
    private static ExecutorMode GetExecutorMode(Delegate @delegate)
    {
        Type returnType = @delegate.Method.ReturnType;
        if (returnType == typeof(void))
        {
            return ExecutorMode.Void;
        }

        if (returnType == typeof(Task))
        {
            return ExecutorMode.Task;
        }

        if (returnType == typeof(ValueTask))
        {
            return ExecutorMode.ValueTask;
        }
        
        // if (returnType == typeof(UniTask))
        // {
        //     return ExecutorMode.UniTask;
        // }

        throw new InvalidOperationException($"Return type {returnType} is not supported");
    }

    public RegisteredCommand SetProtection(ICommandProtection protection)
    {
        Protection = protection;
        return this;
    }
    
    public RegisteredCommand SetCooldown(ICooldownHandler cooldown)
    {
        Cooldown = cooldown;
        return this;
    }
    
    public RegisteredCommand SetPermission(IPermissionHandler permission)
    {
        Permission = permission;
        return this;
    }
    
    public RegisteredCommand SetPlayerNoPermission(OnPlayerNoPermission onPlayerNoPermission)
    {
        OnPlayerNoPermission = onPlayerNoPermission;
        return this;
    }
    
    public RegisteredCommand SetPlayerCooldown(OnPlayerCooldown onPlayerCooldown)
    {
        OnPlayerCooldown = onPlayerCooldown;
        return this;
    }
    
    public RegisteredCommand SetPlayerProtectionFailed(OnPlayerProtectionFailed onPlayerProtectionFailed)
    {
        OnPlayerProtectionFailed = onPlayerProtectionFailed;
        return this;
    }
    
    public RegisteredCommand SetRunOnCooldown(bool runOnCooldown)
    {
        RunOnCooldown = runOnCooldown;
        return this;
    }
    
    public RegisteredCommand SetRunOnNoPermission(bool runOnNoPermission)
    {
        RunOnNoPermission = runOnNoPermission;
        return this;
    }
}