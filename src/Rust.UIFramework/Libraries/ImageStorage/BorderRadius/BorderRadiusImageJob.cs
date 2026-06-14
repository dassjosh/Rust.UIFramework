using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Oxide.Ext.UiFramework.Libraries;

[BurstCompile]
public struct BorderRadiusImageJob : IJobParallelFor
{
    public int Width;
    public int Height;

    // Horizontal radii (rx)
    public float Tlx, Trx, Brx, Blx;

    // Vertical radii (ry)
    public float Tly, Try, Bry, Bly;

    public bool AntiAlias;
    public float EdgeWidth;

    public NativeArray<Color32> Image;   // Input image
    public NativeArray<Color32> Pixels;  // Output image

    public Color32 Replacement;

    public void Execute(int index)
    {
        int x = index % Width;
        int y = index / Width;

        float2 pos = new float2(x, y);

        float maxRx = Width * 0.5f;
        float maxRy = Height * 0.5f;

        // Clamp radii
        float2 tl = new(math.min(Tlx, maxRx), math.min(Tly, maxRy));
        float2 tr = new(math.min(Trx, maxRx), math.min(Try, maxRy));
        float2 br = new(math.min(Brx, maxRx), math.min(Bry, maxRy));
        float2 bl = new(math.min(Blx, maxRx), math.min(Bly, maxRy));

        bool isCorner = false;
        float2 center = 0f;
        float2 radius = 0;

        // Bottom-left
        if (x < bl.x && y < bl.y)
        {
            isCorner = bl.x > 0f && bl.y > 0f;
            center = new float2(bl.x, bl.y);
            radius = bl;
        }
        // Bottom-right
        else if (x > Width - br.x && y < br.y)
        {
            isCorner = br.x > 0f && br.y > 0f;
            center = new float2(Width - br.x, br.y);
            radius = br;
        }
        // Top-left
        else if (x < tl.x && y > Height - tl.y)
        {
            isCorner = tl.x > 0f && tl.y > 0f;
            center = new float2(tl.x, Height - tl.y);
            radius = tl;
        }
        // Top-right
        else if (x > Width - tr.x && y > Height - tr.y)
        {
            isCorner = tr.x > 0f && tr.y > 0f;
            center = new float2(Width - tr.x, Height - tr.y);
            radius = tr;
        }

        // Not a corner → copy pixel directly
        if (!isCorner)
        {
            Pixels[index] = Image[index];
            return;
        }

        // Ellipse equation
        float2 d = pos - center;

        float nx = d.x / radius.x;
        float ny = d.y / radius.y;

        float dist = nx * nx + ny * ny;

        // No AA
        if (!AntiAlias)
        {
            Pixels[index] = dist <= 1f ? Image[index] : Replacement;
            return;
        }

        // Anti-aliased edge
        float gradient = (1f - dist) / EdgeWidth;
        float alpha = math.saturate(gradient);
        if (alpha <= 0f)
        {
            Pixels[index] = Replacement;
            return;
        }

        // Blend original pixel with transparency
        Color32 src = Image[index];
        src.a = (byte)(src.a * alpha);
        Pixels[index] = src;
    }
}
