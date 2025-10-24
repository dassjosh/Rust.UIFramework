namespace Oxide.Ext.UiFramework.Interfaces;

public interface IItemIconComponent : IImageComponent
{
    public int ItemId { get; set; }
    public ulong SkinId { get; set; }
}