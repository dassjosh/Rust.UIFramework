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

    // Outer border
    // public bool EnableBorder;
    // public float BorderWidth;
    // public Color32 BorderColor;
    //
    // // Dashed border
    // public bool EnableDashedBorder;
    // public float DashLength;
    // public float GapLength;
    //
    // // Gradient border
    // public bool EnableBorderGradient;
    // public bool BorderGradientAngular;
    // public Color32 BorderColorStart;
    // public Color32 BorderColorEnd;
    //
    // // Inset border (inner stroke)
    // public bool EnableInset;
    // public float InsetWidth;
    // public Color32 InsetColor;

    public NativeArray<Color32> Pixels;

    public void Execute(int index)
    {
        int x = index % Width;
        int y = index / Width;

        float2 pos = new(x, y);

        float maxRx = Width * 0.5f;
        float maxRy = Height * 0.5f;

        float2 tl = new(math.min(Tlx, maxRx), math.min(Tly, maxRy));
        float2 tr = new(math.min(Trx, maxRx), math.min(Try, maxRy));
        float2 br = new(math.min(Brx, maxRx), math.min(Bry, maxRy));
        float2 bl = new(math.min(Blx, maxRx), math.min(Bly, maxRy));

        bool isCorner = false;
        float2 center = 0f;
        float2 radius = 0f;

        if (x < bl.x && y < bl.y)
        {
            isCorner = bl.x > 0f && bl.y > 0f;
            center = new float2(bl.x, bl.y);
            radius = bl;
        }
        else if (x > Width - br.x && y < br.y)
        {
            isCorner = br.x > 0f && br.y > 0f;
            center = new float2(Width - br.x, br.y);
            radius = br;
        }
        else if (x < tl.x && y > Height - tl.y)
        {
            isCorner = tl.x > 0f && tl.y > 0f;
            center = new float2(tl.x, Height - tl.y);
            radius = tl;
        }
        else if (x > Width - tr.x && y > Height - tr.y)
        {
            isCorner = tr.x > 0f && tr.y > 0f;
            center = new float2(Width - tr.x, Height - tr.y);
            radius = tr;
        }

        if (!isCorner)
        {
            Pixels[index] = Fill;
            return;
        }

        float distance = DistanceToEllipseEdge(pos, center, radius);
        if(distance <= 0f)
        {
            Pixels[index] = Fill;
            return;
        }

        if(distance > EdgeWidth)
        {
            Pixels[index] = Transparent;
            return;
        }

        if (AntiAlias)
        {
            float alpha = math.saturate(1f - (distance / EdgeWidth));
            Color32 baseColor = Fill;
            baseColor.a = (byte)(baseColor.a * alpha);
            Pixels[index] = baseColor;
            return;
        }

        Pixels[index] = Fill;
        return;

        // float2 d = pos - center;
        // float distance = math.sqrt(d.x * d.x + d.y * d.y);
        //
        // // --- OUTER DISTANCE (signed) ---
        // float nx = d.x / radius.x;
        // float ny = d.y / radius.y;
        // float lenOuter = math.sqrt(nx * nx + ny * ny);
        // float distOuter = lenOuter - 1f;
        //
        // if (distance <= radius.x)
        // {
        //     Pixels[index] = Transparent;
        //     return;
        // }
        //
        // if (distOuter < 0f)
        // {
        //     float alpha = math.saturate(1f - (distOuter / EdgeWidth));
        //
        //     // Color32 baseColor = EnableBorder
        //     //     ? GetBorderColor(d, radius, distOuter, distInner)
        //     //     : Fill;
        //     //
        //     // if (EnableBorder && EnableDashedBorder && !IsDash(d, radius))
        //     // {
        //     //     baseColor = Transparent;
        //     // }
        //
        //     Color32 baseColor = Fill;
        //     baseColor.a = (byte)(baseColor.a * alpha);
        //     Pixels[index] = baseColor;
        //     return;
        // }

       //  // Fast path
       //  //if (!EnableBorder && !EnableInset)
       // // {
       //      if (!AntiAlias)
       //      {
       //          Pixels[index] = distOuter <= 0f ? Fill : Transparent;
       //          return;
       //      }
       //
       //      if (distOuter > -EdgeWidth)
       //      {
       //          float alpha = math.saturate(-distOuter / EdgeWidth);
       //          if (alpha <= 0f)
       //          {
       //              Pixels[index] = Transparent;
       //              return;
       //          }
       //
       //          Color32 c = Fill;
       //          c.a = (byte)(Fill.a * alpha);
       //          Pixels[index] = c;
       //          return;
       //      }
       //
       //      Pixels[index] = Fill;
       //      return;
       // // }

        // // --- INNER DISTANCE ---
        // float2 innerRadius = radius;
        // if (EnableBorder && BorderWidth > 0f)
        // {
        //     innerRadius = math.max(radius - new float2(BorderWidth, BorderWidth), 0.0001f);
        // }
        //
        // float inx = d.x / innerRadius.x;
        // float iny = d.y / innerRadius.y;
        // float lenInner = math.sqrt(inx * inx + iny * iny);
        // float distInner = lenInner - 1f;
        //
        // // --- INSET DISTANCE ---
        // float distInset = 0f;
        // if (EnableInset && InsetWidth > 0f)
        // {
        //     float2 insetRadius = math.max(innerRadius - new float2(InsetWidth, InsetWidth), 0.0001f);
        //     float inx2 = d.x / insetRadius.x;
        //     float iny2 = d.y / insetRadius.y;
        //     float lenInset = math.sqrt(inx2 * inx2 + iny2 * iny2);
        //     distInset = lenInset - 1f;
        // }
        //
        // // --- NON-AA ---
        // if (!AntiAlias)
        // {
        //     if (distOuter > 0f)
        //     {
        //         Pixels[index] = Transparent;
        //         return;
        //     }
        //
        //     if (EnableBorder && distInner > 0f)
        //     {
        //         if (EnableDashedBorder && !IsDash(d, radius))
        //         {
        //             Pixels[index] = Transparent;
        //             return;
        //         }
        //
        //         Pixels[index] = GetBorderColor(d, radius, distOuter, distInner);
        //         return;
        //     }
        //
        //     if (EnableInset && distInset <= 0f)
        //     {
        //         Pixels[index] = InsetColor;
        //         return;
        //     }
        //
        //     Pixels[index] = Fill;
        //     return;
        // }
        //
        // // --- AA PATH ---
        //
        // // Outer edge AA
        // if (distOuter > 0f)
        // {
        //     float alpha = math.saturate(-distOuter / EdgeWidth);
        //     if (alpha <= 0f)
        //     {
        //         Pixels[index] = Transparent;
        //         return;
        //     }
        //
        //     Color32 baseColor = EnableBorder
        //         ? GetBorderColor(d, radius, distOuter, distInner)
        //         : Fill;
        //
        //     if (EnableBorder && EnableDashedBorder && !IsDash(d, radius))
        //     {
        //         baseColor = Transparent;
        //     }
        //
        //     baseColor.a = (byte)(baseColor.a * alpha);
        //     Pixels[index] = baseColor;
        //     return;
        // }
        //
        // // Border region
        // if (EnableBorder && distInner > 0f)
        // {
        //     if (EnableDashedBorder && !IsDash(d, radius))
        //     {
        //         Pixels[index] = Transparent;
        //         return;
        //     }
        //
        //     Color32 bc = GetBorderColor(d, radius, distOuter, distInner);
        //
        //     float alphaInner = math.saturate(distInner / EdgeWidth);
        //     bc.a = (byte)(bc.a * alphaInner);
        //
        //     Pixels[index] = bc;
        //     return;
        // }
        //
        // // Inset region
        // if (EnableInset && distInset <= 0f)
        // {
        //     Color32 ic = InsetColor;
        //
        //     float alphaInset = math.saturate(distInset / EdgeWidth);
        //     ic.a = (byte)(ic.a * alphaInset);
        //
        //     Pixels[index] = ic;
        //     return;
        // }
        //
        // Pixels[index] = Fill;
    }


    // --- Helpers ---

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private float DistanceToEllipseEdge(float2 point, float2 center, float2 radius)
    {
        float2 p = point - center;

        float distanceToCenter = math.sqrt(p.x * p.x + p.y * p.y);

        // Ellipse normalized value
        float value =
            (p.x * p.x) / (radius.x * radius.x) +
            (p.y * p.y) / (radius.y * radius.y);

        // Handle center case to avoid divide-by-zero
        if (distanceToCenter == 0f)
        {
            return math.min(radius.x, radius.y);
        }

        // Distance from center to ellipse edge in this direction
        float edgeDistance = distanceToCenter / math.sqrt(value);

        // Final distance from point to edge
        return distanceToCenter - edgeDistance;
    }

    // private bool IsDash(float2 d, float2 radius)
    // {
    //     if (!EnableDashedBorder)
    //     {
    //         return true;
    //     }
    //
    //     float rx = radius.x;
    //     float ry = radius.y;
    //
    //     float angle = math.atan2(d.y / ry, d.x / rx);
    //     float approxR = (rx + ry) * 0.5f;
    //     float arc = math.abs(angle) * approxR;
    //
    //     float dashCycle = DashLength + GapLength;
    //     if (dashCycle <= 0.0001f)
    //     {
    //         return true;
    //     }
    //
    //     float m = arc % dashCycle;
    //     return m < DashLength;
    // }
    //
    // private Color32 GetBorderColor(float2 d, float2 radius, float distOuter, float distInner)
    // {
    //     if (!EnableBorderGradient)
    //     {
    //         return BorderColor;
    //     }
    //
    //     float t;
    //
    //     if (BorderGradientAngular)
    //     {
    //         float angle = math.atan2(d.y / radius.y, d.x / radius.x);
    //         t = (angle + math.PI) * (1f / (2f * math.PI));
    //     }
    //     else
    //     {
    //         float innerDist = math.sqrt(math.max(distInner, 0f));
    //         float outerDist = math.sqrt(math.max(distOuter, 0f));
    //         float denom = outerDist - innerDist;
    //         denom = math.abs(denom) < 1e-5f ? 1e-5f : denom;
    //         t = math.saturate((innerDist - 1f) / denom);
    //     }
    //
    //     return Color32.Lerp(BorderColorStart, BorderColorEnd, t);
    // }
}