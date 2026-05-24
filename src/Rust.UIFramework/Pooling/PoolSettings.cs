namespace Oxide.Ext.UiFramework.Pooling;

/// <summary>
/// Settings for the pools
/// </summary>
public class PoolSettings
{
    private const int CollectionMaxSize = 1 << 9;
    private const int ObjectMaxSize = 1 << 16;
    private const int InternalMultiplier = 1 << 2;
        
    /// <summary>
    /// Size of the <see cref="HashPool{TKey,TValue}"/>
    /// </summary>
    public PoolSize ArrayPoolSize { get; set; } = new(CollectionMaxSize);
    
    /// <summary>
    /// Size of the <see cref="HashPool{TKey,TValue}"/>
    /// </summary>
    public PoolSize HashPoolSize { get; set; } = new(CollectionMaxSize);
        
    /// <summary>
    /// Size of the <see cref="HashSetPool{T}"/>
    /// </summary>
    public PoolSize HashSetPoolSize { get; set; } = new(CollectionMaxSize);
        
    /// <summary>
    /// Size of the <see cref="ListPool{T}"/>
    /// </summary>
    public PoolSize ListPoolSize { get; set; } = new(CollectionMaxSize);
        
    /// <summary>
    /// Size of the <see cref="ListPool{T}"/>
    /// </summary>
    public PoolSize DictionaryPoolSize { get; set; } = new(CollectionMaxSize);
        
    /// <summary>
    /// Size of the <see cref="MemoryStreamPool"/>
    /// </summary>
    public PoolSize MemoryStreamPoolSize { get; set; } = new(CollectionMaxSize);
        
    /// <summary>
    /// Size of the <see cref="ObjectPool{T}"/>
    /// </summary>
    public PoolSize ObjectPoolSize { get; set; } = new(ObjectMaxSize);
        
    /// <summary>
    /// Size of the <see cref="ObjectPool{T}"/>
    /// </summary>
    public PoolSize StringBuilderPoolSize { get; set; } = new(CollectionMaxSize);
        
    internal static PoolSettings CreateInternal() => new()
    {
        HashPoolSize = new PoolSize(CollectionMaxSize * InternalMultiplier),
        HashSetPoolSize = new PoolSize(CollectionMaxSize * InternalMultiplier),
        ListPoolSize = new PoolSize(CollectionMaxSize * InternalMultiplier),
        DictionaryPoolSize = new PoolSize(CollectionMaxSize * InternalMultiplier),
        MemoryStreamPoolSize = new PoolSize(CollectionMaxSize * InternalMultiplier),
        ObjectPoolSize = new PoolSize(ObjectMaxSize * InternalMultiplier),
        StringBuilderPoolSize = new PoolSize(CollectionMaxSize * InternalMultiplier),
    };
}