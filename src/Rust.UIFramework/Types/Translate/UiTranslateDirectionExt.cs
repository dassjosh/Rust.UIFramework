namespace Oxide.Ext.UiFramework.Types;

public static class UiTranslateDirectionExt
{
    extension(UiTranslateDirection)
    {
        public static UiTranslateDirection Distance(float value) => new(value, UiTranslateType.Px);
        public static UiTranslateDirection Percentage(float value) => new(value, UiTranslateType.Percent);
    }

    extension(UiTranslateDirection direction)
    {
        public UiTranslate X() => new(direction, default);
        public UiTranslate Y() => new(default, direction);
        public UiTranslate XY() => new(direction, direction);
    }
    
    extension((UiTranslateDirection x, UiTranslateDirection y) tuple)
    {
        public UiTranslate XY() => new(tuple.x, tuple.y);
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