using System;

namespace Oxide.Ext.UiFramework.Logging;

/// <summary>
/// Represents a specific logger output
/// </summary>
public interface IOutputLogger
{
    /// <summary>
    /// Adds a message to the logger
    /// </summary>
    /// <param name="level"></param>
    /// <param name="log"></param>
    /// <param name="args"></param>
    /// <param name="ex"></param>
    void AddMessage(UiLogLevel level, string log, object[] args, Exception ex);

    /// <summary>
    /// Shuts down the logger output
    /// </summary>
    void OnShutdown();
}