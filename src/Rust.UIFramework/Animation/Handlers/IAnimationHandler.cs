namespace Oxide.Ext.UiFramework.Animation;

internal interface IAnimationHandler
{
    void OnInit(AnimationHandler handler);
    void OnAnimationQueued();
    void OnServerShutdown();
}