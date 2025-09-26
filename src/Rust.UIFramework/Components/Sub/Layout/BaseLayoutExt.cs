using Oxide.Ext.UiFramework.Types;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components;

public static class BaseLayoutExt
{
    public static T SetChildAlignment<T>(this T layout, TextAnchor childAlignment) where T : BaseLayoutComponent
    {
        layout.ChildAlignment = childAlignment;
        return layout;
    }
    
    public static T SetPadding<T>(this T layout, in UiPadding padding) where T : BaseLayoutComponent 
    {
        layout.Padding = padding;
        return layout;
    }
}