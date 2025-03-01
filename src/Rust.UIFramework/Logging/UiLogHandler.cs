using System;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Logging;

internal class UiLogHandler(string pluginName, string type, IUiLoggingConfig config, bool isExtension)
{
    private readonly UiConsoleLogger _consoleLogger = isExtension || config.ConsoleLogLevel != UiLogLevel.Off ? new UiConsoleLogger(pluginName, type) : null;
    private readonly UiFileLogger _fileLogger = isExtension || config.FileLogLevel != UiLogLevel.Off ? Singleton<UiFileLoggerFactory>.Instance.CreateLogger(pluginName, type, config.FileDateTimeFormat) : null;
    public bool IsShutdown { get; private set; }

    public void LogConsole(UiLogLevel level, string log, object[] args, Exception exception = null)
    {
        _consoleLogger?.AddMessage(level, log, args, exception);
    }

    public void LogFile(UiLogLevel level, string log, object[] args, Exception exception = null)
    {
        _fileLogger?.AddMessage(level, log, args, exception);
    }

    public void Shutdown()
    {
        _consoleLogger?.OnShutdown();
        _fileLogger?.OnShutdown();
        IsShutdown = true;
    }
}