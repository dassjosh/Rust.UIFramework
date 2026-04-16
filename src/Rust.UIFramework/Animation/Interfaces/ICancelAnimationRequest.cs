namespace Oxide.Ext.UiFramework.Animation;

public interface ICancelAnimationRequest
{
    string Name { get; }

    void OnAnimationCancelled(ulong playerId, IAnimation animation);
}