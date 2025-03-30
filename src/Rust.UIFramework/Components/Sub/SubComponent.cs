namespace Oxide.Ext.UiFramework.Components;

public abstract class SubComponent : BaseTypedComponent, ISubComponent
{
    public abstract bool AllowMultiple { get; }
}