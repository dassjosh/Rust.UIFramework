using System;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Logging;

internal class UiLogHandler(PluginId plugin, IUiLoggingConfig config, bool isExtension)
{
    private UiConsoleLogger _consoleLogger = isExtension || config.ConsoleLogLevel != UiLogLevel.Off ? new UiConsoleLogger(plugin) : null;
    private UiFileLogger _fileLogger = isExtension || config.FileLogLevel != UiLogLevel.Off ? Singleton<UiFileLoggerFactory>.Instance.CreateLogger(plugin, config.FileDateTimeFormat) : null;
    
    public bool IsShutdown { get; private set; }

    public void LogConsole(UiLogLevel level, string type, string log, object[] args, Exception exception = null)
    {
        _consoleLogger?.AddMessage(level, type, log, args, exception);
    }

    public void LogFile(UiLogLevel level, string type, string log, object[] args, Exception exception = null)
    {
        _fileLogger?.AddMessage(level, type, log, args, exception);
    }

    public void Shutdown()
    {
        IsShutdown = true;
        _consoleLogger?.OnShutdown();
        _consoleLogger = null;
        _fileLogger?.OnShutdown();
        _fileLogger = null;
    }
}