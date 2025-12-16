using System;
using System.Collections.Concurrent;
using Oxide.Ext.UiFramework.Config;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Logging;

/// <summary>
/// Factory for creating UiFramework Loggers
/// </summary>
public sealed class UiLoggerFactory : ISingleton
{
    private readonly ConcurrentDictionary<PluginId, UiLogHandler> _handlers = new();

    private UiLoggerFactory() {}
        
    /// <summary>
    /// Returns a newly created <see cref="UiLogger"/> for a given plugin
    /// </summary>
    /// <param name="plugin">Plugin the logger is for</param>
    /// <param name="logLevel">The current LogLevel for the logger</param>
    /// <param name="config">The config for the logger</param>
    /// <returns><see cref="UiLogger"/></returns>
    public UiLogger CreateLogger(IUiFrameworkPlugin plugin, UiLogLevel logLevel, IUiLoggingConfig config)
    {
        if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        return GetLoggerInternal(plugin.Id(), logLevel, config, null, false);
    }
    
    public UiLogger<T> CreateLogger<T>(IUiFrameworkPlugin plugin, UiLogLevel logLevel, IUiLoggingConfig config)
    {
        if (plugin == null) throw new ArgumentNullException(nameof(plugin));
        return GetLoggerInternal<T>(plugin.Id(), logLevel, config, false);
    }

    private static UiLogLevel GetLogLevel()
    {
        return UiFrameworkConfig.Instance.Logging.ConsoleLogLevel <= UiFrameworkConfig.Instance.Logging.FileLogLevel ? UiFrameworkConfig.Instance.Logging.ConsoleLogLevel : UiFrameworkConfig.Instance.Logging.FileLogLevel;
    }

    private static UiLogLevel GetLogLevel(UiLogLevel level)
    {
        UiLogLevel globalLevel = GetLogLevel();
        return level < globalLevel ? level : globalLevel;
    }

    internal UiLogger CreateGlobalLogger() => CreateGlobalLogger(GetLogLevel());
    private UiLogger CreateGlobalLogger(UiLogLevel logLevel) => GetLoggerInternal(UiFrameworkExtension.Instance.PluginId, GetLogLevel(logLevel), UiFrameworkConfig.Instance.Logging, null, true);
    internal UiLogger CreateExtensionLogger(Type type) => CreateExtensionLogger(GetLogLevel(), type);
    private UiLogger CreateExtensionLogger(UiLogLevel logLevel, Type type) => GetLoggerInternal(UiFrameworkExtension.Instance.PluginId, GetLogLevel(logLevel), UiFrameworkConfig.Instance.Logging, type, true);
    internal UiLogger<T> CreateExtensionLogger<T>() => CreateExtensionLogger<T>(GetLogLevel());
    private UiLogger<T> CreateExtensionLogger<T>(UiLogLevel logLevel) => GetLoggerInternal<T>(UiFrameworkExtension.Instance.PluginId, GetLogLevel(logLevel), UiFrameworkConfig.Instance.Logging, true);

    private UiLogger<T> GetLoggerInternal<T>(PluginId plugin, UiLogLevel logLevel, IUiLoggingConfig config, bool isExtension)
    {
        UiLogHandler handler = CreateLogHandler(plugin, config, isExtension);
        return new UiLogger<T>(logLevel, config, handler);
    }

    private UiLogger GetLoggerInternal(PluginId plugin, UiLogLevel logLevel, IUiLoggingConfig config, Type type, bool isExtension)
    {
        UiLogHandler handler = CreateLogHandler(plugin, config, isExtension);
        return new UiLogger(logLevel, config, handler, type);
    }
    
    private UiLogHandler CreateLogHandler(PluginId plugin, IUiLoggingConfig config, bool isExtension)
    {
        if (!_handlers.TryGetValue(plugin, out UiLogHandler handler))
        {
            _handlers[plugin] = handler = new UiLogHandler(plugin, config, isExtension);
        }
        return handler;
    }

    internal void OnPluginUnloaded(IUiFrameworkPlugin plugin)
    {
        if(_handlers.TryRemove(plugin.Id(), out UiLogHandler removed))
        {
            removed.Shutdown();
        }
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