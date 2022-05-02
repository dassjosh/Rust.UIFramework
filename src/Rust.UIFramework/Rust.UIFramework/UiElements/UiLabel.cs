using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using UnityEngine;

namespace Oxide.Ext.UiFramework.UiElements
{
    public class UiLabel : BaseUiTextOutline
    {
        public TextComponent Text;
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

        public void AddCountdown(int startTime, int endTime, int step, string command)
        {
            Countdown = UiFrameworkPool.Get<CountdownComponent>();
            Countdown.StartTime = startTime;
            Countdown.EndTime = endTime;
            Countdown.Step = step;
            Countdown.Command = command;
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
            UiFrameworkPool.Free(ref Text);

            if (Countdown != null)
            {
                UiFrameworkPool.Free(ref Countdown);
            }
        }

        protected override void LeavePool()
        {
            base.LeavePool();
            Text = UiFrameworkPool.Get<TextComponent>();
        }

        public override void SetFadeIn(float duration)
        {
            Text.FadeIn = duration;
        }
    }
}