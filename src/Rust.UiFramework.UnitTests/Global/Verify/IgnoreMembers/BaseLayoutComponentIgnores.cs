using Oxide.Ext.UiFramework.Components;

namespace Rust.UiFramework.UnitTests.Global.Verify.IgnoreMembers;

public class BaseLayoutComponentIgnores : IVerifyIgnoreMembers
{
    public void Register()
    {
        VerifierSettings.IgnoreMember<BaseLayoutComponent>(nameof(BaseLayoutComponent.Owner));
        VerifierSettings.IgnoreMember<BaseLayoutComponent>(nameof(BaseLayoutComponent.Reference));
    }
}