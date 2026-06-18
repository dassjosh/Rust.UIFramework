using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using Oxide.Ext.UiFramework.Extensions;
using Oxide.Ext.UiFramework.Guards;
using Unity.Collections;
using UnityEngine;
#if !SERVER
using System.Drawing;
using System.IO;
#endif

namespace Oxide.Ext.UiFramework.Libraries;

internal static class ImageEncoding
{
    #if SERVER
    public static byte[] EncodeToPng(NativeArray<Color32> pixels, int width, int height)
    {
        Texture2D tex = null;
        try
        {
            tex = new Texture2D(width, height, TextureFormat.RGBA32, false)
            {
                filterMode = FilterMode.Bilinear,
                wrapMode = TextureWrapMode.Clamp
            };

            tex.SetPixelData(pixels, 0);
            tex.Apply(false, false);

            byte[] bytes = tex.EncodeToPNG();
            return bytes;
        }
        finally
        {
            if (tex)
            {
                Object.Destroy(tex);
            }
        }
    }

    public static bool LoadImage(byte[] data, Allocator allocator, out NativeArray<Color32> output, out int width, out int height)
    {
        Texture2D image = null;
        width = 0;
        height = 0;
        output = default;
        try
        {
            image = new Texture2D(2, 2, TextureFormat.RGBA32, false);
            if(!image.LoadImage(data))
            {
                return false;
            }

            width = image.width;
            height = image.height;
            output = new NativeArray<Color32>(width * height, allocator, NativeArrayOptions.UninitializedMemory);
            output.CopyFrom(image.GetPixels32());
            return true;
        }
        finally
        {
            if (image)
            {
                Object.Destroy(image);
            }
        }
    }
#endif

#if !SERVER
    public static byte[] EncodeToPng(Color32[] pixels, int width, int height)
    {
        using Bitmap bitmap = new(width, height, PixelFormat.Format32bppArgb);
        Rectangle rect = new(0, 0, width, height);
        BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, bitmap.PixelFormat);

        Marshal.Copy(pixels.Select(p => p.ToArgb()).ToArray(), 0, bmpData.Scan0, pixels.Length);
        bitmap.UnlockBits(bmpData);
        using MemoryStream stream = new();
        bitmap.Save(stream, ImageFormat.Png);
        return stream.ToArray();
    }

    public static bool LoadImage(byte[] data, out Color32[] output, out int width, out int height)
    {
        using MemoryStream stream = new(data);
        using Bitmap bitmap = new(stream);
        width = bitmap.Width;
        height = bitmap.Height;
        output = new Color32[width * height];
        Rectangle rect = new(0, 0, width, height);
        BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.ReadOnly, bitmap.PixelFormat);

        int[] dest = new int[output.Length];
        Marshal.Copy(bmpData.Scan0, dest, 0, dest.Length);
        bitmap.UnlockBits(bmpData);

        for (int i = 0; i < dest.Length; i++)
        {
            int argb = dest[i];

            byte alpha = (byte)(argb >> 24);
            byte red = (byte)(argb >> 16);
            byte green = (byte)(argb >> 8);
            byte blue = (byte)argb;

            output[i] = new Color32(red, green, blue, alpha);
        }

        return true;
    }
#endif

}