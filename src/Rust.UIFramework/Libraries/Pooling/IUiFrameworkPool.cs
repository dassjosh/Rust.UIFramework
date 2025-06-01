namespace Oxide.Ext.UiFramework.Libraries;

/// <summary>
/// Interface for plugins to use that need access to a pool
/// </summary>
public interface IUiFrameworkPool
{
    /// <summary>
    /// Pool for plugins to use
    /// </summary>
    UiFrameworkPluginPool Pool { get; set; }
}