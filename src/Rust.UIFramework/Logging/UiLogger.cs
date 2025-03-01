using System;
using Oxide.Ext.UiFramework.Exceptions.Logging;

namespace Oxide.Ext.UiFramework.Logging;

/// <summary>
/// Represents a Ui Framework Extension Logger
/// </summary>
public class UiLogger : IUiLogger
{
    ///<inheritdoc/>
    public UiLogLevel LogLevel { get; private set; }
    private readonly IUiLoggingConfig _config;
    private readonly UiLogHandler _handler;

    /// <summary>
    /// Creates a new logger with the given log level
    /// </summary>
    /// <param name="logLevel">Log level of the logger</param>
    /// <param name="config">Configuration for the logger</param>
    /// <param name="handler">Handler for the logger</param>
    internal UiLogger(UiLogLevel logLevel, IUiLoggingConfig config, UiLogHandler handler)
    {
        LogLevel = logLevel;
        _config = config;
        _handler = handler;
    }

    /// <inheritdoc/>
    public void Log(UiLogLevel level, string log, object[] args, Exception exception = null)
    {
        UiLoggerException.ThrowIfShutdown(_handler);
        if (IsConsoleLogging(level))
        {
            _handler.LogConsole(level, log, args,  exception);
        }

        if (IsFileLogging(level))
        {
            _handler.LogFile(level, log, args, exception);
        }
    }

    /// <inheritdoc/>
    public void UpdateLogLevel(UiLogLevel level)
    {
        LogLevel = level;
    }

    /// <inheritdoc/>
    public bool IsLogging(UiLogLevel level)
    {
        return level >= LogLevel && (level >= _config.ConsoleLogLevel || level >= _config.FileLogLevel);
    }
        
    /// <inheritdoc/>
    public bool IsConsoleLogging(UiLogLevel level)
    {
        return level >= LogLevel && level >= _config.ConsoleLogLevel;
    }
        
    /// <inheritdoc/>
    public bool IsFileLogging(UiLogLevel level)
    {
        return level >= LogLevel && level >= _config.FileLogLevel;
    }
        
    /// <inheritdoc/>
    public void Shutdown()
    {
        _handler.Shutdown();
    }
}

public class UiLogger<T> : UiLogger, ILogger<T>
{
    internal UiLogger(UiLogLevel logLevel, IUiLoggingConfig config, UiLogHandler handler) : base(logLevel, config, handler) { }
}