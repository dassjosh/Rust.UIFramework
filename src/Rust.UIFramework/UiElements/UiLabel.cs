using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using UnityEngine;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiLabel : BaseUiComponent
{
    public readonly TextComponent Text = new();
    internal override CoreComponent Component => Text;

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
        
    public void SetFadeIn(float duration)
    {
        Text.FadeIn = duration;
    }
}