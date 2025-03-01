using Oxide.Ext.UiFramework.Logging;

namespace Oxide.Ext.UiFramework.Exceptions.Logging
{
    /// <summary>
    /// Exceptions for the <see cref="UiLogger"/>
    /// </summary>
    public class UiLoggerException : BaseUiFrameworkException
    {
        private UiLoggerException(string message) : base(message) {}

        internal static void ThrowIfShutdown(UiLogHandler handler)
        {
            if (handler.IsShutdown)
            {
                throw new UiLoggerException("Cannot log into a logger that has been shutdown!");
            }
        }
    }
}