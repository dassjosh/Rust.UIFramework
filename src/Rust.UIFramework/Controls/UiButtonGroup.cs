using System.Collections.Generic;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Controls.Data;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;


namespace Oxide.Ext.UiFramework.Controls;

public class UiButtonGroup : BaseUiControl
{
    public DirectionalLayoutComponent Layout;
    public List<UiButton> Buttons;
        
    public static UiButtonGroup Create(BaseUiBuilder builder, in UiReference parent, in UiPosition pos, in UiOffset offset, List<ButtonGroupData> buttons, int textSize, UiColor textColor, UiColor buttonColor, UiColor activeButtonColor)
    {
        UiButtonGroup control = CreateControl<UiButtonGroup>(builder);
        var section = builder.Section(parent, pos, offset);
        control.Layout = builder.DirectionalLayout(section, LayoutDirection.Horizontal);
        
        for (int i = 0; i < buttons.Count; i++)
        {
            ButtonGroupData buttonData = buttons[i];
            control.Buttons.Add(builder.TextButton(control.Layout, buttonData.DisplayName, textSize, textColor, buttonData.IsActive ? activeButtonColor : buttonColor, buttonData.Command));
        }

        return control;
    }
        
    public static UiButtonGroup CreateNumeric(BaseUiBuilder builder, in UiReference parent, in UiPosition pos, in UiOffset offset, int value, int minValue, int maxValue, int textSize, UiColor textColor, UiColor buttonColor, UiColor activeButtonColor, ICommandBuilder<int> command)
    {
        List<ButtonGroupData> data = builder.PluginPool.GetList<ButtonGroupData>();
        for (int i = minValue; i <= maxValue; i++)
        {
            string num = StringCache<int>.ToString(i);
            data.Add(new ButtonGroupData(num, command.Build(i), i == value));
        }
            
        UiButtonGroup control = Create(builder, parent, pos, offset, data, textSize, textColor, buttonColor, activeButtonColor);
        builder.PluginPool.FreeList(data);

        return control;
    }
        
    protected override void LeavePool()
    {
        base.LeavePool();
        Buttons = PluginPool.GetList<UiButton>();
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        PluginPool.FreeList(Buttons);
        Layout = null;
    }
}