namespace Oxide.Ext.UiFramework.Animation;

public readonly record struct CancelAnimationRequest(string Name) : ICancelAnimationRequest
{
    public void OnAnimationCancelled(ulong playerId, IAnimation animation) { }
}