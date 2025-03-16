using System;
using System.Collections.Generic;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Layouts;

public class UiDockLayout : BaseLayout
{
    public LayoutPadding LayoutPadding;
    public UiPadding Padding;
    public readonly List<LayoutState> Elements = [];

    public static UiDockLayout Create(in UiReference reference)
    {
        return CreateBase<UiDockLayout>(reference);
    }

    public override void AddElement(BaseUiComponent element)
    {
        AddElement(element, DockPosition.Center, 0f);
    }

    public void AddElement(BaseUiComponent element, DockPosition position, float dockSize)
    {
        Elements.Add(new LayoutState(element, position, dockSize));
    }

    public override void CalculateElementPositions()
    {
        UiOffset padding = Padding.ToOffset();
        UiPosition remainingArea = new(0, 0, 1, 1);

        foreach (LayoutState state in Elements)
        {
            UiPosition dockedPosition = CalculateDockedPosition(state, ref remainingArea);
            state.Element.SetPosition(dockedPosition.Shrink(LayoutPadding.Horizontal, LayoutPadding.Vertical), padding);
        }
    }

    private static UiPosition CalculateDockedPosition(in LayoutState state, ref UiPosition remainingArea)
    {
        switch (state.DockPosition)
        {
            case DockPosition.Top:
                UiPosition topDock = remainingArea.SetY(remainingArea.Min.y, remainingArea.Min.y + state.DockSize);
                remainingArea = remainingArea.WithYMin(topDock.Max.y);
                return topDock;

            case DockPosition.Bottom:
                UiPosition bottomDock = remainingArea.SetY(remainingArea.Max.y - state.DockSize, remainingArea.Max.y);
                remainingArea = remainingArea.WithYMax(bottomDock.Min.y);
                return bottomDock;

            case DockPosition.Left:
                UiPosition leftDock = remainingArea.SetX(remainingArea.Min.x, remainingArea.Min.x + state.DockSize);
                remainingArea = remainingArea.WithXMin(leftDock.Max.x);
                return leftDock;

            case DockPosition.Right:
                UiPosition rightDock = remainingArea.SetX(remainingArea.Max.x - state.DockSize, remainingArea.Max.x);
                remainingArea = remainingArea.WithXMax(rightDock.Min.x);
                return rightDock;

            case DockPosition.Center:
                UiPosition centerDock = remainingArea;
                remainingArea = UiPosition.None; // No further adjustments needed
                return centerDock;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public readonly struct LayoutState(BaseUiComponent element, DockPosition dockPosition, float dockSize)
    {
        public readonly BaseUiComponent Element = element;
        public readonly DockPosition DockPosition = dockPosition;
        public readonly float DockSize = dockSize;
    }
}