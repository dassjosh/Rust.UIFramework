using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Positions;
using UnityEngine;
using Pool = Facepunch.Pool;

namespace Oxide.Ext.UiFramework.UiElements
{
    public class UiLabel : BaseUiComponent
    {
        public TextComponent TextComponent;
        public OutlineComponent Outline;
        public CountdownComponent Countdown;

        public static UiLabel Create(string text, int size, UiColor color, UiPosition pos, string font, TextAnchor align = TextAnchor.MiddleCenter)
        {
            UiLabel label = CreateBase<UiLabel>(pos);
            TextComponent textComp = label.TextComponent;
            textComp.Text = text;
            textComp.FontSize = size;
            textComp.Color = color;
            textComp.Align = align;
            textComp.Font = font;
            return label;
        }

        public void AddTextOutline(UiColor color)
        {
            Outline = Pool.Get<OutlineComponent>();
            Outline.Color = color;
        }

        public void AddTextOutline(UiColor color, string distance)
        {
            AddTextOutline(color);
            Outline.Distance = distance;
        }

        public void AddTextOutline(UiColor color, string distance, bool useGraphicAlpha)
        {
            AddTextOutline(color, distance);
            Outline.UseGraphicAlpha = useGraphicAlpha;
        }

        public void AddCountdown(int startTime, int endTime, int step, string command)
        {
            Countdown = Pool.Get<CountdownComponent>();
            Countdown.StartTime = startTime;
            Countdown.EndTime = endTime;
            Countdown.Step = step;
            Countdown.Command = command;
        }

        public override void WriteComponents(JsonTextWriter writer)
        {
            JsonCreator.Add(writer, TextComponent);
            if (Outline != null)
            {
                JsonCreator.Add(writer, Outline);
            }

            if (Countdown != null)
            {
                JsonCreator.Add(writer, Countdown);
            }

            base.WriteComponents(writer);
        }

        public override void EnterPool()
        {
            base.EnterPool();
            Pool.Free(ref TextComponent);
            if (Outline != null)
            {
                Pool.Free(ref Outline);
            }
        }

        public override void LeavePool()
        {
            base.LeavePool();
            TextComponent = Pool.Get<TextComponent>();
        }

        public override void SetFadeIn(float duration)
        {
            TextComponent.FadeIn = duration;
        }
    }
}