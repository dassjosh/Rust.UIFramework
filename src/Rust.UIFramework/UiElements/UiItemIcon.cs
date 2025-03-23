using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;

namespace Oxide.Ext.UiFramework.UiElements;

public class UiItemIcon : BaseUiComponent, IMaterial<UiItemIcon>, IFadeIn<UiItemIcon>, IUiColor<UiItemIcon>
{
    public readonly ItemIconComponent Icon = new();
    internal override CoreComponent Component => Icon;

    public static UiItemIcon Create(in UiPosition pos, in UiOffset offset, UiColor color, int itemId, ulong skinId = 0)
    {
        UiItemIcon icon = CreateBase<UiItemIcon>(pos, offset);
        icon.Icon.Color = color;
        icon.Icon.ItemId = itemId;
        icon.Icon.SkinId = skinId;
        return icon;
    }
    
    public UiColor GetColor() => Icon.Color;
    
    void IMaterial.SetMaterial(string material) => SetMaterial(material);
    void IFadeIn.SetFadeIn(float duration) => SetFadeIn(duration);
    void IUiColor.SetColor(UiColor color) => SetColor(color);
        
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