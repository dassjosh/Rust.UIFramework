using System;
using System.Text;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Plugins;

namespace Oxide.Ext.UiFramework.Logging;

/// <summary>
/// Represents a Console Logger for Ui Framework
/// </summary>
internal class UiConsoleLogger(PluginId pluginId) : IOutputLogger
{
    /// <summary>
    /// Adds a message to the server console
    /// </summary>
    /// <param name="level"></param>
    /// <param name="type"></param>
    /// <param name="log"></param>
    /// <param name="args"></param>
    /// <param name="ex"></param>
    public void AddMessage(UiLogLevel level, string type, string log, object[] args, Exception ex)
    {
        StringBuilder sb = UiPool.Internal.GetStringBuilder();
        sb.Clear();
        sb.Append('[');
        sb.Append(pluginId.Id);
        sb.Append("] ");
        sb.Append('[');
        sb.Append(EnumCache<UiLogLevel>.ToString(level));
        sb.Append("] ");
        if (type != null)
        {
            sb.Append('[');
            sb.Append(type);
            sb.Append("]: ");
        }
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
                OxideLibrary.LogWarning(message);
                break;
            case UiLogLevel.Error:
                OxideLibrary.LogError(message);
                break;
            case UiLogLevel.Exception:
                OxideLibrary.LogException(message, ex);
                break;
            case UiLogLevel.Verbose:
            case UiLogLevel.Info:
                OxideLibrary.LogInfo(message);
                break;
            case UiLogLevel.Off:
                break;
        }
        
        UiPool.Internal.FreeStringBuilder(sb);
    }

    public void OnShutdown() {}
}