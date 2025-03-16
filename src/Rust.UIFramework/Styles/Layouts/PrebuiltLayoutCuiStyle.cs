using Oxide.Ext.UiFramework.Offsets;

namespace Oxide.Ext.UiFramework.Styles;

public class PrebuiltLayoutCuiStyle
{
    public static readonly PrebuiltLayoutCuiStyle Default = new();
    
    public float RowSpacing { get; init; } = 0f;
    public float ColSpacing { get; init; } = 0f;
    public UiPadding Padding { get; init; } = default;
}