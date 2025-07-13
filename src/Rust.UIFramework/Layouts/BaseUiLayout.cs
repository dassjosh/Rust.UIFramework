using Oxide.Ext.UiFramework.Libraries;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Layouts;

public abstract class BaseUiLayout : BasePoolable
{
    public UiReference Reference;
    public UiScrollView ScrollView;

    public static T CreateBase<T>(UiPluginPool pool, in UiReference reference) where T : BaseUiLayout, new()
    {
        T layout = pool.Get<T>();
        layout.Reference = reference;
        return layout;
    }

    public abstract void AddElement(BaseUiComponent element);
    
    public void ForScrollView(UiScrollView scrollView) => ScrollView = scrollView;
    
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
    
    protected void ScaleScrollView(LayoutDirection direction, float scale)
    {
        if (ScrollView != null && scale != 1f)
        {
            UiPosition position = ScrollView.ScrollView.ContentTransform.Position;
            switch (direction)
            {
                case LayoutDirection.Horizontal:
                    ScrollView.UpdateContentTransform(position.WithXMax(1 / scale));
                    break;
                case LayoutDirection.Vertical:
                    ScrollView.UpdateContentTransform(position.WithYMin(1 - 1 / scale));
                    break;
            }
        }
    }
    
    protected float GetScrollViewScale(float totalSpan, float numElements, float scale = 1f) => ScrollView == null ? scale : numElements / totalSpan;

    protected override void EnterPool()
    {
        Reference = default;
        ScrollView = null;
    }
}