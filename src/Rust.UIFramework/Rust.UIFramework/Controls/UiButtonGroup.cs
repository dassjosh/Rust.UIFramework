using System.Collections.Generic;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Controls.Data;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Controls
{
    public class UiButtonGroup : BaseUiControl
    {
        public UiSection Base;
        public List<UiButton> Buttons;
        
        public static UiButtonGroup Create(UiBuilder builder, BaseUiComponent parent, UiPosition pos, UiOffset offset, List<ButtonGroupData> buttons, int textSize, UiColor textColor, UiColor buttonColor, UiColor activeButtonColor, string command)
        {
            UiButtonGroup control = CreateControl<UiButtonGroup>();
            control.Base = builder.Section(parent, pos, offset);
            
            float buttonSize = 1f / (buttons.Count + 1);
            for (int i = 0; i <= buttons.Count; i++)
            {
                ButtonGroupData button = buttons[i];
                
                UiPosition buttonPos = UiPosition.Full.SliceHorizontal(buttonSize * i, buttonSize * (i + 1));
                control.Buttons.Add(builder.TextButton(control.Base, buttonPos, button.DisplayName, textSize, textColor, button.IsActive ? activeButtonColor : buttonColor, $"{command} {button.CommandArgs}"));
            }

            return control;
        }
        
        public static UiButtonGroup CreateNumeric(UiBuilder builder, BaseUiComponent parent, UiPosition pos, UiOffset offset, int value, int minValue, int maxValue, int textSize, UiColor textColor, UiColor buttonColor, UiColor activeButtonColor, string command)
        {
            List<ButtonGroupData> data = UiFrameworkPool.GetList<ButtonGroupData>();
            for (int i = minValue; i <= maxValue; i++)
            {
                string num = StringCache<int>.ToString(i);
                data.Add(new ButtonGroupData(num, num, i == value));
            }
            
            UiButtonGroup control = Create(builder, parent, pos, offset, data, textSize, textColor, buttonColor, activeButtonColor, command);
            UiFrameworkPool.FreeList(data);

            return control;
        }
        
        protected override void LeavePool()
        {
            base.LeavePool();
            Buttons = UiFrameworkPool.GetList<UiButton>();
        }

        protected override void EnterPool()
        {
            base.EnterPool();
            UiFrameworkPool.FreeList(Buttons);
            Base = null;
        }
    }
}