namespace Oxide.Ext.UiFramework.Pooling;

/// <summary>
/// Represents size constraints for a pool
/// </summary>
public readonly struct PoolSize
{
    /// <summary>
    /// Max size of the pool
    /// </summary>
    public readonly int MaxSize;
        
    /// <summary>
    /// If the pool size is valid
    /// </summary>
    public bool IsValid => MaxSize > 0;

    /// <summary>
    /// Constructor settings the startingSize and maxSize
    /// </summary>
    /// <param name="maxSize">Max size of the pool</param>
    public PoolSize(int maxSize)
    {
        MaxSize = maxSize;
    }
}