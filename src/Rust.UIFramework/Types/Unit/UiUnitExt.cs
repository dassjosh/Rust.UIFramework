namespace Oxide.Ext.UiFramework.Types;

public static class UiUnitExt
{
    extension(UiUnit)
    {
        public static UiUnit Distance(float value) => new(value, UiUnitType.Px);
        public static UiUnit Percentage(float value) => new(value, UiUnitType.Percent);
    }

    extension(UiUnit direction)
    {
        public UiTranslate X() => new(direction, default);
        public UiTranslate Y() => new(default, direction);
        public UiTranslate XY() => new(direction, direction);
    }
    
    extension((UiUnit x, UiUnit y) tuple)
    {
        public UiTranslate XY() => new(tuple.x, tuple.y);
    }
    
    extension(int value)
    {
        public UiUnit Px() => UiUnit.Distance(value);
        public UiUnit Percent() => UiUnit.Percentage(value);
    }
    
    extension(float value)
    {
        public UiUnit Px() => UiUnit.Distance(value);
        public UiUnit Percent() => UiUnit.Percentage(value);
    }
    
    extension(double value)
    {
        public UiUnit Px() => UiUnit.Distance((float)value);
        public UiUnit Percent() => UiUnit.Percentage((float)value);
    }
}