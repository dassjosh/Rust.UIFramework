using Newtonsoft.Json;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Positions;
using UnityEngine;
using Pool = Facepunch.Pool;

namespace Oxide.Ext.UiFramework.UiElements
{
    public class UiLabel : BaseUiComponent
    {
        public TextComponent Text;
        public OutlineComponent Outline;
        public CountdownComponent Countdown;

        public static UiLabel Create(string text, int size, UiColor color, UiPosition pos, string font, TextAnchor align = TextAnchor.MiddleCenter)
        {
            UiLabel label = CreateBase<UiLabel>(pos);
            TextComponent textComp = label.Text;
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

        public void AddTextOutline(UiColor color, Vector2 distance)
        {
            AddTextOutline(color);
            Outline.Distance = distance;
        }

        public void AddTextOutline(UiColor color, Vector2 distance, bool useGraphicAlpha)
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

        protected override void WriteComponents(JsonTextWriter writer)
        {
            Text.WriteComponent(writer);
            Outline?.WriteComponent(writer);
            Countdown?.WriteComponent(writer);
            base.WriteComponents(writer);
        }

        public override void EnterPool()
        {
            base.EnterPool();
            Pool.Free(ref Text);
            if (Outline != null)
            {
                Pool.Free(ref Outline);
            }
            
            if (Countdown != null)
            {
                Pool.Free(ref Countdown);
            }
        }

        public override void LeavePool()
        {
            base.LeavePool();
            Text = Pool.Get<TextComponent>();
        }

        public override void SetFadeIn(float duration)
        {
            Text.FadeIn = duration;
        }
    }
}