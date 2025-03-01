using System;
using System.Collections.Generic;
using System.Threading;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Logging;

internal sealed class UiFileLoggerFactory : ISingleton
{
    private readonly Thread _writerThread;
    private readonly List<UiFileLogger> _loggers = [];
    private readonly AutoResetEvent _reset = new(false);
    private bool _exit;

    private UiFileLoggerFactory()
    {
        _writerThread = new Thread(WriteLogThread)
        {
            IsBackground = true,
            Name = nameof(UiFileLogger)
        };
        _writerThread.Start();
    }

    public UiFileLogger CreateLogger(string pluginName, string type, string dateTimeFormat)
    {
        UiFileLogger logger = new(pluginName, type, dateTimeFormat, _reset);
        _loggers.Add(logger);
        return logger;
    }
        
    private void WriteLogThread()
    {
        try
        {
            while (!_exit)
            {
                _reset.WaitOne();
                for (int index = 0; index < _loggers.Count; index++)
                {
                    _loggers[index].WriteLog();
                }
            }
        }
        catch (ThreadAbortException) { }
        catch (Exception ex)
        {
            UiFrameworkExtension.GlobalLogger.Exception("An exception occured writing log file.", ex);
            WriteLogThread();
        }
    }
        
    internal void OnServerShutdown()
    {
        _exit = true;
        _reset.Set();
        _writerThread.Join(TimeSpan.FromSeconds(5));
    }

    internal void RemoveLogger(UiFileLogger logger)
    {
        _loggers.Remove(logger);
    }
}