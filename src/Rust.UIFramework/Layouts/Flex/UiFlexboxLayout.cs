using System;
using System.Collections.Generic;
using System.Linq;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Layouts;

public class UiFlexBoxLayout : BaseLayout
{
    public FlexDirection Direction;
    public FlexWrap Wrap;
    public FlexCrossAlignment CrossAlignment;
    public FlexJustifyContent JustifyContent;
    public UiPadding Padding;
    public float Gap;
    public readonly List<LayoutState> Elements = [];

    public static UiFlexBoxLayout Create(
        in UiReference reference,
        FlexDirection direction,
        FlexWrap wrap,
        FlexCrossAlignment crossAlignment,
        FlexJustifyContent justifyContent,
        in UiPadding padding,
        float gap = 0f)
    {
        UiFlexBoxLayout layout = CreateBase<UiFlexBoxLayout>(reference);
        layout.Direction = direction;
        layout.Wrap = wrap;
        layout.CrossAlignment = crossAlignment;
        layout.JustifyContent = justifyContent;
        layout.Padding = padding;
        layout.Gap = gap;
        return layout;
    }

    public override void AddElement(BaseUiComponent element) => AddElement(element, 1f);

    public void AddElement(BaseUiComponent element, float baseSpan, float flexBasis = 0f, float flexGrow = 1f, float flexShrink = 1f)
    {
        Elements.Add(new LayoutState(element, baseSpan, flexBasis, flexGrow, flexShrink));
    }

    public override void CalculateElementPositions()
    {
        UiOffset padding = Padding.ToOffset();
        List<List<LayoutState>> lines = WrapElements();

        try
        {
            float crossAxisSize = lines.Count;

            for (int lineIndex = 0; lineIndex < lines.Count; lineIndex++)
            {
                List<LayoutState> line = lines[lineIndex];
                HandleLinePositioning(line, lineIndex, crossAxisSize, padding);
            }
        }
        finally
        {
            FreePooledResources(lines);
        }
    }

    private void HandleLinePositioning(List<LayoutState> line, int lineIndex, float crossAxisSize, in UiOffset padding)
    {
        SumState(line, out float totalFlexBasis, out float totalFlexGrow, out float totalFlexShrink);
        float totalGap = (line.Count - 1) * Gap;
        float totalMainAxisSpan = totalFlexBasis + totalGap;
        float availableSpace = 1f - totalMainAxisSpan;

        float currentMainPos = CalculateJustifyContentOffset(availableSpace, line.Count);

        for (int index = 0; index < line.Count; index++)
        {
            LayoutState state = line[index];
            float elementSpan = CalculateElementSpan(state, availableSpace, totalFlexGrow, totalFlexShrink);
            SetElementPosition(state, currentMainPos, elementSpan, lineIndex, crossAxisSize, padding);
            currentMainPos += elementSpan + Gap;
        }
    }
    
    private void SumState(List<LayoutState> lines, out float totalFlexBasis, out float totalFlexGrow, out float totalFlexShrink)
    {
        totalFlexBasis = 0f;
        totalFlexGrow = 0f;
        totalFlexShrink = 0f;

        for (int index = 0; index < lines.Count; index++)
        {
            LayoutState state = lines[index];
            totalFlexBasis += state.FlexBasis > 0 ? state.FlexBasis : state.BaseSpan;
            totalFlexGrow += state.FlexGrow;
            totalFlexShrink += state.FlexShrink;
        }
    }

    private float CalculateElementSpan(in LayoutState state, float availableSpace, float totalFlexGrow, float totalFlexShrink)
    {
        float elementSpan = state.FlexBasis > 0 ? state.FlexBasis : state.BaseSpan;

        if (availableSpace > 0 && totalFlexGrow > 0)
        {
            // Distribute extra space based on FlexGrow
            elementSpan += (state.FlexGrow / totalFlexGrow) * availableSpace;
        }
        else if (availableSpace < 0 && totalFlexShrink > 0)
        {
            // Reduce size based on FlexShrink
            elementSpan += (state.FlexShrink / totalFlexShrink) * availableSpace;
        }

        return elementSpan;
    }

    private void SetElementPosition(in LayoutState state, float currentMainPos, float elementSpan, int lineIndex, float crossAxisSize, in UiOffset padding)
    {
        (float crossStart, float crossEnd) = CalculateCrossAlignment(lineIndex, crossAxisSize);

        UiPosition position = Direction switch
        {
            FlexDirection.Row or FlexDirection.RowReverse => new UiPosition(
                currentMainPos, crossStart,
                currentMainPos + elementSpan, crossEnd),

            FlexDirection.Column or FlexDirection.ColumnReverse => new UiPosition(
                crossStart, currentMainPos,
                crossEnd, currentMainPos + elementSpan),

            _ => throw new ArgumentOutOfRangeException(nameof(Direction)),
        };

        state.Element.SetPosition(position, padding);
    }

    private void FreePooledResources(List<List<LayoutState>> lines)
    {
        foreach (List<LayoutState> line in lines)
        {
            UiFrameworkPool.FreeList(line);
        }
        UiFrameworkPool.FreeList(lines);
    }

    private (float crossStart, float crossEnd) CalculateCrossAlignment(int lineIndex, float crossAxisSize)
    {
        return CrossAlignment switch
        {
            FlexCrossAlignment.Start => (lineIndex / crossAxisSize, (lineIndex + 1) / crossAxisSize),
            FlexCrossAlignment.Center => (
                lineIndex / crossAxisSize + 1f / (2 * crossAxisSize),
                (lineIndex + 1) / crossAxisSize - 1f / (2 * crossAxisSize)),
            FlexCrossAlignment.End => (
                (lineIndex + 1) / crossAxisSize - 1f / crossAxisSize,
                (lineIndex + 1) / crossAxisSize),
            FlexCrossAlignment.Stretch => (lineIndex / crossAxisSize, (lineIndex + 1) / crossAxisSize),
            _ => throw new ArgumentOutOfRangeException(nameof(CrossAlignment)),
        };
    }

    private List<List<LayoutState>> WrapElements()
    {
        List<List<LayoutState>> lines = UiFrameworkPool.GetList<List<LayoutState>>();
        List<LayoutState> currentLine = UiFrameworkPool.GetList<LayoutState>();
        float currentLineSpan = 0f;

        for (int index = 0; index < Elements.Count; index++)
        {
            LayoutState state = Elements[index];
            float elementSpan = state.FlexBasis > 0 ? state.FlexBasis : state.BaseSpan;

            if (Wrap == FlexWrap.Wrap && currentLineSpan + elementSpan + (currentLine.Count > 0 ? Gap : 0f) > 1f)
            {
                lines.Add(currentLine);
                currentLine = UiFrameworkPool.GetList<LayoutState>();
                currentLineSpan = 0f;
            }

            currentLine.Add(state);
            currentLineSpan += elementSpan + (currentLine.Count > 1 ? Gap : 0f);
        }

        if (currentLine.Count > 0)
        {
            lines.Add(currentLine);
        }
        else
        {
            UiFrameworkPool.FreeList(currentLine);
        }

        return lines;
    }

    private float CalculateJustifyContentOffset(float availableSpace, int elementCount)
    {
        return JustifyContent switch
        {
            FlexJustifyContent.Start => 0f,
            FlexJustifyContent.End => availableSpace,
            FlexJustifyContent.Center => availableSpace / 2f,
            FlexJustifyContent.SpaceBetween => elementCount > 1 ? 0f : availableSpace,
            FlexJustifyContent.SpaceAround => availableSpace / (2 * elementCount),
            FlexJustifyContent.SpaceEvenly => availableSpace / (elementCount + 1),
            _ => throw new ArgumentOutOfRangeException(nameof(JustifyContent))
        };
    }

    public readonly struct LayoutState(BaseUiComponent element, float baseSpan, float flexBasis, float flexGrow, float flexShrink)
    {
        public readonly BaseUiComponent Element = element;
        public readonly float BaseSpan = baseSpan;
        public readonly float FlexBasis = flexBasis;
        public readonly float FlexGrow = flexGrow;
        public readonly float FlexShrink = flexShrink;
    }
}