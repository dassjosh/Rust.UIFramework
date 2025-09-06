using System.IO;

namespace Oxide.Ext.UiFramework.Pooling;

/// <summary>
/// Pool for MemorySteam
/// </summary>
internal class MemoryStreamPool : BasePool<MemoryStream, MemoryStreamPool>
{
    protected override PoolSize GetPoolSize(PoolSettings settings) => settings.MemoryStreamPoolSize;
    protected override MemoryStream CreateNew() => new();

    ///<inheritdoc/>
    protected override bool OnFreeItem(ref MemoryStream item)
    {
        item.SetLength(0);
        return true;
    }
}