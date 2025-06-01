using System.Text;

namespace Oxide.Ext.UiFramework.Pooling;

/// <summary>
/// Pool for StringBuilders
/// </summary>
public class StringBuilderPool : BasePool<StringBuilder, StringBuilderPool>
{
    protected override PoolSize GetPoolSize(PoolSettings settings) => settings.StringBuilderPoolSize;
    protected override StringBuilder CreateNew() => new();

    ///<inheritdoc/>
    protected override bool OnFreeItem(ref StringBuilder item)
    {
        item.Length = 0;
        return true;
    }
}