using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Interfaces;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiItemIcon : BaseUiComponent, IMaterial<UiItemIcon>, IFadeIn<UiItemIcon>, IUiColor<UiItemIcon>
{
    public readonly ItemIconComponent Icon = new();
    internal override CoreComponent Component => Icon;
    
    public string Material { get => Icon.Material; set => Icon.Material = value; }
    public float FadeIn { get => Icon.FadeIn; set => Icon.FadeIn = value; }
    public UiColor Color { get => Icon.Color; set => Icon.Color = value; }

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
        Icon.FadeIn = duration;
        return this;
    }
        
    public UiItemIcon SetMaterial(string material)
    {
        Icon.Material = material;
        return this;
    }
    
    public UiItemIcon SetColor(UiColor color)
    {
        Icon.Color = color;
        return this;
    }
    
    public UiItemIcon SetItemId(int itemId)
    {
        Icon.ItemId = itemId;
        return this;
    }
    
    public UiItemIcon SetSkinId(ulong skinId)
    {
        Icon.SkinId = skinId;
        return this;
    }
}