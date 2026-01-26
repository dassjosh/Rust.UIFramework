using System;

namespace Oxide.Ext.UiFramework.Libraries;

public class GetImageOptions : IGetImageOptions
{
    internal static readonly GetImageOptions Default = new();
    
    public string FallbackImageNameOrUrl { get; set; }
}

[Obsolete("Please use GetImageOptions instead")]
public class ImageDownloadOptions : GetImageOptions;