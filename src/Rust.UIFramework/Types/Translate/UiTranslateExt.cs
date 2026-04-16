namespace Oxide.Ext.UiFramework.Types;

public static class UiTranslateExt
{
    extension(UiTranslate)
    {
        public static UiTranslate X(UiTranslateDirection x) => new(x, default);
        public static UiTranslate Y(UiTranslateDirection y) => new(default, y);
        public static UiTranslate XY(UiTranslateDirection x, UiTranslateDirection y) => new(x, y);
        public static UiTranslate Top(UiTranslateDirection direction) => new(default, direction with { Value = -direction.Value });
        public static UiTranslate Left(UiTranslateDirection direction) => new(direction, default);
        public static UiTranslate Right(UiTranslateDirection direction) => new(direction with { Value = -direction.Value }, default);
        public static UiTranslate Bottom(UiTranslateDirection direction) => new(default, direction);
    }

}