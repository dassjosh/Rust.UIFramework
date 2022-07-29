using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Controls
{
    public class UiInputBackground : BaseUiControl
    {
        public UiInput Input;
        public UiPanel Background;
        
        public static UiInputBackground Create(UiBuilder builder, BaseUiComponent parent, UiPosition pos, UiOffset offset, string text, int fontSize, UiColor textColor, UiColor backgroundColor, string command, TextAnchor align = TextAnchor.MiddleCenter, int charsLimit = 0, InputMode mode = InputMode.Default, InputField.LineType lineType = InputField.LineType.SingleLine)
        {
            UiInputBackground control = CreateControl<UiInputBackground>();
            control.Background = builder.Panel(parent,  pos, offset, backgroundColor);
            control.Input = builder.Input(control.Background, UiPosition.HorizontalPaddedFull, text, fontSize, textColor, command, align, charsLimit, mode, lineType);
            return control;
        }

        protected override void EnterPool()
        {
            base.EnterPool();
            Input = null;
            Background = null;
        }

        public override void DisposeInternal()
        {
            UiFrameworkPool.Free(this);
        }
    }
}