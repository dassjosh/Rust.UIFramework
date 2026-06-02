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
    /// Size of the <see cref="ArrayPool{T}"/>
    /// </summary>
    public int ArrayPoolSize { get; set; } = CollectionSize;
    
    /// <summary>
    /// Size of the <see cref="HashPool{TKey,TValue}"/>
    /// </summary>
    public int HashPoolSize { get; set; } = CollectionSize;
        
    /// <summary>
    /// Size of the <see cref="HashSetPool{T}"/>
    /// </summary>
    public int HashSetPoolSize { get; set; } = CollectionSize;
        
    /// <summary>
    /// Size of the <see cref="ListPool{T}"/>
    /// </summary>
    public int ListPoolSize { get; set; } = CollectionSize;
        
    /// <summary>
    /// Size of the <see cref="ListPool{T}"/>
    /// </summary>
    public int DictionaryPoolSize { get; set; } = CollectionSize;
        
    /// <summary>
    /// Size of the <see cref="MemoryStreamPool"/>
    /// </summary>
    public int MemoryStreamPoolSize { get; set; } = CollectionSize;
        
    /// <summary>
    /// Size of the <see cref="ObjectPool{T}"/>
    /// </summary>
    public int ObjectPoolSize { get; set; } = ObjectSize;
        
    /// <summary>
    /// Size of the <see cref="ObjectPool{T}"/>
    /// </summary>
    public int StringBuilderPoolSize { get; set; } = CollectionSize;
        
    internal static PoolSettings CreateInternal() => new()
    {
        HashPoolSize = CollectionSize * InternalMultiplier,
        HashSetPoolSize = CollectionSize * InternalMultiplier,
        ListPoolSize = CollectionSize * InternalMultiplier,
        DictionaryPoolSize = CollectionSize * InternalMultiplier,
        MemoryStreamPoolSize = CollectionSize * InternalMultiplier,
        ObjectPoolSize = ObjectSize * InternalMultiplier,
        StringBuilderPoolSize = CollectionSize * InternalMultiplier,
    };
}