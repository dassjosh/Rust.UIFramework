using Oxide.Ext.UiFramework.UiElements;

namespace Rust.UiFramework.UnitTests.Global.Verify.IgnoreMembers;

public class BaseUiComponentIgnores : IVerifyIgnoreMembers
{
    public void Register()
    {
        VerifierSettings.IgnoreMember<BaseUiComponent>(nameof(BaseUiComponent.RectTransform));
        VerifierSettings.IgnoreMember<BaseUiComponent>(nameof(BaseUiComponent.Position));
        VerifierSettings.IgnoreMember<BaseUiComponent>(nameof(BaseUiComponent.Offset));
        VerifierSettings.IgnoreMember<BaseUiComponent>(nameof(BaseUiComponent.Rotation));
        VerifierSettings.IgnoreMember<BaseUiComponent>(nameof(BaseUiComponent.Padding));
    }
}