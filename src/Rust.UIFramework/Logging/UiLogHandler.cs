using System;

namespace Oxide.Ext.UiFramework.Logging;

internal class UiLogHandler(string pluginName, IUiLoggingConfig config, bool isExtension)
{
    private UiConsoleLogger _consoleLogger = isExtension || config.ConsoleLogLevel != UiLogLevel.Off ? new UiConsoleLogger(pluginName) : null;
#if !UNIT_TESTS && !BENCHMARKS
    private UiFileLogger _fileLogger = isExtension || config.FileLogLevel != UiLogLevel.Off ? Singleton<UiFileLoggerFactory>.Instance.CreateLogger(pluginName, config.FileDateTimeFormat) : null;
#else
    private UiFileLogger _fileLogger;
#endif
    
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