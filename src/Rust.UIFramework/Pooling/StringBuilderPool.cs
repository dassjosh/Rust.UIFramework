using System.Text;

namespace Oxide.Ext.UiFramework.Pooling;

/// <summary>
/// Pool for StringBuilders
/// </summary>
internal class StringBuilderPool : BasePool<StringBuilder, StringBuilderPool>
{
    protected override PoolSize GetPoolSize(PoolSettings settings) => settings.StringBuilderPoolSize;
    protected override StringBuilder CreateNew() => new();

    ///<inheritdoc/>
    protected override bool OnFreeItem(StringBuilder item)
    {
        item.Length = 0;
        return true;
    }
}