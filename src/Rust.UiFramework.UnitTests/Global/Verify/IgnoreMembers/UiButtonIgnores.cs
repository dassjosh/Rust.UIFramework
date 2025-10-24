using Oxide.Ext.UiFramework.UiElements;

namespace Rust.UiFramework.UnitTests.Global.Verify.IgnoreMembers;

public class UiButtonIgnores : IVerifyIgnoreMembers
{
    public void Register()
    {
        VerifierSettings.IgnoreMember<UiButton>(nameof(UiButton.ColorMultiplier));
        VerifierSettings.IgnoreMember<UiButton>(nameof(UiButton.FadeDuration));
        VerifierSettings.IgnoreMember<UiButton>(nameof(UiButton.HighlightedColor));
        VerifierSettings.IgnoreMember<UiButton>(nameof(UiButton.PressedColor));
        VerifierSettings.IgnoreMember<UiButton>(nameof(UiButton.SelectedColor));
    }
}