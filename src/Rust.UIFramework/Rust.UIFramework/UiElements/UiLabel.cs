using Newtonsoft.Json;
using UI.Framework.Rust.Colors;
using UI.Framework.Rust.Components;
using UI.Framework.Rust.Json;
using UI.Framework.Rust.Positions;
using UnityEngine;
using Pool = Facepunch.Pool;

namespace UI.Framework.Rust.UiElements
{
    public class UiLabel : BaseUiComponent
    {
        public TextComponent TextComponent;
        public OutlineComponent Outline;

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

        public override void WriteComponents(JsonTextWriter writer)
        {
            JsonCreator.Add(writer, TextComponent);
            if (Outline != null)
            {
                JsonCreator.Add(writer, Outline);
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