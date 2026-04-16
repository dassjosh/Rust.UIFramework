namespace Oxide.Ext.UiFramework.Logging;

/// <summary>
/// Represents the log level for a logger
/// </summary>
public enum UiLogLevel : byte
{
    /// <summary>
    /// Verbose log level displays all message
    /// </summary>
    Verbose,
        
    /// <summary>
    /// Debug log level display all messages up to and including Debug
    /// </summary>
    Debug,
        
    /// <summary>
    /// Info log level display all messages up to and including Info
    /// </summary>
    Info,
        
    /// <summary>
    /// Warning log level display all messages up to and including Warning
    /// </summary>
    Warning,
        
    /// <summary>
    /// Error log level display all messages up to and including Error
    /// </summary>
    Error,
        
    /// <summary>
    /// Exception log level display all messages up to and including Exception
    /// </summary>
    Exception,
        
    /// <summary>
    /// Disables all logging
    /// </summary>
    Off
}