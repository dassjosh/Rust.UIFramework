using System;
using System.Collections.Concurrent;
using Oxide.Ext.UiFramework.Enums;
using ProtoBuf;

namespace Oxide.Ext.UiFramework.Data;

[ProtoContract]
internal class AvatarData : BaseDataFile<AvatarData>
{
    [ProtoMember(1)]
    public readonly ConcurrentDictionary<ulong, string> Avatars = new();
    
    public void AddAvatar(ulong steamId, string avatar)
    {
        if(!Avatars.TryGetValue(steamId, out string existingAvatar) || existingAvatar != avatar)
        {
            Avatars[steamId] = avatar;
            OnDataChanged();
        }
    }

    public string GetAvatar(ulong steamId, AvatarType type)
    {
        if(!Avatars.TryGetValue(steamId, out string avatar))
        {
            return null;
        }

        return type switch
        {
            AvatarType.Small => $"https://avatars.steamstatic.com/{avatar}.jpg",
            AvatarType.Medium => $"https://avatars.steamstatic.com/{avatar}_medium.jpg",
            AvatarType.Large => $"https://avatars.steamstatic.com/{avatar}_full.jpg",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}