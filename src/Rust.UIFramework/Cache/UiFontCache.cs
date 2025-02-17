using System.Collections.Generic;
using Oxide.Ext.UiFramework.Enums;

namespace Oxide.Ext.UiFramework.Cache;

public static class UiFontCache
{
    public const string NotosansarabicBold = "_nonenglish/arabic/notosansarabic-bold.ttf";
    public const string NotosansarabicRegular = "_nonenglish/arabic/notosansarabic-regular.ttf";
    public const string NotosanshebrewBold = "_nonenglish/hebrew/notosanshebrew-bold.ttf";
    public const string NotoemojiRegular = "_nonenglish/notoemoji-regular.ttf";
    public const string NotosanscjkscBold = "_nonenglish/notosanscjksc-bold.otf";
    public const string Droidsansmono = "droidsansmono.ttf";
    public const string Lcd = "lcd.ttf";
    public const string Permanentmarker = "permanentmarker.ttf";
    public const string Poxel = "poxel.otf";
    public const string Pressstart2pRegular = "pressstart2p-regular.ttf";
    public const string RobotocondensedBold = "robotocondensed-bold.ttf";
    public const string RobotocondensedRegular = "robotocondensed-regular.ttf";
    public const string RobotomonoRegular = "robotomono-regular.ttf";

    private static readonly IReadOnlyDictionary<UiFont, string> Fonts = new Dictionary<UiFont, string>
    {
        [UiFont.DroidSansMono] = Droidsansmono,
        [UiFont.PermanentMarker] = Permanentmarker,
        [UiFont.RobotoCondensedBold] = RobotocondensedBold,
        [UiFont.RobotoCondensedRegular] = RobotocondensedRegular,
        [UiFont.PressStart2PRegular] = Pressstart2pRegular
    };

    public static string GetUiFont(UiFont font) => Fonts[font];
}