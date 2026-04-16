using Network;
using Oxide.Ext.UiFramework.Json;

namespace Oxide.Ext.UiFramework.Animation;

public interface ISendableAnimation : IAnimation
{
    SendInfo Send { get; set; }
    bool IsSending { get; }
    void Serialize(JsonFrameworkWriter writer);
    void AddPlayer(BasePlayer player) => AddPlayer(player?.net?.connection);
    void AddPlayer(Connection connection);
    void RemovePlayer(ulong playerId);
}