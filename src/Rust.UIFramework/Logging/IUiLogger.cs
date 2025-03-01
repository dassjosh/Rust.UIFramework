using System;

namespace Oxide.Ext.UiFramework.Logging;

/// <summary>
/// Represents an interface for a logger
/// </summary>
public interface IUiLogger
{
    /// <summary>
    /// Current Log level of the logger
    /// </summary>
    UiLogLevel LogLevel { get; }
        
    /// <summary>
    /// Updates the log level for the current logger
    /// </summary>
    /// <param name="level">Level to update the logger to</param>
    void UpdateLogLevel(UiLogLevel level);

    /// <summary>
    /// Returns true if the logger is logging for the passed log level
    /// </summary>
    /// <param name="level">Log Level to check</param>
    /// <returns>True if the logger is logging for the given log level</returns>
    bool IsLogging(UiLogLevel level);

    /// <summary>
    /// Returns if the logger is logging for server console
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    bool IsConsoleLogging(UiLogLevel level);

    /// <summary>
    /// Returns if the logger is logging for file logger
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    bool IsFileLogging(UiLogLevel level);

    /// <summary>
    /// Log the message with the specified level
    /// </summary>
    /// <param name="level">Log Level for the message</param>
    /// <param name="log">Message format to log</param>
    /// <param name="args">Message args</param>
    /// <param name="exception">Exception for the log</param>
    void Log(UiLogLevel level, string log, object[] args, Exception exception = null);

    /// <summary>
    /// Shuts down the logger
    /// </summary>
    void Shutdown();
}

public interface ILogger<T> : IUiLogger;