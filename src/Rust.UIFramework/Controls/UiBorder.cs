using System;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Controls;

[Obsolete("Use UiNineSlice Border(UiReference parent, UiBorderWidth width, UiColor? color = default) instead")]
public class UiBorder : BaseUiControl
{
    public UiPanel Left;
    public UiPanel Right;
    public UiPanel Top;
    public UiPanel Bottom;

    public static UiBorder Create(BaseUiBuilder builder, in UiReference parent, UiColor color, in UiBorderWidth width, BorderMode border = BorderMode.All)
    {
        UiBorder control = CreateControl<UiBorder>(builder.PluginPool);
        if (width.IsEmpty())
        {
            return control;
        }
            
        bool top = HasBorderFlag(border, BorderMode.Top);
        bool left = HasBorderFlag(border, BorderMode.Left);
        bool bottom = HasBorderFlag(border, BorderMode.Bottom);
        bool right = HasBorderFlag(border, BorderMode.Right);
        
        float topWidth = top ? width.Top : 0;
        float leftWidth = left ? -width.Left : 0;
        float bottomWidth = bottom ? -width.Bottom : 0;
        float rightWidth = right ? width.Right : 0;

        if (top)
        {
            control.Top = builder.Panel(parent, UiPosition.Top, new UiOffset(leftWidth, 0, rightWidth, topWidth), color);
        }
            
        if (left)
        {
            control.Left = builder.Panel(parent, UiPosition.Left, new UiOffset(leftWidth, bottomWidth, 0, topWidth), color);
        }
            
        if (bottom)
        {
            control.Bottom = builder.Panel(parent, UiPosition.Bottom, new UiOffset(leftWidth, bottomWidth, rightWidth, 0), color);
        }
            
        if (right)
        {
            control.Right = builder.Panel(parent, UiPosition.Right, new UiOffset(0, bottomWidth, rightWidth, topWidth), color);
        }

        return control;
    }
        
    private static bool HasBorderFlag(BorderMode mode, BorderMode flag)
    {
        return (mode & flag) != 0;
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        Left = null;
        Top = null;
        Right = null;
        Bottom = null;
    }
}