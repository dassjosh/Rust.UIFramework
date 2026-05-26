namespace Oxide.Ext.UiFramework.Pooling;

/// <summary>
/// Settings for the pools
/// </summary>
public class PoolSettings
{
    private const int CollectionSize = 1 << 9;
    private const int ObjectSize = 1 << 16;
    private const int InternalMultiplier = 1 << 2;

    public static readonly PoolSettings Default = new();
        
    /// <summary>
    /// Size of the <see cref="HashPool{TKey,TValue}"/>
    /// </summary>
    public PoolSize ArrayPoolSize { get; set; } = new(CollectionSize);
    
    /// <summary>
    /// Size of the <see cref="HashPool{TKey,TValue}"/>
    /// </summary>
    public PoolSize HashPoolSize { get; set; } = new(CollectionSize);
        
    /// <summary>
    /// Size of the <see cref="HashSetPool{T}"/>
    /// </summary>
    public PoolSize HashSetPoolSize { get; set; } = new(CollectionSize);
        
    /// <summary>
    /// Size of the <see cref="ListPool{T}"/>
    /// </summary>
    public PoolSize ListPoolSize { get; set; } = new(CollectionSize);
        
    /// <summary>
    /// Size of the <see cref="ListPool{T}"/>
    /// </summary>
    public PoolSize DictionaryPoolSize { get; set; } = new(CollectionSize);
        
    /// <summary>
    /// Size of the <see cref="MemoryStreamPool"/>
    /// </summary>
    public PoolSize MemoryStreamPoolSize { get; set; } = new(CollectionSize);
        
    /// <summary>
    /// Size of the <see cref="ObjectPool{T}"/>
    /// </summary>
    public PoolSize ObjectPoolSize { get; set; } = new(ObjectSize);
        
    /// <summary>
    /// Size of the <see cref="ObjectPool{T}"/>
    /// </summary>
    public PoolSize StringBuilderPoolSize { get; set; } = new(CollectionSize);
        
    internal static PoolSettings CreateInternal() => new()
    {
        HashPoolSize = new PoolSize(CollectionSize * InternalMultiplier),
        HashSetPoolSize = new PoolSize(CollectionSize * InternalMultiplier),
        ListPoolSize = new PoolSize(CollectionSize * InternalMultiplier),
        DictionaryPoolSize = new PoolSize(CollectionSize * InternalMultiplier),
        MemoryStreamPoolSize = new PoolSize(CollectionSize * InternalMultiplier),
        ObjectPoolSize = new PoolSize(ObjectSize * InternalMultiplier),
        StringBuilderPoolSize = new PoolSize(CollectionSize * InternalMultiplier),
    };
}