using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using UnityEngine;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiLabel : BaseUiText<UiLabel>
{
    public readonly TextComponent Text = new();
    internal override CoreComponent Component => Text;

    public static UiLabel Create(UiColor color, string text, int size, string font, TextAnchor align = TextAnchor.MiddleCenter)
    {
        UiLabel label = CreateBase<UiLabel>();
        ConfigureLabel(label, color, text, size, font, align);
        return label;
    }
    
    public static UiLabel Create(in UiPosition pos, in UiOffset offset, UiColor color, string text, int size, string font, TextAnchor align = TextAnchor.MiddleCenter)
    {
        UiLabel label = CreateBase<UiLabel>(pos, offset);
        ConfigureLabel(label, color, text, size, font, align);
        return label;
    }

    private static void ConfigureLabel(UiLabel label, UiColor color, string text, int size, string font, TextAnchor align = TextAnchor.MiddleCenter)
    {
        TextComponent textComp = label.Text;
        textComp.Text = text;
        textComp.FontSize = size;
        textComp.Color = color;
        textComp.Align = align;
        textComp.Font = font;
    }

    public CountdownComponent AddCountdown(float startTime, float endTime, string command, 
        float step = JsonDefaults.Countdown.StepValue, 
        float interval = JsonDefaults.Countdown.IntervalValue, 
        TimerFormat timerFormat = JsonDefaults.Countdown.TimeFormatValue, 
        string numberFormat = JsonDefaults.Countdown.NumberFormatValue, 
        bool destroyIfDone = JsonDefaults.Countdown.DestroyIfDone)
    {
        CountdownComponent countdown = Text.AddSubComponent<CountdownComponent>();
        countdown.StartTime = startTime;
        countdown.EndTime = endTime;
        countdown.Step = step;
        countdown.Interval = interval;
        countdown.TimerFormat = timerFormat;
        countdown.NumberFormat = numberFormat;
        countdown.DestroyIfDone = destroyIfDone;
        countdown.Command = command;
        return countdown;
    }
}