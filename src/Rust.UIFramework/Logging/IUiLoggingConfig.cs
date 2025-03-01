namespace Oxide.Ext.UiFramework.Logging;

/// <summary>
/// Interface for Ui Framework Logging Configuration
/// </summary>
public interface IUiLoggingConfig
{
    /// <summary>
    /// Log Level for the Console
    /// </summary>
    UiLogLevel ConsoleLogLevel { get; }
        
    /// <summary>
    /// Log Level for file Logging
    /// </summary>
    UiLogLevel FileLogLevel { get; }
        
    /// <summary>
    /// File Logging DateTime format
    /// </summary>
    string FileDateTimeFormat { get; }
}