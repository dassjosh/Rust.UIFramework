using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;
using Oxide.Ext.UiFramework.UiElements;
using UnityEngine;
using UnityEngine.UI;

namespace Rust.UiFramework.UnitTests.Components.Core;

public static class ComponentHelpers
{
    public static void PopulateImage(ImageComponent image)
    {
        image.Color = UiColors.Red;
        image.FadeIn = 1;
        image.Sprite = UiSprites.Content.Ui.UiBackgroundRounded;
        image.Material = UiMaterials.Icons.IconMaterial;
        image.ImageType = Image.Type.Sliced;
        image.PlaceholderFor = new UiReference("parent", "name");
        image.FillCenter = false;
    } 
    
    public static void PopulateNineSlice(NineSliceComponent slice)
    {
        slice.Png = "png";
        slice.Slice = new UiBorderWidth(1, 2, 4, 8);
    }

    public static void PopulateButton(ButtonComponent button)
    {
        button.Color = UiColors.Red;
        button.FadeIn = 1;
        button.Sprite = UiSprites.Content.Ui.UiBackgroundRounded;
        button.Material = UiMaterials.Icons.IconMaterial;
        button.ImageType = Image.Type.Sliced;
        button.Command = "command";
    }
    
    public static void PopulateItemIcon(ItemIconComponent icon)
    {
        PopulateImage(icon);
        icon.ItemId = 123;
        icon.SkinId = 123ul << 48;
    }
    
    public static void PopulateText(TextComponent text)
    {
        text.Color = UiColors.Green;
        text.FadeIn = 2;
        text.FontSize = 24;
        text.Font = UiFontCache.GetUiFont(UiFont.PermanentMarker);
        text.Align = TextAnchor.UpperRight;
        text.Text = "text 123";
        text.VerticalOverflow = VerticalWrapMode.Overflow;
        text.PlaceholderFor = new UiReference("parent", "name");
    }

    public static void PopulateInput(InputComponent input)
    {
        PopulateText(input);
        input.CharsLimit = 16;
        input.Command = "command";
        input.Mode = InputMode.NeedsKeyboard | InputMode.AutoFocus;
        input.LineType = InputField.LineType.MultiLineNewline;
        input.Placeholder = new UiReference("parent", "name");
    }

    public static void PopulateRawImage(RawImageComponent image)
    {
        image.Color = UiColors.Yellow;
        image.FadeIn = 3.5f;
        image.Material = UiMaterials.Content.Ui.NameFontMaterial;
        image.Image = "https://www.example.com";
        image.PlaceholderFor = new UiReference("parent", "name");
    }
    
    public static void PopulatePlayerAvatar(PlayerAvatarComponent avatar)
    {
        PopulateRawImage(avatar);
        avatar.AvatarType = AvatarType.Medium;
        avatar.SteamId = 123ul << 48;
    }
    
    public static void PopulateScrollView(ScrollViewComponent scrollView)
    {
        scrollView.Elasticity = 100f;
        scrollView.MovementType = ScrollRect.MovementType.Elastic;
        scrollView.Inertia = true;
        scrollView.DecelerationRate = 200f;
        scrollView.ScrollSensitivity = 300f;
        scrollView.HorizontalScrollProgress = 0.1f;
        scrollView.VerticalScrollProgress = 0.2f;
        scrollView.UpdateContentTransform(new UiPosition(0.1f, 0.2f, 0.3f, 0.4f), new UiOffset(100, 200, 300, 400), new Vector2(0.1f, 0.2f));
    }

    public static void PopulatePlayingCard(PlayingCardComponent card)
    {
        card.Suit = UiSuit.Clubs;
        card.Rank = UiRank.Eight;
        card.CardType = UiCardType.SmallTransparent;
        card.FadeIn = 1.5f;
        card.Material = UiMaterials.Content.Ui.UiBackgroundBlur;
        card.Color = UiColors.Orange;
    }
}