using System;
using System.Linq;
using Oxide.Ext.UiFramework.Components;
using Oxide.Ext.UiFramework.Interfaces;
using Oxide.Ext.UiFramework.Libraries;

namespace Oxide.Ext.UiFramework.Extensions;

public static class ComponentTypeExt
{
    public static readonly ComponentType CoreStart;
    public static readonly ComponentType CoreEnd;
    public static readonly ComponentType SubStart;
    public static readonly ComponentType SubEnd;
    public static readonly ComponentType ChildStart;
    public static readonly ComponentType ChildEnd;

    static ComponentTypeExt()
    {
        const int subStartIndex = 100;
        const int childStartIndex = 1000;
        
        ComponentType[] types = Enum.GetValues(typeof(ComponentType)).Cast<ComponentType>().OrderBy(c => c).ToArray();
        CoreStart = types.Where(c => (int)c < subStartIndex).Min(c => c);
        CoreEnd = types.Where(c => (int)c < subStartIndex).Max(c => c);
        SubStart = types.Where(c => (int)c >= subStartIndex).Min(c => c);
        SubEnd = types.Where(c => (int)c >= subStartIndex && (int)c < childStartIndex).Max(c => c);
        ChildStart = types.Where(c => (int)c >= childStartIndex).Min(c => c);
        ChildEnd = types.Where(c => (int)c >= childStartIndex).Max(c => c);
    }
    
    public static ISubComponent GetSubComponent(this ComponentType type, UiPluginPool pluginPool)
    {
        return type switch
        {
            ComponentType.Empty => pluginPool.Get<EmptyComponent>(),
            ComponentType.Text => pluginPool.Get<TextComponent>(),
            ComponentType.Image => pluginPool.Get<ImageComponent>(),
            ComponentType.RawImage => pluginPool.Get<RawImageComponent>(),
            ComponentType.Button => pluginPool.Get<ButtonComponent>(),
            ComponentType.Input => pluginPool.Get<InputComponent>(),
            ComponentType.ScrollView => pluginPool.Get<ScrollViewComponent>(),
            ComponentType.ItemIcon => pluginPool.Get<ItemIconComponent>(),
            ComponentType.PlayerAvatar => pluginPool.Get<PlayerAvatarComponent>(),
            ComponentType.NineSlice => pluginPool.Get<NineSliceComponent>(),
            ComponentType.PlayingCard => pluginPool.Get<PlayingCardComponent>(),
            ComponentType.RectTransform => pluginPool.Get<RectTransformComponent>(),
            ComponentType.NeedsKeyboard => pluginPool.Get<NeedsKeyboardComponent>(),
            ComponentType.NeedsMouse => pluginPool.Get<NeedsMouseComponent>(),
            ComponentType.Outline => pluginPool.Get<OutlineComponent>(),
            ComponentType.Countdown => pluginPool.Get<CountdownComponent>(),
            ComponentType.Draggable => pluginPool.Get<DraggableComponent>(),
            ComponentType.Slot => pluginPool.Get<SlotComponent>(),
            ComponentType.DirectionalLayout => pluginPool.Get<DirectionalLayoutComponent>(),
            ComponentType.GridLayout => pluginPool.Get<GridLayoutComponent>(),
            ComponentType.ContentSizeFitter => pluginPool.Get<ContentSizeFitterComponent>(),
            ComponentType.LayoutElement => pluginPool.Get<LayoutElementComponent>(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Component Type is not a supported SubComponent")
        };
    }
}