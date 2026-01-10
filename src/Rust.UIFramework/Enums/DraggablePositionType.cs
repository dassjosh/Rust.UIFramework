namespace Oxide.Ext.UiFramework.Enums;

/// <summary>
/// Defines how the draggable position is reported back to the server via RPC
/// </summary>
public enum DraggablePositionType : byte
{
    /// <summary>Position normalized to screen coordinates (0-1)</summary>
    NormalizedScreen = 0,

    /// <summary>Position normalized to parent element coordinates (0-1)</summary>
    NormalizedParent = 1,

    /// <summary>Position relative to last drop position in pixels</summary>
    Relative = 2,

    /// <summary>Position relative to anchor point in pixels</summary>
    RelativeAnchor = 3
}
