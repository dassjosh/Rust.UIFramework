using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiItemIcon : BaseUiComponent, IMaterial<UiItemIcon>, IFadeIn<UiItemIcon>, IUiColor<UiItemIcon>
{
    public readonly ItemIconComponent Icon;

    public UiItemIcon() : this(new ItemIconComponent()) { }

    private UiItemIcon(ItemIconComponent component) : base(component)
    {
        Icon = component;
    }
    
    public string Material { get => Icon.Material; set => Icon.Material = value; }
    public float FadeIn { get => Icon.FadeIn; set => Icon.FadeIn = value; }
    public UiColor Color { get => Icon.Color; set => Icon.Color = value; }
    public int ItemId { get => Icon.ItemId; set => Icon.ItemId = value; }
    public ulong SkinId { get => Icon.SkinId; set => Icon.SkinId = value; }

    public static UiItemIcon Create(int itemId, ulong skinId, UiColor color)
    {
        UiItemIcon icon = CreateBase<UiItemIcon>();
        icon.Icon.Color = color;
        icon.Icon.ItemId = itemId;
        icon.Icon.SkinId = skinId;
        return icon;
    }
        
    public UiItemIcon SetFadeIn(float duration)
    {
        FadeIn = duration;
        return this;
    }
        
    public UiItemIcon SetMaterial(string material)
    {
        Material = material;
        return this;
    }
    
    public UiItemIcon SetColor(UiColor color)
    {
        Color = color;
        return this;
    }
    
    public UiItemIcon SetItemId(int itemId)
    {
        ItemId = itemId;
        return this;
    }
    
    public UiItemIcon SetSkinId(ulong skinId)
    {
        SkinId = skinId;
        return this;
    }
}