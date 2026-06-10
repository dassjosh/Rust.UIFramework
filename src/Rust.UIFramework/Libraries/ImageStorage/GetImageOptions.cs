using System;

namespace Oxide.Ext.UiFramework.Libraries;

public class GetImageOptions : IGetImageOptions
{
    internal static readonly GetImageOptions Default = new();

    ///<inheritdoc/>
    public string FallbackImage { get; set; }

    [Obsolete($"Please use {nameof(FallbackImage)} instead.")]
    public string FallbackImageNameOrUrl
    {
        get => FallbackImage;
        set => FallbackImage = value;
    }
}

[Obsolete($"Please use {nameof(GetImageOptions)} instead")]
public class ImageDownloadOptions : GetImageOptions;