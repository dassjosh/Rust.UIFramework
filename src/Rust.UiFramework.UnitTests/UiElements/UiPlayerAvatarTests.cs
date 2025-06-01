using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Constants;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.UiElements;

namespace Rust.UiFramework.UnitTests.UiElements;

public class UiPlayerAvatarTests() : BasePopulateUiElementsTests<UiPlayerAvatar>(PopulateFluent, PopulateSetters)
{
    private static readonly PlayerAvatarComponent Image = new()
    {
        Color = UiColors.Gray,
        FadeIn = 1.5f,
        Material = UiMaterials.Icons.IconMaterial,
        SteamId = 112233,
        AvatarType = AvatarType.Small
    };

    private static void PopulateFluent(UiPlayerAvatar image)
    {
        image
            .SetColor(Image.Color)
            .SetFadeIn(Image.FadeIn)
            .SetMaterial(Image.Material)
            .SetSteamId(Image.SteamId)
            .SetAvatarType(Image.AvatarType);
    }

    private static void PopulateSetters(UiPlayerAvatar image)
    {
        image.Color = Image.Color;
        image.FadeIn = Image.FadeIn;
        image.Material = Image.Material;
        image.SteamId = Image.SteamId;
        image.AvatarType = Image.AvatarType;
    }

    protected override void AssertValues(UiPlayerAvatar element)
    {
        element.Color.Should().Be(Image.Color);
        element.FadeIn.Should().Be(Image.FadeIn);
        element.Material.Should().Be(Image.Material);
        element.SteamId.Should().Be(Image.SteamId);
        element.AvatarType.Should().Be(Image.AvatarType);
    }
}