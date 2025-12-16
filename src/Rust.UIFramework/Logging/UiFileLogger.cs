using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Threading;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Logging;

/// <summary>
/// Represents a File Logger for Ui Framework
/// </summary>
internal class UiFileLogger : IOutputLogger
{
    private readonly ConcurrentQueue<UiPooledArray<char>> _messages = new();
    private readonly StreamWriter _writer;
    public readonly PluginId PluginId;
    private readonly string _dateTimeFormat;
    private readonly AutoResetEvent _reset;

    internal UiFileLogger(PluginId pluginId, string dateTimeFormat, AutoResetEvent reset)
    {
        _writer = File.AppendText(GetFilePath(pluginId));
        PluginId = pluginId;
        _dateTimeFormat = dateTimeFormat;
        _reset = reset;
    }

    public void AddMessage(UiLogLevel level, string type, string log, object[] args, Exception ex)
    {
        StringBuilder sb = UiPool.Internal.GetStringBuilder();
        sb.Clear();
        DateTime.Now.TryFormat(out ReadOnlySpan<char> written, _dateTimeFormat);
        sb.Append(written);
        sb.Append(" [");
        sb.Append(EnumCache<UiLogLevel>.ToString(level));
        sb.Append("] ");
        
        if (type != null)
        {
            sb.Append("[");
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

        sb.AppendLine();
        
        if (ex != null)
        {
            sb.AppendLine(ex.ToString());
        }

        int length = sb.Length;
        UiPooledArray<char> array = UiPool.Internal.GetArray<char>(length);
        Span<char> span = array.AsSpan();
        sb.CopyTo(0, span, length);
        array.WithLength(length);
        _messages.Enqueue(array);
        _reset.Set();
        UiPool.Internal.FreeStringBuilder(sb);
    }
        
    internal void WriteLog()
    {
        if (_messages.IsEmpty)
        {
            return;
        }
        
        while (_messages.TryDequeue(out UiPooledArray<char> message))
        {
            using (message)
            {
                _writer.Write(message.AsReadOnlySpan());
            }
        }
        
        _writer.Flush();
    }
    
    private static string GetFilePath(PluginId id)
    {
        string logPath = Path.Combine(OxideLibrary.LogFolder, id.Id);
        if (!Directory.Exists(logPath))
        {
            Directory.CreateDirectory(logPath);
        }
            
        return Path.Combine(logPath, $"{id.Id}-{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt");
    }

    public void OnShutdown()
    {
        WriteLog();
        Singleton<UiFileLoggerFactory>.Instance.RemoveLogger(this);
    }
}