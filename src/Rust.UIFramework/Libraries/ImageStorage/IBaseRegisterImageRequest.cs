using System;
using System.Net;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Plugins;
using Oxide.Ext.UiFramework.Types;

namespace Oxide.Ext.UiFramework.Libraries;

public interface IBaseRegisterImageRequest
{
    PluginId PluginId { get; }
    string Name { get; }
    IRegisterImageOptions Options { get; }
    void OnSuccess(Action<RegisterSuccessEventArgs> callback);
    void OnStoredFailed(Action<RegisterFailedEventArgs> callback);
    void OnFailed(Action<IRegisterImageFailureResult> callback);
}

public interface IRegisterImageRequest : IBaseRegisterImageRequest
{
    byte[] Image { get; }
}

public interface IDownloadImageRequest : IBaseRegisterImageRequest
{
    string Url { get; }
    IDownloadImageState State { get; }
    void OnDownloadFailed(Action<DownloadFailedEventArgs> callback);
}

internal interface IDownloadImageRequestHandler
{
    string Url { get; }
    ConcurrentList<RegisterImageRequest> Requests { get; }
    IDownloadImageState State { get; }
    
    void AddRequest(RegisterImageRequest request);
    void OnDownloadStarted();
    void OnDownloadFailed(HttpStatusCode code, string message);
    void OnDownloadComplete(byte[] image);
}

internal interface IRegisterImageRequestHandler
{
    byte[] Image { get; }
    ConcurrentList<RegisterImageRequest> Requests { get; }
    void AddRequest(RegisterImageRequest request);
    void OnImageRegistered(ImageId imageId);
    void OnImageInvalid(RegisterImageErrorCode errorCode);
}

public interface IDownloadImageState
{
    DownloadState State { get; }
    int Attempts { get; }
    bool IsDownloading { get; }
    bool IsCompleted { get; }
    bool HadDownloadError { get; }
    bool IsOutOfAttempts { get; }
}

public interface IRegisterImageFailureResult;

public interface IRegisterImageOptions
{
    bool EnableClientPrecache { get; init; }
}

public interface IGetImageOptions
{
    string FallbackImageNameOrUrl { get; set; }
}

public interface IImageAnimationOptions
{
    string DownloadingImageNameOrUrl { get; set; }
    string TimeoutImageNameOrUrl { get; set; }
    string FailedImageNameOrUrl { get; set; }
    TimeSpan Timeout { get; set; }
}

public readonly record struct DownloadFailedEventArgs(IDownloadImageRequest Request, HttpStatusCode Code, string Message) : IRegisterImageFailureResult;
public readonly record struct RegisterSuccessEventArgs(IDownloadImageRequest Request, ImageId ImageId);
public readonly record struct RegisterFailedEventArgs(IDownloadImageRequest Request, byte[] Image, RegisterImageErrorCode ErrorCode) : IRegisterImageFailureResult;