namespace Oxide.Ext.UiFramework.Types;

public static class UiTranslateExt
{
    extension(UiTranslate)
    {
        public static UiTranslate X(UiUnit x) => new(x, default);
        public static UiTranslate Y(UiUnit y) => new(default, y);
        public static UiTranslate XY(UiUnit x, UiUnit y) => new(x, y);
        public static UiTranslate Top(UiUnit direction) => new(default, direction with { Value = -direction.Value });
        public static UiTranslate Left(UiUnit direction) => new(direction, default);
        public static UiTranslate Right(UiUnit direction) => new(direction with { Value = -direction.Value }, default);
        public static UiTranslate Bottom(UiUnit direction) => new(default, direction);
    }

}