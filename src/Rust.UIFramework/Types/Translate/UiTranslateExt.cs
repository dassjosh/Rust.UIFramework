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

    extension(UiTranslateDirection)
    {
        public static UiTranslateDirection Distance(float value) => new(value, UiTranslateType.Distance);
        public static UiTranslateDirection Percentage(float value) => new(value, UiTranslateType.Percentage);
    }
    
    extension(int value)
    {
        public UiTranslateDirection Px() => UiTranslateDirection.Distance(value);
        public UiTranslateDirection Percent() => UiTranslateDirection.Percentage(value);
    }
    
    extension(float value)
    {
        public UiTranslateDirection Px() => UiTranslateDirection.Distance(value);
        public UiTranslateDirection Percent() => UiTranslateDirection.Percentage(value);
    }
    
    extension(double value)
    {
        public UiTranslateDirection Px() => UiTranslateDirection.Distance((float)value);
        public UiTranslateDirection Percent() => UiTranslateDirection.Percentage((float)value);
    }
}