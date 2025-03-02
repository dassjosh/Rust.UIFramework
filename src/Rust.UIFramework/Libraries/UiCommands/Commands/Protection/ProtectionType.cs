namespace Oxide.Ext.UiFramework.Libraries.UiCommands;

public enum ProtectionType
{
    /// <summary>
    /// No Protection is added to this command
    /// </summary>
    None,
    
    /// <summary>
    /// A single protection key is generated for this command
    /// Every command call will use the same protection key
    /// </summary>
    Simple,
    
    /// <summary>
    /// A unique protection key is generated for each call to this command
    /// </summary>
    Advanced,
    
    /// <summary>
    /// Command args are not passed to the client.
    /// Args stay server side and a unique command key is generated for the client to use
    /// </summary>
    Extreme
}