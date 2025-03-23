using System.Collections.Generic;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Layouts;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;


namespace Oxide.Ext.UiFramework.Controls;

public class UiButtonGroup : BaseUiControl
{
    public UiDirectionalLayout Layout;
    public List<UiButton> Buttons;
        
    public static UiButtonGroup Create(BaseUiBuilder builder, in UiReference parent, in UiPosition pos, in UiOffset offset, List<ButtonGroupData> buttons, int textSize, UiColor textColor, UiColor buttonColor, UiColor activeButtonColor, string command)
    {
        UiButtonGroup control = CreateControl<UiButtonGroup>();
        control.Layout = builder.DirectionalLayout(parent, pos, offset, buttons.Count);
        
        for (int i = 0; i < buttons.Count; i++)
        {
            ButtonGroupData buttonData = buttons[i];
            control.Buttons.Add(builder.TextButton(control.Layout, buttonData.DisplayName, textSize, textColor, buttonData.IsActive ? activeButtonColor : buttonColor, $"{command} {buttonData.CommandArgs}"));
        }

        return control;
    }
        
    public static UiButtonGroup CreateNumeric(BaseUiBuilder builder, in UiReference parent, in UiPosition pos, in UiOffset offset, int value, int minValue, int maxValue, int textSize, UiColor textColor, UiColor buttonColor, UiColor activeButtonColor, string command)
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
        Layout = null;
    }
}