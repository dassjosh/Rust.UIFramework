using System;
using Oxide.Ext.UiFramework.Colors;

namespace Oxide.Ext.UiFramework.Extensions;

public static class UiColorExt
{
    [Obsolete("Use UiColor.Lerp instead")]
    public static UiColor Lerp(this UiColor start, UiColor end, float value) => UiColor.Lerp(start, end, value);
}