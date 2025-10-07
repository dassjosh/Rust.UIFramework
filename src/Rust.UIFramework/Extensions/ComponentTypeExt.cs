using System;
using System.Linq;
using Oxide.Ext.UiFramework.Components;

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
}