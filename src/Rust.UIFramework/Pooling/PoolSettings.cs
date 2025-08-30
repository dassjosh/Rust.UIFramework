namespace Oxide.Ext.UiFramework.Pooling
{
    /// <summary>
    /// Settings for the pools
    /// </summary>
    public class PoolSettings
    {
        /// <summary>
        /// Size of the <see cref="HashPool{TKey,TValue}"/>
        /// </summary>
        public PoolSize HashPoolSize { get; set; } = new(32, 256);
        
        /// <summary>
        /// Size of the <see cref="HashSetPool{T}"/>
        /// </summary>
        public PoolSize HashSetPoolSize { get; set; } = new(32, 256);
        
        /// <summary>
        /// Size of the <see cref="ListPool{T}"/>
        /// </summary>
        public PoolSize ListPoolSize { get; set; } = new(32, 256);
        
        /// <summary>
        /// Size of the <see cref="ListPool{T}"/>
        /// </summary>
        public PoolSize DictionaryPoolSize { get; set; } = new(32, 256);
        
        /// <summary>
        /// Size of the <see cref="MemoryStreamPool"/>
        /// </summary>
        public PoolSize MemoryStreamPoolSize { get; set; } = new(32, 256);
        
        /// <summary>
        /// Size of the <see cref="ObjectPool{T}"/>
        /// </summary>
        public PoolSize ObjectPoolSize { get; set; } = new(32, 4096);
        
        /// <summary>
        /// Size of the <see cref="ObjectPool{T}"/>
        /// </summary>
        public PoolSize StringBuilderPoolSize { get; set; } = new(32, 256);
        
        internal static PoolSettings CreateInternal() => new()
        {
            HashPoolSize = new PoolSize(128, 4096),
            HashSetPoolSize = new PoolSize(128, 4096),
            ListPoolSize = new PoolSize(128, 4096),
            DictionaryPoolSize = new PoolSize(128, 4096),
            MemoryStreamPoolSize = new PoolSize(128, 4096),
            ObjectPoolSize = new PoolSize(128, 65536),
            StringBuilderPoolSize = new PoolSize(128, 1024),
        };
    }
}