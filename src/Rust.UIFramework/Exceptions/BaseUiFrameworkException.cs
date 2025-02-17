using System;

namespace Oxide.Ext.UiFramework.Exceptions
{
    /// <summary>
    /// Represents a base UI Framework exception
    /// </summary>
    public abstract class BaseUiFrameworkException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        protected BaseUiFrameworkException() {}
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Exception message</param>
        protected BaseUiFrameworkException(string message) : base($"UiFramework Extension Exception ({UiFrameworkExtension.Instance.Version}): {message}")
        {
            
        }
    }
}