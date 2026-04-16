using System;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Controls;

public abstract class BaseUiControl : BasePoolable
{
    [Obsolete("Use CreateControl<T>(BaseBuilder builder) instead.")]
    protected static T CreateControl<T>() where T : BaseUiControl, new() => UiFrameworkPool.Get<T>();
    
    protected static T CreateControl<T>(BaseBuilder builder) where T : BaseUiControl, new() => builder.PluginPool.Get<T>();
}