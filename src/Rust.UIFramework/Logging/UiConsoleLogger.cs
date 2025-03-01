using System;
using System.Text;
using System.Threading;
using Oxide.Core;
using Oxide.Ext.UiFramework.Cache;

namespace Oxide.Ext.UiFramework.Logging;

/// <summary>
/// Represents a Console Logger for Ui Framework
/// </summary>
internal class UiConsoleLogger(string pluginName, string type) : IOutputLogger
{
    private readonly string _prefix = !string.IsNullOrEmpty(type) ?  $"[{pluginName}] [{type}] " : $"[{pluginName}] ";

    private static readonly ThreadLocal<StringBuilder> Builder = new(() => new StringBuilder());

    /// <summary>
    /// Adds a message to the server console
    /// </summary>
    /// <param name="level"></param>
    /// <param name="log"></param>
    /// <param name="args"></param>
    /// <param name="ex"></param>
    public void AddMessage(UiLogLevel level, string log, object[] args, Exception ex)
    {
        StringBuilder sb = Builder.Value;
        sb.Clear();
        sb.Append(_prefix);
        sb.Append('[');
        sb.Append(EnumCache<UiLogLevel>.ToString(level));
        sb.Append("]: ");
        if (args.Length != 0)
        {
            sb.AppendFormat(log, args);
        }
        else
        {
            sb.Append(log);
        }

        string message = sb.ToString();

        switch (level)
        {
            case UiLogLevel.Debug:
            case UiLogLevel.Warning:
                Interface.Oxide.LogWarning(message);
                break;
            case UiLogLevel.Error:
                Interface.Oxide.LogError(message);
                break;
            case UiLogLevel.Exception:
                Interface.Oxide.LogException(message, ex);
                break;
            default:
                Interface.Oxide.LogInfo(message);
                break;
        }
    }

    public void OnShutdown() {}
}