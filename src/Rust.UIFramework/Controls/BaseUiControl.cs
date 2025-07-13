using System;
using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Controls;

public abstract class BaseUiControl : BasePoolable
{
    [Obsolete]
    protected static T CreateControl<T>() where T : BaseUiControl, new() => UiFrameworkPool.Get<T>();
    
    protected static T CreateControl<T>(UiPluginPool pool) where T : BaseUiControl, new() => pool.Get<T>();
}