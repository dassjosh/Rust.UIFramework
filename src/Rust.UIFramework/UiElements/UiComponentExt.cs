using System.Runtime.CompilerServices;
using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.Types;

// ReSharper disable MemberCanBePrivate.Global
namespace Oxide.Ext.UiFramework.UiElements;

public static class UiComponentExt
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T SetName<T>(this T component, string name) where T : BaseUiComponent
    {
        component.Name = name;
        return component;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T SetParent<T>(this T component, string parent) where T : BaseUiComponent
    {
        component.Parent = parent;
        return component;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T SetParent<T>(this T component, UiLayer layer) where T : BaseUiComponent => SetParent(component, UiLayerCache.GetLayer(layer));
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T SetReference<T>(this T component, in UiReference reference) where T : BaseUiComponent
    {
        component.Reference = reference;
        return component;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T SetPosition<T>(this T component, in UiPosition position) where T : BaseUiComponent
    {
        component.Position = position;
        return component;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T SetPosition<T>(this T component, in UiPosition position, in UiOffset offset) where T : BaseUiComponent
    {
        component.Position = position;
        component.Offset = offset;
        return component; 
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T SetOffset<T>(this T component, in UiOffset offset) where T : BaseUiComponent
    {
        component.Offset = offset;
        return component;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T SetRotation<T>(this T component, UiRotation rotation) where T : BaseUiComponent
    {
        component.Rotation = rotation;
        return component;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T SetPadding<T>(this T component, in UiPadding padding) where T : BaseUiComponent
    {
        component.Padding = padding;
        return component;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T SetEnabled<T>(this T component, bool enabled) where T : BaseUiComponent
    {
        component.Enabled = enabled;
        return component;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T SetActive<T>(this T component, bool active) where T : BaseUiComponent
    {
        component.Active = active;
        return component;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T SetFadeOut<T>(this T component, float duration) where T : BaseUiComponent
    {
        component.FadeOut = duration;
        return component;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T SetUpdate<T>(this T component, UpdateMode mode) where T : BaseUiComponent
    {
        component.Update = mode;
        return component;
    }
}