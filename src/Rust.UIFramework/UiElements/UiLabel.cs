using System;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using UnityEngine;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiLabel : BaseUiComponent
{
    public readonly TextComponent Text = new();
    internal override CoreComponent Component => Text;

    public static UiLabel Create(UiColor color, string text, int size, string font, TextAnchor align = TextAnchor.MiddleCenter)
    {
        UiLabel label = CreateBase<UiLabel>();
        TextComponent textComp = label.Text;
        textComp.Text = text;
        textComp.FontSize = size;
        textComp.Color = color;
        textComp.Align = align;
        textComp.Font = font;
        return label;
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

    [Obsolete]
    public CountdownComponent AddCountdown(float startTime, float endTime, float step, float interval, TimerFormat timerFormat, string numberFormat, bool destroyIfDone, string command) => AddCountdown(startTime, endTime, command, step, interval, timerFormat, numberFormat, destroyIfDone);
}