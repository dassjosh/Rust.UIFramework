using System;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Types;
using Oxide.Plugins;

namespace Oxide.Ext.UiFramework.Logging;

/// <summary>
/// Factory for creating UiFramework Loggers
/// </summary>
public sealed class UiLoggerFactory : ISingleton
{
    private readonly Hash<string, UiLogHandler> _handlers = new();

    private UiLoggerFactory() {}
        
    /// <summary>
    /// Returns a newly created <see cref="UiLogger"/> for a given plugin
    /// </summary>
    /// <param name="plugin">Plugin the logger is for</param>
    /// <param name="logLevel">The current LogLevel for the logger</param>
    /// <param name="config">The config for the logger</param>
    /// <returns><see cref="UiLogger"/></returns>
    public UiLogger CreateLogger(Plugin plugin, UiLogLevel logLevel, IUiLoggingConfig config)
    {
        if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        return GetLoggerInternal(plugin.Name, null, logLevel, config, false);
    }

    private static UiLogLevel GetLogLevel()
    {
        return UiFrameworkConfig.Instance.Logging.ConsoleLogLevel <= UiFrameworkConfig.Instance.Logging.FileLogLevel ? UiFrameworkConfig.Instance.Logging.ConsoleLogLevel : UiFrameworkConfig.Instance.Logging.FileLogLevel;
    }

    private UiLogLevel GetLogLevel(UiLogLevel level)
    {
        UiLogLevel globalLevel = GetLogLevel();
        return level < globalLevel ? level : globalLevel;
    }

    internal UiLogger CreateGlobalLogger() => CreateGlobalLogger(GetLogLevel());
    private UiLogger CreateGlobalLogger(UiLogLevel logLevel) => GetLoggerInternal(UiFrameworkExtension.Instance.Name, null, GetLogLevel(logLevel), UiFrameworkConfig.Instance.Logging, true);
    internal UiLogger<T> CreateExtensionLogger<T>() => CreateExtensionLogger<T>(GetLogLevel());
    private UiLogger<T> CreateExtensionLogger<T>(UiLogLevel logLevel) => GetLoggerInternal<T>(UiFrameworkExtension.Instance.Name, GetLogLevel(logLevel), UiFrameworkConfig.Instance.Logging, true);

    private UiLogger<T> GetLoggerInternal<T>(string pluginName, UiLogLevel logLevel, IUiLoggingConfig config, bool isExtension)
    {
        string type = typeof(T).GetRealTypeName();
        UiLogHandler handler = CreateLogHandler(pluginName, type, config, isExtension);
        return new UiLogger<T>(logLevel, config, handler);
    }

    private UiLogger GetLoggerInternal(string pluginName, string type, UiLogLevel logLevel, IUiLoggingConfig config, bool isExtension)
    {
        UiLogHandler handler = CreateLogHandler(pluginName, type, config, isExtension);
        return new UiLogger(logLevel, config, handler);
    }
    
    private UiLogHandler CreateLogHandler(string pluginName, string type, IUiLoggingConfig config, bool isExtension)
    {
        string key = pluginName;
        if (!string.IsNullOrEmpty(type))
        {
            key = $"{pluginName}-{type}";
        }
        UiLogHandler handler = _handlers[key];
        if (handler == null)
        {
            _handlers[key] = handler = new UiLogHandler(pluginName, type, config, isExtension);
        }

        return handler;
    }

    internal void OnPluginUnloaded(Plugin plugin)
    {
        string name = plugin.Name;
        _handlers[name]?.Shutdown();
        _handlers.Remove(name);
    }
        
    internal void OnServerShutdown()
    {
        foreach (UiLogHandler handler in _handlers.Values)
        {
            handler.Shutdown();
        }
            
        _handlers.Clear();
        Singleton<UiFileLoggerFactory>.Instance.OnServerShutdown();
    }
}