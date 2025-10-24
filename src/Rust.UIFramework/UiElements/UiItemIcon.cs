using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Interfaces;
using Rust.UiFramework.SourceGenerators.Attributes;

namespace Oxide.Ext.UiFramework.UiElements;

[GenerateUiElement(typeof(IUiItemIcon))]
public partial class UiItemIcon : BaseUiComponent, IUiItemIcon
{
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