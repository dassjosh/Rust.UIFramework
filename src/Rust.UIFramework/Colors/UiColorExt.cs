using System;

namespace Oxide.Ext.UiFramework.Colors;

public static class UiColorExt
{
    private static readonly UiColor Black =  "#000000";
    private static readonly UiColor White = "#FFFFFF";
    private static readonly UiColor Silver =  "#C0C0C0";
    private static readonly UiColor Gray = "#808080";
    private static readonly UiColor Red = "#FF0000";
    private static readonly UiColor Maroon = "#800000";
    private static readonly UiColor Orange = "#FFA500";
    private static readonly UiColor Yellow = "#FFEB04";
    private static readonly UiColor Olive = "#808000";
    private static readonly UiColor Lime = "#00FF00";
    private static readonly UiColor Green = "#008000";
    private static readonly UiColor Teal = "#008080";
    private static readonly UiColor Cyan = "#00FFFF";
    private static readonly UiColor Blue = "#0000FF";
    private static readonly UiColor Navy = "#000080";
    private static readonly UiColor Magenta = "#FF00FF";
    private static readonly UiColor Purple = "#800080";
    private static readonly UiColor Clear = "#00000000";
    
    extension(UiColor)
    {
        #region Static Colors
        [Obsolete("Use UiColors.Black Instead")] public static UiColor Black => Black;
        [Obsolete("Use UiColors.White Instead")] public static UiColor White => White;
        [Obsolete("Use UiColors.Silver Instead")] public static UiColor Silver => Silver;
        [Obsolete("Use UiColors.Gray Instead")] public static UiColor Gray => Gray;
        [Obsolete("Use UiColors.Red Instead")] public static UiColor Red => Red;
        [Obsolete("Use UiColors.Maroon Instead")] public static UiColor Maroon => Maroon;
        [Obsolete("Use UiColors.Orange Instead")] public static UiColor Orange => Orange;
        [Obsolete("Use UiColors.Yellow Instead")] public static UiColor Yellow => Yellow;
        [Obsolete("Use UiColors.Olive Instead")] public static UiColor Olive => Olive;
        [Obsolete("Use UiColors.Lime Instead")] public static UiColor Lime => Lime;
        [Obsolete("Use UiColors.Green Instead")] public static UiColor Green => Green;
        [Obsolete("Use UiColors.Teal Instead")] public static UiColor Teal => Teal;
        [Obsolete("Use UiColors.Cyan Instead")] public static UiColor Cyan => Cyan;
        [Obsolete("Use UiColors.Blue Instead")] public static UiColor Blue => Blue;
        [Obsolete("Use UiColors.Navy Instead")] public static UiColor Navy => Navy;
        [Obsolete("Use UiColors.Magenta Instead")] public static UiColor Magenta => Magenta;
        [Obsolete("Use UiColors.Purple Instead")] public static UiColor Purple => Purple;
        [Obsolete("Use UiColors.Clear Instead")] public static UiColor Clear => Clear;
        #endregion
    }
}