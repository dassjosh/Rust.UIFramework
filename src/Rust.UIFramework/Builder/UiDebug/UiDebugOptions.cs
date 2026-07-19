using System.Diagnostics.Contracts;

namespace Oxide.Ext.UiFramework.Builder;

public readonly record struct UiDebugOptions(string Identifier, UiDebugModes Mode = UiDebugModes.File)
{
    [Pure]
    public UiDebugOptions WithModes(UiDebugModes mode) => this with { Mode = mode };
    
    [Pure]
    public UiDebugOptions NotWithModes(UiDebugModes modes) => this with { Mode = Mode & ~modes };
    
    [Pure]
    public UiDebugOptions WithIdentifier(string identifier) => this with { Identifier = identifier };
}