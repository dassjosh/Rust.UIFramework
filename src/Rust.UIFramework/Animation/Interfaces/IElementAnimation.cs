namespace Oxide.Ext.UiFramework.Animation;

public interface IElementAnimation : ISendableAnimation
{
    public string Reference { get; }
    bool Tracked { get; set; }
}