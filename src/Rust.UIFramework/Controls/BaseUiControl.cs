using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Controls;

public abstract class BaseUiControl : BasePoolable
{
    protected static T CreateControl<T>() where T : BaseUiControl, new() => UiFrameworkPool.Get<T>();
}