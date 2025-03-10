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
    
    public abstract void OffsetElements(float numElements);
    public abstract LayoutPosition GetPosition(float elementSpan);
    
    public static implicit operator LayoutPosition(BaseLayout component) => component.GetPosition(1f);

    protected override void EnterPool()
    {
        Reference = default;
    }
}