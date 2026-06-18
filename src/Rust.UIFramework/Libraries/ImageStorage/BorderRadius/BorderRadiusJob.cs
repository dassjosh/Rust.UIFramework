using System.Runtime.CompilerServices;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Libraries;

[BurstCompile]
public struct BorderRadiusJob : IJobParallelFor
{
    public int Width;
    public int Height;

    // Horizontal radii (rx)
    public float Tlx, Trx, Brx, Blx;

    // Vertical radii (ry)
    public float Tly, Try, Bry, Bly;

    // AntiAlias
    public bool AntiAlias;
    public float EdgeWidth;

    // Fill
    public Color32 Fill;
    public Color32 Transparent;

    // Border
    public bool EnableBorder;
    public float BorderWidth;
    public Color32 BorderColor;

    // Dashed border
    public bool EnableDashedBorder;
    public float DashLength;
    public float GapLength;

    public bool UseInputImage;

    #if SERVER
    [Unity.Collections.ReadOnly]
    public NativeArray<Color32> Input;

    [WriteOnly]
    public NativeArray<Color32> Output;
#else
    public Color32[] Input;
    public Color32[] Output;
#endif

    public void Execute(int index)
    {
        int x = index % Width;
        int y = index / Width;

        // Use pixel centers. This gives more stable AA and is closer to how rasterizers sample.
        float2 pos = new(x + 0.5f, y + 0.5f);

        float width = Width;
        float height = Height;

        float2 tl = new(math.max(0f, Tlx), math.max(0f, Tly));
        float2 tr = new(math.max(0f, Trx), math.max(0f, Try));
        float2 br = new(math.max(0f, Brx), math.max(0f, Bry));
        float2 bl = new(math.max(0f, Blx), math.max(0f, Bly));

        NormalizeRadii(width, height, ref tl, ref tr, ref br, ref bl);

        float aa = math.max(EdgeWidth, 0.0001f);

        float outerSd = SignedDistanceRoundedRect(pos, 0f, 0f, width, height, tl, tr, br, bl);
        float outerCoverage = CoverageFromSignedDistance(outerSd, aa);
        if (outerCoverage <= 0f)
        {
            Output[index] = Transparent;
            return;
        }

        Color32 pixelColor = GetPixelColor(index);

        bool drawBorder = EnableBorder && BorderWidth > 0f;
        if (!drawBorder)
        {
            Output[index] = ApplyCoverage(pixelColor, outerCoverage);
            return;
        }

        float borderWidth = math.max(0f, BorderWidth);

        float innerLeft = borderWidth;
        float innerBottom = borderWidth;
        float innerRight = width - borderWidth;
        float innerTop = height - borderWidth;

        float innerCoverage = 0f;

        if (innerRight > innerLeft && innerTop > innerBottom)
        {
            float2 innerTl = new(math.max(0f, tl.x - borderWidth), math.max(0f, tl.y - borderWidth));
            float2 innerTr = new(math.max(0f, tr.x - borderWidth), math.max(0f, tr.y - borderWidth));
            float2 innerBr = new(math.max(0f, br.x - borderWidth), math.max(0f, br.y - borderWidth));
            float2 innerBl = new(math.max(0f, bl.x - borderWidth), math.max(0f, bl.y - borderWidth));

            NormalizeRadii(
                innerRight - innerLeft,
                innerTop - innerBottom,
                ref innerTl,
                ref innerTr,
                ref innerBr,
                ref innerBl);

            float innerSd = SignedDistanceRoundedRect(
                pos,
                innerLeft,
                innerBottom,
                innerRight,
                innerTop,
                innerTl,
                innerTr,
                innerBr,
                innerBl);

            innerCoverage = CoverageFromSignedDistance(innerSd, aa);
        }

        // Base fill is painted across the full rounded rect.
        // This makes dashed gaps show Fill, similar to normal CSS background painting.
        Color32 baseColor = ApplyCoverage(pixelColor, outerCoverage);

        float borderCoverage = outerCoverage * (1f - innerCoverage);

        if (EnableDashedBorder)
        {
            float dashCoverage = DashCoverage(
                pos,
                width,
                height,
                borderWidth,
                tl,
                tr,
                br,
                bl,
                aa);

            borderCoverage *= dashCoverage;
        }

        if (borderCoverage <= 0f)
        {
            Output[index] = baseColor;
            return;
        }

        Color32 borderColor = ApplyCoverage(BorderColor, borderCoverage);

        Output[index] = AlphaComposite(borderColor, baseColor);
    }

    // ---------------------------------------------------------------------
    // Rounded rectangle SDF
    // ---------------------------------------------------------------------

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private float SignedDistanceRoundedRect(
        float2 point,
        float left,
        float bottom,
        float right,
        float top,
        float2 topLeft,
        float2 topRight,
        float2 bottomRight,
        float2 bottomLeft)
    {
        if (right <= left || top <= bottom)
        {
            return 1e6f;
        }

        // Bottom-left corner.
        if (bottomLeft.x > 0f && bottomLeft.y > 0f &&
            point.x < left + bottomLeft.x &&
            point.y < bottom + bottomLeft.y)
        {
            float2 center = new(left + bottomLeft.x, bottom + bottomLeft.y);
            return DistanceToEllipseEdge(point, center, bottomLeft);
        }

        // Bottom-right corner.
        if (bottomRight.x > 0f && bottomRight.y > 0f &&
            point.x > right - bottomRight.x &&
            point.y < bottom + bottomRight.y)
        {
            float2 center = new(right - bottomRight.x, bottom + bottomRight.y);
            return DistanceToEllipseEdge(point, center, bottomRight);
        }

        // Top-left corner.
        if (topLeft.x > 0f && topLeft.y > 0f &&
            point.x < left + topLeft.x &&
            point.y > top - topLeft.y)
        {
            float2 center = new(left + topLeft.x, top - topLeft.y);
            return DistanceToEllipseEdge(point, center, topLeft);
        }

        // Top-right corner.
        if (topRight.x > 0f && topRight.y > 0f &&
            point.x > right - topRight.x &&
            point.y > top - topRight.y)
        {
            float2 center = new(right - topRight.x, top - topRight.y);
            return DistanceToEllipseEdge(point, center, topRight);
        }

        // Axis-aligned rectangle distance for non-corner regions.
        float dxOutside = math.max(math.max(left - point.x, point.x - right), 0f);
        float dyOutside = math.max(math.max(bottom - point.y, point.y - top), 0f);

        if (dxOutside > 0f || dyOutside > 0f)
        {
            return math.sqrt(dxOutside * dxOutside + dyOutside * dyOutside);
        }

        float insideDistance = math.min(
            math.min(point.x - left, right - point.x),
            math.min(point.y - bottom, top - point.y));

        return -insideDistance;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private float DistanceToEllipseEdge(float2 point, float2 center, float2 radius)
    {
        float2 p = point - center;

        float distanceToCenter = math.sqrt(p.x * p.x + p.y * p.y);

        if (distanceToCenter <= 0.00001f)
        {
            return -math.min(radius.x, radius.y);
        }

        float rx = math.max(radius.x, 0.00001f);
        float ry = math.max(radius.y, 0.00001f);

        float value =
            (p.x * p.x) / (rx * rx) +
            (p.y * p.y) / (ry * ry);

        float edgeDistance = distanceToCenter / math.sqrt(value);

        return distanceToCenter - edgeDistance;
    }

    // ---------------------------------------------------------------------
    // Radius normalization
    // ---------------------------------------------------------------------

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void NormalizeRadii(float width, float height, ref float2 topLeft, ref float2 topRight, ref float2 bottomRight, ref float2 bottomLeft)
    {
        // CSS border-radius does not simply clamp every radius independently.
        // If the sum of adjacent radii exceeds the box size, all radii are scaled
        // by the smallest required factor.
        float scale = 1f;

        scale = math.min(scale, ScaleForPair(width, topLeft.x + topRight.x));
        scale = math.min(scale, ScaleForPair(width, bottomLeft.x + bottomRight.x));
        scale = math.min(scale, ScaleForPair(height, topLeft.y + bottomLeft.y));
        scale = math.min(scale, ScaleForPair(height, topRight.y + bottomRight.y));

        topLeft *= scale;
        topRight *= scale;
        bottomRight *= scale;
        bottomLeft *= scale;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static float ScaleForPair(float size, float sum) => sum <= 0f || sum <= size ? 1f : size / sum;

    // ---------------------------------------------------------------------
    // Coverage and compositing
    // ---------------------------------------------------------------------

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private float CoverageFromSignedDistance(float signedDistance, float edgeWidth)
    {
        if (!AntiAlias)
        {
            return signedDistance <= 0f ? 1f : 0f;
        }

        // signedDistance < 0 means inside.
        // At the mathematical edge, coverage is 0.5.
        return math.saturate(0.5f - signedDistance / edgeWidth);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Color32 ApplyCoverage(Color32 color, float coverage)
    {
        coverage = math.saturate(coverage);

        Color32 result = color;
        result.a = (byte)math.round(color.a * coverage);
        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Color32 AlphaComposite(Color32 src, Color32 dst)
    {
        float srcA = src.a / 255f;
        float dstA = dst.a / 255f;

        float outA = srcA + dstA * (1f - srcA);

        if (outA <= 0.00001f)
        {
            return Transparent;
        }

        float3 srcRgb = new float3(src.r, src.g, src.b) / 255f;
        float3 dstRgb = new float3(dst.r, dst.g, dst.b) / 255f;

        float3 outRgb = (srcRgb * srcA + dstRgb * dstA * (1f - srcA)) / outA;

        return new Color32(
            (byte)math.round(math.saturate(outRgb.x) * 255f),
            (byte)math.round(math.saturate(outRgb.y) * 255f),
            (byte)math.round(math.saturate(outRgb.z) * 255f),
            (byte)math.round(math.saturate(outA) * 255f));
    }

    // ---------------------------------------------------------------------
    // Dashed border
    // ---------------------------------------------------------------------

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private float DashCoverage(
        float2 point,
        float width,
        float height,
        float borderWidth,
        float2 tl,
        float2 tr,
        float2 br,
        float2 bl,
        float aa)
    {
        if (DashLength <= 0f)
        {
            return 0f;
        }

        float gap = math.max(0f, GapLength);
        float period = DashLength + gap;

        if (period <= 0.00001f)
        {
            return 1f;
        }

        float s = BorderPathCoordinate(point, width, height, borderWidth, tl, tr, br, bl);

        float t = math.fmod(s, period);
        if (t < 0f)
        {
            t += period;
        }

        if (!AntiAlias)
        {
            return t < DashLength ? 1f : 0f;
        }

        float signedDistanceToDash;

        if (t < DashLength)
        {
            // Inside the dash. Negative distance.
            float distanceToStart = t;
            float distanceToEnd = DashLength - t;
            signedDistanceToDash = -math.min(distanceToStart, distanceToEnd);
        }
        else
        {
            // Inside the gap. Positive distance.
            float distanceToPreviousEnd = t - DashLength;
            float distanceToNextStart = period - t;
            signedDistanceToDash = math.min(distanceToPreviousEnd, distanceToNextStart);
        }

        return CoverageFromSignedDistance(signedDistanceToDash, aa);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private float BorderPathCoordinate(
        float2 point,
        float width,
        float height,
        float borderWidth,
        float2 outerTl,
        float2 outerTr,
        float2 outerBr,
        float2 outerBl)
    {
        // CSS-style dashes are positioned along the border path.
        // Approximate that path using the centerline of the border.
        float inset = borderWidth * 0.5f;

        float left = inset;
        float bottom = inset;
        float right = width - inset;
        float top = height - inset;

        float2 tl = new(math.max(0f, outerTl.x - inset), math.max(0f, outerTl.y - inset));
        float2 tr = new(math.max(0f, outerTr.x - inset), math.max(0f, outerTr.y - inset));
        float2 br = new(math.max(0f, outerBr.x - inset), math.max(0f, outerBr.y - inset));
        float2 bl = new(math.max(0f, outerBl.x - inset), math.max(0f, outerBl.y - inset));

        NormalizeRadii(
            math.max(0f, right - left),
            math.max(0f, top - bottom),
            ref tl,
            ref tr,
            ref br,
            ref bl);

        float topLen = math.max(0f, right - tr.x - (left + tl.x));
        float rightLen = math.max(0f, top - tr.y - (bottom + br.y));
        float bottomLen = math.max(0f, right - br.x - (left + bl.x));
        float leftLen = math.max(0f, top - tl.y - (bottom + bl.y));

        float tlArc = QuarterEllipseLength(tl);
        float trArc = QuarterEllipseLength(tr);
        float brArc = QuarterEllipseLength(br);
        float blArc = QuarterEllipseLength(bl);

        float topStart = 0f;
        float trStart = topStart + topLen;
        float rightStart = trStart + trArc;
        float brStart = rightStart + rightLen;
        float bottomStart = brStart + brArc;
        float blStart = bottomStart + bottomLen;
        float leftStart = blStart + blArc;
        float tlStart = leftStart + leftLen;

        // Prefer corner arcs when the sample lies in the corresponding outer corner region.
        // This keeps dashes continuous around rounded corners.
        if (tr.x > 0f && tr.y > 0f &&
            point.x > right - tr.x &&
            point.y > top - tr.y)
        {
            float2 center = new(right - tr.x, top - tr.y);
            float progress = TopRightArcProgress(point, center, tr);
            return trStart + trArc * progress;
        }

        if (br.x > 0f && br.y > 0f &&
            point.x > right - br.x &&
            point.y < bottom + br.y)
        {
            float2 center = new(right - br.x, bottom + br.y);
            float progress = BottomRightArcProgress(point, center, br);
            return brStart + brArc * progress;
        }

        if (bl.x > 0f && bl.y > 0f &&
            point.x < left + bl.x &&
            point.y < bottom + bl.y)
        {
            float2 center = new(left + bl.x, bottom + bl.y);
            float progress = BottomLeftArcProgress(point, center, bl);
            return blStart + blArc * progress;
        }

        if (tl.x > 0f && tl.y > 0f &&
            point.x < left + tl.x &&
            point.y > top - tl.y)
        {
            float2 center = new(left + tl.x, top - tl.y);
            float progress = TopLeftArcProgress(point, center, tl);
            return tlStart + tlArc * progress;
        }

        // For straight sections, choose the nearest side of the centerline box.
        float dTop = math.abs(top - point.y);
        float dRight = math.abs(right - point.x);
        float dBottom = math.abs(bottom - point.y);
        float dLeft = math.abs(left - point.x);

        float minD = math.min(math.min(dTop, dRight), math.min(dBottom, dLeft));

        if (minD == dTop)
        {
            float px = math.clamp(point.x, left + tl.x, right - tr.x);
            return topStart + px - (left + tl.x);
        }

        if (minD == dRight)
        {
            float py = math.clamp(point.y, bottom + br.y, top - tr.y);
            return rightStart + (top - tr.y - py);
        }

        if (minD == dBottom)
        {
            float px = math.clamp(point.x, left + bl.x, right - br.x);
            return bottomStart + (right - br.x - px);
        }

        {
            float py = math.clamp(point.y, bottom + bl.y, top - tl.y);
            return leftStart + (py - (bottom + bl.y));
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private float QuarterEllipseLength(float2 radius)
    {
        if (radius.x <= 0f || radius.y <= 0f)
        {
            return 0f;
        }

        // Ramanujan circumference approximation, divided by four.
        float a = radius.x;
        float b = radius.y;

        float circumference = math.PI * (3f * (a + b) - math.sqrt((3f * a + b) * (a + 3f * b)));

        return circumference * 0.25f;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private float TopRightArcProgress(float2 point, float2 center, float2 radius)
    {
        float2 p = NormalizedEllipseVector(point, center, radius);
        float theta = math.atan2(p.y, p.x);

        // Top -> right, clockwise: pi/2 -> 0.
        return math.saturate((math.PI * 0.5f - theta) / (math.PI * 0.5f));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private float BottomRightArcProgress(float2 point, float2 center, float2 radius)
    {
        float2 p = NormalizedEllipseVector(point, center, radius);
        float theta = math.atan2(p.y, p.x);

        // Right -> bottom, clockwise: 0 -> -pi/2.
        return math.saturate(-theta / (math.PI * 0.5f));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private float BottomLeftArcProgress(float2 point, float2 center, float2 radius)
    {
        float2 p = NormalizedEllipseVector(point, center, radius);
        float theta = math.atan2(p.y, p.x);

        // Bottom -> left, clockwise: -pi/2 -> -pi.
        if (theta > 0f)
        {
            theta -= math.PI * 2f;
        }

        return math.saturate((-math.PI * 0.5f - theta) / (math.PI * 0.5f));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private float TopLeftArcProgress(float2 point, float2 center, float2 radius)
    {
        float2 p = NormalizedEllipseVector(point, center, radius);
        float theta = math.atan2(p.y, p.x);

        // Left -> top, clockwise: pi -> pi/2.
        return math.saturate((math.PI - theta) / (math.PI * 0.5f));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private float2 NormalizedEllipseVector(float2 point, float2 center, float2 radius)
    {
        return new float2(
            (point.x - center.x) / math.max(radius.x, 0.00001f),
            (point.y - center.y) / math.max(radius.y, 0.00001f));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Color32 GetPixelColor(int index)
    {
        return UseInputImage ? Input[index] : Fill;
    }
}