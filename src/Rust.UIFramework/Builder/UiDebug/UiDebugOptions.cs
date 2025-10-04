using System.Diagnostics.Contracts;

namespace Oxide.Ext.UiFramework.Builder;

public readonly struct UiDebugOptions(string identifier, UiDebugModes modes = UiDebugModes.File)
{
    public readonly string Identifier = identifier;
    public readonly UiDebugModes Mode = modes;
    
    [Pure]
    public UiDebugOptions WithModes(UiDebugModes mode) => new(Identifier, mode);
    
    [Pure]
    public UiDebugOptions NotWithModes(UiDebugModes modes) => new(Identifier, Mode & ~modes);
    
    [Pure]
    public UiDebugOptions WithIdentifier(string identifier) => new(identifier, Mode);
}