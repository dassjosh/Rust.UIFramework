using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Controls
{
    public class UiPicker : BaseUiControl
    {
        public UiButton Previous;
        public UiLabel Value;
        public UiButton Next;

        public static UiPicker Create(UiBuilder builder, UiOffset pos, string value, int fontSize, UiColor textColor, UiColor backgroundColor, float height, string incrementCommand, string decrementCommand)
        {
            UiPicker control = CreateControl<UiPicker>();
            
            UiOffset slice = pos.SliceVertical(0, (int)height * 2);
            control.Next =  builder.TextButton(builder.Root, UiPosition.BottomLeft, slice, "˅", fontSize, textColor, backgroundColor, decrementCommand);
            slice = slice.MoveY(height);
            control.Value = builder.Label(builder.Root, UiPosition.BottomLeft, slice, value, fontSize, textColor);
            slice = slice.MoveY(height);
            control.Previous = builder.TextButton(builder.Root, UiPosition.BottomLeft, slice, "˄", fontSize, textColor, backgroundColor, incrementCommand);
            
            return control;
        }

        protected override void EnterPool()
        {
            base.EnterPool();
            Previous = null;
            Value = null;
            Next = null;
        }

        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
        }
    }
}