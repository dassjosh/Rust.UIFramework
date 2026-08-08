using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Interfaces;
using Rust.UiFramework.SourceGenerators.Attributes;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.UiElements;

[GenerateUiElement]
[GenerateBuilderMethods]
public partial class UiItemIcon : BaseUiComponent, IMaterial<UiItemIcon>, IFadeIn<UiItemIcon>, IUiColor<UiItemIcon>, IImageType<UiItemIcon>
{
    public partial int ItemId { get; set; }
    public partial ulong SkinId { get; set; }
    public partial Image.Type ImageType { get; set; }
    public partial string Sprite { get; set; }
    public partial string Material { get; set; }
    public partial float FadeIn { get; set; }
    public partial UiColor Color { get; set; }
    public partial bool AllowRaycast { get; set; }
    
    public readonly ItemIconComponent Icon;
    
    public UiItemIcon() : this(new ItemIconComponent()) { }

    private UiItemIcon(ItemIconComponent component) : base(component)
    {
        Icon = component;
    }

    public UiItemIcon Init(int itemId, ulong skinId, UiColor color)
    {
        Color = color;
        ItemId = itemId;
        SkinId = skinId;
        return this;
    }
}