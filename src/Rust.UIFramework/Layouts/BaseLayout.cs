using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Layouts;

public abstract class BaseLayout : BasePoolable
{
    public UiReference Reference;

    protected static T CreateBase<T>(in UiReference reference) where T : BaseLayout, new()
    {
        T layout = UiFrameworkPool.Get<T>();
        layout.Reference = reference;
        return layout;
    }
    
    public static implicit operator UiReference(BaseLayout component) => component.Reference;

    protected override void EnterPool()
    {
        Reference = default;
    }
}