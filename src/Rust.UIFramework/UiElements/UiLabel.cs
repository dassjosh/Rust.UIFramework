using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using UnityEngine;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiLabel : BaseUiOutline
{
    public readonly TextComponent Text = new();
    public CountdownComponent Countdown;

    public static UiLabel Create(in UiPosition pos, in UiOffset offset, UiColor color, string text, int size, string font, TextAnchor align = TextAnchor.MiddleCenter)
    {
        UiLabel label = CreateBase<UiLabel>(pos, offset);
        TextComponent textComp = label.Text;
        textComp.Text = text;
        textComp.FontSize = size;
        textComp.Color = color;
        textComp.Align = align;
        textComp.Font = font;
        return label;
    }

    public CountdownComponent AddCountdown(float startTime, float endTime, float step, float interval, TimerFormat timerFormat, string numberFormat, bool destroyIfDone, string command)
    {
        Countdown = UiFrameworkPool.Get<CountdownComponent>();
        Countdown.StartTime = startTime;
        Countdown.EndTime = endTime;
        Countdown.Step = step;
        Countdown.Interval = interval;
        Countdown.TimerFormat = timerFormat;
        Countdown.NumberFormat = numberFormat;
        Countdown.DestroyIfDone = destroyIfDone;
        Countdown.Command = command;
        return Countdown;
    }
        
    public void SetFadeIn(float duration)
    {
        Text.FadeIn = duration;
    }

    protected override void WriteComponents(JsonFrameworkWriter writer)
    {
        Text.WriteComponent(writer);
        Countdown?.WriteComponent(writer);
        base.WriteComponents(writer);
    }

    protected override void EnterPool()
    {
        base.EnterPool();
        Text.Reset();
        Countdown?.Dispose();
        Countdown = null;
    }
}