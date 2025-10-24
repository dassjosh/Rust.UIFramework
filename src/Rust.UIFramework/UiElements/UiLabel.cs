using System;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine;

namespace Oxide.Ext.UiFramework.UiElements;

[GenerateUiElement(typeof(IUiLabel))]
public partial class UiLabel : BaseUiComponent, IUiLabel
{
    public readonly TextComponent Text;

    public UiLabel() : this(new TextComponent()) { }

    private UiLabel(TextComponent component) : base(component)
    {
        Text = component;
    }

    public UiLabel Init(string text, int size, UiColor color, TextAnchor align, string font)
    {
        TextValue = text;
        FontSize = size;
        Color = color;
        Align = align;
        Font = font;
        return this;
    }

    public UiLabel SetPlaceholderFor(UiInput input) => SetPlaceholderFor(input.Reference);
    
    public CountdownComponent AddCountdown() => Text.GetOrAddSubComponent<CountdownComponent>();
    
    public CountdownComponent AddCountdown(float startTime, float endTime, string command, 
        float step = JsonDefaults.Countdown.Step, 
        float interval = JsonDefaults.Countdown.Interval, 
        TimerFormat timerFormat = JsonDefaults.Countdown.TimerFormat, 
        string numberFormat = JsonDefaults.Countdown.NumberFormat, 
        bool destroyIfDone = JsonDefaults.Countdown.DestroyIfDone)
    {
        CountdownComponent countdown = AddCountdown();
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

    [Obsolete("Use AddCountdown(float startTime, float endTime, string command, float step = JsonDefaults.Countdown.Step,  float interval = JsonDefaults.Countdown.Interval, TimerFormat timerFormat = JsonDefaults.Countdown.TimerFormat, string numberFormat = JsonDefaults.Countdown.NumberFormat, bool destroyIfDone = JsonDefaults.Countdown.DestroyIfDone) Instead")]
    public CountdownComponent AddCountdown(float startTime, float endTime, float step, float interval, TimerFormat timerFormat, string numberFormat, bool destroyIfDone, string command) => AddCountdown(startTime, endTime, command, step, interval, timerFormat, numberFormat, destroyIfDone);
}