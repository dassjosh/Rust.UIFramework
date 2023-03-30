using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Json;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using UnityEngine;

namespace Oxide.Ext.UiFramework.UiElements
{
    public class UiLabel : BaseUiOutline
    {
        public TextComponent Text;
        public CountdownComponent Countdown;

        public static UiLabel Create(UiPosition pos, UiOffset offset, UiColor color, string text, int size, string font, TextAnchor align = TextAnchor.MiddleCenter)
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

        public void AddCountdown(int startTime, int endTime, int step, string command)
        {
            Countdown = UiFrameworkPool.Get<CountdownComponent>();
            Countdown.StartTime = startTime;
            Countdown.EndTime = endTime;
            Countdown.Step = step;
            Countdown.Command = command;
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
            Text.Dispose();
            Text = null;
            Countdown?.Dispose();
            Countdown = null;
        }

        protected override void LeavePool()
        {
            base.LeavePool();
            Text = UiFrameworkPool.Get<TextComponent>();
        }
    }
}