using UnityEngine;

namespace Oxide.Ext.UiFramework.Extensions;

public static class Color32Ext
{
    public static int ToArgb(this Color32 color)
    {
        return color.a << 24 | color.r << 16 | color.g << 8 | color.b;
    }
}