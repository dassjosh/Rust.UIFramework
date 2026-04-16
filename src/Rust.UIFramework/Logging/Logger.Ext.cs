using System;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Types;

#pragma warning disable CS1591

namespace Oxide.Ext.UiFramework.Logging;

public static class LoggerExt
{
    #region Verbose
    extension(IUiLogger logger)
    {
        public void Verbose(string message)
        {
            if (logger.IsLogging(UiLogLevel.Verbose))
            {
                HandleLog(logger, UiLogLevel.Verbose, message);
            }
        }

        public void Verbose<T1>(string message, T1 arg0)
        {
            if (logger.IsLogging(UiLogLevel.Verbose))
            {
                HandleLog(logger, UiLogLevel.Verbose, message, arg0);
            }
        }

        public void Verbose<T1, T2>(string message, T1 arg0, T2 arg1)
        {
            if (logger.IsLogging(UiLogLevel.Verbose))
            {
                HandleLog(logger, UiLogLevel.Verbose, message, arg0, arg1);
            }
        }

        public void Verbose<T1, T2, T3>(string message, T1 arg0, T2 arg1, T3 arg2)
        {
            if (logger.IsLogging(UiLogLevel.Verbose))
            {
                HandleLog(logger, UiLogLevel.Verbose, message, arg0, arg1, arg2);
            }
        }

        public void Verbose<T1, T2, T3, T4>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3)
        {
            if (logger.IsLogging(UiLogLevel.Verbose))
            {
                HandleLog(logger, UiLogLevel.Verbose, message, arg0, arg1, arg2, arg3);
            }
        }

        public void Verbose<T1, T2, T3, T4, T5>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4)
        {
            if (logger.IsLogging(UiLogLevel.Verbose))
            {
                HandleLog(logger, UiLogLevel.Verbose, message, arg0, arg1, arg2, arg3, arg4);
            }
        }

        public void Verbose<T1, T2, T3, T4, T5, T6>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5)
        {
            if (logger.IsLogging(UiLogLevel.Verbose))
            {
                HandleLog(logger, UiLogLevel.Verbose, message, arg0, arg1, arg2, arg3, arg4, arg5);
            }
        }

        public void Verbose<T1, T2, T3, T4, T5, T6, T7>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6)
        {
            if (logger.IsLogging(UiLogLevel.Verbose))
            {
                HandleLog(logger, UiLogLevel.Verbose, message, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
            }
        }

        public void Verbose<T1, T2, T3, T4, T5, T6, T7, T8>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6, T8 arg7)
        {
            if (logger.IsLogging(UiLogLevel.Verbose))
            {
                HandleLog(logger, UiLogLevel.Verbose, message, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            }
        }

        public void Verbose<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6, T8 arg7, T9 arg8)
        {
            if (logger.IsLogging(UiLogLevel.Verbose))
            {
                HandleLog(logger, UiLogLevel.Verbose, message, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            }
        }

        public void Verbose<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6, T8 arg7, T9 arg8, T10 arg9)
        {
            if (logger.IsLogging(UiLogLevel.Verbose))
            {
                HandleLog(logger, UiLogLevel.Verbose, message, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            }
        }

        public void Verbose<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6, T8 arg7, T9 arg8, T10 arg9, T11 arg10)
        {
            if (logger.IsLogging(UiLogLevel.Verbose))
            {
                HandleLog(logger, UiLogLevel.Verbose, message, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
            }
        }
    }

    #endregion

    #region Debug
    extension(IUiLogger logger)
    {
        public void Debug(string message)
        {
            if (logger.IsLogging(UiLogLevel.Debug))
            {
                HandleLog(logger, UiLogLevel.Debug, message);
            }
        }

        public void Debug<T1>(string message, T1 arg0)
        {
            if (logger.IsLogging(UiLogLevel.Debug))
            {
                HandleLog(logger, UiLogLevel.Debug, message, arg0);
            }
        }

        public void Debug<T1, T2>(string message, T1 arg0, T2 arg1)
        {
            if (logger.IsLogging(UiLogLevel.Debug))
            {
                HandleLog(logger, UiLogLevel.Debug, message, arg0, arg1);
            }
        }

        public void Debug<T1, T2, T3>(string message, T1 arg0, T2 arg1, T3 arg2)
        {
            if (logger.IsLogging(UiLogLevel.Debug))
            {
                HandleLog(logger, UiLogLevel.Debug, message, arg0, arg1, arg2);
            }
        }

        public void Debug<T1, T2, T3, T4>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3)
        {
            if (logger.IsLogging(UiLogLevel.Debug))
            {
                HandleLog(logger, UiLogLevel.Debug, message, arg0, arg1, arg2, arg3);
            }
        }

        public void Debug<T1, T2, T3, T4, T5>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4)
        {
            if (logger.IsLogging(UiLogLevel.Debug))
            {
                HandleLog(logger, UiLogLevel.Debug, message, arg0, arg1, arg2, arg3, arg4);
            }
        }

        public void Debug<T1, T2, T3, T4, T5, T6>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5)
        {
            if (logger.IsLogging(UiLogLevel.Debug))
            {
                HandleLog(logger, UiLogLevel.Debug, message, arg0, arg1, arg2, arg3, arg4, arg5);
            }
        }

        public void Debug<T1, T2, T3, T4, T5, T6, T7>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6)
        {
            if (logger.IsLogging(UiLogLevel.Debug))
            {
                HandleLog(logger, UiLogLevel.Debug, message, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
            }
        }

        public void Debug<T1, T2, T3, T4, T5, T6, T7, T8>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6, T8 arg7)
        {
            if (logger.IsLogging(UiLogLevel.Debug))
            {
                HandleLog(logger, UiLogLevel.Debug, message, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            }
        }

        public void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6, T8 arg7, T9 arg8)
        {
            if (logger.IsLogging(UiLogLevel.Debug))
            {
                HandleLog(logger, UiLogLevel.Debug, message, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            }
        }

        public void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6, T8 arg7, T9 arg8, T10 arg9)
        {
            if (logger.IsLogging(UiLogLevel.Debug))
            {
                HandleLog(logger, UiLogLevel.Debug, message, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            }
        }

        public void Debug<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6, T8 arg7, T9 arg8, T10 arg9, T11 arg10)
        {
            if (logger.IsLogging(UiLogLevel.Debug))
            {
                HandleLog(logger, UiLogLevel.Debug, message, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
            }
        }
    }

    #endregion
        
    #region Info
    extension(IUiLogger logger)
    {
        public void Info(string message)
        {
            if (logger.IsLogging(UiLogLevel.Info))
            {
                HandleLog(logger, UiLogLevel.Info, message);
            }
        }

        public void Info<T1>(string message, T1 arg0)
        {
            if (logger.IsLogging(UiLogLevel.Info))
            {
                HandleLog(logger, UiLogLevel.Info, message, arg0);
            }
        }

        public void Info<T1, T2>(string message, T1 arg0, T2 arg1)
        {
            if (logger.IsLogging(UiLogLevel.Info))
            {
                HandleLog(logger, UiLogLevel.Info, message, arg0, arg1);
            }
        }

        public void Info<T1, T2, T3>(string message, T1 arg0, T2 arg1, T3 arg2)
        {
            if (logger.IsLogging(UiLogLevel.Info))
            {
                HandleLog(logger, UiLogLevel.Info, message, arg0, arg1, arg2);
            }
        }

        public void Info<T1, T2, T3, T4>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3)
        {
            if (logger.IsLogging(UiLogLevel.Info))
            {
                HandleLog(logger, UiLogLevel.Info, message, arg0, arg1, arg2, arg3);
            }
        }

        public void Info<T1, T2, T3, T4, T5>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4)
        {
            if (logger.IsLogging(UiLogLevel.Info))
            {
                HandleLog(logger, UiLogLevel.Info, message, arg0, arg1, arg2, arg3, arg4);
            }
        }

        public void Info<T1, T2, T3, T4, T5, T6>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5)
        {
            if (logger.IsLogging(UiLogLevel.Info))
            {
                HandleLog(logger, UiLogLevel.Info, message, arg0, arg1, arg2, arg3, arg4, arg5);
            }
        }

        public void Info<T1, T2, T3, T4, T5, T6, T7>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6)
        {
            if (logger.IsLogging(UiLogLevel.Info))
            {
                HandleLog(logger, UiLogLevel.Info, message, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
            }
        }

        public void Info<T1, T2, T3, T4, T5, T6, T7, T8>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6, T8 arg7)
        {
            if (logger.IsLogging(UiLogLevel.Info))
            {
                HandleLog(logger, UiLogLevel.Info, message, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            }
        }

        public void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6, T8 arg7, T9 arg8)
        {
            if (logger.IsLogging(UiLogLevel.Info))
            {
                HandleLog(logger, UiLogLevel.Info, message, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            }
        }

        public void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6, T8 arg7, T9 arg8, T10 arg9)
        {
            if (logger.IsLogging(UiLogLevel.Info))
            {
                HandleLog(logger, UiLogLevel.Info, message, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            }
        }

        public void Info<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6, T8 arg7, T9 arg8, T10 arg9, T11 arg10)
        {
            if (logger.IsLogging(UiLogLevel.Info))
            {
                HandleLog(logger, UiLogLevel.Info, message, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
            }
        }
    }

    #endregion
        
    #region Warning
    extension(IUiLogger logger)
    {
        public void Warning(string message)
        {
            if (logger.IsLogging(UiLogLevel.Warning))
            {
                HandleLog(logger, UiLogLevel.Warning, message);
            }
        }

        public void Warning<T1>(string message, T1 arg0)
        {
            if (logger.IsLogging(UiLogLevel.Warning))
            {
                HandleLog(logger, UiLogLevel.Warning, message, arg0);
            }
        }

        public void Warning<T1, T2>(string message, T1 arg0, T2 arg1)
        {
            if (logger.IsLogging(UiLogLevel.Warning))
            {
                HandleLog(logger, UiLogLevel.Warning, message, arg0, arg1);
            }
        }

        public void Warning<T1, T2, T3>(string message, T1 arg0, T2 arg1, T3 arg2)
        {
            if (logger.IsLogging(UiLogLevel.Warning))
            {
                HandleLog(logger, UiLogLevel.Warning, message, arg0, arg1, arg2);
            }
        }

        public void Warning<T1, T2, T3, T4>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3)
        {
            if (logger.IsLogging(UiLogLevel.Warning))
            {
                HandleLog(logger, UiLogLevel.Warning, message, arg0, arg1, arg2, arg3);
            }
        }

        public void Warning<T1, T2, T3, T4, T5>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4)
        {
            if (logger.IsLogging(UiLogLevel.Warning))
            {
                HandleLog(logger, UiLogLevel.Warning, message, arg0, arg1, arg2, arg3, arg4);
            }
        }

        public void Warning<T1, T2, T3, T4, T5, T6>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5)
        {
            if (logger.IsLogging(UiLogLevel.Warning))
            {
                HandleLog(logger, UiLogLevel.Warning, message, arg0, arg1, arg2, arg3, arg4, arg5);
            }
        }

        public void Warning<T1, T2, T3, T4, T5, T6, T7>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6)
        {
            if (logger.IsLogging(UiLogLevel.Warning))
            {
                HandleLog(logger, UiLogLevel.Warning, message, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
            }
        }

        public void Warning<T1, T2, T3, T4, T5, T6, T7, T8>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6, T8 arg7)
        {
            if (logger.IsLogging(UiLogLevel.Warning))
            {
                HandleLog(logger, UiLogLevel.Warning, message, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            }
        }

        public void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6, T8 arg7, T9 arg8)
        {
            if (logger.IsLogging(UiLogLevel.Warning))
            {
                HandleLog(logger, UiLogLevel.Warning, message, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            }
        }

        public void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6, T8 arg7, T9 arg8, T10 arg9)
        {
            if (logger.IsLogging(UiLogLevel.Warning))
            {
                HandleLog(logger, UiLogLevel.Warning, message, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            }
        }

        public void Warning<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6, T8 arg7, T9 arg8, T10 arg9, T11 arg10)
        {
            if (logger.IsLogging(UiLogLevel.Warning))
            {
                HandleLog(logger, UiLogLevel.Warning, message, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
            }
        }
    }

    #endregion
        
    #region Error
    extension(IUiLogger logger)
    {
        public void Error(string message)
        {
            if (logger.IsLogging(UiLogLevel.Error))
            {
                HandleLog(logger, UiLogLevel.Error, message);
            }
        }

        public void Error<T1>(string message, T1 arg0)
        {
            if (logger.IsLogging(UiLogLevel.Error))
            {
                HandleLog(logger, UiLogLevel.Error, message, arg0);
            }
        }

        public void Error<T1, T2>(string message, T1 arg0, T2 arg1)
        {
            if (logger.IsLogging(UiLogLevel.Error))
            {
                HandleLog(logger, UiLogLevel.Error, message, arg0, arg1);
            }
        }

        public void Error<T1, T2, T3>(string message, T1 arg0, T2 arg1, T3 arg2)
        {
            if (logger.IsLogging(UiLogLevel.Error))
            {
                HandleLog(logger, UiLogLevel.Error, message, arg0, arg1, arg2);
            }
        }

        public void Error<T1, T2, T3, T4>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3)
        {
            if (logger.IsLogging(UiLogLevel.Error))
            {
                HandleLog(logger, UiLogLevel.Error, message, arg0, arg1, arg2, arg3);
            }
        }

        public void Error<T1, T2, T3, T4, T5>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4)
        {
            if (logger.IsLogging(UiLogLevel.Error))
            {
                HandleLog(logger, UiLogLevel.Error, message, arg0, arg1, arg2, arg3, arg4);
            }
        }

        public void Error<T1, T2, T3, T4, T5, T6>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5)
        {
            if (logger.IsLogging(UiLogLevel.Error))
            {
                HandleLog(logger, UiLogLevel.Error, message, arg0, arg1, arg2, arg3, arg4, arg5);
            }
        }

        public void Error<T1, T2, T3, T4, T5, T6, T7>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6)
        {
            if (logger.IsLogging(UiLogLevel.Error))
            {
                HandleLog(logger, UiLogLevel.Error, message, arg0, arg1, arg2, arg3, arg4, arg5, arg6);
            }
        }

        public void Error<T1, T2, T3, T4, T5, T6, T7, T8>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6, T8 arg7)
        {
            if (logger.IsLogging(UiLogLevel.Error))
            {
                HandleLog(logger, UiLogLevel.Error, message, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
            }
        }

        public void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6, T8 arg7, T9 arg8)
        {
            if (logger.IsLogging(UiLogLevel.Error))
            {
                HandleLog(logger, UiLogLevel.Error, message, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8);
            }
        }

        public void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6, T8 arg7, T9 arg8, T10 arg9)
        {
            if (logger.IsLogging(UiLogLevel.Error))
            {
                HandleLog(logger, UiLogLevel.Error, message, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9);
            }
        }

        public void Error<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6, T8 arg7, T9 arg8, T10 arg9, T11 arg10)
        {
            if (logger.IsLogging(UiLogLevel.Error))
            {
                HandleLog(logger, UiLogLevel.Error, message, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
            }
        }
    }

    #endregion
        
    #region Exception
    extension(IUiLogger logger)
    {
        public void Exception(string message, Exception exception)
        {
            if (logger.IsLogging(UiLogLevel.Exception))
            {
                HandleLog(logger, UiLogLevel.Exception, message, exception);
            }
        }

        public void Exception<T1>(string message, T1 arg0, Exception exception)
        {
            if (logger.IsLogging(UiLogLevel.Exception))
            {
                HandleLog(logger, UiLogLevel.Exception, message, arg0, exception);
            }
        }

        public void Exception<T1, T2>(string message, T1 arg0, T2 arg1, Exception exception)
        {
            if (logger.IsLogging(UiLogLevel.Exception))
            {
                HandleLog(logger, UiLogLevel.Exception, message, arg0, arg1, exception);
            }
        }

        public void Exception<T1, T2, T3>(string message, T1 arg0, T2 arg1, T3 arg2, Exception exception)
        {
            if (logger.IsLogging(UiLogLevel.Exception))
            {
                HandleLog(logger, UiLogLevel.Exception, message, arg0, arg1, arg2, exception);
            }
        }

        public void Exception<T1, T2, T3, T4>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, Exception exception)
        {
            if (logger.IsLogging(UiLogLevel.Exception))
            {
                HandleLog(logger, UiLogLevel.Exception, message, arg0, arg1, arg2, arg3, exception);
            }
        }

        public void Exception<T1, T2, T3, T4, T5>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, Exception exception)
        {
            if (logger.IsLogging(UiLogLevel.Exception))
            {
                HandleLog(logger, UiLogLevel.Exception, message, arg0, arg1, arg2, arg3, arg4, exception);
            }
        }

        public void Exception<T1, T2, T3, T4, T5, T6>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, Exception exception)
        {
            if (logger.IsLogging(UiLogLevel.Exception))
            {
                HandleLog(logger, UiLogLevel.Exception, message, arg0, arg1, arg2, arg3, arg4, arg5, exception);
            }
        }

        public void Exception<T1, T2, T3, T4, T5, T6, T7>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6, Exception exception)
        {
            if (logger.IsLogging(UiLogLevel.Exception))
            {
                HandleLog(logger, UiLogLevel.Exception, message, arg0, arg1, arg2, arg3, arg4, arg5, arg6, exception);
            }
        }

        public void Exception<T1, T2, T3, T4, T5, T6, T7, T8>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6, T8 arg7, Exception exception)
        {
            if (logger.IsLogging(UiLogLevel.Exception))
            {
                HandleLog(logger, UiLogLevel.Exception, message, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, exception);
            }
        }

        public void Exception<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6, T8 arg7, T9 arg8, Exception exception)
        {
            if (logger.IsLogging(UiLogLevel.Exception))
            {
                HandleLog(logger, UiLogLevel.Exception, message, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, exception);
            }
        }

        public void Exception<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6, T8 arg7, T9 arg8, T10 arg9, Exception exception)
        {
            if (logger.IsLogging(UiLogLevel.Exception))
            {
                HandleLog(logger, UiLogLevel.Exception, message, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, exception);
            }
        }

        public void Exception<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6, T8 arg7, T9 arg8, T10 arg9, T11 arg10, Exception exception)
        {
            if (logger.IsLogging(UiLogLevel.Exception))
            {
                HandleLog(logger, UiLogLevel.Exception, message, arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, exception);
            }
        }
    }

    #endregion

    #region HandleLog
    private static void HandleLog(IUiLogger logger, UiLogLevel level, string message, Exception exception = null)
    {
        logger.Log(level, message, [], exception);
    }
        
    private static void HandleLog<T1>(IUiLogger logger, UiLogLevel level, string message, T1 arg0, Exception exception = null)
    {
        object[] array = Singleton<UiArrayPool<object>>.Instance.Get(1);
        array[0] = arg0;
        logger.Log(level, message, array, exception);
        Singleton<UiArrayPool<object>>.Instance.Free(ref array);
    }
        
    private static void HandleLog<T1, T2>(IUiLogger logger, UiLogLevel level, string message, T1 arg0, T2 arg1, Exception exception = null)
    {
        object[] array = Singleton<UiArrayPool<object>>.Instance.Get(2);
        array[0] = arg0;
        array[1] = arg1;
        logger.Log(level, message, array, exception);
        Singleton<UiArrayPool<object>>.Instance.Free(ref array);
    }
        
    private static void HandleLog<T1, T2, T3>(IUiLogger logger, UiLogLevel level, string message, T1 arg0, T2 arg1, T3 arg2, Exception exception = null)
    {
        object[] array = Singleton<UiArrayPool<object>>.Instance.Get(3);
        array[0] = arg0;
        array[1] = arg1;
        array[2] = arg2;
        logger.Log(level, message, array, exception);
        Singleton<UiArrayPool<object>>.Instance.Free(ref array);
    }
        
    private static void HandleLog<T1, T2, T3, T4>(IUiLogger logger, UiLogLevel level, string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, Exception exception = null)
    {
        object[] array = Singleton<UiArrayPool<object>>.Instance.Get(4);
        array[0] = arg0;
        array[1] = arg1;
        array[2] = arg2;
        array[3] = arg3;
        logger.Log(level, message, array, exception);
        Singleton<UiArrayPool<object>>.Instance.Free(ref array);
    }
        
    private static void HandleLog<T1, T2, T3, T4, T5>(IUiLogger logger, UiLogLevel level, string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, Exception exception = null)
    {
        object[] array = Singleton<UiArrayPool<object>>.Instance.Get(5);
        array[0] = arg0;
        array[1] = arg1;
        array[2] = arg2;
        array[3] = arg3;
        array[4] = arg4;
        logger.Log(level, message, array, exception);
        Singleton<UiArrayPool<object>>.Instance.Free(ref array);
    }
        
    private static void HandleLog<T1, T2, T3, T4, T5, T6>(IUiLogger logger, UiLogLevel level, string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, Exception exception = null)
    {
        object[] array = Singleton<UiArrayPool<object>>.Instance.Get(6);
        array[0] = arg0;
        array[1] = arg1;
        array[2] = arg2;
        array[3] = arg3;
        array[4] = arg4;
        array[5] = arg5;
        logger.Log(level, message, array, exception);
        Singleton<UiArrayPool<object>>.Instance.Free(ref array);
    }
        
    private static void HandleLog<T1, T2, T3, T4, T5, T6, T7>(IUiLogger logger, UiLogLevel level, string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6, Exception exception = null)
    {
        object[] array = Singleton<UiArrayPool<object>>.Instance.Get(7);
        array[0] = arg0;
        array[1] = arg1;
        array[2] = arg2;
        array[3] = arg3;
        array[4] = arg4;
        array[5] = arg5;
        array[6] = arg6;
        logger.Log(level, message, array, exception);
        Singleton<UiArrayPool<object>>.Instance.Free(ref array);
    }
        
    private static void HandleLog<T1, T2, T3, T4, T5, T6, T7, T8>(IUiLogger logger, UiLogLevel level, string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6, T8 arg7, Exception exception = null)
    {
        object[] array = Singleton<UiArrayPool<object>>.Instance.Get(8);
        array[0] = arg0;
        array[1] = arg1;
        array[2] = arg2;
        array[3] = arg3;
        array[4] = arg4;
        array[5] = arg5;
        array[6] = arg6;
        array[7] = arg7;
        logger.Log(level, message, array, exception);
        Singleton<UiArrayPool<object>>.Instance.Free(ref array);
    }
        
    private static void HandleLog<T1, T2, T3, T4, T5, T6, T7, T8, T9>(IUiLogger logger, UiLogLevel level, string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6, T8 arg7, T9 arg8, Exception exception = null)
    {
        object[] array = Singleton<UiArrayPool<object>>.Instance.Get(9);
        array[0] = arg0;
        array[1] = arg1;
        array[2] = arg2;
        array[3] = arg3;
        array[4] = arg4;
        array[5] = arg5;
        array[6] = arg6;
        array[7] = arg7;
        array[8] = arg8;
        logger.Log(level, message, array, exception);
        Singleton<UiArrayPool<object>>.Instance.Free(ref array);
    }
        
    private static void HandleLog<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(IUiLogger logger, UiLogLevel level, string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6, T8 arg7, T9 arg8, T10 arg9, Exception exception = null)
    {
        object[] array = Singleton<UiArrayPool<object>>.Instance.Get(10);
        array[0] = arg0;
        array[1] = arg1;
        array[2] = arg2;
        array[3] = arg3;
        array[4] = arg4;
        array[5] = arg5;
        array[6] = arg6;
        array[7] = arg7;
        array[8] = arg8;
        array[9] = arg9;
        logger.Log(level, message, array, exception);
        Singleton<UiArrayPool<object>>.Instance.Free(ref array);
    }
        
    private static void HandleLog<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(IUiLogger logger, UiLogLevel level, string message, T1 arg0, T2 arg1, T3 arg2, T4 arg3, T5 arg4, T6 arg5, T7 arg6, T8 arg7, T9 arg8, T10 arg9, T11 arg10, Exception exception = null)
    {
        object[] array = Singleton<UiArrayPool<object>>.Instance.Get(11);
        array[0] = arg0;
        array[1] = arg1;
        array[2] = arg2;
        array[3] = arg3;
        array[4] = arg4;
        array[5] = arg5;
        array[6] = arg6;
        array[7] = arg7;
        array[8] = arg8;
        array[9] = arg9;
        array[10] = arg10;
        logger.Log(level, message, array, exception);
        Singleton<UiArrayPool<object>>.Instance.Free(ref array);
    }
    #endregion
}