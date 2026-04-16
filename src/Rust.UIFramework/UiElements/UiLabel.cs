using System;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Json;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine;

namespace Oxide.Ext.UiFramework.UiElements;

[GenerateUiElement]
[GenerateBuilderMethods]
public partial class UiLabel : BaseUiComponent, IFadeIn<UiLabel>, IUiColor<UiLabel>
{
    public partial int FontSize { get; set; }
    public partial string Font { get; set; }
    public partial TextAnchor Align { get; set; }
    [PropertyName(nameof(TextComponent.Text))]
    public partial string TextValue { get; set; }
    public partial VerticalWrapMode VerticalOverflow { get; set; }
    public partial UiReference PlaceholderFor { get; set; }
    public partial float FadeIn { get; set; }
    public partial UiColor Color { get; set; }
    
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