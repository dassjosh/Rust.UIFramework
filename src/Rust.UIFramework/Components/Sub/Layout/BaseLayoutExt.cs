using Oxide.Ext.UiFramework.Types;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Components;

public static class BaseLayoutExt
{
    extension<T>(T layout) where T : BaseLayoutComponent
    {
        public T SetChildAlignment(TextAnchor childAlignment)
        {
            layout.ChildAlignment = childAlignment;
            return layout;
        }

        public T SetPadding(in UiPadding padding)
        {
            layout.Padding = padding;
            return layout;
        }
    }
}