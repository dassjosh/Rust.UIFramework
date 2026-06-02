using System.Text;

namespace Oxide.Ext.UiFramework.Pooling;

/// <summary>
/// Pool for StringBuilders
/// </summary>
internal class StringBuilderPool() : BaseObjectPool<StringBuilder>(StringBuilderPoolPolicy.Instance)
{
    private sealed class StringBuilderPoolPolicy : IPooledObjectPolicy<StringBuilder>
    {
        public static readonly StringBuilderPoolPolicy Instance = new();

        private const int MaxCapacity = 1024 * 4;

        public int GetPoolSize(PoolSettings settings) => settings.StringBuilderPoolSize;
        public StringBuilder Create() => new();
        public void Get(StringBuilder item) { }
        public bool Return(StringBuilder item)
        {
            if (item.Capacity > MaxCapacity)
            {
                return false;
            }

            item.Clear();
            return true;
        }
    }
}