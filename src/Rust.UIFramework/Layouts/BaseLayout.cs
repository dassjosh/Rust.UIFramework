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

    public abstract void AddElement(BaseUiComponent element);
    
    public abstract void CalculateElementPositions();
    
    protected static float GetAlignmentOffset(LayoutAlignment alignment, float numElements, float maxElements)
    {
        if(numElements < maxElements)
        {
            return alignment switch
            {
                LayoutAlignment.Middle => (maxElements - numElements) / 2f,
                LayoutAlignment.End => maxElements - numElements,
                _ => 0f
            };
        }

        return 0f;
    }

    protected override void EnterPool()
    {
        Reference = default;
    }
}