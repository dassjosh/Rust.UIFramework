using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Controls
{
    public class UiColorPicker : BaseUiControl
    {
        public UiSection Anchor;
        public UiButton Button;
        public UiLabel Text;
        public UiPanel Color;
        
        public static UiColorPicker Create(UiBuilder builder, BaseUiComponent parent, UiPosition pos, UiOffset offset, UiColor selectedColor, int fontSize, UiColor textColor, UiColor backgroundColor, string openCommand)
        {
            UiColorPicker control = CreateControl<UiColorPicker>();
            control.Anchor = builder.Anchor(parent, pos, offset);
            control.Button = builder.CommandButton(parent, pos, offset, backgroundColor, $"{openCommand} {control.Anchor.Name}");
            control.Text = builder.Label(control.Button, UiPosition.Full, new UiOffset(5, 0, 0, 0), selectedColor.ToHtmlColor(), fontSize, textColor, TextAnchor.MiddleLeft);
            control.Color = builder.Panel(control.Button, UiPosition.Full.SliceHorizontal(1f - (pos.Max.y - pos.Min.y), 1), new UiOffset(-4, 4, -4, -4), selectedColor);
            return control;
        }
        
        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
        }
    }
}