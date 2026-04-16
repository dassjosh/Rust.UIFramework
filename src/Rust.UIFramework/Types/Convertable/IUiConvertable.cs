namespace Oxide.Ext.UiFramework.Types;

public interface IUiConvertable<out TTo, in TFrom>
{
    TTo Convert(TFrom from);
}