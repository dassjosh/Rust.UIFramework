namespace Oxide.Ext.UiFramework.Libraries;

public interface ICommunityEntity
{
    public ulong Id { get; }
}

internal readonly struct CommunityEntityImpl(CommunityEntity entity) : ICommunityEntity
{
    public ulong Id { get; } = entity.net.ID.Value;
}