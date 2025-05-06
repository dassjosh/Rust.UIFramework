using Network;
using Oxide.Ext.UiFramework.Builder.UI;

namespace Oxide.Ext.UiFramework.Builder.Cached;

public class CachedUiBuilder : BaseBuilder
{
    private readonly byte[] _cachedJson;

    private CachedUiBuilder(UiBuilder builder)
    {
        _cachedJson = builder.GetBytes();
        RootName = builder.GetRootName();
    }

    internal static CachedUiBuilder CreateCachedBuilder(UiBuilder builder) => new(builder);

    public override byte[] GetBytes() => _cachedJson;
        
    internal override void SendUi(SendInfo send)
    {
        AddUi(send, GetBytes());
    }
}