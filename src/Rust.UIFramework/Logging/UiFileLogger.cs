using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Threading;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Logging;

/// <summary>
/// Represents a File Logger for Ui Framework
/// </summary>
internal class UiFileLogger : IOutputLogger
{
    private readonly ConcurrentQueue<string> _messages = new();
    private readonly string _logFileName;
    private readonly string _dateTimeFormat;
    private readonly AutoResetEvent _reset;
        
    private static readonly ThreadLocal<StringBuilder> Builder = new(() => new StringBuilder());

    internal UiFileLogger(string pluginName, string dateTimeFormat, AutoResetEvent reset)
    {
        _dateTimeFormat = dateTimeFormat;
        _reset = reset;
        string logPath = Path.Combine(OxideLibrary.LogFolder, pluginName);
        if (!Directory.Exists(logPath))
        {
            Directory.CreateDirectory(logPath);
        }
            
        _logFileName = Path.Combine(logPath, $"{pluginName}-{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt");
    }

    public void AddMessage(UiLogLevel level, string type, string log, object[] args, Exception ex)
    {
        StringBuilder sb = Builder.Value;
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
            
        _messages.Enqueue(sb.ToString());
        if (ex != null)
        {
            _messages.Enqueue(ex.ToString());
        }
        _reset.Set();
    }
        
    internal void WriteLog()
    {
        if (_messages.IsEmpty)
        {
            return;
        }

        using StreamWriter fileWriter = File.AppendText(_logFileName);
        while (_messages.TryDequeue(out string message))
        {
            fileWriter.WriteLine(message);
        }
    }

    public void OnShutdown()
    {
        WriteLog();
        Singleton<UiFileLoggerFactory>.Instance.RemoveLogger(this);
    }
}