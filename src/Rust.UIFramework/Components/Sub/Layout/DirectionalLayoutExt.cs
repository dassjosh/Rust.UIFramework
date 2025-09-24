namespace Oxide.Ext.UiFramework.Components;

public static class DirectionalLayoutExt
{
    public static T SetSpacing<T>(this T layout, float spacing) where T : BaseDirectionalLayoutComponent
    {
        layout.Spacing = spacing;
        return layout;
    }
    
    public static T SetChildForceExpandWidth<T>(this T layout, bool childForceExpandWidth) where T : BaseDirectionalLayoutComponent
    {
        layout.ChildForceExpandWidth = childForceExpandWidth;
        return layout;
    }
    
    public static T SetChildForceExpandHeight<T>(this T layout, bool childForceExpandHeight) where T : BaseDirectionalLayoutComponent
    {
        layout.ChildForceExpandHeight = childForceExpandHeight;
        return layout;
    }

    public static T SetChildControlWidth<T>(this T layout, bool childControlWidth) where T : BaseDirectionalLayoutComponent
    {
        layout.ChildControlWidth = childControlWidth;
        return layout;
    }
    
    public static T SetChildControlHeight<T>(this T layout, bool childControlHeight) where T : BaseDirectionalLayoutComponent
    {
        layout.ChildControlHeight = childControlHeight;
        return layout;
    }
    
    public static T SetChildScaleWidth<T>(this T layout, bool childScaleWidth) where T : BaseDirectionalLayoutComponent
    {
        layout.ChildScaleWidth = childScaleWidth;
        return layout;
    }
    
    public static T SetChildScaleHeight<T>(this T layout, bool childScaleHeight) where T : BaseDirectionalLayoutComponent
    {
        layout.ChildScaleHeight = childScaleHeight;
        return layout;
    }
}