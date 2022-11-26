using Network;

namespace Oxide.Ext.UiFramework.Builder.Cached
{
    public class CachedUiBuilder : BaseUiBuilder
    {
        private readonly byte[] _cachedJson;

        private CachedUiBuilder(UiBuilder builder)
        {
            _cachedJson = builder.GetBytes();
            RootName = builder.GetRootName();
        }

        internal static CachedUiBuilder CreateCachedBuilder(UiBuilder builder) => new CachedUiBuilder(builder);

        public override byte[] GetBytes() => _cachedJson;
        
        protected override void AddUi(SendInfo send) => AddUi(send, GetBytes());
    }
}