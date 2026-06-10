using System;

namespace Oxide.Ext.UiFramework.Libraries;

public static class UiImageValidation
{
    private static readonly byte[] SignaturePNG = [137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82];

    public static bool TryGetImageType(byte[] image, out UiImageType type)
    {
        if (IsRustPng(image))
        {
            type = UiImageType.Png;
            return true;
        }
        if (IsJpegImage(image))
        {
            type = UiImageType.Jpg;
            return true;
        }

        type = UiImageType.Unknown;
        return false;
    }

    private static bool IsRustPng(byte[] image) => image.AsSpan().StartsWith(SignaturePNG);
    private static bool IsJpegImage(byte[] image) => image is [0xFF, 0xD8, ..];
}