using Oxide.Ext.UiFramework.Components;

namespace Oxide.Ext.UiFramework.Extensions;

public static class BaseComponentExt
{
    public static bool TryAreEquivalent(this BaseComponent self, BaseComponent other)
    {
        return ReferenceEquals(self, other) || (self?.AreEquivalent(other) ?? false); 
    }
}