using System.IO;

namespace Oxide.Ext.UiFramework.Pooling;

/// <summary>
/// Pool for MemorySteam
/// </summary>
internal class MemoryStreamPool() : BaseObjectPool<MemoryStream>(MemoryStreamPoolPolicy.Instance)
{
    private sealed class MemoryStreamPoolPolicy : IPooledObjectPolicy<MemoryStream>
    {
        public static readonly MemoryStreamPoolPolicy Instance = new();

        public int GetPoolSize(PoolSettings settings) => settings.MemoryStreamPoolSize;
        public MemoryStream Create() => new();
        public void Get(MemoryStream item) { }
        public bool Return(MemoryStream item)
        {
            item.SetLength(0);
            return true;
        }
    }
}