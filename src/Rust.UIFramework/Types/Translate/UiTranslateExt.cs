namespace Oxide.Ext.UiFramework.Types;

public static class UiTranslateExt
{
    extension(UiTranslate)
    {
        public static UiTranslate X(UiTranslateDirection x) => new(x, default);
        public static UiTranslate Y(UiTranslateDirection y) => new(default, y);
        public static UiTranslate XY(UiTranslateDirection x, UiTranslateDirection y) => new(x, y);
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