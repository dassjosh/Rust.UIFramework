using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Layouts;

public abstract class BaseLayout : BasePoolable
{
    public UiReference Reference;
    public int NumElements;

    protected static T CreateBase<T>(in UiReference reference, int numElements) where T : BaseLayout, new()
    {
        T layout = UiFrameworkPool.Get<T>();
        layout.Reference = reference;
        layout.NumElements = numElements;
        return layout;
    }

    public abstract void Add(BaseUiComponent component);
    public abstract void Add(BaseUiComponent component, float elementSpan);
    public abstract void OffsetElements(float numElements);
    public abstract LayoutSlice WithSlice(float elementSpan);
    
    public static implicit operator UiReference(BaseLayout component) => component.Reference;
    public static implicit operator LayoutSlice(BaseLayout component) => component.WithSlice(1f);

    protected override void EnterPool()
    {
        Reference = default;
    }
}