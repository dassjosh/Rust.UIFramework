using Oxide.Ext.UiFramework.Pooling;

namespace Rust.UiFramework.UnitTests.Global.Verify.IgnoreMembers;

public class BasePoolableIgnores : IVerifyIgnoreMembers
{
    public void Register()
    {
        VerifierSettings.IgnoreMember<BasePoolable>(nameof(BasePoolable.IsPooled));
    }
}