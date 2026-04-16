namespace Oxide.Ext.UiFramework.Enums;

public enum NamingMode : byte
{
    /// <summary>
    /// Name will not automatically be set by the builder
    /// </summary>
    None, 
    
    /// <summary>
    /// Uses the passed in reference for the reference for the newly added UI Element
    /// </summary>
    Reference,
    
    /// <summary>
    /// Adds the newly added element as a child of the reference
    /// </summary>
    Child,
    
    /// <summary>
    /// Can be used by custom naming strategies
    /// </summary>
    Custom
}