using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Enums;

namespace Rust.UiFramework.UnitTests.Components.Core;

public class PlayerAvatarComponentTests() : BaseTheoryComponentTests<PlayerAvatarComponent, PlayerAvatarComponentTests.TheoryRow>(ComponentHelpers.PopulatePlayerAvatar)
{
    public record TheoryRow(AvatarType AvatarType, ulong SteamId);
    
    protected override void PopulateTheory(PlayerAvatarComponent component, TheoryRow row)
    {
        component.AvatarType = row.AvatarType;
        component.SteamId = row.SteamId;
    }
    
    public static TheoryData<TheoryRow> TheoryData =>
    [
        new TheoryRow(AvatarType.Small, UnitTestsConstants.AvatarSteamId),
        new TheoryRow(AvatarType.Medium, UnitTestsConstants.AvatarSteamId),
        new TheoryRow(AvatarType.Large, UnitTestsConstants.AvatarSteamId),
        new TheoryRow(AvatarType.Large, 999),
    ];
}