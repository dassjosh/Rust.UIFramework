using Oxide.Core;

namespace Oxide.Ext.UiFramework.Plugins;

/// <summary>
/// Represents an interface for a plugin
/// </summary>
public interface IPluginBase
{
    /// <summary>
    /// Name of the plugin
    /// </summary>
    string Name { get; }
        
    /// <summary>
    /// Title of the plugin
    /// </summary>
    string Title { get; }

    /// <summary>
    /// Description of the plugin
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Author of the plugin
    /// </summary>
    string Author { get; }

    /// <summary>
    /// Version of the plugin
    /// </summary>
    VersionNumber Version { get; }
    
    /// <summary>
    /// Whether the plugin is loaded
    /// </summary>
    bool IsLoaded { get; }


    object CallHook(string hook, params object[] args);
}

public static class IPluginBaseExt
{
    extension(IPluginBase plugin)
    {
        public bool IsValid => plugin is { IsLoaded: true };
    }
}