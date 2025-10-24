using Oxide.Ext.UiFramework.Components;

namespace Rust.UiFramework.UnitTests.Global.Verify.IgnoreMembers;

public class CoreComponentIgnore : IVerifyIgnoreMembers
{
    public void Register()
    {
        VerifierSettings.IgnoreMember<CoreComponent>(nameof(CoreComponent.AllowMultiple));
    }
}